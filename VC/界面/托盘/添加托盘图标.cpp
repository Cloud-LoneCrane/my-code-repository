/************************************************************************/
/* 托盘                                                            					              */
/************************************************************************/
/**  托盘
 * 一 自定义消息,定义NOTIFYICONDATA对象
 * 二 为NOTIFYICONDATA对象赋值,包括设置点击托盘图片的回调消息
 * 三 添加void OnNotifyIcon(WPARAM wParam,LPARAM IParam);声明和定义
 * 四 在BEGIN_MESSAGE_MAP()和END_MESSAGE_MAP()之间添加
 *ON_MESSAGE(WM_MYNOTIFY,OnNotifyIcon) 建立起消息和函数的关联
*/
	
//Shell_NotifyIcon
//   Header: Declared in Shellapi.h. 
//   Import Library: Shell32.lib. 



//-------------------------------------------------------------------
//一 自定义消息,定义NOTIFYICONDATA对象
#define WM_NOTIFY_MESSAGE WM_USER+1 //定义回调消息

	NOTIFYICONDATA m_traydata; //定义通告数据对象,在.h头文件中
	
//-------------------------------------------------------------------
//二 为NOTIFYICONDATA对象赋值,包括设置点击托盘图片的回调消息
	m_traydata.cbSize = sizeof(NOTIFYICONDATA); //数据结构的大小
	m_traydata.hIcon = AfxGetApp()->LoadIcon(IDI_TRAYICON);//设置托盘图标
	m_traydata.hWnd = m_hWnd;//托盘关联的程序

	char  *m_str = "系统管理"; //tooltip提示文字
	strncpy(m_traydata.szTip,m_str,strlen(m_str)+1);//strlen +1表示将空字符拷贝到目标字符串中
	m_traydata.uCallbackMessage = WM_NOTIFY_MESSAGE; //回调消息
	m_traydata.uFlags = NIF_ICON|NIF_MESSAGE|NIF_TIP;//标记
	
	


//-------------------------------------------------------------------
//三 添加void OnNotifyIcon(WPARAM wParam,LPARAM IParam);声明和定义
void OnNotifyIcon(WPARAM wParam, LPARAM IParam)
{
	if ((IParam == WM_LBUTTONDBLCLK) || (IParam == WM_RBUTTONDOWN))
	{ 
		ModifyStyleEx(0,WS_EX_TOPMOST);
		ShowWindow(SW_SHOW);
		Shell_NotifyIcon(NIM_DELETE, &m_traydata);
    }
}



//-------------------------------------------------------------------
//四 绑定消息和方法
//BEGIN_MESSAGE_MAP()和END_MESSAGE_MAP()之间添加
ON_MESSAGE(WM_NOTIFY_MESSAGE,OnNotifyIcon)

//-------------------------------------------------------------------
//操作托盘图标
	//添加系统托盘
	Shell_NotifyIcon(NIM_ADD,&m_traydata);

	//删除系统托盘
	Shell_NotifyIcon(NIM_DELETE,&m_traydata);



	