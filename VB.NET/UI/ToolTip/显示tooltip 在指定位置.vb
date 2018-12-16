''' <summary>
''' 显示tooltip 在指定位置
''' </summary>
''' <param name="ttTip">tooltip控件</param>
''' <param name="ctrl"></param>
''' <param name="strTitle">标题</param>
''' <param name="strInfo">显示信息</param>
''' <param name="pos">位置</param>
''' <param name="duration">持续时间</param>
''' <returns>是否成功</returns>
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