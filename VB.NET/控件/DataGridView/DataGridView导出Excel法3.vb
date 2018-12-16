#Region "DataGridView导出到Excel，有一定的判断性"
    ''' <summary>    
    '''方法，导出DataGridView中的数据到Excel文件    
    ''' </summary>    
    ''' <remarks>   
    ''' add com "Microsoft Excel 11.0 Object Library"   
    ''' using Excel=Microsoft.Office.Interop.Excel;   
    ''' using System.Reflection;   
    ''' </remarks>   
    ''' <param name= "dgv"> DataGridView </param>    
    Public Sub DataGridViewToExcel(ByVal dgv As DataGridView)


        '#Region "验证可操作性"

        '申明保存对话框    
        Dim dlg As New SaveFileDialog()
        '默然文件后缀    
        dlg.DefaultExt = "xls "
        '文件后缀列表    
        dlg.Filter = "EXCEL文件(*.XLS)|*.xls "
        '默然路径是系统当前路径    
        dlg.InitialDirectory = Directory.GetCurrentDirectory()
        '打开保存对话框    
        If dlg.ShowDialog() = DialogResult.Cancel Then
            Return
        End If
        '返回文件路径    
        Dim fileNameString As String = dlg.FileName
        '验证strFileName是否为空或值无效    
        If fileNameString.Trim() = " " Then
            Return
        End If
        '定义表格内数据的行数和列数    
        Dim rowscount As Integer = dgv.Rows.Count
        Dim colscount As Integer = dgv.Columns.Count
        '行数必须大于0    
        If rowscount <= 0 Then
            MessageBox.Show("没有数据可供保存 ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        '列数必须大于0    
        If colscount <= 0 Then
            MessageBox.Show("没有数据可供保存 ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        '行数不可以大于65536    
        If rowscount > 65536 Then
            MessageBox.Show("数据记录数太多(最多不能超过65536条)，不能保存 ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        '列数不可以大于255    
        If colscount > 255 Then
            MessageBox.Show("数据记录行数太多，不能保存 ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        '验证以fileNameString命名的文件是否存在，如果存在删除它    
        Dim file As New FileInfo(fileNameString)
        If file.Exists Then
            Try
                file.Delete()
            Catch [error] As Exception
                MessageBox.Show([error].Message, "删除失败 ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End Try
        End If
        '#End Region
        Dim objExcel As Excel.Application = Nothing
        Dim objWorkbook As Excel.Workbook = Nothing
        Dim objsheet As Excel.Worksheet = Nothing
        Try
            '申明对象    
            objExcel = New Excel.Application()
            objWorkbook = objExcel.Workbooks.Add(Missing.Value)
            objsheet = DirectCast(objWorkbook.ActiveSheet, Excel.Worksheet)
            '设置EXCEL不可见    
            objExcel.Visible = False

            '向Excel中写入表格的表头    
            Dim displayColumnsCount As Integer = 1
            For i As Integer = 0 To dgv.ColumnCount - 1
                If dgv.Columns(i).Visible = True Then
                    objExcel.Cells(1, displayColumnsCount) = dgv.Columns(i).HeaderText.Trim()
                    displayColumnsCount += 1
                End If
            Next
            '设置进度条    
            'tempProgressBar.Refresh();    
            'tempProgressBar.Visible   =   true;    
            'tempProgressBar.Minimum=1;    
            'tempProgressBar.Maximum=dgv.RowCount;    
            'tempProgressBar.Step=1;    
            '向Excel中逐行逐列写入表格中的数据    
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
            '隐藏进度条    
            'tempProgressBar.Visible   =   false;    
            '保存文件    
            objWorkbook.SaveAs(fileNameString, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, _
             Excel.XlSaveAsAccessMode.xlShared, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value)
        Catch [error] As Exception
            MessageBox.Show([error].Message, "警告 ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        Finally
            '关闭Excel应用    
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
        MessageBox.Show(fileNameString & vbLf & vbLf & "导出完毕! ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

#End Region