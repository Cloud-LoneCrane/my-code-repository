''' <summary>
''' ���͵����ʼ�
''' </summary>
''' <param name="ToMail">�ռ���</param>
''' <param name="subject">����</param>
''' <param name="mailContent">����</param>
''' <param name="strFromUser">�����û���</param>
''' <param name="strFromPwd">��������</param>
''' <remark>����ʹ��gmail.smtp.com,ע��:����ͻ����Ļ�����Ϊ���ֿ��ܷ���ʧ��</remark>
Public Sub SendMail(ToMail As String,subject As String ,mailContent As String , _
	strFromUser As String,strFromPwd As String) As Boolean
	
	Dim msg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage()
	msg.[To].Add(ToMail)
	'�ռ���
	'��������Ϣ
	msg.From = New System.Net.Mail.MailAddress(strFromUser, _
		"ҡǮ�����ɹ��������������", System.Text.Encoding.UTF8)
	msg.Subject = subject
	'�ʼ�����
	msg.SubjectEncoding = System.Text.Encoding.UTF8
	'�������
	msg.Body = mailContent
	'�ʼ�����
	msg.BodyEncoding = System.Text.Encoding.UTF8
	msg.IsBodyHtml = True
	'�Ƿ�HTML
	msg.Priority = System.Net.Mail.MailPriority.High
	'���ȼ�
	Dim client As New System.Net.Mail.SmtpClient()
	'����GMail���������
	client.Credentials = New System.Net.NetworkCredential(strFromUser, strFromPwd)
	client.Port = 587
	client.Host = "smtp.gmail.com"
	client.EnableSsl = True
	Dim userState As Object = msg
	Try
		client.Send(msg)
		return true
	Catch ex As SmtpException
		Console.WriteLine("Send EMail failed.")
		return False
	End Try
	
	return true
End Sub