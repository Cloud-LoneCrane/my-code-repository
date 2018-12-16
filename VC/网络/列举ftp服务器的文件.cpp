//==================================================================
//	����:�оٳ�ftp�����������е��ļ�
//	����:
//		char* pSzServer �����������ֻ�ip��ַ
//		char* pSzUserName ftp�˺�
//		char* pSzPassword �˺Ŷ�Ӧ������
//		int iPort	�˿ں�
//		char* pSzFileName	���ش洢�ļ��б���ļ���(�����ļ���ȫ·��)
//	��ע:
//		���е��ַ���������һ��'\0'����β��
//==================================================================
bool    ListAllFileFromFtp(char* pSzServer,char* pSzUserName,char* pSzPassword ,int iPort,
                char* pSzFileName)
{
//--------------------------------------------------
// 1 ����һ����ftp������������
// 2 ��λ���ļ�����Ŀ¼
// 3 �����ļ�������
// 4 �ƺ��� 
//--------------------------------------------------
	CInternetSession session("FTP Session"); //�½�һ��session����
	CFtpConnection* pConn = NULL; //�½�һ��ftpConnect����

   	LPCTSTR pStrServer,pStrUserName,pStrPassword;  
	BOOL bPassive ; //��ʾ�Ǳ�������������
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
     
     
  	//----------------------------2 ���������ļ�,�����ļ�·��д�뵽�ļ��� -----------------------
     //����һ�ļ����Ҷ������о��ļ�
       CFtpFileFind finder(pConn);
       //�򿪱����ļ�,�洢�ļ��б�
        CFile LocFile;
        BOOL    bOpenFlag=LocFile.Open(pSzFileName,
                            CFile::modeCreate | CFile::modeWrite | CFile::shareDenyRead ,NULL);
        if(!bOpenFlag){
             MessageBox(NULL,_T("Can't Create Local File."),NULL,MB_OK|MB_ICONINFORMATION);
             return false;
            }

    try{
      //��ʼѭ��
      bool bWorking=finder.FindFile(_T("*"));
      char buf[1024];
            
       
       while(bWorking)
       {
           //������һ���ļ�
            bWorking =finder.FindNextFile();
          
           //���ļ������Ƹ�һ��������
            memset(buf,0,sizeof(buf)/sizeof(char));
            strcpy(buf,(LPCTSTR)finder.GetFileURL());
           //���ļ�·��д�뵽�ļ�
           
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

   //�ر��ļ�
    LocFile.Close();
     
	//------------------------- 3 �ƺ��� ---------------------------------	
	if(pConn)   
        delete pConn;
	session.Close(); //�ر�CInternetSession �Ի�
    
    return true;
   
}     