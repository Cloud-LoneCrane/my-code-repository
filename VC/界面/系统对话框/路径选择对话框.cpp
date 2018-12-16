//================================================================
//功能:	选择路径
//参数:
//	pFilePath [out] 保存文件的字符数组
//	size [in] 字符数组的大小
//================================================================
bool SelectPath(TCHAR* pFilePath,int size)
{
	TCHAR   szPath[_MAX_PATH];   
	BROWSEINFO   bi;   
	//指定父窗口，在对话框显示期间，父窗口将被禁用   
	bi.hwndOwner   =   NULL;   
	//如果指定NULL，就以“桌面”为根   
	bi.pidlRoot   =   NULL;   
	//这一行将显示在对话框的顶端   
	bi.lpszTitle   =     _T("请选择一个文件夹");   
	bi.pszDisplayName   =   szPath;   
	//只返回文件系统中存在的文件夹   
	bi.ulFlags   =   BIF_RETURNONLYFSDIRS;   
	bi.lpfn   =   NULL;   //回调函数的指针   
	bi.lParam   =   NULL;   //传向回调函数的参数   

	//现在，调用函数来显示对话框   
	//它总与Windows的外壳程序Explorer保持相同的外观   
	LPITEMIDLIST   pItemIDList = SHBrowseForFolder( &bi);   

	if(pItemIDList)   //点按了“确定”按钮   
	{   
		TCHAR szPath[_MAX_PATH];   
		if(SHGetPathFromIDList(pItemIDList,szPath)   )   
		{   
			/*
			//成功地取得了文件夹信息   
			CString   strMessage;   
			strMessage.Format("选定的文件夹是\'%s\'",   szPath);   
			AfxMessageBox(strMessage); 
			*/

			_tcscpy(pFilePath,szPath);
			return TRUE;
		}   
		//防止内存泄漏，要使用IMalloc接口   
		IMalloc*   pMalloc;   
		if   (   SHGetMalloc(   &pMalloc   )   !=   NOERROR   )   
		{   
			//未返回有效的IMalloc接口指针   
			TRACE(_T("无法取得外壳程序的IMalloc接口\n"));   
		}   
		pMalloc->Free(   pItemIDList   );   
		if(pMalloc)   
			pMalloc->Release();   

		return TRUE;
	}  

	return FALSE;
}