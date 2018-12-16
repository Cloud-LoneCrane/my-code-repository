
//获得360浏览器指定控件的text

		char buf[512];
		memset(buf,0,sizeof(buf)/sizeof(char));
	 	HWND hWndPar=NULL,hWndChild=NULL;
		hWndPar=::FindWindow("XFrame_Wnd",NULL);
		
		hWndChild =::FindWindowEx(hWndPar,NULL,"XCtrl_Wnd","TopBar");
		
		hWndChild =::FindWindowEx(hWndChild,NULL,"XCtrl_Wnd","AddressBar");

		//0x44C 是控件ID的十六进制形式,控件id是固定不变的
		::GetDlgItemText(hWndChild,0x44C,buf,sizeof(buf)/sizeof(char));
		cout<<buf<<endl;

// 傲游浏览器
	char buf[512];
		memset(buf,0,sizeof(buf)/sizeof(char));
	 	HWND hWndPar=NULL,hWndChild=NULL;
		hWndPar=::FindWindow("Maxthon2_Frame",NULL);
		
		hWndChild =::FindWindowEx(hWndPar,NULL,"XTPDockBar","xtpBarTop");
		hWndChild =::FindWindowEx(hWndChild,NULL,"XTPToolBar",_T("地址栏"));
		
		hWndChild =::FindWindowEx(hWndChild,NULL,"RichEdit20W",NULL);
		
		int len=::SendMessage(hWndChild, WM_GETTEXT,(WPARAM)(sizeof(buf)/sizeof(char)),(
						LPARAM)buf);
	//不能获取到
	//::GetWindowText(hWndChild,buf,sizeof(buf)/sizeof(char));

/* Remarks:
 *     思路: 
 *         (1)通过spy++获得控件和窗口的关系
 *         (2)获得控件的ID
 *         (3)利用FindWindow,FindWindowEx获得相应的句柄
 *         (4)最后,利用::GetDlgItem获得文本
 *     
 *     注意:
 *         对于非windows控件,因为控件是程序员绘制的.不能采用上述方法,可以考虑拦截消息
 *     通过Hook.
 *     
 *      */
