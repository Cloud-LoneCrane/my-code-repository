//===============================================================
//����:	��������������ȫ·��
//����: 
//		��
//����ֵ:
//	CString ·��
//===============================================================
CString GetTaskMgrPath()
{
	CString strDir;
	UINT iLength=0;
	TCHAR szDir[MAX_PATH]={0};
	
	iLength =GetSystemDirectory(szDir,sizeof(szDir)/sizeof(TCHAR));
	strDir = szDir;
	
	if (strDir.GetLength())
		strDir +=_T("\\taskmgr.exe");

	return strDir;
}