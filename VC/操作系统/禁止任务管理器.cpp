CFile g_taskMgrFile; //һ��Ҫ��ȫ�ֵ�
CString strTaskMgrPath;

strTaskMgrPath = GetTaskMgrPath();
g_taskMgrFile.Open(strTaskMgrPath,CFile::shareExclusive,NULL);


if(g_taskMgrFile.m_hFile != (UINT)CFile::hFileNull )
	g_taskMgrFile.Close(); //�ͷ�
 
 
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