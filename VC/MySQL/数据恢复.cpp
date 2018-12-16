
//调用格式
/*	char szErr[512]={0};

	if (TRUE == RestoreMySQLBak("mysql.exe","yqsnew.bak","root","jift","yqsnew",szErr))
	{
		CString strCaption;
		GetWindowText(strCaption);
		::MessageBox(NULL,"数据库还原成功",strCaption,NULL);
	}
	else
	{
		CString strCaption;
		GetWindowText(strCaption);
		::MessageBox(NULL,szErr ,strCaption,NULL);
	}
	*/
	
//=========================================================
//	功能：还原MySQL数据库
//	备注：
//		需要完整的包含数据结构和数据的备份脚本，建议使用
//	mysqldump.exe备份
//=========================================================
BOOL CMySQLToolDlg::RestoreMySQLBak(char* pSzMySqlPath,char* pSzDbBakPath,
									char* pSzUser,char* pSzPwd,char* pSzDBName,char* pSzErr)
{
	CString strTmp;
	
	strTmp = pSzMySqlPath;
	strTmp += " -u";
	strTmp += pSzUser ;
	strTmp += " -p";
	strTmp += pSzPwd;
	strTmp += " ";
	strTmp += pSzDBName;
	strTmp += " ";


	SECURITY_ATTRIBUTES sa; 
	HANDLE hRead_in, hWrite_in;
	
	sa.nLength = sizeof(sa);
	sa.lpSecurityDescriptor = NULL;
	sa.bInheritHandle = TRUE;
	
	//创建输入输出管道
	if(!CreatePipe(&hRead_in, &hWrite_in, &sa, 0))
	{
	//	::MessageBox(NULL,"创建管道失败",NULL,NULL);
		strcpy(pSzErr,"CreatePipe Failed.");
		return FALSE;
	}
	SetHandleInformation(hWrite_in, HANDLE_FLAG_INHERIT, 0);
	
	STARTUPINFO si = { 0 };
	PROCESS_INFORMATION pi = { 0 };
	
	si.cb =sizeof(STARTUPINFO);
	//指定标准输入为管道的hRead_in
	si.hStdInput = hRead_in;
	si.dwFlags = STARTF_USESTDHANDLES;
	
	char szTmp[512] ={0};
	
	strcpy(szTmp, (LPCTSTR)strTmp);
	if(!CreateProcess(NULL,szTmp , NULL, NULL, TRUE, 0, NULL, NULL, &si, &pi))
	{
		strcpy(pSzErr,"CreatePipe Failed.");
		return FALSE;
	}
	
	TCHAR chBuf[1024];
	int BUFSIZE=1024;
	DWORD dwRead = 0, dwWritten = 0;
	BOOL bSuccess;
	
	//打开sql脚本文件
	HANDLE hInputFile = CreateFile(
		pSzDbBakPath, 
		GENERIC_READ, 
		0, 
		NULL, 
		OPEN_EXISTING, 
		FILE_ATTRIBUTE_READONLY, 
		NULL); 
	
	for(;;) 
	{ 
		//读入脚本文件
		bSuccess = ReadFile(hInputFile, chBuf, BUFSIZE, &dwRead, NULL);
		if(!bSuccess || dwRead == 0) break;
		//写向管道
		bSuccess = WriteFile(hWrite_in, chBuf, dwRead, &dwWritten, NULL);
		if(!bSuccess) break; 
	} 
	
	
	//关闭句柄
	CloseHandle(hInputFile);
	CloseHandle(hWrite_in);
	CloseHandle(hRead_in);

	return TRUE;
}