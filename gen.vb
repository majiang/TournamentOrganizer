Public Class PlayerArrangementCompleteException
    Inherits ApplicationException
    Public complete As Integer()(,)

    Public Sub New(ByVal complete As Integer()(,))
        MyBase.New("プレイヤーの配置が完了しました。")
        Me.complete = complete
    End Sub

End Class

Public Class InevitableDoublePlayException
    Inherits ApplicationException
    Public Sub New()
        MyBase.New("再同卓を回避できません。")
    End Sub
End Class

Public Class PlayerArrangementTimeoutException
    Inherits ApplicationException
    Public Sub New()
        MyBase.New("プレイヤーの配置に失敗しました。")
    End Sub
End Class

Public Class InevitablePlayAgainstTeammateException
    Inherits ApplicationException
    Public Sub New()
        MyBase.New("同門同卓が不可避です。")
    End Sub
End Class

Public Class AssertError
    Inherits ApplicationException
    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
End Class

Public Class TeamArrangementGenerator
    Private TeamSizes() As Integer
    Private TableCount As Integer
    Private TeamCount As Integer

    Private Function Generate(ByVal GenerateCount As Integer, ByVal RoundCount As Integer, ByVal LoopCount As Integer) As Integer()(,)
        Return BestCombination(GenerateRounds(GenerateCount), RoundCount, LoopCount)
    End Function

    Public Shared Function Generate(ByVal TeamSizes() As Integer, ByVal GenerateCount As Integer, ByVal RoundCount As Integer, ByVal LoopCount As Integer) As Integer()(,)
        Return New TeamArrangementGenerator(TeamSizes).Generate(GenerateCount, RoundCount, LoopCount)
    End Function

    Private Sub New(ByVal TeamSizes() As Integer)
        'Debug.WriteLine(dump(TeamSizes))
        'Stop
        Me.TeamSizes = TeamSizes.Clone()
        Me.TableCount = Me.TeamSizes.Sum >> 2
        Me.TeamCount = Me.TeamSizes.Length
    End Sub

    Private Function GenerateRound() As Integer(,)
        Dim round(TableCount - 1, 4 - 1) As Integer
        Dim rest(TableCount - 1) As Integer
start:
        For table As Integer = 0 To TableCount - 1
            rest(table) = 4
        Next
        For team As Integer = 0 To TeamCount - 1
            Try
                For Each membertable As Integer In util.RandomChooseSomeNumber(rest, TeamSizes(team))
                    round(membertable, 4 - rest(membertable)) = team
                    rest(membertable) -= 1
                Next
            Catch ex As InevitablePlayAgainstTeammateException
                GoTo start
            End Try
        Next
        Return round
    End Function

    Private Function GenerateRounds(ByVal roundCount As Integer) As Integer()(,)
        Dim rounds As New List(Of Integer(,))
        For i As Integer = 0 To roundCount - 1
            rounds.Add(GenerateRound)
        Next
        Return rounds.ToArray
    End Function

    Private Function BestCombination(ByVal rounds As Integer()(,), ByVal roundCount As Integer, ByVal LoopCount As Integer) As Integer()(,)
        Dim average As Double()() = getPlayedWithAverage(rounds)
        Dim best As Integer()(,)
        best = New Integer()(,) {}
        Dim bestScore As Double
        For i As Integer = 1 To LoopCount
            Dim currentList As New List(Of Integer(,))
            For Each j As Integer In util.RandomChooseSomeNumber(util.ones(rounds.Length), roundCount)
                currentList.Add(rounds(j))
            Next
            Dim current As Integer()(,) = currentList.ToArray
            Dim currentScore = Grade(getPlayedWithAverage(current), average)
            If currentScore < bestScore Then Continue For
            best = current
            bestScore = currentScore
        Next
        Return best
    End Function

    Private Function getPlayedWithAverage(ByVal rounds As Integer()(,)) As Double()()
        Dim PlayedWith(TeamCount - 1)() As Double
        For team As Integer = 0 To TeamCount - 1
            PlayedWith(team) = New Double(team - 1) {}
        Next
        For Each round As Integer(,) In rounds
            Dim c As Integer()() = getPlayedWith(round)
            For i As Integer = 0 To TeamCount - 1
                For j As Integer = 0 To i - 1
                    PlayedWith(i)(j) += c(i)(j)
                Next
            Next
        Next
        For i As Integer = 0 To TeamCount - 1
            For j As Integer = 0 To i - 1
                PlayedWith(i)(j) /= rounds.Length
            Next
        Next
        Return PlayedWith
    End Function

    Private Function getPlayedWith(ByVal round As Integer(,)) As Integer()()
        Dim PlayedWith(TeamCount - 1)() As Integer
        For team As Integer = 0 To TeamCount - 1
            PlayedWith(team) = New Integer(team - 1) {}
        Next
        Dim tableCount As Integer = round.GetUpperBound(0) + 1
        For table As Integer = 0 To tableCount - 1
            For seat0 As Integer = 0 To 2
                For seat1 As Integer = seat0 + 1 To 3
                    PlayedWith(round(table, seat1))(round(table, seat0)) += 1
                Next
            Next
        Next
        Return PlayedWith
    End Function

    Private Shared Function Grade(ByVal x As Double()(), ByVal y As Double()()) As Double
        Dim sp As Double, sx As Double, sy As Double
        For i As Integer = 0 To x.Length - 1
            For j As Integer = 0 To i - 1
                sx += x(i)(j) * x(i)(j)
                sp += x(i)(j) * y(i)(j)
                sy += y(i)(j) * y(i)(j)
            Next
        Next
        Return (sp * sp) / (sx * sy)
    End Function

End Class

Public Class PlayerArrangementGenerator
    Private TeamList()() As Integer
    Private SeatsForTeam()()() As Integer
    Private PlayerCount, RoundCount, TableCount, TeamCount As Integer
    Private Function empty()
        Dim ret As New List(Of Integer(,))
        For round As Integer = 0 To RoundCount - 1
            Dim current(TableCount - 1, 4 - 1) As Integer
            For table As Integer = 0 To TableCount - 1
                For seat As Integer = 0 To 4 - 1
                    current(table, seat) = -1
                Next
            Next
            ret.Add(current)
        Next
        Return ret.ToArray()
    End Function
    Private Function Generate(ByVal timeout As TimeSpan) As Integer()(,)
        Try
            PutAnotherPlayer(empty(), 0, 0, New Integer() {}, Now() + timeout)
        Catch ex As PlayerArrangementCompleteException
            Return ex.complete
        End Try
        Throw New InevitableDoublePlayException()
    End Function

    Private Function getHistory(ByVal seated()(,) As Integer, ByVal currentRound As Integer, ByVal Player As Integer) As Integer()
        Dim ret(3 * currentRound - 1) As Integer
        Dim i As Integer = 0
        For round As Integer = 0 To currentRound - 1
            For table As Integer = 0 To TableCount - 1
                Dim found As Integer = -1
                For seat As Integer = 0 To 4 - 1
                    If seated(round)(table, seat) = Player Then found = seat
                Next
                If found < 0 Then Continue For
                For opseat As Integer = 0 To 4 - 1
                    If opseat = found Then Continue For
                    ret(i) = seated(round)(table, opseat)
                    i += 1
                Next
                Exit For
            Next
        Next
        Array.Sort(ret)
        'Debug.WriteLine(Player.ToString & " -> " & dump(ret))
        Return ret
    End Function

    Private Sub PutAnotherPlayer(ByVal Seated()(,) As Integer, ByVal Round As Integer, ByVal Player As Integer, ByVal Vacant() As Integer, ByVal timeout As Date)
        'Debug.WriteLine("round = " & Round.ToString & "; Player = " & Player.ToString)
        If timeout < Now Then Throw New PlayerArrangementTimeoutException()
        If Player >= PlayerCount Then
            Player = 0
            Round += 1
        End If
        If Round >= RoundCount Then
            Throw New PlayerArrangementCompleteException(Seated) ' 例外処理を利用して再帰を一気に抜ける
        End If
        If timeout < Now Then
            Throw New PlayerArrangementTimeoutException()
        End If

        Dim team As Integer = TeamList(Player)(0)
        Dim tplayer As Integer = TeamList(Player)(1)
        If tplayer = 0 Then Vacant = SeatsForTeam(Round)(team) ' 空席準備

        Dim table, itable As Integer
        Dim op As Integer
        Dim seat As Integer
        Dim h As Integer() = getHistory(Seated, Round, Player)
        For itable = tplayer To Vacant.GetUpperBound(0)
            table = Vacant(itable)
            For seat = 0 To 3
                op = Seated(Round)(table, seat)
                If op = -1 Then ' 空席に辿りついたら
                    ' 着席処理
                    Seated(Round)(table, seat) = Player
                    util.swap(Vacant, itable, tplayer)
                    ' 再帰
                    Try
                        PutAnotherPlayer(Seated, Round, Player + 1, Vacant, timeout)
                    Catch ex As PlayerArrangementCompleteException
                        Throw ex
                    End Try
                    ' 着席処理を戻す
                    Seated(Round)(table, seat) = -1
                    util.swap(Vacant, itable, tplayer)
                    Exit For
                End If
                If 0 <= Array.BinarySearch(h, op) Then Exit For
            Next
        Next
    End Sub
    Public Shared Function Generate(ByVal TeamSizes As Integer(), ByVal TeamArrangement As Integer()(,), ByVal timeout As TimeSpan) As String
        Dim instance As New PlayerArrangementGenerator(TeamSizes, TeamArrangement)
        Return New PlayerArrangementFormatter(instance.TeamList).format(instance.Generate(timeout))
    End Function
    Private Sub New(ByVal TeamSizes As Integer(), ByVal TeamArrangement As Integer()(,))
        Me.RoundCount = TeamArrangement.Length
        Me.PlayerCount = TeamArrangement(0).Length
        Me.TableCount = Me.PlayerCount >> 2
        Me.TeamCount = TeamSizes.Length
        Me.TeamList = getTeamList(TeamSizes)
        Me.SeatsForTeam = getSeatsForTeam(TeamArrangement)
    End Sub
    Private Function getTeamList(ByVal TeamSizes() As Integer) As Integer()()
        Dim ret As New List(Of Integer())
        Dim team As Integer = 0
        For Each teamSize As Integer In TeamSizes
            For i As Integer = 0 To teamSize - 1
                ret.Add(New Integer() {team, i})
            Next
            team += 1
        Next
        Return ret.ToArray()
    End Function
    Private Function getSeatsForTeam(ByVal round As Integer(,)) As Integer()()
        Dim ret(TeamCount - 1) As List(Of Integer)
        For team As Integer = 0 To TeamCount - 1
            ret(team) = New List(Of Integer)
        Next
        For table = 0 To TableCount - 1
            For seat As Integer = 0 To 3
                ret(round(table, seat)).Add(table)
            Next
        Next
        Dim result As New List(Of Integer())
        For team = 0 To TeamCount - 1
            result.Add(util.shuffled(ret(team).ToArray()))
        Next
        Return result.ToArray()
    End Function
    Private Function getSeatsForTeam(ByVal TeamArrangement As Integer()(,)) As Integer()()()
        Dim ret As New List(Of Integer()())
        For Each round As Integer(,) In TeamArrangement
            ret.Add(getSeatsForTeam(round))
        Next
        Return ret.ToArray
    End Function

End Class


Public Class PlayerArrangementFormatter
    Private TeamList()() As Integer

    Public Sub New(ByVal teamList As Integer()())
        Me.TeamList = teamList.Clone
    End Sub

    Private Function getPlayerName(ByVal player As Integer) As String
        Dim TeamName As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Return TeamName(TeamList(player)(0)).ToString & " - " & (TeamList(player)(1) + 1).ToString
    End Function

    Public Function format(ByVal tournament()(,) As Integer) As String
        format = ""
        For Each round As Integer(,) In tournament
            For table As Integer = 0 To round.GetUpperBound(0)
                For seat As Integer = 0 To 4 - 1
                    If seat Then format &= " "
                    format &= getPlayerName(round(table, seat))
                Next
                format &= vbCrLf
            Next
            format &= vbCrLf
        Next
    End Function

End Class

Module util
    Dim r As New Random()

    Public Function RandomChooseSomeNumber(ByVal weight() As Integer, ByVal n As Integer) As Integer()
        Dim _weight(weight.Length - 1) As Integer
        Array.Copy(weight, _weight, weight.Length)
        Dim ret As New List(Of Integer)
        Dim count_positive As Integer
        For Each w As Integer In _weight
            If w Then count_positive += 1
        Next
        If count_positive < n Then Throw New InevitablePlayAgainstTeammateException()
        While n > 0
            Dim partial_sum As Integer() = PartialSum(_weight)
            Dim x As Integer = r.Next(_weight.Sum)
            For i As Integer = 0 To _weight.Length - 1
                If partial_sum(i) <= x Then Continue For
                ret.Add(i)
                If _weight(i) = 0 Then Throw New AssertError("空席のない卓が選択されました。")
                _weight(i) = 0
                n -= 1
                Exit For
            Next
        End While
        ret.Sort()
        Return ret.ToArray()
    End Function

    Private Function PartialSum(ByVal x As Integer()) As Integer()
        Dim ret(x.Length - 1) As Integer
        For i As Integer = 0 To x.Length - 1
            ret(i) = x(i)
            If i Then ret(i) += ret(i - 1)
        Next
        Return ret
    End Function

    Public Function ones(ByVal n As Integer) As Integer()
        Dim ret(n - 1) As Integer
        For i As Integer = 0 To n - 1
            ret(i) = 1
        Next
        Return ret
    End Function

    Public Function shuffled(ByVal x As Integer()) As Integer()
        Dim n As Integer = x.Length
        Dim ret(n - 1) As Double
        For i As Integer = 0 To n - 1
            ret(i) = r.NextDouble()
        Next
        Array.Sort(ret, x)
        Return x
    End Function

    Public Sub swap(ByVal list As Integer(), ByVal i As Integer, ByVal j As Integer)
        Dim t As Integer = list(i)
        list(i) = list(j)
        list(j) = t
    End Sub

    Public Function dump(ByVal x As Integer()) As String
        dump = "{"
        Dim sep As String = ""
        For Each e As Integer In x
            dump &= sep & e.ToString()
            sep = ", "
        Next
        dump &= "}"
    End Function

End Module
