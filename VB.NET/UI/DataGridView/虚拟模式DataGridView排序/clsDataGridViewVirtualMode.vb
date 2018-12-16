'------------------------------------------------------------------------------------------
'���ܣ�����ģʽDataGridView����
'��д��: jifle
'��дʱ��: 2010-06-03 13:55
'ʹ������:
' һ new ������
' �� ÿ�θ���ʱ��������SetDataBinding ���� ��Ҫ�󶨵�DataTable��DataGridView�ؼ�������
' �� ע�� ��һ��ʵ��ֻ�ܰ�һ���ؼ�
'  eg.  .SetDataBinding(g_Conn.CreateDataTable("select * from view_zhxx limit 100000"), dgvTest)

'  Dim dgvVm As New clsDgvVirtualMode
'
'Private Sub btnFill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFill.Click
'    With dgvVm
'        .SetDataBinding(g_Conn.CreateDataTable("select * from view_zhxx limit 100000"), dgvTest)
'        .SetRowBackColor(Drawing.Color.White, Drawing.Color.Lavender)
'    End With
'    clsDgvVirtualMode.m_FirstColumnName = "���"

'End Sub
'
''------------------------------------------------------------------------------------------

Imports System.Windows.Forms

''' <summary>
''' ����ģʽDataGridView����
''' </summary>
''' <remarks></remarks>
Public Class clsDgvVirtualMode
    ''' <summary>
    ''' �����洢DataGridView
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dgvTmp As DataGridView

    ''' <summary>
    ''' ���ݱ�
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dttTmp As DataTable

    ''' <summary>
    ''' ��ͼ
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dtvTmp_ASC As New DataView
    Private m_dtvTmp_DESC As New DataView

    Private m_CurDataView As DataView

    Private m_CurColumn As DataGridViewColumn
    Private m_blnAsc As Boolean = True
    Private m_strSort As String = "��"
    Private m_LastColumn As New DataGridViewColumn
    Private m_Binding As Boolean = False

    ''' <summary>
    ''' ��ͷ��������ʾ
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared m_FirstColumnName As String = "No."

    ''' <summary>
    ''' ���ݱ�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DataTable() As DataTable
        Get
            Return m_dttTmp
        End Get

    End Property



    ''' <summary>
    ''' ���ݰ�
    ''' </summary>
    ''' <param name="dttTmp"></param>
    ''' <param name="dgvCtrl"></param>
    ''' <remarks></remarks>
    Public Sub SetDataBinding(ByVal dttTmp As DataTable, ByVal dgvCtrl As DataGridView)
        Dim strTmp As String = ""
        Dim i As Int32 = 0

        m_dgvTmp = dgvCtrl
        m_dttTmp = dttTmp

        If m_dgvTmp Is Nothing Then
            Return
        End If

        If m_dttTmp Is Nothing Then
            Return
        End If

        '���ÿؼ�Ϊ����ģʽ
        m_dgvTmp.VirtualMode = True

        '����������
        m_dgvTmp.Columns.Clear()
        For i = 0 To m_dttTmp.Columns.Count - 1

            Dim column As New DataGridViewTextBoxColumn
            'Dim column As New DataGridViewColumn

            column.Name = m_dttTmp.Columns(i).ColumnName
            column.HeaderText = m_dttTmp.Columns(i).Caption '����
            column.ValueType = m_dttTmp.Columns(i).DataType '������������ʱ����

            With m_dgvTmp
                .Columns.Add(column)
            End With
        Next

        '���ÿؼ�Ϊ����ģʽ
        m_dgvTmp.VirtualMode = True
        '������
        m_dgvTmp.Rows.Clear()
        m_dgvTmp.RowCount = m_dttTmp.Rows.Count

        '��ͼ
        '����
        m_dtvTmp_ASC = New DataView(m_dttTmp)

        If m_dttTmp.Columns.Count > 0 Then
            strTmp = m_dttTmp.Columns(0).ColumnName '����
        End If
        'strTmp = "�û����"
        With m_dtvTmp_ASC
            .Sort = strTmp & " " & "ASC"
        End With

        '����
        m_dtvTmp_DESC = New DataView(m_dttTmp)

        If m_dttTmp.Columns.Count > 0 Then
            strTmp = m_dttTmp.Columns(0).ColumnName '����
        End If

        With m_dtvTmp_DESC
            .Sort = strTmp & " " & "DESC"
        End With


        m_CurDataView = m_dtvTmp_ASC

        If Not m_Binding Then
            'Debug.Print("     '���¼��������")
            '���¼��������
            AddHandler m_dgvTmp.CellValueNeeded, AddressOf CellValueNeeded
            AddHandler m_dgvTmp.ColumnHeaderMouseClick, AddressOf MyColumnHeaderMouseClick
            AddHandler m_dgvTmp.RowPostPaint, AddressOf RowPostPaint

            m_Binding = True
        End If

    End Sub

    Private Sub CellValueNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs)
        GetCellValue(sender, e)
    End Sub

    Private Sub MyColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
        SortByColumnName(sender, e)
    End Sub

    Private Sub RowPostPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs)
        clsDgvVirtualMode.DataGridView_RowNum_ADD(sender, e)
    End Sub
    ''' <summary>
    ''' ��������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub SortByColumnName(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)

        If m_dgvTmp Is Nothing Then
            Return
        End If

        If m_dttTmp Is Nothing Then
            Return
        End If


        Dim dgvTmp As DataGridView

        dgvTmp = sender
        m_CurColumn = dgvTmp.Columns(e.ColumnIndex)

        m_CurDataView = GetDataView(m_CurColumn)
        '   m_dgvTmp.DataSource = Nothing

        '    Debug.Print(m_CurDataView.Sort)
        ' Debug.Print(m_CurDataView.ApplyDefaultSort)

        'If m_dgvTmp.DataSource Is Nothing Then
        'Else
        '    'Debug.Print(m_dgvTmp.SortedColumn.Name)
        '    'Debug.Print(m_dgvTmp.SortOrder)
        '    'Debug.Print(m_dgvTmp.DataSource.ToString)
        'End If

        m_dgvTmp.Refresh()

        Dim pos As Point
        With pos
            .X = 0
            .Y = 0
        End With
        m_dgvTmp.AutoScrollOffset = pos

    End Sub

    ''' <summary>
    ''' ȡDataView
    ''' </summary>
    ''' <param name="column">�����������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDataView(ByVal column As DataGridViewColumn) As DataView

        If m_dgvTmp Is Nothing Then
            Return Nothing
        End If

        If m_dttTmp Is Nothing Then
            Return Nothing
        End If


        Dim strTmp As String = ""

        strTmp = column.Name

        If Not (m_LastColumn Is Nothing) Then
            For Each tmp As DataGridViewColumn In m_dgvTmp.Columns
                If tmp.Name = m_LastColumn.Name Then
                    tmp.HeaderText = m_LastColumn.HeaderText
                End If
            Next
        End If

        m_LastColumn = column.Clone

        If m_strSort = "��" Then
            column.HeaderText = m_LastColumn.HeaderText + " " + m_strSort
            m_strSort = "�� "
        Else
            column.HeaderText = m_LastColumn.HeaderText + " " + m_strSort
            m_strSort = "��"
        End If

        'm_dtvTmp_ASC.Dispose()
        'm_dtvTmp_ASC = New DataView(m_dttTmp)

        'With m_dtvTmp_ASC
        '    .Sort = strTmp & " " & "ASC"
        'End With

        'm_dtvTmp_DESC.Dispose()
        'm_dtvTmp_DESC = New DataView(m_dttTmp)
        'With m_dtvTmp_DESC
        '    .Sort = strTmp & " " & "DESC"
        'End With

        With m_dtvTmp_ASC
            .Sort = strTmp & " " & "ASC"
        End With

        With m_dtvTmp_DESC
            .Sort = strTmp & " " & "DESC"
        End With

        'Return m_dtvTmp_ASC
        If m_blnAsc Then
            m_blnAsc = Not m_blnAsc
            Return m_dtvTmp_ASC
        Else
            m_blnAsc = Not m_blnAsc
            Return m_dtvTmp_DESC
        End If
    End Function

    ''' <summary>
    ''' ȡ��Ԫ��ֵ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCellValue(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValueEventArgs) As Object

        If m_dgvTmp Is Nothing Then
            Return Nothing
        End If

        If m_dttTmp Is Nothing Then
            Return Nothing
        End If


        Dim dtvTmp As DataView

        dtvTmp = m_CurDataView

        If dtvTmp Is Nothing Then
            Return Nothing
        End If
        If e.ColumnIndex < dtvTmp.Table.Columns.Count Then
            If e.RowIndex < dtvTmp.Table.Rows.Count Then
                'Debug.Print("---------- GetCellValue ----------- " & dtvTmp.Sort)
                '   e.Value = dtvTmp.Table.Rows.Item(e.RowIndex).ItemArray(e.ColumnIndex)
                e.Value = dtvTmp.Item(e.RowIndex).Item(e.ColumnIndex)
                Return e.Value
            End If
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' ΪDataGridView����к�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>�� DataGridView1_RowPostPaint()�����е��� eg.DataGridView_RowNum_ADD(sender, e)</remarks>
    Public Shared Sub DataGridView_RowNum_ADD(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs)
        Dim dgvTmp As System.Windows.Forms.DataGridView

        dgvTmp = sender
        Dim rectangle As Rectangle = New Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, _
              dgvTmp.RowHeadersWidth - 4, _
              e.RowBounds.Height)

        TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), _
         dgvTmp.RowHeadersDefaultCellStyle.Font, _
         rectangle, _
         dgvTmp.RowHeadersDefaultCellStyle.ForeColor, _
         TextFormatFlags.VerticalCenter Or TextFormatFlags.Right)

        '��� ����
        Dim rectangle1 As Rectangle = New Rectangle(0, 2.55, _
            dgvTmp.RowHeadersWidth - 4, dgvTmp.ColumnHeadersHeight)

        TextRenderer.DrawText(dgvTmp.CreateGraphics, m_FirstColumnName, _
         dgvTmp.RowHeadersDefaultCellStyle.Font, _
         rectangle1, _
         dgvTmp.RowHeadersDefaultCellStyle.ForeColor, _
         TextFormatFlags.VerticalCenter Or TextFormatFlags.Right)
    End Sub

    ''' <summary>
    ''' �����еı�����ɫ
    ''' </summary>
    ''' <param name="oddColor">�����б���ɫ</param>
    ''' <param name="evenColor">ż���б���ɫ</param>
    ''' <remarks></remarks>
    Public Sub SetRowBackColor(ByVal oddColor As Color, ByVal evenColor As Color)
        If m_dgvTmp Is Nothing Then
            Return
        End If

        If m_dttTmp Is Nothing Then
            Return
        End If

        '��ɫ
        With m_dgvTmp
            .AlternatingRowsDefaultCellStyle.BackColor = oddColor
            .DefaultCellStyle.BackColor = evenColor
        End With
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()

        m_dtvTmp_ASC.Dispose()
        m_dtvTmp_DESC.Dispose()

    End Sub

  
End Class