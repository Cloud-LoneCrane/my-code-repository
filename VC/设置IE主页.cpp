//===============================================================
//功能: 设置IE主页
//参数:
//        [in] url
//                主页网址
//返回值:
//        成功,返回TRUR;否则,返回FALSE;
//备注:
//        需要使用CRegistry类
//===============================================================
BOOL SetIeHomePage(CString url)
{
	BOOL                bResult = FALSE;
	CRegistry                clsReg;
	CString                strHomePage= _T("");
	CString                strNewHomePage = url;
	
	if(clsReg.OpenKey(CRegistry::currentUser, _T("Software\\Microsoft\\Internet Explorer\\Main")))
	{
		clsReg.GetValue(_T("Start Page"), strHomePage);
		
		if(strHomePage != strNewHomePage)
		{
			if(clsReg.SetValue("Start Page",strNewHomePage))
				bResult = TRUE;
			else
				bResult = FALSE;
			
		}
		else
		{
			bResult = TRUE;
		}
		clsReg.CloseKey();
	}
	else
		bResult = FALSE;
	
	return bResult;
}

