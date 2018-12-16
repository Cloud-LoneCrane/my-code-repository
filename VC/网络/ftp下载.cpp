//==================================================================
//	����:��ftp������������һ���ļ�������
//	����:
//		char* pSzServer �����������ֻ�ip��ַ
//		char* pSzUserName ftp�˺�
//		char* pSzPassword �˺Ŷ�Ӧ������
//		int iPort	�˿ں�
//		char* pSzFileName	��Ҫ�����ļ�������
//		char* pSzDir	�ڵ����ļ���Ŀ¼(ʹ��"\\"����"\" eg."D:\\")
//	��ע:
//		���е��ַ���������һ��'\0'����β��
//	���ø�ʽ��
//			DownloadFileFromFtp("127.0.0.1","001","0",21,"FoxitReader.rar","d:\\");
//==================================================================
bool DownloadFileFromFtp(char* pSzServer,char* pSzUserName,char* pSzPassword ,int iPort,
				char* pSzFileName,char* pSzDir)
{
//--------------------------------------------------
// 1 ����һ����ftp������������
// 2 ��λ���ļ�����Ŀ¼
// 3 �����ļ�������
// 4 �ƺ��� 
//--------------------------------------------------	
//assumes server and file names have been initialized
//�ٶ����������ļ����Ѿ���ʼ��
	CInternetSession session("FTP Session"); //�½�һ��session����
	CFtpConnection* pConn = NULL; //�½�һ��ftpConnect����
	LPCTSTR pStrServer,pStrUserName,pStrPassword;  
	BOOL bPassive=TRUE; //��ʾ�Ǳ�������������
	INTERNET_PORT nPort; //�˿�
	pStrServer=pSzServer,pStrUserName=pSzUserName,pStrPassword=pSzPassword;
	nPort = (INTERNET_PORT)iPort;

	char szLocFileName[1024];
	memset(szLocFileName,0,sizeof(szLocFileName)/sizeof(char));

	strcpy(szLocFileName,pSzDir);
	strcat(szLocFileName,pSzFileName);

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

		 //----------------------------2 �����ļ� -----------------------
		 //���ļ�
		 CInternetFile* pFile=NULL;
		 CFile LocFile;
		 char szFileName[1024]=""; //�ڷ��������½��ļ�������    
		 
	 //�ڷ������ϴ��ļ�
		 pFile = pConn->OpenFile(pSzFileName,GENERIC_READ,FTP_TRANSFER_TYPE_BINARY,
								1);
		 if(!pFile)
			 {
				 MessageBox(NULL,_T("Can't Open File."),NULL,MB_OK|MB_ICONINFORMATION);
				 return false;
			 }
		 //�򿪱����ļ�
		 BOOL    bOpenFlag=LocFile.Open(szLocFileName, //pSzFileName
									CFile::modeCreate | CFile::modeWrite|CFile::shareDenyRead,NULL);
		 if(!bOpenFlag){
			 MessageBox(NULL,_T("Can't Create Local File."),NULL,MB_OK|MB_ICONINFORMATION);
			 return false;
		 }
	
	try{
						 
		//�����������洢�������͵�����
		char szBuf[512]="";
		UINT nCount=1;

		while(nCount>0)
		{
			//��ftp�������ϵ��ļ����뵽������
			nCount=pFile->Read(szBuf,sizeof(szBuf)/sizeof(char));
			//������������д�뵽�����ļ�
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
				
				//�ر��ļ�
				LocFile.Close();
				pFile->Close();
				
				//------------------------- 3 �ƺ��� ---------------------------------
				if(pConn)   
					delete pConn;
				session.Close(); //�ر�CInternetSession �Ի�
				
				return true;
}