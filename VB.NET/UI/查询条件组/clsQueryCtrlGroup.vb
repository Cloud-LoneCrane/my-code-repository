
'-------------------------------------------------------------------------------------------------------------------------------------
'���ܣ���ѯ�����飬����
'��д�ˣ�jifle
'��дʱ�䣺20-10-06-02
'ʹ��˵����
'   һ New һ����Ķ���
'   �� ����Ҫ�����ݵ�����ComboBox�ؼ�
'   �� ����ItemsAdd
'   �� ����Initial �Ϳ�����
'
'Dim m_qcgTmp As New clsQueryCtrlGroup
'Private Sub init()

'    With m_qcgTmp
'        .cboItem = cboItem
'        .cboOperator = cboQuery
'        .cboValue = cboValue
'    End With

'    m_qcgTmp.ItemsAdd("�ʺ�", "�ʺ�", clsQueryCtrlGroup.enmValueDataValue.DataValue_Text)
'    m_qcgTmp.ItemsAdd("����", "����", clsQueryCtrlGroup.enmValueDataValue.DataValue_Text)
'    m_qcgTmp.ItemsAdd("���", "���", clsQueryCtrlGroup.enmValueDataValue.DataValue_Numeric)

'    m_qcgTmp.ItemsAdd("����", "����", clsQueryCtrlGroup.enmValueDataValue.DataValue_Numeric)
'    m_qcgTmp.ItemsAdd("��Ч��", "��Ч��", clsQueryCtrlGroup.enmValueDataValue.DataValue_Bool)

'    m_qcgTmp.ItemsAdd("��Ա�ȼ�", "��Ա�ȼ�", clsQueryCtrlGroup.enmValueDataValue.DataValue_TextArray, _
'                                        modFunc.GetAccLevelNameArray(m_AccLevelArray))
'    m_qcgTmp.ItemsAdd("֤����", "֤����", clsQueryCtrlGroup.enmValueDataValue.DataValue_Text)

'    m_qcgTmp.ItemsAdd("����Ա", "����Ա", clsQueryCtrlGroup.enmValueDataValue.DataValue_Text)
'    m_qcgTmp.ItemsAdd("������", "������", clsQueryCtrlGroup.enmValueDataValue.DataValue_Numeric)

'    m_qcgTmp.ItemsAdd("����ʱ��", "����ʱ��", clsQueryCtrlGroup.enmValueDataValue.DataValue_DateTime)
'
'    '���ó�ʼ������
'    m_qcgTmp.Initial()
'
'End Sub
'-------------------------------------------------------------------------------------------------------------------------------------------------
Imports System.Windows.Forms
Imports System.Collections.ObjectModel

''' <summary>
''' ��ѯ���������
''' </summary>
''' <remarks></remarks>
Public Class clsQueryCtrlGroup

    '------------------------------------------------------------------------------------------------
    '��������
    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <remarks></remarks>
    Const cntEqual As String = "����"
    ''' <summary>
    ''' ������
    ''' </summary>
    ''' <remarks></remarks>
    Const cntNotEqual As String = "������"
    ''' <summary>
    ''' ������
    ''' </summary>
    ''' <remarks></remarks>
    Const cntLike As String = "������"
    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <remarks></remarks>
    Const cntGreater As String = "����"
    ''' <summary>
    ''' С��
    ''' </summary>
    ''' <remarks></remarks>
    Const cntLess As String = "С��"
    ''' <summary>
    ''' ���ڵ���
    ''' </summary>
    ''' <remarks></remarks>
    Const cntGreaterAndEqual As String = "���ڵ���"
    ''' <summary>
    ''' С�ڵ���
    ''' </summary>
    ''' <remarks></remarks>
    Const cntLesserAndEqual As String = "С�ڵ���"

    Const cntDateTime_Today As String = "����"
    Const cntDateTime_Yesterday As String = "����"
    Const cntDateTime_TheDayBeforeYesterday As String = "ǰ��"
    Const cntDateTime_ThisWeek As String = "����"
    Const cntDateTime_LastWeek As String = "����"
    Const cntDateTime_CurrentMonth As String = "����"
    Const cntDateTime_Ultimo As String = "����"
    Const cntDateTime_ThisYear As String = "����"
    Const cntDateTime_LastYear As String = "ȥ��"

    Const cntDateTime_Custom As String = "�Զ���"

    Const cntBool_Valid As String = "��Ч"
    Const cntBool_Invalid As String = "����"

    '------------------------------------------------------------------------------------------------

    ''' <summary>
    ''' ��ѯ��Ŀ
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure strtQueryItem
        ''' <summary>
        ''' ����Ҳ����Key
        ''' </summary>
        ''' <remarks></remarks>
        Dim Name As String
        ''' <summary>
        ''' ��ʾ�ı� 
        ''' </summary>
        ''' <remarks></remarks>
        Dim Text As String
        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <remarks></remarks>
        Dim index As Int32
        ''' <summary>
        ''' ��������
        ''' </summary>
        ''' <remarks></remarks>
        Dim DataType As enmValueDataValue
    End Structure

    ''' <summary>
    ''' ��ѯ����
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure strtQueryOperator
        ''' <summary>
        ''' ���ֺ�key�ȼ�
        ''' </summary>
        ''' <remarks></remarks>
        Dim Name As String
        ''' <summary>
        ''' ��ʾ�ı�
        ''' </summary>
        ''' <remarks></remarks>
        Dim Text As String
        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <remarks></remarks>
        Dim index As Int32
        '''' <summary>
        '''' ��ѯ��Ŀ
        '''' </summary>
        '''' <remarks></remarks>
        'Dim QueryItemKey As String
    End Structure

    ''' <summary>
    ''' ��ѯֵ
    ''' </summary>
    ''' <remarks></remarks>
    Structure strtQueryValue
        ''' <summary>
        ''' ���ֺ�key�ȼ�
        ''' </summary>
        ''' <remarks></remarks>
        Dim Name As String
        ''' <summary>
        ''' ��ʾ�ı�
        ''' </summary>
        ''' <remarks></remarks>
        Dim Text As String
        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <remarks></remarks>
        Dim index As Int32
        ''' <summary>
        ''' ��ѯ��Ŀ
        ''' </summary>
        ''' <remarks></remarks>
        Dim QueryItemKey As String
        ''' <summary>
        ''' ��ѯ����
        ''' </summary>
        ''' <remarks></remarks>
        Dim QueryOperatorKey As String
    End Structure

    ''' <summary>
    ''' ��ѯֵ����������
    ''' </summary>
    ''' <remarks></remarks>
    Enum enmValueDataValue
        ''' <summary>
        ''' �ı�
        ''' </summary>
        ''' <remarks></remarks>
        DataValue_Text = 0
        ''' <summary>
        ''' ��ֵ
        ''' </summary>
        ''' <remarks></remarks>
        DataValue_Numeric
        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <remarks></remarks>
        DataValue_Bool
        ''' <summary>
        ''' ����ʱ��
        ''' </summary>
        ''' <remarks></remarks>
        DataValue_DateTime
        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <remarks></remarks>
        DataValue_Date
        ''' <summary>
        ''' ʱ��
        ''' </summary>
        ''' <remarks></remarks>
        DataValue_Time
        ''' <summary>
        ''' �ı�����
        ''' </summary>
        ''' <remarks></remarks>
        DataValue_TextArray
    End Enum

    ''' <summary>
    ''' ��ѯ��
    ''' </summary>
    ''' <remarks></remarks>
    Structure strtQueryGroup
        ''' <summary>
        ''' ��ѯ��Ŀ
        ''' </summary>
        ''' <remarks></remarks>
        Dim Item As strtQueryItem
        ''' <summary>
        ''' ������
        ''' </summary>
        ''' <remarks></remarks>
        Dim Opera() As strtQueryOperator
        ''' <summary>
        ''' ��ѯֵ
        ''' </summary>
        ''' <remarks></remarks>
        Dim Value() As String
    End Structure

    ''' <summary>
    ''' ��ѯ��Ŀԭ��
    ''' </summary>
    ''' <remarks></remarks>
    Structure strtQuery
        ''' <summary>
        ''' ��ѯ��Ŀ
        ''' </summary>
        ''' <remarks></remarks>
        Dim Item As strtQueryItem
        ''' <summary>
        ''' ��ѯֵ
        ''' </summary>
        ''' <remarks></remarks>
        Dim Value() As String
    End Structure

    ''' <summary>
    ''' ��ѯ��Ŀ
    ''' </summary>
    ''' <remarks></remarks>
    Private m_cboItem As ComboBox

    ''' <summary>
    ''' ��ѯ����
    ''' </summary>
    ''' <remarks></remarks>
    Private m_cboOperator As ComboBox

    ''' <summary>
    '''��ѯֵ
    ''' </summary>
    ''' <remarks></remarks>
    Private m_cboValue As ComboBox

    ''' <summary>
    ''' ��ѯ��
    ''' </summary>
    ''' <remarks></remarks>
    Private m_QueryCtrlValueArray() As strtQueryGroup = Nothing

    ''' <summary>
    ''' ��ѯֵ
    ''' </summary>
    ''' <remarks></remarks>
    Public QueryItems As New Collection(Of strtQuery)

    ''' <summary>
    ''' ��ò�ѯ���
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

                '��ѯ����
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

                    Case clsQueryCtrlGroup.enmValueDataValue.DataValue_Bool '����
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
    ''' ��þ����ʱ���
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

            Case cntDateTime_CurrentMonth '����
                dteStartTime = New Date(Now.Year, Now.Month, 1)
                'dteStartTime = DateAdd(DateInterval.Day, -1, dteStartTime)

                dteEndTime = New Date(Now.Year, Now.Month + 1, 1)
                ' dteEndTime = DateAdd(DateInterval.Day, -1, dteEndTime)
                strStartTime = dteStartTime.ToString("yyyy-MM-dd") & " " & "00:00:00"
                strEndTime = dteEndTime.ToString("yyyy-MM-dd") & " " & "00:00:00"

            Case cntDateTime_Ultimo '����
                '��ʼʱ��
                dteStartTime = New Date(Now.Year, Now.Month - 1, 1)

                '����ʱ��
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

            Case cntDateTime_ThisWeek '����
                Dim dteTmp As Date
                Dim intTmp As Int16 = 0

                dteTmp = Now.Date '����
                intTmp = dteTmp.DayOfWeek

                dteStartTime = DateAdd(DateInterval.Day, -intTmp + 1, dteTmp)
                dteEndTime = DateAdd(DateInterval.Day, +7, dteStartTime)

                strStartTime = dteStartTime.ToString("yyyy-MM-dd") & " " & "00:00:00"
                strEndTime = dteEndTime.ToString("yyyy-MM-dd") & " " & "00:00:00"
            Case cntDateTime_LastWeek
                Dim dteTmp As Date
                Dim intTmp As Int16 = 0

                dteTmp = Now.Date '����
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
    ''' ��ʼ��
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Initial()
        Try

            Dim i As Int32 = 0

            '��ѯ��Ŀ���ڴ��б�ʾ�ĳ�ʼ��
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

            '�Կؼ����г�ʼ��
            With m_cboItem
                .Items.Clear()
                For i = 0 To m_QueryCtrlValueArray.Length - 1
                    '��Ӳ�ѯ��Ŀ
                    .Items.Add(m_QueryCtrlValueArray(i).Item.Text)
                Next

                If .Items.Count > 0 Then
                    .SelectedIndex = 0
                End If
            End With

            '���¼��������
            AddHandler cboItem.SelectedIndexChanged, AddressOf Update
            AddHandler m_cboValue.SelectedIndexChanged, AddressOf UpdateValue

            UpdateOpertorAndValue(m_cboItem.Text)

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' ���²�ѯֵ
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
    ''' ���²�ѯ�����Ͳ�ѯֵ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub Update(ByVal sender As System.Object, ByVal e As System.EventArgs)
        UpdateOpertorAndValue(CType(sender, ComboBox).Text)
    End Sub

    ''' <summary>
    ''' ���²������Ͳ�ѯֵ
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


                '��ѯ����
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

                '��ѯֵ
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
    ''' ȡ��ѯ���������
    ''' </summary>
    ''' <param name="DataType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetQueryOperator(ByVal DataType As enmValueDataValue) As strtQueryOperator()

        Dim OperatorArray() As strtQueryOperator = Nothing

        '��ѯ����
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
    ''' ��ò�����������ʾ���ı�
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
    ''' ���Item
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
    ''' ���캯��
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
    ''' ��ѯ��Ŀ�ؼ�
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
    ''' ��ѯֵ�ؼ�
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
    ''' ��ѯ����
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
