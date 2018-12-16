
'-----------------------------------------------------------------------
'���ø�ʽ
Dim arStruct() As strtCertType

arStruct = ModVar.g_CertTypeArray

Dim arStr() As String = {"֤����", "֤������"}

'���ListView
ListView_Fill(lvwMain, arStruct, arStr)
'-----------------------------------------------------------------------

''' <summary>
'''  ʹ�ýṹ������ ���ListView
''' </summary>
''' <param name="lvwT">listView�ؼ�</param>
''' <param name="arObj">��������</param>
''' <param name="arColumnName">��ͷ����������</param>
''' <remarks>Sub FillListView(Of T) ʹ����ģ�庯��</remarks>
Public Sub ListView_Fill(Of T)(ByVal lvwT As ListView, ByVal arObj() As T, Optional ByVal arColumnName() As String = Nothing)
	Dim arFileInfo() As System.Reflection.FieldInfo
	Dim ObjType As System.Type
	Dim i As Int32 = 0
	Dim tmp As System.Reflection.FieldInfo
	Dim ColumnCount As Int32 = 0
	Dim blnUseColumnNameArray As Boolean = False '�Ƿ�ʹ��ָ��������


	blnUseColumnNameArray = Not (arColumnName Is Nothing)

	If arObj.Length > 0 Then
		'---------------------------------------------------
		' 1 д����ͷ
		ObjType = arObj(0).GetType()
		arFileInfo = ObjType.GetFields() '������г�Ա������

		With lvwT
			.Clear()
			.View = View.Details
		End With

		ColumnCount = arFileInfo.Length '�еĸ���

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
		' 2 д������
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




