/************************************************************
ʹ��˵��:
	һ CTcpSocket class ֻ����֧��Winsock��MFC������ʹ��;
	�� �������ͺ���ָ��һ����ʽ�ĺ���,���ú���ָ��;
	�� �����Ϳ����ڶ���ĺ����д���������tcp����˵��¼���.
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
