//===========================================
// ����:
//	���ϵͳ��ʱ�ļ���
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
