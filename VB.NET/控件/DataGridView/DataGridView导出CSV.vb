#Region "DateGridView������csv��ʽ��Excel"
	''' <summary>   
	''' ���÷�������֮���\t��һ��һ����������ļ���ʵ��csv�ļ�������Ĭ�Ͽ��Ե���Excel�򿪡�   
	''' </summary>   
	''' <remarks>   
	''' using System.IO;   
	''' </remarks>   
	''' <param name="dgv"></param>   
	Private Sub DataGridViewToExcel(dgv As DataGridView)
		Dim dlg As New SaveFileDialog()
		dlg.Filter = "Execl files (*.xls)|*.xls"
		dlg.FilterIndex = 0
		dlg.RestoreDirectory = True
		dlg.CreatePrompt = True
		dlg.Title = "����ΪExcel�ļ�"

		If dlg.ShowDialog() = DialogResult.OK Then
			Dim myStream As Stream
			myStream = dlg.OpenFile()
			Dim sw As New StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0))
			Dim columnTitle As String = ""
			Try
				'д���б���   
				For i As Integer = 0 To dgv.ColumnCount - 1
					If i > 0 Then
						columnTitle += vbTab
					End If
					columnTitle += dgv.Columns(i).HeaderText
				Next
				sw.WriteLine(columnTitle)

				'д��������   
				For j As Integer = 0 To dgv.Rows.Count - 1
					Dim columnValue As String = ""
					For k As Integer = 0 To dgv.Columns.Count - 1
						If k > 0 Then
							columnValue += vbTab
						End If
						If dgv.Rows(j).Cells(k).Value Is Nothing Then
							columnValue += ""
						Else
							columnValue += dgv.Rows(j).Cells(k).Value.ToString().Trim()
						End If
					Next
					sw.WriteLine(columnValue)
				Next
				sw.Close()
				myStream.Close()
			Catch e As Exception
				MessageBox.Show(e.ToString())
			Finally
				sw.Close()
				myStream.Close()
			End Try
		End If
	End Sub
	#End Region