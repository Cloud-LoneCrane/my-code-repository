//===============================================================
//����:
//	ɾ��Ŀ¼�������ļ�
//����:
//	const char* 
//		Ŀ¼����
//����ֵ:
//	void
//��ע:
//	ɾ��Ŀ¼�������ļ������ӣ�DeleteFiles("C:\\Test\\");
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
//����:
//	����ie Cookie
//����:
//	��
//����ֵ:
//	void
//��ע:
//===============================================================
void CleanIECookie()
{
	TCHAR buf[1024]={0};
	if(SHGetSpecialFolderPath(NULL,buf,CSIDL_COOKIES,0))
	{
		strcat(buf,"\\");
		DeleteFiles(buf); //ɾ��cookieĿ¼�������ļ�
	}
}