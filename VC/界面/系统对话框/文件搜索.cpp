//================================================================
//����:	�ļ�����
//����:
//	lpPath ����·��
//================================================================
void find(char * lpPath)
{
	char szFind[MAX_PATH]; //����·��
	WIN32_FIND_DATA FindFileData; //�����ļ����ݽṹ

	//���ò�ѯ��·��
	strcpy(szFind,lpPath); 
	strcat(szFind,"\\*.*");

	//�ļ��ľ��
	HANDLE hFind=::FindFirstFile(szFind,&FindFileData);
	if(INVALID_HANDLE_VALUE == hFind)    return; //û���ҵ��ļ�

	while(TRUE) 
	{
		if(FindFileData.dwFileAttributes & FILE_ATTRIBUTE_DIRECTORY)
		{//�����Ŀ¼

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
			//����ļ���
			TRACE(FindFileData.cFileName);
			TRACE("\n");
		}
		if(!FindNextFile(hFind,&FindFileData))    break;
	}
	FindClose(hFind);
}
