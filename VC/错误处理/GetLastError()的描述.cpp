/************************************************************************/
/* 功能: 获得错误描述字符串 GetLastError()的描述                        */
/* 使用说明:															*/
/*		AfxMessageBox(GetLastErrorDescr(),MB_OK,NULL);					*/
/************************************************************************/
CString GetLastErrorDescr() 
{
	LPVOID lpMsgBuf;
	FormatMessage( 
		FORMAT_MESSAGE_ALLOCATE_BUFFER | 
		FORMAT_MESSAGE_FROM_SYSTEM | 
		FORMAT_MESSAGE_IGNORE_INSERTS,
		NULL,
		GetLastError(),
		MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), // Default language
		(LPTSTR) &lpMsgBuf,
		0,
		NULL 
		);
	// Display the string.
	CString strTmp = (LPCTSTR)lpMsgBuf;
	//::MessageBox( NULL, (LPCTSTR)lpMsgBuf, "Error", MB_OK | MB_ICONINFORMATION );
	// Free the buffer.
	LocalFree( lpMsgBuf );
	
	return strTmp;
}