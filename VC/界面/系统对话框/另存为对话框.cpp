//////////////////////////////////////////////////////////////////
//	另存为对话框 
//  remark:
//	GetOpenFileName 打开对话框
//////////////////////////////////////////////////////////////////
	OPENFILENAME ofn;       // 公共对话框结构。
    TCHAR szFile[MAX_PATH]; // 保存获取文件名称的缓冲区。           
 
	// 初始化选择文件对话框。
	ZeroMemory(&ofn, sizeof(ofn));
	ofn.lStructSize = sizeof(ofn);
	ofn.hwndOwner = m_hWnd;
	ofn.lpstrFile = szFile;

	//
	ofn.lpstrFile[0] = _T('\0');
	ofn.nMaxFile = sizeof(szFile);
	ofn.lpstrFilter = _T("All\0*.*\0Text\0*.TXT\0");
	ofn.nFilterIndex = 1;
	ofn.lpstrFileTitle = NULL;
	ofn.nMaxFileTitle = 0;
	ofn.lpstrInitialDir = NULL;
	ofn.Flags = OFN_SHOWHELP | OFN_OVERWRITEPROMPT;

   // 显示打开选择文件对话框。
   if (GetSaveFileName(&ofn) )
   {
           //显示选择的文件。
           OutputDebugString(szFile);
           OutputDebugString(_T("\r\n"));
		   TRACE("%s",szFile);
    }