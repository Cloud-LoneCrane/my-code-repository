CFile g_taskMgrFile; //一定要是全局的
CString strTaskMgrPath;

strTaskMgrPath = GetTaskMgrPath();
g_taskMgrFile.Open(strTaskMgrPath,CFile::shareExclusive,NULL);


if(g_taskMgrFile.m_hFile != (UINT)CFile::hFileNull )
	g_taskMgrFile.Close(); //释放
 
 
 //===============================================================
//功能:	获得任务管理器的全路径
//参数: 
//		无
//返回值:
//	CString 路径
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