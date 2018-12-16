#Region "DataGridView数据显示到Excel"
    ''' <summary>    
    ''' 打开Excel并将DataGridView控件中数据导出到Excel   
    ''' </summary>    
    ''' <param name="dgv">DataGridView对象 </param>    
    ''' <param name="isShowExcle">是否显示Excel界面 </param>    
    ''' <remarks>   
    ''' add com "Microsoft Excel 11.0 Object Library"   
    ''' using Excel=Microsoft.Office.Interop.Excel;   
    ''' </remarks>   
    ''' <returns> </returns>    
    Public Function DataGridviewShowToExcel(ByVal dgv As DataGridView, ByVal isShowExcle As Boolean) As Boolean
        If dgv.Rows.Count = 0 Then
            Return False
        End If
        '建立Excel对象    
        Dim excel As New Excel.Application()
        excel.Application.Workbooks.Add(True)
        excel.Visible = isShowExcle
        '生成字段名称    
        For i As Integer = 0 To dgv.ColumnCount - 1
            excel.Cells(1, i + 1) = dgv.Columns(i).HeaderText
        Next
        '填充数据    
        For i As Integer = 0 To dgv.RowCount - 2
            For j As Integer = 0 To dgv.ColumnCount - 1
                If dgv(j, i).ValueType Is GetType(String) Then
                    excel.Cells(i + 2, j + 1) = "'" + dgv(j, i).Value.ToString()
                Else
                    excel.Cells(i + 2, j + 1) = dgv(j, i).Value.ToString()
                End If
            Next
        Next
        Return True
    End Function
#End Region

