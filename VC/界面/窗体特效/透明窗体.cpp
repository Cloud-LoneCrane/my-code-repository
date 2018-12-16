////////////////////////////////////////////////////////////////
//	设置窗体透明度
////////////////////////////////////////////////////////////////

//=============================================================
// 功能：设置窗体透明度
// 参数：
//	HWND	hWnd  句柄（需要改变透明度的窗体）
//	int Layer	N 通明度（百分比）N为透明度(1-100)
// 调用格式：
//	SetTransparent(this->GetSafeHwnd(),N);   // N为透明度(1-100)
//    
//获得全局句柄
//	CMainFrame *pMain=(CMainFrame *)AfxGetApp()->m_pMainWnd;
//	SetTransparent(pMain->GetSafeHwnd(),90); 
//=============================================================
BOOL SetTransparent(HWND hWnd, int LayerN) 
{ 
	HMODULE hModule = GetModuleHandle("User32.DLL"); 
	if(hModule==NULL) 
	{ 
		return FALSE; 
	} 

	if(LayerN<0) LayerN = 0; 
	if(LayerN>100) LayerN =100; 

	typedef BOOL (WINAPI* SETLAYEREDWND)(HWND,COLORREF,BYTE,DWORD); 
	SETLAYEREDWND SetLayeredWindowPtr = NULL; 
	SetLayeredWindowPtr = (SETLAYEREDWND)GetProcAddress(hModule, 
												"SetLayeredWindowAttributes");    
	 
	if(SetLayeredWindowPtr) 
	{   //设置窗口扩展风格
		LONG lStyle = GetWindowLong(hWnd,GWL_EXSTYLE)|0x00080000; 
				SetWindowLong(hWnd, GWL_EXSTYLE, lStyle); 

		SetLayeredWindowPtr(hWnd, 
			RGB(0,0,0), 
			BYTE((255 * LayerN)/100), 2); 
	} 

	return true; 
} 
