

//////////////////////////////////////////////////////////////////////////   
//   DeleteDirectory 
//   ɾ���ļ���   
//   lpszPath   -   Ҫɾ�����ļ���·��   
//   ����ֵ���ɹ�����TRUE�����򷵻�FALSE   
//   ��ע���������ɾ�������ļ�   
//////////////////////////////////////////////////////////////////////////   
BOOL DeleteDirectory(LPCTSTR lpszPath) 
{ 
	char szBuf[1024]={0};
	strcpy(szBuf,lpszPath);

    SHFILEOPSTRUCT FileOp; 
    FileOp.fFlags = FOF_NOCONFIRMATION|FOF_NOERRORUI ;  //ɾ��ʧ��ʱ������ʾ������ʾ
    //FileOp.fFlags = FOF_NOCONFIRMATION ; 
	FileOp.hNameMappings = NULL; 
    FileOp.hwnd = NULL; 
    FileOp.lpszProgressTitle = NULL; 
    FileOp.pFrom = szBuf; //lpszPath; //Ҫ��\0\0��β���������
    FileOp.pTo = NULL; 
    FileOp.wFunc = FO_DELETE; 
    return SHFileOperation(&FileOp) == 0; 
}