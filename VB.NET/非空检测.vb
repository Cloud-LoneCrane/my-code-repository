If True = NullDetect(txtOldPwd, lblOldPwd) Then
		txtOldPwd.Focus()
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

''' <summary>
''' 非空检测
''' </summary>
''' <param name="ctrl"></param>
''' <returns></returns>
''' <remarks></remarks>
Private Function NullDetect(ByVal ctrl As System.Windows.Forms.Control) As Boolean
	If Trim(ctrl.Text) = "" Then
		MessageBox.Show(My.Resources.RS_CanntAllowNull, frmMain.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
		ctrl.Focus()
		Return True
	End If
End Function
