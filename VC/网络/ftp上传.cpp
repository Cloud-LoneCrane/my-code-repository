//==================================================================
//	����:ʵ�ֽ�һ���ļ��ϴ���ftp��������
//	����:
//		char* pSzServer �����������ֻ�ip��ַ
//		char* pSzUserName ftp�˺�
//		char* pSzPassword �˺Ŷ�Ӧ������
//		int iPort	�˿ں�
//		char* pSzFileName	��Ҫ�ϴ��ı����ļ�������
//		char* pSzDir	��Ҫ�ϴ��ļ���ftp������Ŀ¼
//	��ע:
//		���е��ַ���������һ��'\0'����β��
//	���ø�ʽ��
//		UnLoadFileToFtp("127.0.0.1","001","0",21,"D:\\vcʵ��.rar",".\\00\\");
//==================================================================
bool UnLoadFileToFtp(char* pSzServer,char* pSzUserName,char* pSzPassword ,int iPort,
                char* pSzFileName,char* pSzDir)
{
//--------------------------------------------------
// 1 ����һ����ftp������������
// 2 ��ʼ�ϴ��ļ�
// 3 �ƺ��� 
//--------------------------------------------------

	//assumes server and file names have been initialized
	//�ٶ����������ļ����Ѿ���ʼ��
	CInternetSession session("FTP Session"); //�½�һ��session����
	CFtpConnection* pConn = NULL; //�½�һ��ftpConnect����

	LPCTSTR pStrServer,pStrUserName,pStrPassword;  
	BOOL bPassive=true ; //��ʾ�Ǳ�������������
	INTERNET_PORT nPort; //�˿�

	pStrServer=pSzServer,pStrUserName=pSzUserName,pStrPassword=pSzPassword;
	nPort = (INTERNET_PORT)iPort;


	//----------------------------1 ����һ��ftp���� -----------------------
	 try
	 {
		 //���һ��ftpConnection
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
		
	//----------------------------2 �ϴ��ļ� -----------------------
	//���ļ�
    CInternetFile* pFile=NULL;
    char szFileName[1024]="",szTmp[1024]=""; //�ڷ��������½��ļ�������    
    char *q=NULL;
	strcpy(szTmp,pSzFileName);
	q=strrchr(szTmp,'\\');
	//strcpy(szFileName,q+1);
	strcpy(szFileName,pSzDir);
	strcat(szFileName,q+1);

     //�ڷ������ϴ����ļ�
	pFile = pConn->OpenFile(szFileName,GENERIC_WRITE,FTP_TRANSFER_TYPE_BINARY,
                    1);
    if(!pFile)
    {
        MessageBox(NULL,_T("Can't Create File."),NULL,MB_OK|MB_ICONINFORMATION);
        return false;
    }
    //�򿪱����ļ�
    CFile LocFile;
    BOOL    bOpenFlag=LocFile.Open(pSzFileName,
                        CFile::modeRead | CFile::shareDenyWrite,NULL);
    if(!bOpenFlag){
         MessageBox(NULL,_T("Can't Open Local File."),NULL,MB_OK|MB_ICONINFORMATION);
         return false;
        }
    
    try{
        
        //�����������洢�������͵�����
        char szBuf[512]="";
        UINT nCount=1;

        while(nCount>0)
        {
            //�ӱ����ļ��ж������ݵ�������
            nCount=LocFile.Read(szBuf,sizeof(szBuf)/sizeof(char));
            
			if (nCount>0)
			{
				//������������д�뵽ftp�������ϵ��ļ���
				pFile->Write(szBuf,nCount);
			}

        }
    
		//�ر��ļ�
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
        
	//------------------------- 3 �ƺ��� ---------------------------------	
	if(pConn)   
        delete pConn;
	session.Close(); //�ر�CInternetSession �Ի�
    
    return true;
   
}