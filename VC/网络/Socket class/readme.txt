/************************************************************
使用说明:
	一 CTcpSocket class 只能在支持Winsock的MFC程序中使用;
	二 先声明和函数指针一样形式的函数,设置函数指针;
	三 这样就可以在定义的函数中处理来自于tcp服务端的事件了.
************************************************************/

CTcpSocket client;

//------------------Function Declares----------------------
void Client_Connect();
void Client_Close();
void Client_DataArrival(char *Buf,int ilen);
//---------------------end---------------------------------

void Client_DataArrival(char *Buf,int ilen)
{
	CString strTmp = Buf;
	
	AfxMessageBox(strTmp);
	
}

void Client_Connect()
{
	if(client.m_bConnected)
		AfxMessageBox("client connect successed");
	else
		AfxMessageBox("client connect failed.");

}

void Client_Close()
{
	AfxMessageBox("client closed.");
}
void CSocDlg::OnButton1() 
{
	client.Create(0,SOCK_STREAM);
	
	CString strRemoteHost;
	unsigned int intPort;

	intPort = 8455 ;
	strRemoteHost = "192.168.100.188";
	client.Connect(strRemoteHost,intPort);
	client.pDataArrival=Client_DataArrival;
	client.pConnect=Client_Connect;
	client.pClose=Client_Close;

}
