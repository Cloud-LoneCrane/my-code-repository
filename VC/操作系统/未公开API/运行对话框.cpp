	
	CString pszDllName="shell32.dll";
	HINSTANCE hLib = ::LoadLibrary(pszDllName);
	char p[256];
	HICON ico;
//	ico=AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	ico=(HICON)::SendMessage(this->GetSafeHwnd(),WM_GETICON,ICON_SMALL,0);
	//这儿我感觉参数应该是LPCWSTR, 但是网上说得是LPCTSTR
	typedef void (__stdcall *pRunFileDlg)(HWND,HICON, LPCTSTR, LPCTSTR, LPCTSTR, UINT);
	pRunFileDlg RunFileDlg;
	if (hLib==NULL)
	{
		return ;
	}
//	RunFileDlg = (pRunFileDlg)GetProcAddress(hLib, (char *)61);
	RunFileDlg = (pRunFileDlg)GetProcAddress(hLib, MAKEINTRESOURCE(61));
	CString name ="wolfbaby的运行";
	CString sss = "输入程序路径";
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
		); //必须先转换到宽字符,否则后面调用的时候,结果会不对!
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
