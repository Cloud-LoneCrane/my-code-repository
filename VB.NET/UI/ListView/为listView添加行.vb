''' <summary>
''' ΪlistView���һ��
''' </summary>
''' <param name="lvwT">listview�ؼ�</param>
''' <param name="strRowTextArray">����Ҫ������ݵ�����</param>
''' <remarks>����: ListViewRow_Add(lvwT, "PC_001")������Ϊÿ��ColumnHeader����Name����</remarks>
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
'����
With lvwT
	.View = View.Details
	.FullRowSelect = True

	.Columns.Add("HostName", "������", 100, HorizontalAlignment.Left, 0)
	.Columns.Add("IP", "IP", 100, HorizontalAlignment.Left, 0)
	.Columns.Add("Zone", "����", 100, HorizontalAlignment.Left, 0)
End With

'-----------------------------------------------------------------------
'����
Dim strArray() As String = {"PC_AAA", "192.168.100.21", "������"}
	ListViewRow_Add(lvwT, strArray)
	
	