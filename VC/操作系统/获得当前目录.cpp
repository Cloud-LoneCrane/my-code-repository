/** 获得主目录 */
CString GetHomeDir() 
{
	TCHAR szBuf[MAX_PATH]={0};
	::GetModuleFileName(NULL,szBuf,sizeof(szBuf)/sizeof(TCHAR));
	int pos=0;
	CString strTmp;
	CString strHomeDir;
	
	strTmp = szBuf;
	pos = strTmp.ReverseFind('\\');
	if (pos>0)
	{
		strHomeDir =strTmp.Mid(0,pos);
	}
	else
	{
		strHomeDir = _T("");
	}
	
	return strHomeDir;
}
