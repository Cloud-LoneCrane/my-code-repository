//===============================================================
//功能:	判断文件是否存在
//参数: 
//	const CString strFilePath 文件路径
//返回值:
//	文件是否存在
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