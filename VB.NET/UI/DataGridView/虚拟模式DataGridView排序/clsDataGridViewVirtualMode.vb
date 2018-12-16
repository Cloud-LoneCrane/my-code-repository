'------------------------------------------------------------------------------------------
'功能：虚拟模式DataGridView排序
'编写人: jifle
'编写时间: 2010-06-03 13:55
'使用例子:
' 一 new 个对象
' 二 每次更新时，调用下SetDataBinding 设置 需要绑定的DataTable和DataGridView控件，即可
' 三 注意 ：一个实例只能绑定一个控件
'  eg.  .SetDataBinding(g_Conn.CreateDataTable("select * from view_zhxx limit 100000"), dgvTest)

'  Dim dgvVm As New clsDgvVirtualMode
'
'Private Sub btnFill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFill.Click
'    With dgvVm
'        .SetDataBinding(g_Conn.CreateDataTable("select * from view_zhxx limit 100000"), dgvTest)
'        .SetRowBackColor(Drawing.Color.White, Drawing.Color.Lavender)
'    End With
'    clsDgvVirtualMode.m_FirstColumnName = "编号"

'End Sub
'
''------------------------------------------------------------------------------------------

Imports System.Windows.Forms

''' <summary>
''' 虚拟模式DataGridView排序
''' </summary>
''' <remarks></remarks>
Public Class clsDgvVirtualMode
    ''' <summary>
    ''' 用来存储DataGridView
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dgvTmp As DataGridView

    ''' <summary>
    ''' 数据表
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dttTmp As DataTable

    ''' <summary>
    ''' 视图
    ''' </summary>
    ''' <remarks></remarks>
    Private m_dtvTmp_ASC As New DataView
    Private m_dtvTmp_DESC As New DataView

    Private m_CurDataView As DataView

    Private m_CurColumn As DataGridViewColumn
    Private m_blnAsc As Boolean = True
    Private m_strSort As String = "↑"
    Private m_LastColumn As New DataGridViewColumn
    Private m_Binding As Boolean = False

    ''' <summary>
    ''' 表头的文字显示
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared m_FirstColumnName As String = "No."

    ''' <summary>
    ''' 数据表
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
    ''' 数据绑定
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

        '设置控件为虚拟模式
        m_dgvTmp.VirtualMode = True

        '创建所有列
        m_dgvTmp.Columns.Clear()
        For i = 0 To m_dttTmp.Columns.Count - 1

            Dim column As New DataGridViewTextBoxColumn
            'Dim column As New DataGridViewColumn

            column.Name = m_dttTmp.Columns(i).ColumnName
            column.HeaderText = m_dttTmp.Columns(i).Caption '标题
            column.ValueType = m_dttTmp.Columns(i).DataType '数据类型排序时候用

            With m_dgvTmp
                .Columns.Add(column)
            End With
        Next

        '设置控件为虚拟模式
        m_dgvTmp.VirtualMode = True
        '行总数
        m_dgvTmp.Rows.Clear()
        m_dgvTmp.RowCount = m_dttTmp.Rows.Count

        '视图
        '正序
        m_dtvTmp_ASC = New DataView(m_dttTmp)

        If m_dttTmp.Columns.Count > 0 Then
            strTmp = m_dttTmp.Columns(0).ColumnName '列名
        End If
        'strTmp = "用户编号"
        With m_dtvTmp_ASC
            .Sort = strTmp & " " & "ASC"
        End With

        '倒序
        m_dtvTmp_DESC = New DataView(m_dttTmp)

        If m_dttTmp.Columns.Count > 0 Then
            strTmp = m_dttTmp.Columns(0).ColumnName '列名
        End If

        With m_dtvTmp_DESC
            .Sort = strTmp & " " & "DESC"
        End With


        m_CurDataView = m_dtvTmp_ASC

        If Not m_Binding Then
            'Debug.Print("     '绑定事件处理过程")
            '绑定事件处理过程
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
    ''' 按列排序
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
    ''' 取DataView
    ''' </summary>
    ''' <param name="column">用来排序的列</param>
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

        If m_strSort = "↑" Then
            column.HeaderText = m_LastColumn.HeaderText + " " + m_strSort
            m_strSort = "↓ "
        Else
            column.HeaderText = m_LastColumn.HeaderText + " " + m_strSort
            m_strSort = "↑"
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
    ''' 取单元格值
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
    ''' 为DataGridView添件行号
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>在 DataGridView1_RowPostPaint()过程中调用 eg.DataGridView_RowNum_ADD(sender, e)</remarks>
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

        '编号 标题
        Dim rectangle1 As Rectangle = New Rectangle(0, 2.55, _
            dgvTmp.RowHeadersWidth - 4, dgvTmp.ColumnHeadersHeight)

        TextRenderer.DrawText(dgvTmp.CreateGraphics, m_FirstColumnName, _
         dgvTmp.RowHeadersDefaultCellStyle.Font, _
         rectangle1, _
         dgvTmp.RowHeadersDefaultCellStyle.ForeColor, _
         TextFormatFlags.VerticalCenter Or TextFormatFlags.Right)
    End Sub

    ''' <summary>
    ''' 设置行的背景颜色
    ''' </summary>
    ''' <param name="oddColor">奇数行背景色</param>
    ''' <param name="evenColor">偶数行背景色</param>
    ''' <remarks></remarks>
    Public Sub SetRowBackColor(ByVal oddColor As Color, ByVal evenColor As Color)
        If m_dgvTmp Is Nothing Then
            Return
        End If

        If m_dttTmp Is Nothing Then
            Return
        End If

        '颜色
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