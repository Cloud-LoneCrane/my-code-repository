
//���ָ���Ĵ��ھ��
char buf[50];
// 	HWND hWnd=::FindWindow("XFrame_Wnd",NULL);  //��������
    HWND hWnd=::FindWindow(NULL,"MFC֧��socket.txt - ���±�"); //���ݴ�����
    if (hWnd)
    {
        GetWindowText(hWnd,buf,sizeof(buf)/sizeof(char));
        cout<<"���ڵı�����:\n"<<buf<<endl;
    }
    else
    {
        cout<<"û���ҵ�ƥ��Ĵ���."<<endl;
    }

 //���ݾ���������   
    GetClassName(hWnd,buf,sizeof(buf)/sizeof(char));
    cout<<"����:"<<buf<<endl;

    
  �ؼ����  0x000a057c  0002073E
  ���ھ��  0x000705e4  0002075A
    
    196504 ��
    
    
 ����֮��Ĺ�ϵ:
    about:blank - 360��ȫ�����-TopBar-AddressBar-XCtrl_Wnd(se:home)
    