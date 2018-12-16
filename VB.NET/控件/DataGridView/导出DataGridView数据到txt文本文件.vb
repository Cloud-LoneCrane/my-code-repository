''' <summary>
''' 导出DataGridView数据到txt文本文件中
''' </summary>
Public Sub DataGridViewToText(ByVal dgv As DataGridView)
	Dim fw As StreamWriter, ReadString As String = ""
	Dim dlg As New SaveFileDialog()
	dlg.Filter = "Execl files (*.txt)|*.txt"
	dlg.FilterIndex = 0
	dlg.RestoreDirectory = True
	dlg.CreatePrompt = True
	dlg.Title = "保存为文本(*.txt)文件"

	If dlg.ShowDialog() = DialogResult.OK Then
		Dim st As Stream
		st = dlg.OpenFile()
		fw = New StreamWriter(st, Encoding.UTF8)

		If dgv.RowCount < 1 Then
			MessageBox.Show("没有需要打印的数据!", "数据打印", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Exit Sub
		End If
		' fw = New StreamWriter("E:\test.txt", False)

		'------------------------打印列头-------------------------
		ReadString = dgv.Columns(0).HeaderCell.Value.ToString
		For I As Integer = 1 To dgv.ColumnCount - 1
			Dim strTmp As String = dgv.Columns(I).HeaderCell.Value.ToString.Replace(vbTab, "")
			ReadString = ReadString & vbTab & strTmp
		Next
		fw.WriteLine(ReadString)

		'---------------------------------------------------------
		'打印DataGridView中的数据
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

		MessageBox.Show("数据导出完成!", "数据打印", MessageBoxButtons.OK, MessageBoxIcon.Information)
	End If

End Sub