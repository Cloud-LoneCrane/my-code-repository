If True = NullDetect(txtOldPwd, lblOldPwd) Then
		txtOldPwd.Focus()
		Exit Sub
End If

''' <summary>
''' ����Ƿ�Ϊ��,False Ϊ�� True ��Ϊ��
''' </summary>
''' <param name="ctrl">��Ҫ���text���ԵĿؼ�</param>
''' <param name="lblTip">��Ӧ�ı�ʾctrl�ؼ���Ӧ���ݵ�������label</param>
''' <returns>False Ϊ�� True ��Ϊ��</returns>
''' <remarks></remarks>
Public Function NullDetect(ByVal ctrl As System.Windows.Forms.Control, ByVal lblTip As Label) As Boolean
	If Trim(ctrl.Text) = "" Then
		MessageBox.Show(lblTip.Text & My.Resources.RS_CanntAllowNull, frmMain.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
		Return True
	End If
End Function

''' <summary>
''' �ǿռ��
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
