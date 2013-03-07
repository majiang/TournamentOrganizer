Public Class Form1

    Private Sub bGen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bGen.Click
        Dim TeamSizes As Integer() = getCondition()
        Try
            TeamSizes = getCondition()
        Catch ex As InvalidPlayerNumberException
            System.Windows.Forms.MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            Me.Output.Text = PlayerArrangementGenerator.Generate(TeamSizes, TeamArrangementGenerator.Generate(TeamSizes, Loops.Value, Rounds.Value, Loops.Value), New TimeSpan(0, 1, 0))
        Catch ex As InevitableDoublePlayException
            Me.Output.Text = ex.Message
        Catch ex As PlayerArrangementTimeoutException
            Me.Output.Text = ex.Message
        End Try
    End Sub

    Private Function getCondition() As Integer()
        Dim TeamSizes() As Integer = getTeamSizes()
        If TeamSizes.Sum() Mod 4 Then Throw New InvalidPlayerNumberException
        Return TeamSizes
    End Function

    Private Sub Teams_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Teams.KeyPress
        Dim okst As String = "0123456789, "
        For Each c As Char In okst
            If e.KeyChar = c Then
                Exit Sub
            End If
        Next
        If e.KeyChar = vbBack Then
            Exit Sub
        End If
        e.Handled = True
    End Sub

    Private Function getTeamSizes() As Integer()
        Dim teamal As String = Me.Teams.Text.Replace(" ", "").TrimStart("0")
        Dim ol As Integer = teamal.Length() + 1
        Dim l As Integer = teamal.Length()
        While l <> ol
            ol = l
            teamal = teamal.TrimStart("0").Trim(",").Replace(",,", ",").Replace(",0", ",")
            l = teamal.Length
        End While
        Dim team() As String = teamal.Split(",")
        Dim teams(team.Length - 1) As Integer
        For i As Integer = 0 To team.Length - 1
            Try
                teams(i) = CInt(team(i))
            Catch ex As Exception
                teams(i) = 0
            End Try
        Next
        Array.Sort(teams)
        Array.Reverse(teams)
        Return teams
    End Function

    Private Sub print(ByVal x As Object)
        System.Windows.Forms.MessageBox.Show(x.ToString())
    End Sub

    Private Sub printall(Of t)(ByVal x() As t)
        Dim str As String = ""
        For Each e As t In x
            str &= e.ToString() & vbCrLf
        Next
        print(str)
    End Sub
End Class

Public Class InvalidPlayerNumberException
    Inherits ApplicationException
    Public Sub New()
        MyBase.New("人数が4で割り切れません。")
    End Sub
End Class

