	
	CString pszDllName="shell32.dll";
	HINSTANCE hLib = ::LoadLibrary(pszDllName);
	char p[256];
	HICON ico;
//	ico=AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	ico=(HICON)::SendMessage(this->GetSafeHwnd(),WM_GETICON,ICON_SMALL,0);
	//����Ҹо�����Ӧ����LPCWSTR, ��������˵����LPCTSTR
	typedef void (__stdcall *pRunFileDlg)(HWND,HICON, LPCTSTR, LPCTSTR, LPCTSTR, UINT);
	pRunFileDlg RunFileDlg;
	if (hLib==NULL)
	{
		return ;
	}
//	RunFileDlg = (pRunFileDlg)GetProcAddress(hLib, (char *)61);
	RunFileDlg = (pRunFileDlg)GetProcAddress(hLib, MAKEINTRESOURCE(61));
	CString name ="wolfbaby������";
	CString sss = "�������·��";
	LPWSTR wname=new WCHAR[100];
	LPWSTR wsss=new WCHAR[100]; 
	memset(wname,0,sizeof(WCHAR)*100);
	memset(wsss,0,sizeof(WCHAR)*100);
	MultiByteToWideChar(CP_ACP,
		MB_PRECOMPOSED, // character-type options
		name, // address of string to map
		name.GetLength(), // number of bytes in string
		wname, // address of wide-character buffer
		100// 
		); //������ת�������ַ�,���������õ�ʱ��,����᲻��!
	MultiByteToWideChar(CP_ACP,
		MB_PRECOMPOSED, // character-type options
		sss, // address of string to map
		sss.GetLength(), // number of bytes in string
		wsss, // address of wide-character buffer
		100
		);
	

	if(RunFileDlg!=NULL)
	{
		RunFileDlg(this->GetSafeHwnd(),
			ico,
			NULL,
			(LPCTSTR)wname,
			(LPCTSTR)wsss,0x02); 
	}
	::FreeLibrary(hLib);
