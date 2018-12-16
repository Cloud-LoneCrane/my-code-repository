//===============================================================
//功能:
//	判断文件或目录是否存在
//参数:
//	LPCTSTR
//		文件或目录名字
//返回值:
//	存在 TRUE，不存在 FALSE
//===============================================================
BOOL FileOrDirectoryIsExist(LPCTSTR lpszFileOrDirectoryName)
{
	BOOL bRet = FALSE;
	CString strFileOrDirName;
	strFileOrDirName = lpszFileOrDirectoryName; //_T("c:\\vc");
	if(INVALID_FILE_ATTRIBUTES == GetFileAttributes(strFileOrDirName))
		bRet = FALSE;
	else
		bRet = TRUE;
	return bRet;
}
