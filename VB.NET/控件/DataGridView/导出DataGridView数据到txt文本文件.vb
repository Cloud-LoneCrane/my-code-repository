''' <summary>
''' ����DataGridView���ݵ�txt�ı��ļ���
''' </summary>
Public Sub DataGridViewToText(ByVal dgv As DataGridView)
	Dim fw As StreamWriter, ReadString As String = ""
	Dim dlg As New SaveFileDialog()
	dlg.Filter = "Execl files (*.txt)|*.txt"
	dlg.FilterIndex = 0
	dlg.RestoreDirectory = True
	dlg.CreatePrompt = True
	dlg.Title = "����Ϊ�ı�(*.txt)�ļ�"

	If dlg.ShowDialog() = DialogResult.OK Then
		Dim st As Stream
		st = dlg.OpenFile()
		fw = New StreamWriter(st, Encoding.UTF8)

		If dgv.RowCount < 1 Then
			MessageBox.Show("û����Ҫ��ӡ������!", "���ݴ�ӡ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End If
		' fw = New StreamWriter("E:\test.txt", False)

		'------------------------��ӡ��ͷ-------------------------
		ReadString = dgv.Columns(0).HeaderCell.Value.ToString
		For I As Integer = 1 To dgv.ColumnCount - 1
			Dim strTmp As String = dgv.Columns(I).HeaderCell.Value.ToString.Replace(vbTab, "")
			ReadString = ReadString & vbTab & strTmp
		Next
		fw.WriteLine(ReadString)

		'---------------------------------------------------------
		'��ӡDataGridView�е�����
		For K As Integer = 0 To dgv.RowCount - 1
			ReadString = dgv.Rows(K).Cells(0).Value.ToString
			Dim strTmp As String = ""
			For I As Integer = 1 To dgv.ColumnCount - 1
				strTmp = dgv.Rows(K).Cells(I).Value.ToString().Replace(vbCrLf, "")
				strTmp = strTmp.Replace(vbTab, "")
				ReadString = ReadString & vbTab & strTmp
			Next I
			fw.WriteLine(ReadString)
		Next K

		fw.Close()
		st.Close()

		MessageBox.Show("���ݵ������!", "���ݴ�ӡ", MessageBoxButtons.OK, MessageBoxIcon.Information)
	End If

End Sub