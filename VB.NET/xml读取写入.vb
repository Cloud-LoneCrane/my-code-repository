'写入配置文件
Public Sub WriteSetting(ByVal path As String)
	Dim xmlfile As XmlWriter = Nothing
	Dim xmlsetting As New XmlWriterSettings

	'---------------------------------------------
	'配置信息变量
	Dim strlocalhost As String = Nothing
	Dim strdbserver As String = Nothing
	Dim strverifyhost As String = Nothing
	Dim strverifyport As String = Nothing
	Dim strbakmode As String = Nothing
	Dim strbaktime As String = Nothing
	Dim strbakgap As String = Nothing
	Dim strbakdir As String = Nothing

	strlocalhost = "127.0.0.1"
	strdbserver = "127.0.0.1"
	strverifyhost = "127.0.0.1"
	strverifyport = "8621"
	strbakmode = "0"
	strbaktime = "12:00"
	strbakgap = "2"
	strbakdir = "D:\"

	With xmlsetting
		.CheckCharacters = True
		.ConformanceLevel = ConformanceLevel.Document
		.Encoding = System.Text.Encoding.UTF8
		.Indent = True
		.IndentChars = vbTab
		.NewLineChars = vbCrLf
		.NewLineHandling = NewLineHandling.Replace
		.NewLineOnAttributes = True
		.OmitXmlDeclaration = True
	End With

	xmlfile = XmlWriter.Create(path, xmlsetting)

	With xmlfile
		.WriteStartElement("set")

		'-------------------------------------------------
		'连接信息
		.WriteElementString("localhost", "127.0.0.1")
		.WriteElementString("dbserver", "127.0.0.1")
		.WriteElementString("verifyhost", "127.0.0.1")
		.WriteElementString("verifyport", "8621")

		'-------------------------------------------------
		'备份设置 0 定时备份 1 间隔备份
		.WriteElementString("bakmode", "0")
		.WriteElementString("baktime", "12:00")
		.WriteElementString("bakgap", "2") '单位:小时
		.WriteElementString("bakdir", "e:\")

		.WriteEndElement()

		.Flush()
		.Close()
	End With

End Sub

'读入配置文件
Public Sub ReadSetting(ByVal path As String)
	Dim dom As New System.Xml.XmlDocument

	Dim strlocalhost As String = Nothing
	Dim strdbserver As String = Nothing
	Dim strverifyhost As String = Nothing
	Dim strverifyport As String = Nothing
	Dim strbakmode As String = Nothing
	Dim strbaktime As String = Nothing
	Dim strbakgap As String = Nothing
	Dim strbakdir As String = Nothing

	Try
		With dom
			.Load(path)

			If .FirstChild.ChildNodes.Count = 0 Then
				MessageBox.Show("请检查配置文件", "配置配置文件", System.Windows.Forms.MessageBoxButtons.OK _
				, System.Windows.Forms.MessageBoxIcon.Error)
				Exit Sub
			End If

			'------------------------------------------------------
			'读取配置信息
			For Each tmp As XmlNode In dom.FirstChild.ChildNodes
				' Debug.Print(tmp.Name & ":" & tmp.InnerText)
				Debug.Print("" & tmp.Name & " = """" ")
				'Debug.Print("Case """ & tmp.Name & """  " & vbCrLf)

				Select Case tmp.Name
					Case "localhost"
						strlocalhost = tmp.InnerText
					Case "dbserver"
						strdbserver = tmp.InnerText
					Case "verifyhost"
						strverifyhost = tmp.InnerText
					Case "verifyport"
						strverifyport = tmp.InnerText
					Case "bakmode"
						strbakmode = tmp.InnerText
					Case "baktime"
						strbaktime = tmp.InnerText
					Case "bakgap"
						strbakgap = tmp.InnerText
					Case "bakdir"
						strbakdir = tmp.InnerText
					Case Else
						Debug.Print("未知的参数类型")
				End Select
			Next

		End With

	Catch ex As Exception
		MessageBox.Show(ex.ToString, "读取配置文件", System.Windows.Forms.MessageBoxButtons.OK _
		, System.Windows.Forms.MessageBoxIcon.Error)
	End Try

End Sub
