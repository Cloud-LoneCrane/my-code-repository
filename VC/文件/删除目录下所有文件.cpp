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

