//===============================================================
//����:
//	�ж��ļ���Ŀ¼�Ƿ����
//����:
//	LPCTSTR
//		�ļ���Ŀ¼����
//����ֵ:
//	���� TRUE�������� FALSE
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
