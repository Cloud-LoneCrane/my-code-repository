''' <summary>
''' ���һ��listview�����нڵ�ļ���
''' </summary>
''' <param name="lvwT">lsitview�ؼ�</param>
''' <returns> ArrayList ���͵�����</returns>
''' <remarks></remarks>
Private Function GetListViewItems(ByVal lvwT As ListView) As ArrayList
	Dim alTmp As ArrayList = New ArrayList

	Dim i As Integer = 0
	For i = 0 To lvwT.Items.Count - 1
		alTmp.Add(lvwT.Items.Item(i).Text)
	Next

	Return alTmp

End Function