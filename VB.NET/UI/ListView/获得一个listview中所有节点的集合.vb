''' <summary>
''' 获得一个listview中所有节点的集合
''' </summary>
''' <param name="lvwT">lsitview控件</param>
''' <returns> ArrayList 类型的数组</returns>
''' <remarks></remarks>
Private Function GetListViewItems(ByVal lvwT As ListView) As ArrayList
	Dim alTmp As ArrayList = New ArrayList

	Dim i As Integer = 0
	For i = 0 To lvwT.Items.Count - 1
		alTmp.Add(lvwT.Items.Item(i).Text)
	Next

	Return alTmp

End Function