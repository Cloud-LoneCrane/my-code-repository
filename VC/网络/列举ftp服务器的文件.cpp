//==================================================================
//	功能:列举出ftp服务器上所有的文件
//	参数:
//		char* pSzServer 服务器的名字或ip地址
//		char* pSzUserName ftp账号
//		char* pSzPassword 账号对应的密码
//		int iPort	端口号
//		char* pSzFileName	本地存储文件列表的文件名(包含文件完全路径)
//	备注:
//		所有的字符串都是以一个'\0'做结尾的
//==================================================================
bool    ListAllFileFromFtp(char* pSzServer,char* pSzUserName,char* pSzPassword ,int iPort,
                char* pSzFileName)
{
//--------------------------------------------------
// 1 建立一个到ftp服务器的连接
// 2 定位到文件所在目录
// 3 下载文件到本地
// 4 善后处理 
//--------------------------------------------------
	CInternetSession session("FTP Session"); //新建一个session对象
	CFtpConnection* pConn = NULL; //新建一个ftpConnect对象

   	LPCTSTR pStrServer,pStrUserName,pStrPassword;  
	BOOL bPassive ; //表示是被动还是主动的
	INTERNET_PORT nPort; //端口

	pStrServer=pSzServer,pStrUserName=pSzUserName,pStrPassword=pSzPassword;
	nPort = (INTERNET_PORT)iPort;

    
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
     
     
  	//----------------------------2 遍历所有文件,并将文件路径写入到文件中 -----------------------
     //构建一文件查找对象来列举文件
       CFtpFileFind finder(pConn);
       //打开本地文件,存储文件列表
        CFile LocFile;
        BOOL    bOpenFlag=LocFile.Open(pSzFileName,
                            CFile::modeCreate | CFile::modeWrite | CFile::shareDenyRead ,NULL);
        if(!bOpenFlag){
             MessageBox(NULL,_T("Can't Create Local File."),NULL,MB_OK|MB_ICONINFORMATION);
             return false;
            }

    try{
      //开始循环
      bool bWorking=finder.FindFile(_T("*"));
      char buf[1024];
            
       
       while(bWorking)
       {
           //查找下一个文件
            bWorking =finder.FindNextFile();
          
           //将文件名复制给一个缓冲区
            memset(buf,0,sizeof(buf)/sizeof(char));
            strcpy(buf,(LPCTSTR)finder.GetFileURL());
           //将文件路径写入到文件
           
           LocFile.Write(buf,strlen(buf));
           LocFile.Write(_T("\r\n"),2);
       }
   
    }
   catch (CInternetException* pEx)
   {
      TCHAR sz[1024];
      pEx->GetErrorMessage(sz, 1024);
      MessageBox(NULL,sz,NULL,MB_OK|MB_ICONINFORMATION);
      pEx->Delete();
       
      return false; 
   }

   //关闭文件
    LocFile.Close();
     
	//------------------------- 3 善后处理 ---------------------------------	
	if(pConn)   
        delete pConn;
	session.Close(); //关闭CInternetSession 对话
    
    return true;
   
}     