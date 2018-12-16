//===========================================
// 功能:
//	获得系统临时文件夹
//===========================================
CString GetSysTmpDir()
{
	CString strTmpDir;
	DWORD dwLen = 0;
	DWORD dwRet = 0;
	TCHAR buf[MAX_PATH] = {0};
	
	dwLen = sizeof(buf);
	dwRet = GetTempPath(dwLen,buf);
	if (dwRet > 0)
	{
		strTmpDir = buf;
	}
	return strTmpDir;
}
