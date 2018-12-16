Imports System.Windows.Forms
Imports System.Windows.Forms.ProgressBar
Imports System.ComponentModel

''' <summary>
''' ����ҳ���кţ���ɫ,��������DataGridView
''' </summary>
''' <remarks>����ҳ���кţ���ɫ,��������DataGridView</remarks>
<Browsable(True), _
              DefaultValue(""), _
              Description("����ҳ���кţ���ɫ,��������DataGridView,��д��:jiftle")> _
Public Class YQSDataGridView
    Inherits System.Windows.Forms.DataGridView
    Private ds As New DataSet
    Private ToltalPage As Integer = 0
    Private NowPage As Integer = 0
    Private OnePageRow As Integer = 100
    Private nowrowno As Integer = 0


    Private m_BarProgressBarAdded As Boolean = False

    ''' <summary>
    ''' ������
    ''' </summary>
    ''' <remarks></remarks>
    Public pBar As New System.Windows.Forms.ProgressBar


    ''' <summary>
    ''' ������
    ''' </summary>
    ''' <remarks></remarks>
    Private intTotalRowCount As Int32 = 0

    Private colorOddRowColor As Drawing.Color = Drawing.Color.White
    Private colorEvenRowColor As Drawing.Color = Drawing.Color.Lavender

    Dim m_dtTmp As DataTable

    ''' <summary>
    ''' ������
    ''' </summary>
    ''' <remarks></remarks>
    <Browsable(True), _
               DefaultValue("0"), _
               Description("������")> _
     Public ReadOnly Property TotalRowCount() As Integer
        Get
            Return intTotalRowCount
        End Get
    End Property


    ''' <summary>
    ''' ҳ�Ĵ�С,ÿҳ��������¼
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>ҳ�Ĵ�С,ÿҳ��������¼</remarks>
    <Browsable(True), _
                 DefaultValue("100"), _
                 Description("ҳ�Ĵ�С,ÿҳ��������¼")> _
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
    ''' ��ǰҳ��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(True), _
              DefaultValue("0"), _
              Description("��ǰҳ��")> _
 Public ReadOnly Property CurrentPageNum() As Integer
        Get
            Return NowPage
        End Get
    End Property

    ''' <summary>
    ''' ��ҳ��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(True), _
                 DefaultValue("0"), _
                 Description("��ҳ��")> _
    Public ReadOnly Property TotalPageCount() As Integer
        Get
            Return ToltalPage
        End Get
    End Property


    ''' <summary>
    ''' ��������ɫ 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>��������ɫ</remarks>
    <Browsable(True), _
                  DefaultValue("White"), _
                  Description("��������ɫ")> _
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
    ''' ż������ɫ
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>��������ɫ</remarks>
    <Browsable(True), _
              DefaultValue(""), _
              Description("��������ɫ")> _
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
    ''' ��ӱ�����ɫ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PageDataGridview_DataBindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles Me.DataBindingComplete
        DataGridView_Row_Color(sender, colorOddRowColor, colorEvenRowColor)
    End Sub

    ''''' <summary>
    ''''' ����Դ�ı�
    ''''' </summary>
    ''''' <param name="sender"></param>
    ''''' <param name="e"></param>
    ''''' <remarks></remarks>
    Private Sub PageDataGridview_DataSourceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DataSourceChanged
        Debug.Print("DataSourceChanged ------------" & Now)
        '----------------------------------------------------------------
        '��������ˣ�����¼�, ʹ��Dock���Ե�Fillʱ�����ܻ����
        '�Զ��ı��С��ʾ������������ 
        '���������
        '   ������û����OnDatasourceChanged
        '----------------------------------------------------------------
        Dim Datagrid As DataGridView = CType(sender, DataGridView) '����һ��DatagridView������

        Dim dtTmp As DataTable

        dtTmp = CType(Datagrid.DataSource, DataTable) '��ǰ�ؼ�����Դ���� 

        If m_dtTmp.Rows.Count > 0 Then '��������Դ�еļ�¼��
            ToltalPage = 1
            NowPage = 1
            If PageSize = 0 Then
                '  Exit Sub
                PageSize = 100
            End If

            '��������
            Dim table As DataTable = m_dtTmp
            Dim tol As Integer = 0 '�ܼ�¼��
            Dim temptolpage As Integer = 0 '��ҳ��

            tol = table.Rows.Count '������
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

                    .ForeColor = Drawing.Color.RoyalBlue '��������ǰ��ɫ
                    If m_BarProgressBarAdded Then
                        pBar.Visible = True
                    Else
                        Me.Controls.Add(pBar)
                        m_BarProgressBarAdded = True
                    End If


                    '-----------------------------------------------------------------------
                    '2010.5.29
                    '����������ˮƽ����������ʧ�ܣ�ԭ��δ֪�� 
                    '2010.5.29 11��00
                    '������ ��
                    '       �ڸ�����ʾʱ������ SuspendLayout �� SuspendLayout 

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

            '��ҳ��*ÿҳ��¼�� = �ܼ�¼��
            If temptolpage * PageSize >= tol Then
                ToltalPage = temptolpage
            Else
                ToltalPage = temptolpage + 1 '���С��
            End If

            nowrowno = 0
            ds.Tables.Clear()

            'ҳ���¼��
            Dim intPageSizeTmp As Int32 = 0
            If PageSize > tol Then
                intPageSizeTmp = tol
            Else
                intPageSizeTmp = PageSize
            End If

            '�����б���б���
            For tbcount As Integer = 0 To ToltalPage - 1

                Dim temptable As New DataTable

                '�����ͷ
                For i As Integer = 0 To m_dtTmp.Columns.Count - 1
                    Dim col As New DataColumn
                    col.ColumnName = m_dtTmp.Columns(i).ColumnName
                    temptable.Columns.Add(col)
                Next

                'Ϊÿһҳ(ÿһ����)��Ӽ�¼
                For s As Integer = 1 To intPageSizeTmp
                    '----------------------------------------------------------
                    '������
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

                    '��ȫ��׷����ϣ����ؽ�����
                    If nowrowno = m_dtTmp.Rows.Count Then
                        If pBar Is Nothing Then
                        Else
                            pBar.Value = pBar.Maximum
                            'pBar.Visible = False
                        End If
                        Exit For '����ѭ��
                    End If


                    If m_dtTmp.Rows.Count > nowrowno Then

                        '��������Ϣ
                        If nowrowno Mod 200 = 0 Then
                            Application.DoEvents()
                        End If

                        '������
                        temptable.ImportRow(m_dtTmp.Rows(nowrowno))
                        nowrowno += 1
                    Else
                        Exit For
                    End If

                Next

                '���뵽DataSet��
                ds.Tables.Add(temptable)
            Next

            '���½�����״̬
            If pBar Is Nothing Then
            Else
                pBar.Value = pBar.Maximum
                pBar.Visible = False

            End If


            Debug.Print("DataSourceChanged ------------" & Now)
            ''���������С��0
            'If ds.Tables.Count > 0 Then
            '    dtTmp.Rows.Clear() '��DataSource���

            '    For n As Integer = 0 To intPageSizeTmp - 1

            '        '��������Ϣ
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
    '''  ��д�����OnDataSourceChanged������Ŀ�ļӿ����������ٶ�
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
    ''' ��һҳ
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
    ''' ��һҳ
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
    '''��һҳ 
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
    ''' ���һҳ
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
    ''' ��ת��ָ��ҳ
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
    ''' ΪDataGridView����к�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>�� DataGridView1_RowPostPaint()�����е��� eg.DataGridView_RowNum_ADD(sender, e)</remarks>
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
    ''' ΪDataGridView�������ñ���ɫ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="colorOdd">������</param>
    ''' <param name="colorEven">ż����</param>
    ''' <remarks> dgvAcc_DataBindingComplete() �е��� ,��ʽ:DataGridView_Row_Color(sender, Color.White, Color.Lavender)</remarks>
    Private Sub DataGridView_Row_Color(ByVal sender As Object, ByVal colorOdd As System.Drawing.Color, ByVal colorEven As System.Drawing.Color)
        Dim dgvTmp As System.Windows.Forms.DataGridView

        dgvTmp = sender

        '����ÿһ�� 
        For Each dgvRowTmp As DataGridViewRow In dgvTmp.Rows
            'Application.DoEvents() ��������˸
            'ȡ�ض��е�ֵ����������INDEX 
            Dim i As Int32 = 0
            i = dgvRowTmp.Index '��ǰ������

            If i Mod 2 = 0 Then 'ż��
                dgvRowTmp.DefaultCellStyle.BackColor = colorOdd
            Else '����
                dgvRowTmp.DefaultCellStyle.BackColor = colorEven
            End If

        Next
    End Sub

    ''' <summary>
    ''' �����������ڼ��ض���
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
    ''' ����С�����ı��ʱ��ͨ���ı�DataSource��ֵ��ǿ�Ƹı������С����Ӧ������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>   
    ''' <remarks>��С�����Զ�����Ӧ����DataGridView�ؼ����������</remarks>
    Private Sub YQSDataGridView_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        ToPageNum(NowPage, True)
    End Sub
End Class

