
//======================================================
//	功能：点击按钮并粘贴
//======================================================
//CTRL_ID 表示控件ID	
	::SendMessage(this->GetSafeHwnd(),WM_COMMAND,
					MAKEWPARAM(CTRL_ID, BN_CLICKED), NULL);
	::Sleep(700); //给它个反应时间
	::keybd_event(VK_CONTROL, VK_CONTROL,0,0);   
	::keybd_event('V','V',0,0);   
	::Sleep(300);           //'延时500毫秒 
	::keybd_event('V','V',KEYEVENTF_KEYUP,0);   
	::keybd_event(VK_CONTROL,VK_CONTROL,   KEYEVENTF_KEYUP,   0);   
	
//======================================================
//	功能：抓屏并粘贴
//======================================================	
	::keybd_event(VK_SNAPSHOT, 0, 0, 0); //模拟抓屏按下
	::Sleep(500);	//避免太快没有复制到剪贴板上
	::keybd_event(VK_CONTROL, VK_CONTROL,0,0);   
	::keybd_event('V','V',0,0);   
	::Sleep(500);           //'延时500毫秒 
	::keybd_event('V','V',KEYEVENTF_KEYUP,0);   
	::keybd_event(VK_CONTROL,VK_CONTROL,   KEYEVENTF_KEYUP,   0);   
