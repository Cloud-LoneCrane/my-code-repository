
//���360�����ָ���ؼ���text

		char buf[512];
		memset(buf,0,sizeof(buf)/sizeof(char));
	 	HWND hWndPar=NULL,hWndChild=NULL;
		hWndPar=::FindWindow("XFrame_Wnd",NULL);
		
		hWndChild =::FindWindowEx(hWndPar,NULL,"XCtrl_Wnd","TopBar");
		
		hWndChild =::FindWindowEx(hWndChild,NULL,"XCtrl_Wnd","AddressBar");

		//0x44C �ǿؼ�ID��ʮ��������ʽ,�ؼ�id�ǹ̶������
		::GetDlgItemText(hWndChild,0x44C,buf,sizeof(buf)/sizeof(char));
		cout<<buf<<endl;

// ���������
	char buf[512];
		memset(buf,0,sizeof(buf)/sizeof(char));
	 	HWND hWndPar=NULL,hWndChild=NULL;
		hWndPar=::FindWindow("Maxthon2_Frame",NULL);
		
		hWndChild =::FindWindowEx(hWndPar,NULL,"XTPDockBar","xtpBarTop");
		hWndChild =::FindWindowEx(hWndChild,NULL,"XTPToolBar",_T("��ַ��"));
		
		hWndChild =::FindWindowEx(hWndChild,NULL,"RichEdit20W",NULL);
		
		int len=::SendMessage(hWndChild, WM_GETTEXT,(WPARAM)(sizeof(buf)/sizeof(char)),(
						LPARAM)buf);
	//���ܻ�ȡ��
	//::GetWindowText(hWndChild,buf,sizeof(buf)/sizeof(char));

/* Remarks:
 *     ˼·: 
 *         (1)ͨ��spy++��ÿؼ��ʹ��ڵĹ�ϵ
 *         (2)��ÿؼ���ID
 *         (3)����FindWindow,FindWindowEx�����Ӧ�ľ��
 *         (4)���,����::GetDlgItem����ı�
 *     
 *     ע��:
 *         ���ڷ�windows�ؼ�,��Ϊ�ؼ��ǳ���Ա���Ƶ�.���ܲ�����������,���Կ���������Ϣ
 *     ͨ��Hook.
 *     
 *      */
