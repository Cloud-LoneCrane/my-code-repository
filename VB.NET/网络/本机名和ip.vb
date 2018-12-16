Imports System.Net
Imports System.Net.Dns

'本机机器名
System.Net.Dns.GetHostName()
System.Net.Dns.GetHostByName(电脑名).AddressList
Dim arIp() As System.Net.IPAddress=System.Net.Dns.GetHostByName(电脑名).AddressList
'机器ip
arIp(0).ToString 

''' <summary>
''' cboLANIp初始化
''' </summary>
''' <remarks></remarks>
Private Sub cboLANIp_Init()

	Dim IpHostTmp As New IPHostEntry

	IpHostTmp = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())

	Dim arTmp As New ArrayList
	Dim arIp() As System.Net.IPAddress
	arIp = IpHostTmp.AddressList
	Dim i As Int32 = 0
	For i = 0 To arIp.Length - 1
		arTmp.Add(arIp(i).ToString)
	Next

	cboLANIp.DataSource = arTmp

End Sub