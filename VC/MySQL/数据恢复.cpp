
//���ø�ʽ
/*	char szErr[512]={0};

	if (TRUE == RestoreMySQLBak("mysql.exe","yqsnew.bak","root","jift","yqsnew",szErr))
	{
		CString strCaption;
		GetWindowText(strCaption);
		::MessageBox(NULL,"���ݿ⻹ԭ�ɹ�",strCaption,NULL);
	}
	else
	{
		CString strCaption;
		GetWindowText(strCaption);
		::MessageBox(NULL,szErr ,strCaption,NULL);
	}
	*/
	
//=========================================================
//	���ܣ���ԭMySQL���ݿ�
//	��ע��
//		��Ҫ�����İ������ݽṹ�����ݵı��ݽű�������ʹ��
//	mysqldump.exe����
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
	
	//������������ܵ�
	if(!CreatePipe(&hRead_in, &hWrite_in, &sa, 0))
	{
	//	::MessageBox(NULL,"�����ܵ�ʧ��",NULL,NULL);
		strcpy(pSzErr,"CreatePipe Failed.");
		return FALSE;
	}
	SetHandleInformation(hWrite_in, HANDLE_FLAG_INHERIT, 0);
	
	STARTUPINFO si = { 0 };
	PROCESS_INFORMATION pi = { 0 };
	
	si.cb =sizeof(STARTUPINFO);
	//ָ����׼����Ϊ�ܵ���hRead_in
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
	
	//��sql�ű��ļ�
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
		//����ű��ļ�
		bSuccess = ReadFile(hInputFile, chBuf, BUFSIZE, &dwRead, NULL);
		if(!bSuccess || dwRead == 0) break;
		//д��ܵ�
		bSuccess = WriteFile(hWrite_in, chBuf, dwRead, &dwWritten, NULL);
		if(!bSuccess) break; 
	} 
	
	
	//�رվ��
	CloseHandle(hInputFile);
	CloseHandle(hWrite_in);
	CloseHandle(hRead_in);

	return TRUE;
}