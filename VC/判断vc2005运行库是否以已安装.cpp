//===============================================================
//����:
//	�ж�vc2005���п��Ƿ����Ѱ�װ
//����:
//	��
//����ֵ:
//	�����װ������TRUE,���򣬷���FALSE
//��ע: 
// #include <atlbase.h>
//===============================================================
BOOL VC2005RuntimeIsInstalled()
{
	CRegKey reg;
	HKEY hKey = NULL;
	TCHAR buf[1024] = {0};
	LONG lngRet =0;
	DWORD dwCharCount;
	BOOL bRet = FALSE;
	
	//΢��ٷ����صİ�װ����װ���������
	lngRet = reg.Open(HKEY_LOCAL_MACHINE,
		_T("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{A49F249F-0C91-497F-86DF-B2585E8E76B7}"),
		KEY_ALL_ACCESS);

	if (ERROR_SUCCESS == lngRet)
	{
		dwCharCount = sizeof(buf)/sizeof(TCHAR);
		lngRet = reg.QueryValue(buf,_T("DisplayName"),&dwCharCount);
		
		if (ERROR_SUCCESS == lngRet)
		{
			bRet = TRUE;
		}
	}
	else
	{
		//vs��װʱ���Զ���װ���������
		lngRet = reg.Open(HKEY_LOCAL_MACHINE,_T("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{710f4c1c-cc18-4c49-8cbf-51240c89a1a2}"),KEY_ALL_ACCESS);
	
		if (ERROR_SUCCESS == lngRet)
		{
			//AfxMessageBox(_T("2\r\n"));
			dwCharCount = sizeof(buf)/sizeof(TCHAR);
			lngRet = reg.QueryValue(buf,_T("DisplayName"),&dwCharCount);
			
			if (ERROR_SUCCESS == lngRet)
			{
				bRet = TRUE;
			}
		}
	}
	
	return bRet;
}
