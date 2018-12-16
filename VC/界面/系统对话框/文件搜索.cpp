//================================================================
//功能:	文件搜索
//参数:
//	lpPath 搜索路径
//================================================================
void find(char * lpPath)
{
	char szFind[MAX_PATH]; //查找路径
	WIN32_FIND_DATA FindFileData; //查找文件数据结构

	//设置查询的路径
	strcpy(szFind,lpPath); 
	strcat(szFind,"\\*.*");

	//文件的句柄
	HANDLE hFind=::FindFirstFile(szFind,&FindFileData);
	if(INVALID_HANDLE_VALUE == hFind)    return; //没有找到文件

	while(TRUE) 
	{
		if(FindFileData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
		{//如果是目录

			if(FindFileData.cFileName[0]!='.')
			{
				strcpy(szFind,lpPath);
				strcat(szFind,"\\");
				strcat(szFind,FindFileData.cFileName);
				find(szFind);
			}
		}
		else
		{
			//输出文件名
			TRACE(FindFileData.cFileName);
			TRACE("\n");
		}
		if(!FindNextFile(hFind,&FindFileData))    break;
	}
	FindClose(hFind);
}
