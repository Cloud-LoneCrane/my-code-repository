'д�������ļ�
Public Sub WriteSetting(ByVal path As String)
	Dim xmlfile As XmlWriter = Nothing
	Dim xmlsetting As New XmlWriterSettings

	'---------------------------------------------
	'������Ϣ����
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
		'������Ϣ
		.WriteElementString("localhost", "127.0.0.1")
		.WriteElementString("dbserver", "127.0.0.1")
		.WriteElementString("verifyhost", "127.0.0.1")
		.WriteElementString("verifyport", "8621")

		'-------------------------------------------------
		'�������� 0 ��ʱ���� 1 �������
		.WriteElementString("bakmode", "0")
		.WriteElementString("baktime", "12:00")
		.WriteElementString("bakgap", "2") '��λ:Сʱ
		.WriteElementString("bakdir", "e:\")

		.WriteEndElement()

		.Flush()
		.Close()
	End With

End Sub

'���������ļ�
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
				MessageBox.Show("���������ļ�", "���������ļ�", System.Windows.Forms.MessageBoxButtons.OK _
				, System.Windows.Forms.MessageBoxIcon.Error)
				Exit Sub
			End If

			'------------------------------------------------------
			'��ȡ������Ϣ
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
						Debug.Print("δ֪�Ĳ�������")
				End Select
			Next

		End With

	Catch ex As Exception
		MessageBox.Show(ex.ToString, "��ȡ�����ļ�", System.Windows.Forms.MessageBoxButtons.OK _
		, System.Windows.Forms.MessageBoxIcon.Error)
	End Try

End Sub
