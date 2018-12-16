
int CCHttpDlg::GetHttp()
{
	CInternetSession session; //会话期对象
	CHttpConnection* pServer = NULL; //指向服务器地址(URL)
	CHttpFile * pHttpFile = NULL;//HTTP文件指针 
	CString strServerName; //服务器名 
	CString strObject; //查询对象名(http文件)
	INTERNET_PORT nPort; //端口 
	DWORD dwServiceType; //服务类型
	//请求标志 
	DWORD dwHttpRequestFlags =INTERNET_FLAG_NO_AUTO_REDIRECT; 

    //------------- 设置下载文件名 和 保存路径 -------------------
	LPSTR pURL="http://www.baidu.com",SavePath=".\\已下载.html";
const TCHAR szHeaders[]=_T("Accept: text/*\r\nUser-Agent:　HttpClient\r\n"); 

    //对URL的格式进行检查
BOOL bLegal=AfxParseURL( //词法分析 
				pURL, //被分析URL串
				dwServiceType, //服务类型，ftp，http等
				strServerName, //服务器名 
				strObject, //URL中被查询对象 
				nPort ); //URL指定的端口，可能为空 

	bLegal=bLegal && (dwServiceType == INTERNET_SERVICE_HTTP); 

	if (!bLegal)
	{ //如果合法，向服务器发送请求，建立http连接， 
		MessageBox(_T("URL出错")); //报错 
		return false; 

	} 

	pServer = session.GetHttpConnection(strServerName, nPort); //获得服务器名 

	pHttpFile = pServer->OpenRequest( CHttpConnection::HTTP_VERB_GET,strObject,
						NULL, 1, NULL, NULL,dwHttpRequestFlags); 

	//建立本机上的http文件指针 
	pHttpFile->AddRequestHeaders(szHeaders); 

	pHttpFile->SendRequest(); //发送请求 

	CStdioFile f; //输出文件对象 
	if( !f.Open(SavePath, CFile::modeCreate | CFile::modeReadWrite | CFile::typeText) ) 
	{ 
	MessageBox( _T("Unable to open file")); 
	return false; 

	} 

		CString strLen;
	pHttpFile->QueryInfo(HTTP_QUERY_CONTENT_LENGTH,strLen);

	//获得文件大小
	unsigned long ulFileLenth=atol(strLen)+10;

	double dFileSize ;
	dFileSize=(double) ulFileLenth/1024;

	//strLen.Empty();
	strLen.Format("%0.2f KB",dFileSize);

	//下面将检索结果保存到文件上 
	TCHAR szBuf[1024]; //缓存 

	while (pHttpFile->ReadString(szBuf, sizeof(szBuf) - 1)) 
	{ 
		f.WriteString(szBuf); 
	} 
	f.Close(); //善后工作 

	pHttpFile ->Close(); 

	pServer ->Close(); 

	if (pHttpFile != NULL) delete pHttpFile; 

	if (pServer != NULL) delete pServer; 

	session.Close(); 

	AfxMessageBox("文件大小：" + strLen);
	return true; 

}
