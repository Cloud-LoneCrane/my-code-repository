// API ������������״̬
SHAppBarMessage(ABM_SETSTATE,   pabd);  

//������������������󻯵�ȫ��
	::ShowWindow(::FindWindow("Shell_TrayWnd",NULL),SW_HIDE);   
	int   cxScreen   =   ::GetSystemMetrics(SM_CXSCREEN);     
	int   cyScreen   =   ::GetSystemMetrics(SM_CYSCREEN);     
	MoveWindow(0,   0,   cxScreen,   cyScreen);   

//���أ�   
  CWnd   *dc;   
  dc   =   FindWindow("ProgMan",NULL);   
  dc->ShowWindow(SW_HIDE);   
    
  CWnd   *dc2;   
  CRect taskBarRECT;
  dc2   =   FindWindow("Shell_TrayWnd",NULL);   
  dc2->SetWindowPos(NULL,0,600,0,0,SWP_HIDEWINDOW);   
  
  //��ʾ��   
  CWnd   *dc;   
  dc   =   FindWindow("ProgMan",NULL);   
  dc->ShowWindow(SW_SHOW);   
    
	CWnd   *dc2;   
	dc2   =   FindWindow("Shell_TrayWnd",NULL);   
	dc2->SetWindowPos(NULL,0,0,0,0,SWP_SHOWWINDOW);  