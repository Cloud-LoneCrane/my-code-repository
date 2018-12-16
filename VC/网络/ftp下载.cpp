//==================================================================
//	功能:从ftp服务器上下载一个文件到本地
//	参数:
//		char* pSzServer 服务器的名字或ip地址
//		char* pSzUserName ftp账号
//		char* pSzPassword 账号对应的密码
//		int iPort	端口号
//		char* pSzFileName	需要下载文件的名字
//		char* pSzDir	在当地文件的目录(使用"\\"代替"\" eg."D:\\")
//	备注:
//		所有的字符串都是以一个'\0'做结尾的
//	调用格式：
//			DownloadFileFromFtp("127.0.0.1","001","0",21,"FoxitReader.rar","d:\\");
//==================================================================
bool DownloadFileFromFtp(char* pSzServer,char* pSzUserName,char* pSzPassword ,int iPort,
				char* pSzFileName,char* pSzDir)
{
//--------------------------------------------------
// 1 建立一个到ftp服务器的连接
// 2 定位到文件所在目录
// 3 下载文件到本地
// 4 善后处理 
//--------------------------------------------------	
//assumes server and file names have been initialized
//假定服务器和文件名已经初始化
	CInternetSession session("FTP Session"); //新建一个session对象
	CFtpConnection* pConn = NULL; //新建一个ftpConnect对象
	LPCTSTR pStrServer,pStrUserName,pStrPassword;  
	BOOL bPassive=TRUE; //表示是被动还是主动的
	INTERNET_PORT nPort; //端口
	pStrServer=pSzServer,pStrUserName=pSzUserName,pStrPassword=pSzPassword;
	nPort = (INTERNET_PORT)iPort;

	char szLocFileName[1024];
	memset(szLocFileName,0,sizeof(szLocFileName)/sizeof(char));

	strcpy(szLocFileName,pSzDir);
	strcat(szLocFileName,pSzFileName);

	//----------------------------1 建立一个ftp连接 -----------------------
	 try
	 {
	 //获得一个ftpConnection
		pConn = session.GetFtpConnection(pStrServer,pStrUserName,pStrPassword,nPort,bPassive); 
	 }
	 catch (CInternetException* pEx)
	 {
		  TCHAR sz[1024];
		  pEx->GetErrorMessage(sz, 1024);
		  MessageBox(NULL,sz,NULL,MB_OK|MB_ICONINFORMATION);
		  pEx->Delete();         
		 return false;
	 }

		 //----------------------------2 下载文件 -----------------------
		 //打开文件
		 CInternetFile* pFile=NULL;
		 CFile LocFile;
		 char szFileName[1024]=""; //在服务器上新建文件的名字    
		 
	 //在服务器上打开文件
		 pFile = pConn->OpenFile(pSzFileName,GENERIC_READ,FTP_TRANSFER_TYPE_BINARY,
								1);
		 if(!pFile)
			 {
				 MessageBox(NULL,_T("Can't Open File."),NULL,MB_OK|MB_ICONINFORMATION);
				 return false;
			 }
		 //打开本地文件
		 BOOL    bOpenFlag=LocFile.Open(szLocFileName, //pSzFileName
									CFile::modeCreate | CFile::modeWrite|CFile::shareDenyRead,NULL);
		 if(!bOpenFlag){
			 MessageBox(NULL,_T("Can't Create Local File."),NULL,MB_OK|MB_ICONINFORMATION);
			 return false;
		 }
	
	try{
						 
		//建立缓冲区存储即将发送的数据
		char szBuf[512]="";
		UINT nCount=1;

		while(nCount>0)
		{
			//将ftp服务器上的文件读入到缓冲区
			nCount=pFile->Read(szBuf,sizeof(szBuf)/sizeof(char));
			//缓冲区中数据写入到本地文件
			LocFile.Write(szBuf,nCount);
			
		}
			
	}
	catch(CInternetException* pEx){
				TCHAR sz[1024];
				pEx->GetErrorMessage(sz,1024);
				MessageBox(NULL,sz,NULL,MB_OK|MB_ICONINFORMATION);
				pEx->Delete();
				
				return false;
	}
				
				//关闭文件
				LocFile.Close();
				pFile->Close();
				
				//------------------------- 3 善后处理 ---------------------------------
				if(pConn)   
					delete pConn;
				session.Close(); //关闭CInternetSession 对话
				
				return true;
}