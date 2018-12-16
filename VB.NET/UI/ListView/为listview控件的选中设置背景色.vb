''' <summary>
''' 为listview控件的选中设置背景色
''' </summary>
''' <param name="lvwT">listview控件</param>
''' <param name="colorM">背景色</param>
''' <remarks>在listview的SelectedIndexChanged中调用，如果在Click事件中调用会有闪烁</remarks>
Private Sub ListView_BackColor_Set(ByRef lvwT As ListView, ByVal colorM As Color)

	'清空所有item的颜色
	Dim i As Integer = 0
	For i = 0 To lvwT.Items.Count - 1
		lvwT.Items.Item(i).BackColor = Color.White
	Next

	'为当前选中的item设置背景色
	For i = 0 To lvwT.SelectedItems.Count - 1
		lvwT.SelectedItems.Item(i).BackColor = colorM
	Next
End Sub