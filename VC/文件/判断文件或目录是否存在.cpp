//===============================================================
//����:	�ж��ļ��Ƿ����
//����: 
//	const CString strFilePath �ļ�·��
//����ֵ:
//	�ļ��Ƿ����
//===============================================================
BOOL FileIsExist(const CString strFilePath)
{
	CFileStatus   status; 
	CString   sFile= strFilePath; 
	CString   msg; 
	if(!CFile::GetStatus(strFilePath,status)) 
		return FALSE;
	else	
		return TRUE;
}

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