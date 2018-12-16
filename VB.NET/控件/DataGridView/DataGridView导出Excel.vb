#Region "DataGridView������ʾ��Excel"
    ''' <summary>    
    ''' ��Excel����DataGridView�ؼ������ݵ�����Excel   
    ''' </summary>    
    ''' <param name="dgv">DataGridView���� </param>    
    ''' <param name="isShowExcle">�Ƿ���ʾExcel���� </param>    
    ''' <remarks>   
    ''' add com "Microsoft Excel 11.0 Object Library"   
    ''' using Excel=Microsoft.Office.Interop.Excel;   
    ''' </remarks>   
    ''' <returns> </returns>    
    Public Function DataGridviewShowToExcel(ByVal dgv As DataGridView, ByVal isShowExcle As Boolean) As Boolean
        If dgv.Rows.Count = 0 Then
            Return False
        End If
        '����Excel����    
        Dim excel As New Excel.Application()
        excel.Application.Workbooks.Add(True)
        excel.Visible = isShowExcle
        '�����ֶ�����    
        For i As Integer = 0 To dgv.ColumnCount - 1
            excel.Cells(1, i + 1) = dgv.Columns(i).HeaderText
        Next
        '�������    
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

