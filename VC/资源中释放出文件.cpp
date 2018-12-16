//===============================================================
//����:
//  ����Դ���ͷų��ļ�
//����:
//	LPCTSTR
//		��Դ����
//	int
//		��ԴID
//	LPCTSTR
//		Ŀ��·��
//����ֵ:
//	
//��ע:
//===============================================================

BOOL ExtractFile(LPCTSTR restype, int resid, LPCTSTR destpath)
{
	HRSRC		hRes;
	HGLOBAL		hFileData;
	BOOL		bResult = FALSE;

	hRes = FindResource(NULL, MAKEINTRESOURCE(resid), restype);
	if(hRes)
	{
		DWORD dwSize = SizeofResource(NULL, hRes);
		if(dwSize)
		{
			hFileData = LoadResource(NULL, hRes);
			if(hFileData)
			{
				hFileData = LockResource(hFileData);
				if(hFileData)
				{
					HANDLE hFile = CreateFile(destpath, GENERIC_WRITE, 0, NULL, CREATE_ALWAYS, FILE_ATTRIBUTE_ARCHIVE, 0);
					if(hFile != INVALID_HANDLE_VALUE)
					{
						DWORD dwWritten = 0;
						bResult = WriteFile(hFile, hFileData, dwSize, &dwWritten, NULL);

						CloseHandle(hFile);
					}
				}
			}
		}
	}

	if(!bResult)
	{
		CString		strError;
		DWORD		dwErr = GetLastError();

		strError.Format(_T("ExtractFile %s error %d\r\n"), destpath, dwErr);
	}

	return bResult;
}
