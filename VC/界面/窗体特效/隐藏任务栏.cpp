// API 设置任务栏的状态
SHAppBarMessage(ABM_SETSTATE,   pabd);  

//隐藏任务栏，窗口最大化到全屏
	::ShowWindow(::FindWindow("Shell_TrayWnd",NULL),SW_HIDE);   
	int   cxScreen   =   ::GetSystemMetrics(SM_CXSCREEN);     
	int   cyScreen   =   ::GetSystemMetrics(SM_CYSCREEN);     
	MoveWindow(0,   0,   cxScreen,   cyScreen);   

//隐藏：   
  CWnd   *dc;   
  dc   =   FindWindow("ProgMan",NULL);   
  dc->ShowWindow(SW_HIDE);   
    
  CWnd   *dc2;   
  CRect taskBarRECT;
  dc2   =   FindWindow("Shell_TrayWnd",NULL);   
  dc2->SetWindowPos(NULL,0,600,0,0,SWP_HIDEWINDOW);   
  
  //显示：   
  CWnd   *dc;   
  dc   =   FindWindow("ProgMan",NULL);   
  dc->ShowWindow(SW_SHOW);   
    
	CWnd   *dc2;   
	dc2   =   FindWindow("Shell_TrayWnd",NULL);   
	dc2->SetWindowPos(NULL,0,0,0,0,SWP_SHOWWINDOW);  