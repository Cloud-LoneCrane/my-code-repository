//===============================================================
//功能:
//	删除目录下所有文件
//参数:
//	const char* 
//		目录名字
//返回值:
//	void
//备注:
//	删除目录下所有文件，例子：DeleteFiles("C:\\Test\\");
//===============================================================
void DeleteFiles(const char *dir)
{	
	WIN32_FIND_DATA finder;
	HANDLE hFileFind;
	char search[MAX_PATH];
	strcpy(search, dir);
	strcat(search, "*.*");
	
	hFileFind = FindFirstFile(search, &finder);
	
	if (hFileFind != INVALID_HANDLE_VALUE)
	{
		do
		{
			char path[MAX_PATH];
			strcpy(path, dir);
			strcat(path, finder.cFileName);
			
			if (!(finder.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY))
				DeleteFile(path);
			
		} while (FindNextFile(hFileFind, &finder) != 0);
		
		FindClose(hFileFind);
	}
}

//===============================================================
//功能:
//	清理ie Cookie
//参数:
//	无
//返回值:
//	void
//备注:
//===============================================================
void CleanIECookie()
{
	TCHAR buf[1024]={0};
	if(SHGetSpecialFolderPath(NULL,buf,CSIDL_COOKIES,0))
	{
		strcat(buf,"\\");
		DeleteFiles(buf); //删除cookie目录下所有文件
	}
}