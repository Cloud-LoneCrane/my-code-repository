//===============================================================
//����: ����IE��ҳ
//����:
//        [in] url
//                ��ҳ��ַ
//����ֵ:
//        �ɹ�,����TRUR;����,����FALSE;
//��ע:
//        ��Ҫʹ��CRegistry��
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

