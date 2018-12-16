
    Public Function ImportFromExcel(ByVal strFile As String, Optional ByRef intImportCount As Integer = 0) As Boolean
        Dim bRet As Boolean = False
        Dim strConn As String = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", strFile)
        Dim oledbConn As OleDbConnection = New OleDbConnection(strConn)
        Dim oledbAdapter As OleDbDataAdapter = Nothing
        Dim strSQL As String = Nothing
        Dim dttTmp As DataTable = Nothing
        Dim tmp As strtZhxx = New strtZhxx()
        Dim index As Integer = 0
        Dim tableSchema As DataTable = Nothing
        Dim strTableName As String = Nothing

        oledbConn.Open()

        If oledbConn.State = System.Data.ConnectionState.Open Then

            tableSchema = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})

            strTableName = tableSchema.Rows(0).ItemArray(2).ToString()

            'strSQL = "select * from [sheet1$]"
            strSQL = String.Format("select * from [{0}]", strTableName)

            '获得所有的表名


            oledbAdapter = New OleDbDataAdapter(strSQL, oledbConn)
            dttTmp = New DataTable()
            oledbAdapter.Fill(dttTmp)

            If dttTmp IsNot Nothing AndAlso dttTmp.Rows.Count > 0 Then
                For i As Integer = 0 To dttTmp.Rows.Count - 1
                    With tmp
                        index = clsZhxxTable.GetColumnNo(m_Rules, "账号")
                        If -1 = index Then
                            bRet = False
                            GoTo _Exit
                        Else
                            .strZh = dttTmp.Rows(i).ItemArray(index - 1).ToString.Trim()
                        End If
                        index = clsZhxxTable.GetColumnNo(m_Rules, "姓名")
                        If -1 = index Then
                            .strXm = "用户姓名"
                        Else
                            .strXm = dttTmp.Rows(i).ItemArray(index - 1).ToString.Trim()
                        End If
                        index = clsZhxxTable.GetColumnNo(m_Rules, "余额")
                        If -1 <> index Then
                            .decYe = Convert.ToDouble(dttTmp.Rows(i).ItemArray(index - 1).ToString.Trim())
                        Else
                            .decYe = 0
                        End If
                        index = clsZhxxTable.GetColumnNo(m_Rules, "密码")
                        If -1 <> index Then
                            .strKl = dttTmp.Rows(i).ItemArray(index - 1).ToString.Trim()
                        Else
                            .strKl = modFunc.klEncrypt(.strZh)
                        End If
                        index = clsZhxxTable.GetColumnNo(m_Rules, "证件名称")
                        If -1 <> index Then
                            .strZjdm = modFunc.GetCertTypeID(dttTmp.Rows(i).ItemArray(index - 1))
                        Else
                            .strZjdm = "身份证"
                        End If
                        index = clsZhxxTable.GetColumnNo(m_Rules, "证件号")
                        If -1 <> index Then
                            .strZjh = dttTmp.Rows(i).ItemArray(index - 1)
                        Else
                            .strZjh = "123456"
                        End If
                        .decKhe = .decYe
                        .decLjyfe = .decYe
                        .decDeflje = 0
                        .decLjzse = 0
                        .decSjje = .decYe
                        .decZse = 0
                        .dteKhsj = Now
                        .dteQysj = Now
                        .dteYxq = Now.AddYears(10)
                        .intDeflsc = 0
                        .intHyjf = 0
                        .intLjjs = 0
                        .intSyjs = 0
                        .shtDefl = 0 '是否定额费率
                        .shtFffs = modType.enmPayType.PrePay
                        .shtHy = modType.enmUserType.Acc
                        .shtHydj = 0
                        .shtLjjf = 0
                        .shtMy = 0
                        .shtXq = 127
                        .shtYxx = 1
                        .shtZhlx = enmAccType.Fee '账号类型
                        .shtZxzt = 0 '在线状态
                        .strBz = ""
                        .strCzydh = modVar.g_CurShiftOperator.strOpCode '操作员
                        .strOpenHost = "" '开户主机
                        .strQydm = ""
                        .strSddm = ""
                        .strSfdbh = modVar.g_ServerID '收费端编号
                        .dteDycdlsj = CDate("1900-01-01")
                        .dteZhdlsj = CDate("1900-01-01")
                        .strSddm = 0 '时段代码
                        .strQydm = ""

                    End With

                    Dim strTip As String = Nothing
                    If clsZhxxTable.Get_AccountIsExist(tmp.strZh, modVar.g_Conn) Then
                        strTip = String.Format("第{0}个账号{1}已存在！", i, tmp.strZh)
                        lblTip.Text = strTip
                        Application.DoEvents()
                    Else
                        strTip = String.Format("正在导入第{0}个账号...", i)
                        lblTip.Text = strTip
                        Application.DoEvents()
                        If modFunc.SendData(clsZhxxTable.GetSQL_Insert(tmp)) Then
                            intImportCount += 1
                        End If
                    End If
                Next
            End If
        End If

        bRet = True
_exit:
        Return bRet
    End Function