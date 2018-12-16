//==================================================================
//	功能:实现将一个文件上传到ftp服务器上
//	参数:
//		char* pSzServer 服务器的名字或ip地址
//		char* pSzUserName ftp账号
//		char* pSzPassword 账号对应的密码
//		int iPort	端口号
//		char* pSzFileName	需要上传的本地文件的名字
//		char* pSzDir	需要上传文件的ftp服务器目录
//	备注:
//		所有的字符串都是以一个'\0'做结尾的
//	调用格式：
//		UnLoadFileToFtp("127.0.0.1","001","0",21,"D:\\vc实例.rar",".\\00\\");
//==================================================================
bool UnLoadFileToFtp(char* pSzServer,char* pSzUserName,char* pSzPassword ,int iPort,
                char* pSzFileName,char* pSzDir)
{
//--------------------------------------------------
// 1 建立一个到ftp服务器的连接
// 2 开始上传文件
// 3 善后处理 
//--------------------------------------------------

	//assumes server and file names have been initialized
	//假定服务器和文件名已经初始化
	CInternetSession session("FTP Session"); //新建一个session对象
	CFtpConnection* pConn = NULL; //新建一个ftpConnect对象

	LPCTSTR pStrServer,pStrUserName,pStrPassword;  
	BOOL bPassive=true ; //表示是被动还是主动的
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
		
	//----------------------------2 上传文件 -----------------------
	//打开文件
    CInternetFile* pFile=NULL;
    char szFileName[1024]="",szTmp[1024]=""; //在服务器上新建文件的名字    
    char *q=NULL;
	strcpy(szTmp,pSzFileName);
	q=strrchr(szTmp,'\\');
	//strcpy(szFileName,q+1);
	strcpy(szFileName,pSzDir);
	strcat(szFileName,q+1);

     //在服务器上创建文件
	pFile = pConn->OpenFile(szFileName,GENERIC_WRITE,FTP_TRANSFER_TYPE_BINARY,
                    1);
    if(!pFile)
    {
        MessageBox(NULL,_T("Can't Create File."),NULL,MB_OK|MB_ICONINFORMATION);
        return false;
    }
    //打开本地文件
    CFile LocFile;
    BOOL    bOpenFlag=LocFile.Open(pSzFileName,
                        CFile::modeRead | CFile::shareDenyWrite,NULL);
    if(!bOpenFlag){
         MessageBox(NULL,_T("Can't Open Local File."),NULL,MB_OK|MB_ICONINFORMATION);
         return false;
        }
    
    try{
        
        //建立缓冲区存储即将发送的数据
        char szBuf[512]="";
        UINT nCount=1;

        while(nCount>0)
        {
            //从本地文件中读入数据到缓冲区
            nCount=LocFile.Read(szBuf,sizeof(szBuf)/sizeof(char));
            
			if (nCount>0)
			{
				//将缓冲区数据写入到ftp服务器上的文件中
				pFile->Write(szBuf,nCount);
			}

        }
    
		//关闭文件
		LocFile.Close();
		pFile->Close();   
    }
    catch(CInternetException* pEx){
        TCHAR sz[1024];
        pEx->GetErrorMessage(sz,1024);
        MessageBox(NULL,sz,NULL,MB_OK|MB_ICONINFORMATION);
        pEx->Delete();
        
        return false;
    }
        
	//------------------------- 3 善后处理 ---------------------------------	
	if(pConn)   
        delete pConn;
	session.Close(); //关闭CInternetSession 对话
    
    return true;
   
}