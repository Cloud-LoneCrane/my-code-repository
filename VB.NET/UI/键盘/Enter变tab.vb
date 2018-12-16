
'Enter变Tab
'一、将窗体的属性KeyPreview  = TRUE
'二、
Private Sub frmCreateAcc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
	If e.KeyCode = Keys.Enter Then
		My.Computer.Keyboard.SendKeys("{TAB}")
	End If
End Sub