
//获得指定的窗口句柄
char buf[50];
// 	HWND hWnd=::FindWindow("XFrame_Wnd",NULL);  //根据类名
    HWND hWnd=::FindWindow(NULL,"MFC支持socket.txt - 记事本"); //根据窗口名
    if (hWnd)
    {
        GetWindowText(hWnd,buf,sizeof(buf)/sizeof(char));
        cout<<"窗口的标题是:\n"<<buf<<endl;
    }
    else
    {
        cout<<"没有找到匹配的窗口."<<endl;
    }

 //根据句柄获得类名   
    GetClassName(hWnd,buf,sizeof(buf)/sizeof(char));
    cout<<"类名:"<<buf<<endl;

    
  控件句柄  0x000a057c  0002073E
  窗口句柄  0x000705e4  0002075A
    
    196504 差
    
    
 窗口之间的关系:
    about:blank - 360安全浏览器-TopBar-AddressBar-XCtrl_Wnd(se:home)
    