
//======================================================
//	���ܣ������ť��ճ��
//======================================================
//CTRL_ID ��ʾ�ؼ�ID	
	::SendMessage(this->GetSafeHwnd(),WM_COMMAND,
					MAKEWPARAM(CTRL_ID, BN_CLICKED), NULL);
	::Sleep(700); //��������Ӧʱ��
	::keybd_event(VK_CONTROL, VK_CONTROL,0,0);   
	::keybd_event('V','V',0,0);   
	::Sleep(300);           //'��ʱ500���� 
	::keybd_event('V','V',KEYEVENTF_KEYUP,0);   
	::keybd_event(VK_CONTROL,VK_CONTROL,   KEYEVENTF_KEYUP,   0);   
	
//======================================================
//	���ܣ�ץ����ճ��
//======================================================	
	::keybd_event(VK_SNAPSHOT, 0, 0, 0); //ģ��ץ������
	::Sleep(500);	//����̫��û�и��Ƶ���������
	::keybd_event(VK_CONTROL, VK_CONTROL,0,0);   
	::keybd_event('V','V',0,0);   
	::Sleep(500);           //'��ʱ500���� 
	::keybd_event('V','V',KEYEVENTF_KEYUP,0);   
	::keybd_event(VK_CONTROL,VK_CONTROL,   KEYEVENTF_KEYUP,   0);   
