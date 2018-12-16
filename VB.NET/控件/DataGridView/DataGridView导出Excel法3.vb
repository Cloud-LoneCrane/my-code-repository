#Region "DataGridView������Excel����һ�����ж���"
    ''' <summary>    
    '''����������DataGridView�е����ݵ�Excel�ļ�    
    ''' </summary>    
    ''' <remarks>   
    ''' add com "Microsoft Excel 11.0 Object Library"   
    ''' using Excel=Microsoft.Office.Interop.Excel;   
    ''' using System.Reflection;   
    ''' </remarks>   
    ''' <param name= "dgv"> DataGridView </param>    
    Public Sub DataGridViewToExcel(ByVal dgv As DataGridView)


        '#Region "��֤�ɲ�����"

        '��������Ի���    
        Dim dlg As New SaveFileDialog()
        'ĬȻ�ļ���׺    
        dlg.DefaultExt = "xls "
        '�ļ���׺�б�    
        dlg.Filter = "EXCEL�ļ�(*.XLS)|*.xls "
        'ĬȻ·����ϵͳ��ǰ·��    
        dlg.InitialDirectory = Directory.GetCurrentDirectory()
        '�򿪱���Ի���    
        If dlg.ShowDialog() = DialogResult.Cancel Then
            Return
        End If
        '�����ļ�·��    
        Dim fileNameString As String = dlg.FileName
        '��֤strFileName�Ƿ�Ϊ�ջ�ֵ��Ч    
        If fileNameString.Trim() = " " Then
            Return
        End If
        '�����������ݵ�����������    
        Dim rowscount As Integer = dgv.Rows.Count
        Dim colscount As Integer = dgv.Columns.Count
        '�����������0    
        If rowscount <= 0 Then
            MessageBox.Show("û�����ݿɹ����� ", "��ʾ ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        '�����������0    
        If colscount <= 0 Then
            MessageBox.Show("û�����ݿɹ����� ", "��ʾ ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        '���������Դ���65536    
        If rowscount > 65536 Then
            MessageBox.Show("���ݼ�¼��̫��(��಻�ܳ���65536��)�����ܱ��� ", "��ʾ ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        '���������Դ���255    
        If colscount > 255 Then
            MessageBox.Show("���ݼ�¼����̫�࣬���ܱ��� ", "��ʾ ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        '��֤��fileNameString�������ļ��Ƿ���ڣ��������ɾ����    
        Dim file As New FileInfo(fileNameString)
        If file.Exists Then
            Try
                file.Delete()
            Catch [error] As Exception
                MessageBox.Show([error].Message, "ɾ��ʧ�� ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End Try
        End If
        '#End Region
        Dim objExcel As Excel.Application = Nothing
        Dim objWorkbook As Excel.Workbook = Nothing
        Dim objsheet As Excel.Worksheet = Nothing
        Try
            '��������    
            objExcel = New Excel.Application()
            objWorkbook = objExcel.Workbooks.Add(Missing.Value)
            objsheet = DirectCast(objWorkbook.ActiveSheet, Excel.Worksheet)
            '����EXCEL���ɼ�    
            objExcel.Visible = False

            '��Excel��д����ı�ͷ    
            Dim displayColumnsCount As Integer = 1
            For i As Integer = 0 To dgv.ColumnCount - 1
                If dgv.Columns(i).Visible = True Then
                    objExcel.Cells(1, displayColumnsCount) = dgv.Columns(i).HeaderText.Trim()
                    displayColumnsCount += 1
                End If
            Next
            '���ý�����    
            'tempProgressBar.Refresh();    
            'tempProgressBar.Visible   =   true;    
            'tempProgressBar.Minimum=1;    
            'tempProgressBar.Maximum=dgv.RowCount;    
            'tempProgressBar.Step=1;    
            '��Excel����������д�����е�����    
            For row As Integer = 0 To dgv.RowCount - 1
                'tempProgressBar.PerformStep();    

                displayColumnsCount = 1
                For col As Integer = 0 To colscount - 1
                    If dgv.Columns(col).Visible = True Then
                        Try
                            objExcel.Cells(row + 2, displayColumnsCount) = dgv.Rows(row).Cells(col).Value.ToString().Trim()
                            displayColumnsCount += 1

                        Catch generatedExceptionName As Exception

                        End Try
                    End If
                Next
            Next
            '���ؽ�����    
            'tempProgressBar.Visible   =   false;    
            '�����ļ�    
            objWorkbook.SaveAs(fileNameString, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, _
             Excel.XlSaveAsAccessMode.xlShared, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value)
        Catch [error] As Exception
            MessageBox.Show([error].Message, "���� ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        Finally
            '�ر�ExcelӦ��    
            If objWorkbook IsNot Nothing Then
                objWorkbook.Close(Missing.Value, Missing.Value, Missing.Value)
            End If
            If objExcel.Workbooks IsNot Nothing Then
                objExcel.Workbooks.Close()
            End If
            If objExcel IsNot Nothing Then
                objExcel.Quit()
            End If

            objsheet = Nothing
            objWorkbook = Nothing
            objExcel = Nothing
        End Try
        MessageBox.Show(fileNameString & vbLf & vbLf & "�������! ", "��ʾ ", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

#End Region