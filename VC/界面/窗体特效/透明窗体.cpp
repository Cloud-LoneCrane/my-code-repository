////////////////////////////////////////////////////////////////
//	���ô���͸����
////////////////////////////////////////////////////////////////

//=============================================================
// ���ܣ����ô���͸����
// ������
//	HWND	hWnd  �������Ҫ�ı�͸���ȵĴ��壩
//	int Layer	N ͨ���ȣ��ٷֱȣ�NΪ͸����(1-100)
// ���ø�ʽ��
//	SetTransparent(this->GetSafeHwnd(),N);   // NΪ͸����(1-100)
//    
//���ȫ�־��
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
	{   //���ô�����չ���
		LONG lStyle = GetWindowLong(hWnd,GWL_EXSTYLE)|0x00080000; 
				SetWindowLong(hWnd, GWL_EXSTYLE, lStyle); 

		SetLayeredWindowPtr(hWnd, 
			RGB(0,0,0), 
			BYTE((255 * LayerN)/100), 2); 
	} 

	return true; 
} 
