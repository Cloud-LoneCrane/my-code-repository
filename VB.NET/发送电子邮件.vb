''' <summary>
''' 发送电子邮件
''' </summary>
''' <param name="ToMail">收件人</param>
''' <param name="subject">主题</param>
''' <param name="mailContent">内容</param>
''' <param name="strFromUser">邮箱用户名</param>
''' <param name="strFromPwd">邮箱密码</param>
''' <remark>这里使用gmail.smtp.com,注意:如果客户机的机器名为汉字可能发送失败</remark>
Public Sub SendMail(ToMail As String,subject As String ,mailContent As String , _
	strFromUser As String,strFromPwd As String) As Boolean
	
	Dim msg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage()
	msg.[To].Add(ToMail)
	'收件人
	'发件人信息
	msg.From = New System.Net.Mail.MailAddress(strFromUser, _
		"摇钱树网吧管理软件服务中心", System.Text.Encoding.UTF8)
	msg.Subject = subject
	'邮件标题
	msg.SubjectEncoding = System.Text.Encoding.UTF8
	'标题编码
	msg.Body = mailContent
	'邮件主体
	msg.BodyEncoding = System.Text.Encoding.UTF8
	msg.IsBodyHtml = True
	'是否HTML
	msg.Priority = System.Net.Mail.MailPriority.High
	'优先级
	Dim client As New System.Net.Mail.SmtpClient()
	'设置GMail邮箱和密码
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