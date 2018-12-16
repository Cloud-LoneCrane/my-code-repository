Imports System.Windows.Forms
Imports System.Windows.Forms.ProgressBar
Imports System.ComponentModel

''' <summary>
''' 带分页，行号，颜色,进度条的DataGridView
''' </summary>
''' <remarks>带分页，行号，颜色,进度条的DataGridView</remarks>
<Browsable(True), _
              DefaultValue(""), _
              Description("带分页，行号，颜色,进度条的DataGridView,编写人:jiftle")> _
Public Class YQSDataGridView
    Inherits System.Windows.Forms.DataGridView
    Private ds As New DataSet
    Private ToltalPage As Integer = 0
    Private NowPage As Integer = 0
    Private OnePageRow As Integer = 100
    Private nowrowno As Integer = 0


    Private m_BarProgressBarAdded As Boolean = False

    ''' <summary>
    ''' 进度条
    ''' </summary>
    ''' <remarks></remarks>
    Public pBar As New System.Windows.Forms.ProgressBar


    ''' <summary>
    ''' 总行数
    ''' </summary>
    ''' <remarks></remarks>
    Private intTotalRowCount As Int32 = 0

    Private colorOddRowColor As Drawing.Color = Drawing.Color.White
    Private colorEvenRowColor As Drawing.Color = Drawing.Color.Lavender

    Dim m_dtTmp As DataTable

    ''' <summary>
    ''' 总行数
    ''' </summary>
    ''' <remarks></remarks>
    <Browsable(True), _
               DefaultValue("0"), _
               Description("总行数")> _
     Public ReadOnly Property TotalRowCount() As Integer
        Get
            Return intTotalRowCount
        End Get
    End Property


    ''' <summary>
    ''' 页的大小,每页多少条记录
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>页的大小,每页多少条记录</remarks>
    <Browsable(True), _
                 DefaultValue("100"), _
                 Description("页的大小,每页多少条记录")> _
      Public Property PageSize() As Integer
        Get
            Return OnePageRow
        End Get
        Set(ByVal value As Integer)
            If value < 0 Then
                Exit Property
            End If
            If value = 0 Then
                '  Exit Sub
                OnePageRow = 100
            End If

            OnePageRow = value
        End Set
    End Property

    ''' <summary>
    ''' 当前页号
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(True), _
              DefaultValue("0"), _
              Description("当前页号")> _
 Public ReadOnly Property CurrentPageNum() As Integer
        Get
            Return NowPage
        End Get
    End Property

    ''' <summary>
    ''' 总页数
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(True), _
                 DefaultValue("0"), _
                 Description("总页数")> _
    Public ReadOnly Property TotalPageCount() As Integer
        Get
            Return ToltalPage
        End Get
    End Property


    ''' <summary>
    ''' 奇数行颜色 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>奇数行颜色</remarks>
    <Browsable(True), _
                  DefaultValue("White"), _
                  Description("奇数行颜色")> _
     Public Property OddRowColor() As Drawing.Color
        Get
            Return colorOddRowColor
        End Get
        Set(ByVal value As Drawing.Color)
            If value.R = 0 AndAlso value.G = 0 AndAlso value.B = 0 Then
            Else
                colorOddRowColor = value
            End If

        End Set
    End Property

    ''' <summary>
    ''' 偶数行颜色
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>奇数行颜色</remarks>
    <Browsable(True), _
              DefaultValue(""), _
              Description("奇数行颜色")> _
 Public Property EvenRowColor() As Drawing.Color
        Get
            Return colorEvenRowColor
        End Get
        Set(ByVal value As Drawing.Color)
            If value.R = 0 AndAlso value.G = 0 AndAlso value.B = 0 Then
            Else
                colorEvenRowColor = value
            End If

        End Set
    End Property

    ''' <summary>
    ''' 添加背景颜色
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PageDataGridview_DataBindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles Me.DataBindingComplete
        DataGridView_Row_Color(sender, colorOddRowColor, colorEvenRowColor)
    End Sub

    ''''' <summary>
    ''''' 数据源改变
    ''''' </summary>
    ''''' <param name="sender"></param>
    ''''' <param name="e"></param>
    ''''' <remarks></remarks>
    Private Sub PageDataGridview_DataSourceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DataSourceChanged
        Debug.Print("DataSourceChanged ------------" & Now)
        '----------------------------------------------------------------
        '如果处理了，这个事件, 使用Dock属性的Fill时，可能会出现
        '自动改变大小显示不正常的现象 
        '解决方法：
        '   必须调用基类的OnDatasourceChanged
        '----------------------------------------------------------------
        Dim Datagrid As DataGridView = CType(sender, DataGridView) '创建一个DatagridView的引用

        Dim dtTmp As DataTable

        dtTmp = CType(Datagrid.DataSource, DataTable) '当前控件数据源引用 

        If m_dtTmp.Rows.Count > 0 Then '计算数据源中的记录数
            ToltalPage = 1
            NowPage = 1
            If PageSize = 0 Then
                '  Exit Sub
                PageSize = 100
            End If

            '建立引用
            Dim table As DataTable = m_dtTmp
            Dim tol As Integer = 0 '总记录数
            Dim temptolpage As Integer = 0 '总页数

            tol = table.Rows.Count '总行数
            intTotalRowCount = tol

            If pBar Is Nothing Then
            Else
                With pBar
                    .Visible = True
                    .Style = ProgressBarStyle.Continuous
                    .Maximum = tol
                    .Minimum = 1
                    .Step = 1

                    .Value = 1

                    .Left = MyBase.HorizontalScrollBar.Left
                    .Top = Me.Bottom - pBar.Height
                    .Width = Me.Width

                    .ForeColor = Drawing.Color.RoyalBlue '进度条的前景色
                    If m_BarProgressBarAdded Then
                        pBar.Visible = True
                    Else
                        Me.Controls.Add(pBar)
                        m_BarProgressBarAdded = True
                    End If


                    '-----------------------------------------------------------------------
                    '2010.5.29
                    '在这里设置水平滚动条隐藏失败，原因未知道 
                    '2010.5.29 11：00
                    '问题解决 ：
                    '       在更改显示时，调用 SuspendLayout 和 SuspendLayout 

                    Me.SuspendLayout()
                    Me.HorizontalScrollBar.Visible = False

                    pBar.Visible = True
                    Me.SuspendLayout()
                    '-----------------------------------------------------------------------


                End With


            End If

            Me.Refresh()

            If tol > PageSize Then
                temptolpage = CInt(Int(tol / PageSize))
            Else
                temptolpage = 1
            End If

            '总页数*每页记录数 = 总记录数
            If temptolpage * PageSize >= tol Then
                ToltalPage = temptolpage
            Else
                ToltalPage = temptolpage + 1 '如果小于
            End If

            nowrowno = 0
            ds.Tables.Clear()

            '页面记录数
            Dim intPageSizeTmp As Int32 = 0
            If PageSize > tol Then
                intPageSizeTmp = tol
            Else
                intPageSizeTmp = PageSize
            End If

            '对所有表进行遍历
            For tbcount As Integer = 0 To ToltalPage - 1

                Dim temptable As New DataTable

                '添加列头
                For i As Integer = 0 To m_dtTmp.Columns.Count - 1
                    Dim col As New DataColumn
                    col.ColumnName = m_dtTmp.Columns(i).ColumnName
                    temptable.Columns.Add(col)
                Next

                '为每一页(每一个表)添加记录
                For s As Integer = 1 To intPageSizeTmp
                    '----------------------------------------------------------
                    '进度条
                    If pBar Is Nothing Then
                    Else
                        Dim intTmp As Int32 = 0
                        intTmp = pBar.Value

                        intTmp += 1

                        If intTmp > pBar.Maximum Then
                        Else
                            pBar.Value += 1
                            pBar.Text = pBar.Value & "/" & pBar.Maximum
                        End If

                    End If
                    '----------------------------------------------------------

                    '若全部追加完毕，隐藏进度条
                    If nowrowno = m_dtTmp.Rows.Count Then
                        If pBar Is Nothing Then
                        Else
                            pBar.Value = pBar.Maximum
                            'pBar.Visible = False
                        End If
                        Exit For '跳出循环
                    End If


                    If m_dtTmp.Rows.Count > nowrowno Then

                        '处理窗口消息
                        If nowrowno Mod 200 = 0 Then
                            Application.DoEvents()
                        End If

                        '导入行
                        temptable.ImportRow(m_dtTmp.Rows(nowrowno))
                        nowrowno += 1
                    Else
                        Exit For
                    End If

                Next

                '加入到DataSet中
                ds.Tables.Add(temptable)
            Next

            '更新进度条状态
            If pBar Is Nothing Then
            Else
                pBar.Value = pBar.Maximum
                pBar.Visible = False

            End If


            Debug.Print("DataSourceChanged ------------" & Now)
            ''若表的总数小于0
            'If ds.Tables.Count > 0 Then
            '    dtTmp.Rows.Clear() '对DataSource清空

            '    For n As Integer = 0 To intPageSizeTmp - 1

            '        '处理窗口消息
            '        If n Mod 200 = 0 Then
            '            Application.DoEvents()
            '        End If

            '        dtTmp.ImportRow(ds.Tables(0).Rows(n))
            '    Next
            'End If

        End If

        Me.SuspendLayout()
        pBar.Visible = False
        Me.HorizontalScrollBar.Visible = True
        Me.SuspendLayout()

        Debug.Print("DataSourceChanged ------------" & Now)
    End Sub

    ''' <summary>
    '''  重写基类的OnDataSourceChanged方法，目的加快载入数据速度
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnDataSourceChanged( _
    ByVal e As EventArgs _
    )

        Debug.Print("OnDataSourceChanged ------------" & Now)

        Dim dtTmp As DataTable
        dtTmp = CType(Me.DataSource, DataTable)

        m_dtTmp = dtTmp.Copy

        dtTmp.Rows.Clear()

        If PageSize <= 0 Then
            PageSize = 100
        End If
        Dim i As Int32 = 0
        For Each tmp As DataRow In m_dtTmp.Rows
            If i > PageSize - 1 Then
                Exit For
            End If

            If i Mod 20 = 0 Then
                Application.DoEvents()
            End If
            dtTmp.ImportRow(tmp)
            i += 1

            nowrowno = 1
        Next
        Me.Refresh()

        Debug.Print("OnDataSourceChanged ------------" & Now)

        MyBase.OnDataSourceChanged(e)
        Debug.Print(" MyBase.OnDataSourceChangede ---------------------" & Now)
    End Sub


    ''' <summary>
    ''' 下一页
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ToNext() As Boolean
        ToNext = False

        If Me.DataSource Is Nothing Then
            Exit Function
        End If
        If NowPage = ToltalPage Then
            Exit Function
        Else
            CType(Me.DataSource, DataTable).Rows.Clear()

            For i As Integer = 0 To ds.Tables(NowPage).Rows.Count - 1
                CType(Me.DataSource, DataTable).ImportRow(ds.Tables(NowPage).Rows(i))
            Next
            NowPage += 1
        End If

        ToNext = True
    End Function

    ''' <summary>
    ''' 上一页
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ToUp() As Boolean
        ToUp = False

        If Me.DataSource Is Nothing Then
            Exit Function
        End If
        If NowPage = 1 Then
            Exit Function
        Else
            CType(Me.DataSource, DataTable).Rows.Clear()
            For i As Integer = 0 To ds.Tables(NowPage - 2).Rows.Count - 1
                CType(Me.DataSource, DataTable).ImportRow(ds.Tables(NowPage - 2).Rows(i))
            Next
            NowPage -= 1
        End If

        ToUp = True
    End Function

    ''' <summary>
    '''第一页 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ToFirst() As Boolean
        ToFirst = False

        If Me.DataSource Is Nothing Then
            Exit Function
        End If
        If NowPage = 1 Then
            Exit Function
        Else
            CType(Me.DataSource, DataTable).Rows.Clear()
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                CType(Me.DataSource, DataTable).ImportRow(ds.Tables(0).Rows(i))
            Next
            NowPage = 1
        End If

        ToFirst = True

    End Function

    ''' <summary>
    ''' 最后一页
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ToLast() As Boolean

        ToLast = False

        If Me.DataSource Is Nothing Then
            Exit Function
        End If
        If NowPage = ToltalPage Then
            Exit Function
        Else
            CType(Me.DataSource, DataTable).Rows.Clear()
            For i As Integer = 0 To ds.Tables(ToltalPage - 1).Rows.Count - 1
                CType(Me.DataSource, DataTable).ImportRow(ds.Tables(ToltalPage - 1).Rows(i))
            Next
            NowPage = ToltalPage
        End If
        ToLast = True
    End Function

    ''' <summary>
    ''' 跳转到指定页
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ToPageNum(ByVal intPageNum As Int32, Optional ByVal blnForce As Boolean = False) As Boolean
        ToPageNum = False

        If Me.DataSource Is Nothing Then
            Exit Function
        End If

        If blnForce Then
            If intPageNum <= TotalPageCount Then
                CType(Me.DataSource, DataTable).Rows.Clear()

                Dim i As Int32 = 0
                For i = 0 To ds.Tables(intPageNum - 1).Rows.Count - 1
                    CType(Me.DataSource, DataTable).ImportRow(ds.Tables(intPageNum - 1).Rows(i))
                Next

                NowPage = intPageNum
            End If
        Else
            If intPageNum <= TotalPageCount And intPageNum <> NowPage Then
                CType(Me.DataSource, DataTable).Rows.Clear()

                Dim i As Int32 = 0
                For i = 0 To ds.Tables(intPageNum - 1).Rows.Count - 1
                    CType(Me.DataSource, DataTable).ImportRow(ds.Tables(intPageNum - 1).Rows(i))
                Next

                NowPage = intPageNum
            End If
        End If

        ToPageNum = True
    End Function



    Private Sub PageDataGridview_RowPostPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles Me.RowPostPaint
        DataGridView_RowNum_ADD(sender, e)
    End Sub

    ''' <summary>
    ''' 为DataGridView添件行号
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>在 DataGridView1_RowPostPaint()过程中调用 eg.DataGridView_RowNum_ADD(sender, e)</remarks>
    Private Sub DataGridView_RowNum_ADD(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs)

        If e.RowIndex >= PageSize Then
            Exit Sub

        Else
            If Me.RowCount < PageSize Then
                If e.RowIndex = Me.RowCount - 1 Then
                    Exit Sub
                End If
            End If
        End If


        Dim dgvTmp As System.Windows.Forms.DataGridView

        dgvTmp = sender
        Dim rectangle As Drawing.Rectangle = New Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, _
              dgvTmp.RowHeadersWidth - 4, _
              e.RowBounds.Height)

        TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), _
         dgvTmp.RowHeadersDefaultCellStyle.Font, _
         rectangle, _
         dgvTmp.RowHeadersDefaultCellStyle.ForeColor, _
         TextFormatFlags.VerticalCenter Or TextFormatFlags.Right)

    End Sub

    ''' <summary>
    ''' 为DataGridView的行设置背景色
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="colorOdd">奇数行</param>
    ''' <param name="colorEven">偶数行</param>
    ''' <remarks> dgvAcc_DataBindingComplete() 中调用 ,格式:DataGridView_Row_Color(sender, Color.White, Color.Lavender)</remarks>
    Private Sub DataGridView_Row_Color(ByVal sender As Object, ByVal colorOdd As System.Drawing.Color, ByVal colorEven As System.Drawing.Color)
        Dim dgvTmp As System.Windows.Forms.DataGridView

        dgvTmp = sender

        '遍历每一行 
        For Each dgvRowTmp As DataGridViewRow In dgvTmp.Rows
            'Application.DoEvents() ’画面闪烁
            '取特定列的值，列索引是INDEX 
            Dim i As Int32 = 0
            i = dgvRowTmp.Index '当前的索引

            If i Mod 2 = 0 Then '偶数
                dgvRowTmp.DefaultCellStyle.BackColor = colorOdd
            Else '奇数
                dgvRowTmp.DefaultCellStyle.BackColor = colorEven
            End If

        Next
    End Sub

    ''' <summary>
    ''' 播放数据正在加载动画
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub PlayProgressBarAvi()
        With pBar
            .Visible = True
            .Style = ProgressBarStyle.Marquee
            .Maximum = 100
            .Minimum = 1
            .Step = 1

            .Value = 1

            Me.HorizontalScrollBar.Visible = False
            .Left = Me.HorizontalScrollBar.Left

            .Top = Me.Bottom - .Height
            .Width = Me.Width

            If m_BarProgressBarAdded Then
                pBar.Visible = True
            Else
                Me.Controls.Add(pBar)
                m_BarProgressBarAdded = True
                pBar.Visible = True
            End If

            .ForeColor = Drawing.Color.RoyalBlue
            Me.SuspendLayout()
            Me.HorizontalScrollBar.Visible = False
            pBar.Visible = True
            Me.SuspendLayout()
        End With


        Me.Refresh()
    End Sub

    ''' <summary>
    ''' 当大小发生改变的时候，通过改变DataSource的值来强制改变调整大小以适应其容器
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>   
    ''' <remarks>大小不会自动调整应该是DataGridView控件本身的问题</remarks>
    Private Sub YQSDataGridView_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        ToPageNum(NowPage, True)
    End Sub
End Class

