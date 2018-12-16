''' <summary>
''' 为listView添加一行
''' </summary>
''' <param name="lvwT">listview控件</param>
''' <param name="strRowTextArray">包含要添加内容的数组</param>
''' <remarks>调用: ListViewRow_Add(lvwT, "PC_001")，建议为每个ColumnHeader设置Name属性</remarks>
Public Function ListViewRow_Add(ByVal lvwT As ListView, ByVal strRowTextArray() As String) As ListViewItem
	If lvwT.Columns.Count <= 0 Then
		Return Nothing
	End If

	Dim lvwItemTmp As New ListViewItem
	Try
		With lvwItemTmp
			.Text = strRowTextArray(0)
			.Name = lvwT.Columns.Item(0).Name

			Dim j As Int32 = 0
			For j = 1 To lvwT.Columns.Count - 1
				Dim lvwSubItemTmp As New ListViewItem.ListViewSubItem

				Try
					With lvwSubItemTmp
						.Name = lvwT.Columns.Item(j).Name
						.Text = strRowTextArray(j)
					End With
				Catch ex As Exception
					Dim strErr As String = ""
					strErr = ex.ToString
					MsgBox(strErr)
					Return lvwItemTmp
				End Try

				lvwItemTmp.SubItems.Add(lvwSubItemTmp)
			Next
		End With
		lvwT.Items.Add(lvwItemTmp)
	Catch ex As Exception
		Dim strErr As String = ""
		strErr = ex.ToString
		MsgBox(strErr)
		Return lvwItemTmp
	End Try
	Return lvwItemTmp
End Function

'-----------------------------------------------------------------------
'设置
With lvwT
	.View = View.Details
	.FullRowSelect = True

	.Columns.Add("HostName", "机器名", 100, HorizontalAlignment.Left, 0)
	.Columns.Add("IP", "IP", 100, HorizontalAlignment.Left, 0)
	.Columns.Add("Zone", "区域", 100, HorizontalAlignment.Left, 0)
End With

'-----------------------------------------------------------------------
'调用
Dim strArray() As String = {"PC_AAA", "192.168.100.21", "测试区"}
	ListViewRow_Add(lvwT, strArray)
	
	