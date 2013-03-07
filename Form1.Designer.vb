<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Teams = New System.Windows.Forms.TextBox
        Me.bGen = New System.Windows.Forms.Button
        Me.Output = New System.Windows.Forms.TextBox
        Me.lRound = New System.Windows.Forms.Label
        Me.Rounds = New System.Windows.Forms.NumericUpDown
        Me.Loops = New System.Windows.Forms.NumericUpDown
        Me.lLoop = New System.Windows.Forms.Label
        CType(Me.Rounds, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Loops, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Teams
        '
        Me.Teams.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Teams.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.Teams.Location = New System.Drawing.Point(8, 8)
        Me.Teams.Name = "Teams"
        Me.Teams.Size = New System.Drawing.Size(184, 19)
        Me.Teams.TabIndex = 0
        Me.Teams.Text = "4,5,6,7,7,8,12,13,14"
        '
        'bGen
        '
        Me.bGen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bGen.Location = New System.Drawing.Point(200, 8)
        Me.bGen.Name = "bGen"
        Me.bGen.Size = New System.Drawing.Size(75, 23)
        Me.bGen.TabIndex = 1
        Me.bGen.Text = "&Generate"
        Me.bGen.UseVisualStyleBackColor = True
        '
        'Output
        '
        Me.Output.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Output.Location = New System.Drawing.Point(8, 64)
        Me.Output.Multiline = True
        Me.Output.Name = "Output"
        Me.Output.Size = New System.Drawing.Size(264, 154)
        Me.Output.TabIndex = 2
        '
        'lRound
        '
        Me.lRound.AutoSize = True
        Me.lRound.Location = New System.Drawing.Point(8, 40)
        Me.lRound.Name = "lRound"
        Me.lRound.Size = New System.Drawing.Size(37, 12)
        Me.lRound.TabIndex = 3
        Me.lRound.Text = "&Round"
        '
        'Rounds
        '
        Me.Rounds.Location = New System.Drawing.Point(56, 40)
        Me.Rounds.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.Rounds.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Rounds.Name = "Rounds"
        Me.Rounds.Size = New System.Drawing.Size(72, 19)
        Me.Rounds.TabIndex = 4
        Me.Rounds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Rounds.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'Loops
        '
        Me.Loops.Location = New System.Drawing.Point(200, 40)
        Me.Loops.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.Loops.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.Loops.Name = "Loops"
        Me.Loops.Size = New System.Drawing.Size(72, 19)
        Me.Loops.TabIndex = 6
        Me.Loops.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Loops.Value = New Decimal(New Integer() {10000, 0, 0, 0})
        '
        'lLoop
        '
        Me.lLoop.AutoSize = True
        Me.lLoop.Location = New System.Drawing.Point(152, 40)
        Me.lLoop.Name = "lLoop"
        Me.lLoop.Size = New System.Drawing.Size(29, 12)
        Me.lLoop.TabIndex = 5
        Me.lLoop.Text = "&Loop"
        '
        'Form1
        '
        Me.AcceptButton = Me.bGen
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 225)
        Me.Controls.Add(Me.Loops)
        Me.Controls.Add(Me.lLoop)
        Me.Controls.Add(Me.Rounds)
        Me.Controls.Add(Me.lRound)
        Me.Controls.Add(Me.Output)
        Me.Controls.Add(Me.bGen)
        Me.Controls.Add(Me.Teams)
        Me.MinimumSize = New System.Drawing.Size(300, 150)
        Me.Name = "Form1"
        Me.Text = "Tournament Organizer"
        CType(Me.Rounds, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Loops, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Teams As System.Windows.Forms.TextBox
    Friend WithEvents bGen As System.Windows.Forms.Button
    Friend WithEvents Output As System.Windows.Forms.TextBox
    Friend WithEvents lRound As System.Windows.Forms.Label
    Friend WithEvents Rounds As System.Windows.Forms.NumericUpDown
    Friend WithEvents Loops As System.Windows.Forms.NumericUpDown
    Friend WithEvents lLoop As System.Windows.Forms.Label

End Class
