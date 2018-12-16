''' <summary>
''' ��ʾtooltip ��ָ��λ��
''' </summary>
''' <param name="ttTip">tooltip�ؼ�</param>
''' <param name="ctrl"></param>
''' <param name="strTitle">����</param>
''' <param name="strInfo">��ʾ��Ϣ</param>
''' <param name="pos">λ��</param>
''' <param name="duration">����ʱ��</param>
''' <returns>�Ƿ�ɹ�</returns>
''' <remarks></remarks>
Public Function ShowToolTip(ByVal ttTip As System.Windows.Forms.ToolTip, _
			 ByVal ctrl As Control, ByVal strTitle As String, ByVal strInfo As String, ByVal pos As _
				 System.Drawing.Point, ByVal duration As Int32) As Boolean
	ShowToolTip = False

	With ttTip
		.SetToolTip(ctrl, "")
		.ToolTipIcon = Windows.Forms.ToolTipIcon.Info
		.IsBalloon = True

		.ToolTipTitle = strTitle
		.Show(strInfo, ctrl, pos, duration)
	End With

	ShowToolTip = True
End Function