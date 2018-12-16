//////////////////////////////////////////////////////////////////////////   
//   DeleteDirectory 
//   ɾ��һ���ļ���   
//   lpszPath   -   Ҫɾ�����ļ���·��   
//   ����ֵ���ɹ�����TRUE�����򷵻�FALSE   
//   ��ע���������ɾ�������ļ�,Ŀ¼����ܴ�\
//   eg.
//DeleteDirectory(_T("c:\\vc.txt"));   ��ȷ
//DeleteDirectory(_T("c:\\vc"));        ��ȷ
//DeleteDirectory(_T("c:\\vc\\"));     ����
//////////////////////////////////////////////////////////////////////////   
BOOL DeleteDirectory(LPCTSTR lpszPath) 
{ 
    SHFILEOPSTRUCT FileOp; 
    FileOp.fFlags = FOF_NOCONFIRMATION|FOF_NOERRORUI ;  //ɾ��ʧ��ʱ������ʾ������ʾ
    //FileOp.fFlags = FOF_NOCONFIRMATION ; 
	FileOp.hNameMappings = NULL; 
    FileOp.hwnd = NULL; 
    FileOp.lpszProgressTitle = NULL; 
    FileOp.pFrom = lpszPath; 
    FileOp.pTo = NULL; 
    FileOp.wFunc = FO_DELETE; 
    return SHFileOperation(&FileOp) == 0; 
}