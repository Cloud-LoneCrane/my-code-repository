
'确定
MessageBox.Show("区域名不能为空", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
  
'是否   
Dim strMsg As String = ""
	strMsg = "您确定要解除锁定吗?"
	If System.Windows.Forms.DialogResult.Yes = MessageBox.Show(strMsg, Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
		Me.Close()
	End If
        
If Trim(txtReEnterPwd.Text) <> strPwd Then
	MessageBox.Show(My.Resources.RS_PwdNotSame, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
	txtReEnterPwd.Focus()
	Exit Sub
End If

'检查是否为空,False 为空 True 不为空
If True = modFunc.NullDetect(txtProcName, lblProcName) Then
	Exit Sub
End If


''' <summary>
''' 检查是否为空,False 为空 True 不为空
''' </summary>
''' <param name="ctrl">需要检查text属性的控件</param>
''' <param name="lblTip">对应的表示ctrl控件对应内容的描述的label</param>
''' <returns>False 为空 True 不为空</returns>
''' <remarks></remarks>
Public Function NullDetect(ByVal ctrl As System.Windows.Forms.Control, ByVal lblTip As Label) As Boolean
	If Trim(ctrl.Text) = "" Then
		MessageBox.Show(lblTip.Text & My.Resources.RS_CanntAllowNull, frmMain.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
		Return True
	End If
End Function
