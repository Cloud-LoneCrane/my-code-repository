''' <summary>
''' Ϊlistview�ؼ���ѡ�����ñ���ɫ
''' </summary>
''' <param name="lvwT">listview�ؼ�</param>
''' <param name="colorM">����ɫ</param>
''' <remarks>��listview��SelectedIndexChanged�е��ã������Click�¼��е��û�����˸</remarks>
Private Sub ListView_BackColor_Set(ByRef lvwT As ListView, ByVal colorM As Color)

	'�������item����ɫ
	Dim i As Integer = 0
	For i = 0 To lvwT.Items.Count - 1
		lvwT.Items.Item(i).BackColor = Color.White
	Next

	'Ϊ��ǰѡ�е�item���ñ���ɫ
	For i = 0 To lvwT.SelectedItems.Count - 1
		lvwT.SelectedItems.Item(i).BackColor = colorM
	Next
End Sub