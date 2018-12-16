
'-------------------------------------------------------------------------------------------------------------------------------------
'功能：查询条件组，更新
'编写人：jifle
'编写时间：20-10-06-02
'使用说明：
'   一 New 一个类的对象
'   二 设置要绑定数据的三个ComboBox控件
'   三 调用ItemsAdd
'   四 调用Initial 就可以了
'
'Dim m_qcgTmp As New clsQueryCtrlGroup
'Private Sub init()

'    With m_qcgTmp
'        .cboItem = cboItem
'        .cboOperator = cboQuery
'        .cboValue = cboValue
'    End With

'    m_qcgTmp.ItemsAdd("帐号", "帐号", clsQueryCtrlGroup.enmValueDataValue.DataValue_Text)
'    m_qcgTmp.ItemsAdd("姓名", "姓名", clsQueryCtrlGroup.enmValueDataValue.DataValue_Text)
'    m_qcgTmp.ItemsAdd("余额", "余额", clsQueryCtrlGroup.enmValueDataValue.DataValue_Numeric)

'    m_qcgTmp.ItemsAdd("积分", "积分", clsQueryCtrlGroup.enmValueDataValue.DataValue_Numeric)
'    m_qcgTmp.ItemsAdd("有效性", "有效性", clsQueryCtrlGroup.enmValueDataValue.DataValue_Bool)

'    m_qcgTmp.ItemsAdd("会员等级", "会员等级", clsQueryCtrlGroup.enmValueDataValue.DataValue_TextArray, _
'                                        modFunc.GetAccLevelNameArray(m_AccLevelArray))
'    m_qcgTmp.ItemsAdd("证件号", "证件号", clsQueryCtrlGroup.enmValueDataValue.DataValue_Text)

'    m_qcgTmp.ItemsAdd("操作员", "操作员", clsQueryCtrlGroup.enmValueDataValue.DataValue_Text)
'    m_qcgTmp.ItemsAdd("开户额", "开户额", clsQueryCtrlGroup.enmValueDataValue.DataValue_Numeric)

'    m_qcgTmp.ItemsAdd("开户时间", "开户时间", clsQueryCtrlGroup.enmValueDataValue.DataValue_DateTime)
'
'    '调用初始化过程
'    m_qcgTmp.Initial()
'
'End Sub
'-------------------------------------------------------------------------------------------------------------------------------------------------
Imports System.Windows.Forms
Imports System.Collections.ObjectModel

''' <summary>
''' 查询条件组对象
''' </summary>
''' <remarks></remarks>
Public Class clsQueryCtrlGroup

    '------------------------------------------------------------------------------------------------
    '常量定义
    ''' <summary>
    ''' 等于
    ''' </summary>
    ''' <remarks></remarks>
    Const cntEqual As String = "等于"
    ''' <summary>
    ''' 不等于
    ''' </summary>
    ''' <remarks></remarks>
    Const cntNotEqual As String = "不等于"
    ''' <summary>
    ''' 类似于
    ''' </summary>
    ''' <remarks></remarks>
    Const cntLike As String = "类似于"
    ''' <summary>
    ''' 大于
    ''' </summary>
    ''' <remarks></remarks>
    Const cntGreater As String = "大于"
    ''' <summary>
    ''' 小于
    ''' </summary>
    ''' <remarks></remarks>
    Const cntLess As String = "小于"
    ''' <summary>
    ''' 大于等于
    ''' </summary>
    ''' <remarks></remarks>
    Const cntGreaterAndEqual As String = "大于等于"
    ''' <summary>
    ''' 小于等于
    ''' </summary>
    ''' <remarks></remarks>
    Const cntLesserAndEqual As String = "小于等于"

    Const cntDateTime_Today As String = "今天"
    Const cntDateTime_Yesterday As String = "昨天"
    Const cntDateTime_TheDayBeforeYesterday As String = "前天"
    Const cntDateTime_ThisWeek As String = "本周"
    Const cntDateTime_LastWeek As String = "上周"
    Const cntDateTime_CurrentMonth As String = "本月"
    Const cntDateTime_Ultimo As String = "上月"
    Const cntDateTime_ThisYear As String = "今年"
    Const cntDateTime_LastYear As String = "去年"

    Const cntDateTime_Custom As String = "自定义"

    Const cntBool_Valid As String = "有效"
    Const cntBool_Invalid As String = "禁用"

    '------------------------------------------------------------------------------------------------

    ''' <summary>
    ''' 查询项目
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure strtQueryItem
        ''' <summary>
        ''' 名字也就是Key
        ''' </summary>
        ''' <remarks></remarks>
        Dim Name As String
        ''' <summary>
        ''' 显示文本 
        ''' </summary>
        ''' <remarks></remarks>
        Dim Text As String
        ''' <summary>
        ''' 索引
        ''' </summary>
        ''' <remarks></remarks>
        Dim index As Int32
        ''' <summary>
        ''' 数据类型
        ''' </summary>
        ''' <remarks></remarks>
        Dim DataType As enmValueDataValue
    End Structure

    ''' <summary>
    ''' 查询条件
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure strtQueryOperator
        ''' <summary>
        ''' 名字和key等价
        ''' </summary>
        ''' <remarks></remarks>
        Dim Name As String
        ''' <summary>
        ''' 显示文本
        ''' </summary>
        ''' <remarks></remarks>
        Dim Text As String
        ''' <summary>
        ''' 索引
        ''' </summary>
        ''' <remarks></remarks>
        Dim index As Int32
        '''' <summary>
        '''' 查询项目
        '''' </summary>
        '''' <remarks></remarks>
        'Dim QueryItemKey As String
    End Structure

    ''' <summary>
    ''' 查询值
    ''' </summary>
    ''' <remarks></remarks>
    Structure strtQueryValue
        ''' <summary>
        ''' 名字和key等价
        ''' </summary>
        ''' <remarks></remarks>
        Dim Name As String
        ''' <summary>
        ''' 显示文本
        ''' </summary>
        ''' <remarks></remarks>
        Dim Text As String
        ''' <summary>
        ''' 索引
        ''' </summary>
        ''' <remarks></remarks>
        Dim index As Int32
        ''' <summary>
        ''' 查询项目
        ''' </summary>
        ''' <remarks></remarks>
        Dim QueryItemKey As String
        ''' <summary>
        ''' 查询条件
        ''' </summary>
        ''' <remarks></remarks>
        Dim QueryOperatorKey As String
    End Structure

    ''' <summary>
    ''' 查询值的数据类型
    ''' </summary>
    ''' <remarks></remarks>
    Enum enmValueDataValue
        ''' <summary>
        ''' 文本
        ''' </summary>
        ''' <remarks></remarks>
        DataValue_Text = 0
        ''' <summary>
        ''' 数值
        ''' </summary>
        ''' <remarks></remarks>
        DataValue_Numeric
        ''' <summary>
        ''' 布尔
        ''' </summary>
        ''' <remarks></remarks>
        DataValue_Bool
        ''' <summary>
        ''' 日期时间
        ''' </summary>
        ''' <remarks></remarks>
        DataValue_DateTime
        ''' <summary>
        ''' 日期
        ''' </summary>
        ''' <remarks></remarks>
        DataValue_Date
        ''' <summary>
        ''' 时间
        ''' </summary>
        ''' <remarks></remarks>
        DataValue_Time
        ''' <summary>
        ''' 文本数组
        ''' </summary>
        ''' <remarks></remarks>
        DataValue_TextArray
    End Enum

    ''' <summary>
    ''' 查询组
    ''' </summary>
    ''' <remarks></remarks>
    Structure strtQueryGroup
        ''' <summary>
        ''' 查询项目
        ''' </summary>
        ''' <remarks></remarks>
        Dim Item As strtQueryItem
        ''' <summary>
        ''' 操作符
        ''' </summary>
        ''' <remarks></remarks>
        Dim Opera() As strtQueryOperator
        ''' <summary>
        ''' 查询值
        ''' </summary>
        ''' <remarks></remarks>
        Dim Value() As String
    End Structure

    ''' <summary>
    ''' 查询项目原子
    ''' </summary>
    ''' <remarks></remarks>
    Structure strtQuery
        ''' <summary>
        ''' 查询项目
        ''' </summary>
        ''' <remarks></remarks>
        Dim Item As strtQueryItem
        ''' <summary>
        ''' 查询值
        ''' </summary>
        ''' <remarks></remarks>
        Dim Value() As String
    End Structure

    ''' <summary>
    ''' 查询项目
    ''' </summary>
    ''' <remarks></remarks>
    Private m_cboItem As ComboBox

    ''' <summary>
    ''' 查询条件
    ''' </summary>
    ''' <remarks></remarks>
    Private m_cboOperator As ComboBox

    ''' <summary>
    '''查询值
    ''' </summary>
    ''' <remarks></remarks>
    Private m_cboValue As ComboBox

    ''' <summary>
    ''' 查询组
    ''' </summary>
    ''' <remarks></remarks>
    Private m_QueryCtrlValueArray() As strtQueryGroup = Nothing

    ''' <summary>
    ''' 查询值
    ''' </summary>
    ''' <remarks></remarks>
    Public QueryItems As New Collection(Of strtQuery)

    ''' <summary>
    ''' 获得查询语句
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSqlString() As String
        Dim strItem, strOperator, strValue As String
        Dim dteDateTime As Date = Now

        strItem = ""
        strOperator = ""
        strValue = ""

        strOperator = GetCurOperatorByText(cboOperator.Text)

        If strOperator = "LIKE" Then
            strValue = "%" + Trim(m_cboValue.Text) + "%"
        Else
            strValue = Trim(m_cboValue.Text)
        End If

        For Each tmp As strtQueryGroup In m_QueryCtrlValueArray
            If tmp.Item.Text = m_cboItem.Text Then
                strItem = tmp.Item.Name

                Dim strTmp As String = ""

                '查询条件
                Select Case tmp.Item.DataType
                    Case clsQueryCtrlGroup.enmValueDataValue.DataValue_DateTime

                        Dim strMyTmp As String = ""
                        strMyTmp = GetDetailTime(strValue, strItem)

                        If strMyTmp = "" Then
                            If IsDate(strValue) Then
                                dteDateTime = strValue
                                GetSqlString = " " + strItem + " " + strOperator + " '" + strValue + "' "
                            Else
                                GetSqlString = ""
                                Exit Function
                            End If
                        Else
                            strTmp = strMyTmp
                            GetSqlString = strTmp
                            Exit Function
                        End If


                    Case clsQueryCtrlGroup.enmValueDataValue.DataValue_TextArray, _
                      clsQueryCtrlGroup.enmValueDataValue.DataValue_Text
                        strTmp = "'" & strValue & "'"
                        strValue = strTmp
                            GetSqlString = " " + strItem + " " + strOperator + " " + strValue + " "
                            Exit Function

                    Case clsQueryCtrlGroup.enmValueDataValue.DataValue_Numeric
                        GetSqlString = " " + strItem + " " + strOperator + " " + strValue + " "
                        Exit Function

                    Case clsQueryCtrlGroup.enmValueDataValue.DataValue_Bool '布尔
                        Select Case strValue
                            Case cntBool_Valid
                                strValue = 1
                            Case cntBool_Invalid
                                strValue = 0
                        End Select
                        GetSqlString = " " + strItem + " " + strOperator + " " + strValue + " "
                        Exit Function

                    Case Else
                            strTmp = strValue
                            GetSqlString = " " + strItem + " " + strOperator + " " + strValue + " "
                End Select
                'strValue = strTmp

                Exit For
            End If
        Next




    End Function

    ''' <summary>
    ''' 获得具体的时间段
    ''' </summary>
    ''' <param name="strTimeDescribeConst"></param>
    ''' <param name="strItemName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDetailTime(ByVal strTimeDescribeConst As String, ByVal strItemName As String) As String
        GetDetailTime = ""

        Dim strStartTime, strEndTime, strTmp As String
        Dim dteStartTime, dteEndTime As Date

        strStartTime = ""
        strEndTime = ""
        strTmp = ""
        dteStartTime = Now
        dteEndTime = Now

        Select Case strTimeDescribeConst
            Case cntDateTime_Today
                dteStartTime = Now.Date
                dteEndTime = Now.Date

                strStartTime = dteStartTime.ToString("yyyy-MM-dd") & " " & "00:00:00"

                dteEndTime = DateAdd(DateInterval.Day, 1, dteEndTime)
                strEndTime = dteEndTime.ToString("yyyy-MM-dd") & " " & "00:00:00"

            Case cntDateTime_Yesterday
                dteStartTime = Now.Date
                dteEndTime = Now.Date

                dteStartTime = DateAdd(DateInterval.Day, -1, dteStartTime)
                strStartTime = dteStartTime.ToString("yyyy-MM-dd") & " " & "00:00:00"

                strEndTime = dteEndTime.ToString("yyyy-MM-dd") & " " & "00:00:00"

            Case cntDateTime_TheDayBeforeYesterday
                dteStartTime = Now.Date
                dteEndTime = Now.Date

                dteStartTime = DateAdd(DateInterval.Day, -2, dteStartTime)
                dteEndTime = DateAdd(DateInterval.Day, -1, dteEndTime)

                strStartTime = dteStartTime.ToString("yyyy-MM-dd") & " " & "00:00:00"

                strEndTime = dteEndTime.ToString("yyyy-MM-dd") & " " & "00:00:00"

            Case cntDateTime_CurrentMonth '本月
                dteStartTime = New Date(Now.Year, Now.Month, 1)
                'dteStartTime = DateAdd(DateInterval.Day, -1, dteStartTime)

                dteEndTime = New Date(Now.Year, Now.Month + 1, 1)
                ' dteEndTime = DateAdd(DateInterval.Day, -1, dteEndTime)
                strStartTime = dteStartTime.ToString("yyyy-MM-dd") & " " & "00:00:00"
                strEndTime = dteEndTime.ToString("yyyy-MM-dd") & " " & "00:00:00"

            Case cntDateTime_Ultimo '上月
                '开始时间
                dteStartTime = New Date(Now.Year, Now.Month - 1, 1)

                '结束时间
                dteEndTime = New Date(Now.Year, Now.Month, 1)

                strStartTime = dteStartTime.ToString("yyyy-MM-dd") & " " & "00:00:00"
                strEndTime = dteEndTime.ToString("yyyy-MM-dd") & " " & "00:00:00"

            Case cntDateTime_ThisYear
                Dim dteTmp As Date

                dteTmp = New Date(Now.Year, 1, 1)
                dteStartTime = dteTmp
                dteTmp = New Date(Now.Year + 1, 1, 1)
                dteEndTime = dteTmp

                strStartTime = dteStartTime.ToString("yyyy-MM-dd") & " " & "00:00:00"
                strEndTime = dteEndTime.ToString("yyyy-MM-dd") & " " & "00:00:00"
            Case cntDateTime_LastYear
                Dim dteTmp As Date

                dteTmp = New Date(Now.Year - 1, 1, 1)
                dteStartTime = dteTmp
                dteTmp = New Date(Now.Year, 1, 1)
                dteEndTime = dteTmp

                strStartTime = dteStartTime.ToString("yyyy-MM-dd") & " " & "00:00:00"
                strEndTime = dteEndTime.ToString("yyyy-MM-dd") & " " & "00:00:00"

            Case cntDateTime_ThisWeek '本周
                Dim dteTmp As Date
                Dim intTmp As Int16 = 0

                dteTmp = Now.Date '今天
                intTmp = dteTmp.DayOfWeek

                dteStartTime = DateAdd(DateInterval.Day, -intTmp + 1, dteTmp)
                dteEndTime = DateAdd(DateInterval.Day, +7, dteStartTime)

                strStartTime = dteStartTime.ToString("yyyy-MM-dd") & " " & "00:00:00"
                strEndTime = dteEndTime.ToString("yyyy-MM-dd") & " " & "00:00:00"
            Case cntDateTime_LastWeek
                Dim dteTmp As Date
                Dim intTmp As Int16 = 0

                dteTmp = Now.Date '今天
                intTmp = dteTmp.DayOfWeek

                dteStartTime = DateAdd(DateInterval.Day, -intTmp + 1, dteTmp)

                dteStartTime = DateAdd(DateInterval.Day, -7, dteStartTime)
                dteEndTime = DateAdd(DateInterval.Day, 7, dteStartTime)

                strStartTime = dteStartTime.ToString("yyyy-MM-dd") & " " & "00:00:00"
                strEndTime = dteEndTime.ToString("yyyy-MM-dd") & " " & "00:00:00"

            Case cntDateTime_Custom
                With m_cboValue
                    .Items.Clear()
                    .Text = ""
                    m_cboValue.DropDownStyle = ComboBoxStyle.Simple
                End With

                Exit Function
            Case Else

        End Select

        If strStartTime = "" AndAlso strEndTime = "" Then
            GetDetailTime = ""
        Else
            GetDetailTime = strItemName & " >= " & "'" & strStartTime & "'" & " AND " & strItemName & " < " & "'" & strEndTime & "'"
        End If

        'Debug.Print(GetDetailTime)
    End Function
    ''' <summary>
    ''' 初始化
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Initial()
        Try

            Dim i As Int32 = 0

            '查询项目在内存中表示的初始化
            ReDim m_QueryCtrlValueArray(QueryItems.Count - 1)

            For i = 0 To QueryItems.Count - 1
                With m_QueryCtrlValueArray(i)
                    .Item.Name = QueryItems.Item(i).Item.Name
                    .Item.Text = QueryItems.Item(i).Item.Text
                    .Item.index = i
                    .Item.DataType = QueryItems.Item(i).Item.DataType

                    .Opera = GetQueryOperator(.Item.DataType)

                    If .Item.DataType = enmValueDataValue.DataValue_DateTime Then
                        .Value = New String() {cntDateTime_Today, cntDateTime_Yesterday, cntDateTime_TheDayBeforeYesterday, _
                                cntDateTime_ThisWeek, cntDateTime_LastWeek, _
                                cntDateTime_CurrentMonth, cntDateTime_Ultimo, _
                                cntDateTime_ThisYear, cntDateTime_LastYear, cntDateTime_Custom}
                    ElseIf .Item.DataType = enmValueDataValue.DataValue_Bool Then
                        .Value = New String() {cntBool_Valid, cntBool_Invalid}
                    Else
                        .Value = QueryItems.Item(i).Value
                    End If


                End With
            Next


            Dim intItemIndexTmp As Int32 = 0

            '对控件进行初始化
            With m_cboItem
                .Items.Clear()
                For i = 0 To m_QueryCtrlValueArray.Length - 1
                    '添加查询项目
                    .Items.Add(m_QueryCtrlValueArray(i).Item.Text)
                Next

                If .Items.Count > 0 Then
                    .SelectedIndex = 0
                End If
            End With

            '绑事件处理过程
            AddHandler cboItem.SelectedIndexChanged, AddressOf Update
            AddHandler m_cboValue.SelectedIndexChanged, AddressOf UpdateValue

            UpdateOpertorAndValue(m_cboItem.Text)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' 更新查询值
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub UpdateValue(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CType(sender, ComboBox).SelectedItem.ToString = Trim(cntDateTime_Custom) Then
            cboValue.Items.Clear()
            cboValue.DropDownStyle = ComboBoxStyle.Simple
        End If
    End Sub

    ''' <summary>
    ''' 更新查询条件和查询值
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub Update(ByVal sender As System.Object, ByVal e As System.EventArgs)
        UpdateOpertorAndValue(CType(sender, ComboBox).Text)
    End Sub

    ''' <summary>
    ''' 更新操作符和查询值
    ''' </summary>
    ''' <param name="ItemText"></param>
    ''' <param name="QueryOperator"></param>
    ''' <remarks></remarks>
    Public Sub UpdateOpertorAndValue(ByVal ItemText As String, Optional ByVal QueryOperator As String = "=")

        For Each tmp As strtQueryGroup In m_QueryCtrlValueArray
            If tmp.Item.Text = ItemText Then

                With m_cboValue
                    .Text = ""
                    .Items.Clear()
                    Select Case tmp.Item.DataType
                        Case enmValueDataValue.DataValue_Bool, enmValueDataValue.DataValue_TextArray, _
                            enmValueDataValue.DataValue_DateTime
                            .DropDownStyle = ComboBoxStyle.DropDownList
                        Case Else
                            .DropDownStyle = ComboBoxStyle.Simple
                    End Select
                End With


                '查询条件
                With m_cboOperator
                    .Text = ""
                    .Items.Clear()

                    Dim i As Int32 = 0

                    If tmp.Opera Is Nothing Then
                        Return
                    End If
                    For i = 0 To tmp.Opera.Length - 1
                        If tmp.Opera(i).Text <> "" Then
                            .Items.Add(tmp.Opera(i).Text)
                        End If
                    Next

                    If .Items.Count > 0 Then
                        If .Text <> .Items(0) Then
                            .SelectedIndex = 0
                        End If
                    End If
                End With

                '查询值
                If tmp.Value Is Nothing Then
                Else
                    With m_cboValue
                        .Text = ""
                        .Items.Clear()

                        .Items.AddRange(tmp.Value)

                        If .Items.Count > 0 Then
                            If .Text <> .Items(0) Then
                                .SelectedIndex = 0
                            End If
                        End If
                    End With
                End If


            End If
        Next

    End Sub

    ''' <summary>
    ''' 取查询运算符数组
    ''' </summary>
    ''' <param name="DataType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetQueryOperator(ByVal DataType As enmValueDataValue) As strtQueryOperator()

        Dim OperatorArray() As strtQueryOperator = Nothing

        '查询条件
        Select Case DataType
            Case clsQueryCtrlGroup.enmValueDataValue.DataValue_DateTime
                ReDim OperatorArray(0)
                With OperatorArray(0)
                    .Name = "="
                    .Text = "="
                    .index = 0
                End With


            Case clsQueryCtrlGroup.enmValueDataValue.DataValue_Text
                ReDim OperatorArray(2)

                With OperatorArray(0)
                    .Name = "="
                    .Text = "="
                    .index = 0
                End With

                With OperatorArray(1)
                    .Name = "like"
                    .Text = cntLike
                    .index = 1
                End With

                With OperatorArray(2)
                    .Name = "<>"
                    .Text = cntNotEqual
                    .index = 2
                End With

            Case clsQueryCtrlGroup.enmValueDataValue.DataValue_Numeric
                ReDim OperatorArray(5)

                With OperatorArray(0)
                    .Name = "="
                    .Text = "="
                    .index = 0
                End With

                With OperatorArray(1)
                    .Name = ">"
                    .Text = cntGreater
                    .index = 1
                End With

                With OperatorArray(2)
                    .Name = "<"
                    .Text = cntLess
                    .index = 2
                End With

                With OperatorArray(3)
                    .Name = ">="
                    .Text = cntGreaterAndEqual
                    .index = 3
                End With

                With OperatorArray(4)
                    .Name = "<="
                    .Text = cntLesserAndEqual
                    .index = 4
                End With

                With OperatorArray(5)
                    .Name = "<>"
                    .Text = cntNotEqual
                    .index = 5
                End With

            Case clsQueryCtrlGroup.enmValueDataValue.DataValue_TextArray
                ReDim OperatorArray(1)

                With OperatorArray(0)
                    .Name = "="
                    .Text = "="
                    .index = 0
                End With

                With OperatorArray(1)
                    .Name = "<>"
                    .Text = cntNotEqual
                    .index = 2
                End With

            Case clsQueryCtrlGroup.enmValueDataValue.DataValue_Bool
                ReDim OperatorArray(1)

                With OperatorArray(0)
                    .Name = "="
                    .Text = "="
                    .index = 0
                End With

                With OperatorArray(1)
                    .Name = "<>"
                    .Text = cntNotEqual
                    .index = 2
                End With

            Case Else

        End Select

        Return OperatorArray
    End Function

    ''' <summary>
    ''' 获得操作符根据显示的文本
    ''' </summary>
    ''' <param name="strOperatorText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCurOperatorByText(ByVal strOperatorText As String) As String
        GetCurOperatorByText = "="
        Select Case strOperatorText
            Case cntEqual
                GetCurOperatorByText = "="
            Case cntNotEqual
                GetCurOperatorByText = "<>"
            Case cntGreater
                GetCurOperatorByText = ">"
            Case cntLess
                GetCurOperatorByText = "<"
            Case cntGreaterAndEqual
                GetCurOperatorByText = ">="
            Case cntLesserAndEqual
                GetCurOperatorByText = "<="
            Case cntLike
                GetCurOperatorByText = "LIKE"
            Case Else
                GetCurOperatorByText = "="
        End Select
    End Function

    ''' <summary>
    ''' 添加Item
    ''' </summary>
    ''' <param name="strName"></param>
    ''' <param name="strText"></param>
    ''' <param name="DataType"></param>
    ''' <param name="ValueArray"></param>
    ''' <remarks></remarks>
    Public Sub ItemsAdd(ByVal strName As String, ByVal strText As String, ByVal DataType As enmValueDataValue, Optional ByVal ValueArray() As String = Nothing)
        Dim tmp As New strtQuery
        With tmp.Item
            .Name = strName
            .Text = strText
            .DataType = DataType
        End With
        tmp.Value = ValueArray
        QueryItems.Add(tmp)
    End Sub

    ''' <summary>
    ''' 构造函数
    ''' </summary>
    ''' <param name="cboQueryItem"></param>
    ''' <param name="cboQueryOperator"></param>
    ''' <param name="cboQueryValue"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal cboQueryItem As ComboBox, ByVal cboQueryOperator As ComboBox, _
             ByVal cboQueryValue As ComboBox)
        m_cboItem = cboQueryItem
        m_cboOperator = cboQueryOperator
        m_cboValue = cboQueryValue
    End Sub

    Public Sub New()

    End Sub

    ''' <summary>
    ''' 查询项目控件
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property cboItem() As ComboBox
        Get
            Return m_cboItem
        End Get
        Set(ByVal value As ComboBox)
            m_cboItem = value
            m_cboItem.DropDownStyle = ComboBoxStyle.DropDownList
        End Set
    End Property

    ''' <summary>
    ''' 查询值控件
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property cboValue() As ComboBox
        Get
            Return m_cboValue
        End Get
        Set(ByVal value As ComboBox)
            m_cboValue = value
        End Set
    End Property

    ''' <summary>
    ''' 查询条件
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property cboOperator() As ComboBox
        Get
            Return m_cboOperator
        End Get
        Set(ByVal value As ComboBox)
            m_cboOperator = value
            m_cboOperator.DropDownStyle = ComboBoxStyle.DropDownList
        End Set
    End Property
End Class
