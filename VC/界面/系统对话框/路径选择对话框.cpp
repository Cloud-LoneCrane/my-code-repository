//================================================================
//����:	ѡ��·��
//����:
//	pFilePath [out] �����ļ����ַ�����
//	size [in] �ַ�����Ĵ�С
//================================================================
bool SelectPath(TCHAR* pFilePath,int size)
{
	TCHAR   szPath[_MAX_PATH];   
	BROWSEINFO   bi;   
	//ָ�������ڣ��ڶԻ�����ʾ�ڼ䣬�����ڽ�������   
	bi.hwndOwner   =   NULL;   
	//���ָ��NULL�����ԡ����桱Ϊ��   
	bi.pidlRoot   =   NULL;   
	//��һ�н���ʾ�ڶԻ���Ķ���   
	bi.lpszTitle   =     _T("��ѡ��һ���ļ���");   
	bi.pszDisplayName   =   szPath;   
	//ֻ�����ļ�ϵͳ�д��ڵ��ļ���   
	bi.ulFlags   =   BIF_RETURNONLYFSDIRS;   
	bi.lpfn   =   NULL;   //�ص�������ָ��   
	bi.lParam   =   NULL;   //����ص������Ĳ���   

	//���ڣ����ú�������ʾ�Ի���   
	//������Windows����ǳ���Explorer������ͬ�����   
	LPITEMIDLIST   pItemIDList = SHBrowseForFolder( &bi);   

	if(pItemIDList)   //�㰴�ˡ�ȷ������ť   
	{   
		TCHAR szPath[_MAX_PATH];   
		if(SHGetPathFromIDList(pItemIDList,szPath)   )   
		{   
			/*
			//�ɹ���ȡ�����ļ�����Ϣ   
			CString   strMessage;   
			strMessage.Format("ѡ�����ļ�����\'%s\'",   szPath);   
			AfxMessageBox(strMessage); 
			*/

			_tcscpy(pFilePath,szPath);
			return TRUE;
		}   
		//��ֹ�ڴ�й©��Ҫʹ��IMalloc�ӿ�   
		IMalloc*   pMalloc;   
		if   (   SHGetMalloc(   &pMalloc   )   !=   NOERROR   )   
		{   
			//δ������Ч��IMalloc�ӿ�ָ��   
			TRACE(_T("�޷�ȡ����ǳ����IMalloc�ӿ�\n"));   
		}   
		pMalloc->Free(   pItemIDList   );   
		if(pMalloc)   
			pMalloc->Release();   

		return TRUE;
	}  

	return FALSE;
}