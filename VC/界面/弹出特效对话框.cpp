/////////////////////////////////////////////////////
//	������Ч�Ի���.cpp
/////////////////////////////////////////////////////
 //��������С
 CRect rectWorkArea;
 SystemParametersInfo(SPI_GETWORKAREA,0,&rectWorkArea,SPIF_SENDCHANGE);   
 
 //��öԻ����С
 CRect rectDlg;
 GetWindowRect(&rectDlg);
 int nW = rectDlg.Width();
 int nH = rectDlg.Height();
 
 //���������õ����½�
 ::SetWindowPos(this->m_hWnd,HWND_BOTTOM,
  rectWorkArea.right-nW-5,rectWorkArea.bottom-nH,
  nW,nH,
  SWP_NOZORDER);
 
 //������ʾ
 ::AnimateWindow(GetSafeHwnd(),800,AW_SLIDE|AW_VER_NEGATIVE);
 
//Ҫʹ��AnimateWindow��������StdAfx.h��ӣ� 
#undef WINVER
#define WINVER 0x500 //WINVER>=0X500ָWin2000���ϡ�
//��Щapi�ͳ���ֻ����win2000���ϲ�֧��