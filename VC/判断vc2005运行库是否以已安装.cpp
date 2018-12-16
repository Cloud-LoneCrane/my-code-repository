//===============================================================
//功能:
//	判断vc2005运行库是否以已安装
//参数:
//	无
//返回值:
//	如果安装，返回TRUE,否则，返回FALSE
//备注: 
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
	
	//微软官方下载的安装包安装后是这个键
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
		//vs安装时，自动安装的是这个键
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
