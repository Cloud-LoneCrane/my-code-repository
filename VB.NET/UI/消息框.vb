
'ȷ��
MessageBox.Show("����������Ϊ��", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
  
'�Ƿ�   
Dim strMsg As String = ""
	strMsg = "��ȷ��Ҫ���������?"
	If System.Windows.Forms.DialogResult.Yes = MessageBox.Show(strMsg, Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
		Me.Close()
	End If
        
If Trim(txtReEnterPwd.Text) <> strPwd Then
	MessageBox.Show(My.Resources.RS_PwdNotSame, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
	txtReEnterPwd.Focus()
	Exit Sub
End If

'����Ƿ�Ϊ��,False Ϊ�� True ��Ϊ��
If True = modFunc.NullDetect(txtProcName, lblProcName) Then
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
