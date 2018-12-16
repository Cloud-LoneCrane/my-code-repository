
int CCHttpDlg::GetHttp()
{
	CInternetSession session; //�Ự�ڶ���
	CHttpConnection* pServer = NULL; //ָ���������ַ(URL)
	CHttpFile * pHttpFile = NULL;//HTTP�ļ�ָ�� 
	CString strServerName; //�������� 
	CString strObject; //��ѯ������(http�ļ�)
	INTERNET_PORT nPort; //�˿� 
	DWORD dwServiceType; //��������
	//�����־ 
	DWORD dwHttpRequestFlags =INTERNET_FLAG_NO_AUTO_REDIRECT; 

    //------------- ���������ļ��� �� ����·�� -------------------
	LPSTR pURL="http://www.baidu.com",SavePath=".\\������.html";
const TCHAR szHeaders[]=_T("Accept: text/*\r\nUser-Agent:��HttpClient\r\n"); 

    //��URL�ĸ�ʽ���м��
BOOL bLegal=AfxParseURL( //�ʷ����� 
				pURL, //������URL��
				dwServiceType, //�������ͣ�ftp��http��
				strServerName, //�������� 
				strObject, //URL�б���ѯ���� 
				nPort ); //URLָ���Ķ˿ڣ�����Ϊ�� 

	bLegal=bLegal && (dwServiceType == INTERNET_SERVICE_HTTP); 

	if (!bLegal)
	{ //����Ϸ�����������������󣬽���http���ӣ� 
		MessageBox(_T("URL����")); //���� 
		return false; 

	} 

	pServer = session.GetHttpConnection(strServerName, nPort); //��÷������� 

	pHttpFile = pServer->OpenRequest( CHttpConnection::HTTP_VERB_GET,strObject,
						NULL, 1, NULL, NULL,dwHttpRequestFlags); 

	//���������ϵ�http�ļ�ָ�� 
	pHttpFile->AddRequestHeaders(szHeaders); 

	pHttpFile->SendRequest(); //�������� 

	CStdioFile f; //����ļ����� 
	if( !f.Open(SavePath, CFile::modeCreate | CFile::modeReadWrite | CFile::typeText) ) 
	{ 
	MessageBox( _T("Unable to open file")); 
	return false; 

	} 

		CString strLen;
	pHttpFile->QueryInfo(HTTP_QUERY_CONTENT_LENGTH,strLen);

	//����ļ���С
	unsigned long ulFileLenth=atol(strLen)+10;

	double dFileSize ;
	dFileSize=(double) ulFileLenth/1024;

	//strLen.Empty();
	strLen.Format("%0.2f KB",dFileSize);

	//���潫����������浽�ļ��� 
	TCHAR szBuf[1024]; //���� 

	while (pHttpFile->ReadString(szBuf, sizeof(szBuf) - 1)) 
	{ 
		f.WriteString(szBuf); 
	} 
	f.Close(); //�ƺ��� 

	pHttpFile ->Close(); 

	pServer ->Close(); 

	if (pHttpFile != NULL) delete pHttpFile; 

	if (pServer != NULL) delete pServer; 

	session.Close(); 

	AfxMessageBox("�ļ���С��" + strLen);
	return true; 

}
