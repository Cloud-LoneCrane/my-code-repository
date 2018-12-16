/************************************************************************/
/* ����                                                            					              */
/************************************************************************/
/**  ����
 * һ �Զ�����Ϣ,����NOTIFYICONDATA����
 * �� ΪNOTIFYICONDATA����ֵ,�������õ������ͼƬ�Ļص���Ϣ
 * �� ���void OnNotifyIcon(WPARAM wParam,LPARAM IParam);�����Ͷ���
 * �� ��BEGIN_MESSAGE_MAP()��END_MESSAGE_MAP()֮�����
 *ON_MESSAGE(WM_MYNOTIFY,OnNotifyIcon) ��������Ϣ�ͺ����Ĺ���
*/
	
//Shell_NotifyIcon
//   Header: Declared in Shellapi.h. 
//   Import Library: Shell32.lib. 



//-------------------------------------------------------------------
//һ �Զ�����Ϣ,����NOTIFYICONDATA����
#define WM_NOTIFY_MESSAGE WM_USER+1 //����ص���Ϣ

	NOTIFYICONDATA m_traydata; //����ͨ�����ݶ���,��.hͷ�ļ���
	
//-------------------------------------------------------------------
//�� ΪNOTIFYICONDATA����ֵ,�������õ������ͼƬ�Ļص���Ϣ
	m_traydata.cbSize = sizeof(NOTIFYICONDATA); //���ݽṹ�Ĵ�С
	m_traydata.hIcon = AfxGetApp()->LoadIcon(IDI_TRAYICON);//��������ͼ��
	m_traydata.hWnd = m_hWnd;//���̹����ĳ���

	char  *m_str = "ϵͳ����"; //tooltip��ʾ����
	strncpy(m_traydata.szTip,m_str,strlen(m_str)+1);//strlen +1��ʾ�����ַ�������Ŀ���ַ�����
	m_traydata.uCallbackMessage = WM_NOTIFY_MESSAGE; //�ص���Ϣ
	m_traydata.uFlags = NIF_ICON|NIF_MESSAGE|NIF_TIP;//���
	
	


//-------------------------------------------------------------------
//�� ���void OnNotifyIcon(WPARAM wParam,LPARAM IParam);�����Ͷ���
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
//�� ����Ϣ�ͷ���
//BEGIN_MESSAGE_MAP()��END_MESSAGE_MAP()֮�����
ON_MESSAGE(WM_NOTIFY_MESSAGE,OnNotifyIcon)

//-------------------------------------------------------------------
//��������ͼ��
	//���ϵͳ����
	Shell_NotifyIcon(NIM_ADD,&m_traydata);

	//ɾ��ϵͳ����
	Shell_NotifyIcon(NIM_DELETE,&m_traydata);



	