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