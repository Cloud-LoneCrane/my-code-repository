
'-----------------------------------------------------------------------
'调用格式
Dim arStruct() As strtCertType

arStruct = ModVar.g_CertTypeArray

Dim arStr() As String = {"证件号", "证件名称"}

'填充ListView
ListView_Fill(lvwMain, arStruct, arStr)
'-----------------------------------------------------------------------

''' <summary>
'''  使用结构体数组 填充ListView
''' </summary>
''' <param name="lvwT">listView控件</param>
''' <param name="arObj">任意数组</param>
''' <param name="arColumnName">列头的名字数组</param>
''' <remarks>Sub FillListView(Of T) 使用了模板函数</remarks>
Public Sub ListView_Fill(Of T)(ByVal lvwT As ListView, ByVal arObj() As T, Optional ByVal arColumnName() As String = Nothing)
	Dim arFileInfo() As System.Reflection.FieldInfo
	Dim ObjType As System.Type
	Dim i As Int32 = 0
	Dim tmp As System.Reflection.FieldInfo
	Dim ColumnCount As Int32 = 0
	Dim blnUseColumnNameArray As Boolean = False '是否使用指定的列名


	blnUseColumnNameArray = Not (arColumnName Is Nothing)

	If arObj.Length > 0 Then
		'---------------------------------------------------
		' 1 写入列头
		ObjType = arObj(0).GetType()
		arFileInfo = ObjType.GetFields() '获得所有成员的名字

		With lvwT
			.Clear()
			.View = View.Details
		End With

		ColumnCount = arFileInfo.Length '列的个数

		For i = 0 To arFileInfo.Length - 1
			tmp = arFileInfo(i)
			With lvwT
				If blnUseColumnNameArray Then
					If arColumnName.Length > i Then
						.Columns.Add(tmp.Name, arColumnName(i))
					Else
						.Columns.Add(tmp.Name, tmp.Name)
					End If
				Else
					.Columns.Add(tmp.Name, tmp.Name)
				End If

			End With
		Next

		'---------------------------------------------------
		' 2 写入数据
		For i = 0 To arObj.Length - 1
			Dim strColumnValueArray() As String

			ReDim strColumnValueArray(ColumnCount - 1)
			Dim j As Int32 = 0
			For j = 0 To ColumnCount - 1
				strColumnValueArray(j) = ObjType.GetField(arFileInfo(j).Name).GetValue(arObj(i))
			Next

			With lvwT
				ListViewRow_Add(lvwT, strColumnValueArray)
			End With
		Next

	End If

End Sub
	
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




