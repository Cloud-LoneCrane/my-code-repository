/////////////////////////////////////////////////////
//	弹出特效对话框.cpp
/////////////////////////////////////////////////////
 //获得桌面大小
 CRect rectWorkArea;
 SystemParametersInfo(SPI_GETWORKAREA,0,&rectWorkArea,SPIF_SENDCHANGE);   
 
 //获得对话框大小
 CRect rectDlg;
 GetWindowRect(&rectDlg);
 int nW = rectDlg.Width();
 int nH = rectDlg.Height();
 
 //将窗口设置到右下脚
 ::SetWindowPos(this->m_hWnd,HWND_BOTTOM,
  rectWorkArea.right-nW-5,rectWorkArea.bottom-nH,
  nW,nH,
  SWP_NOZORDER);
 
 //动画显示
 ::AnimateWindow(GetSafeHwnd(),800,AW_SLIDE|AW_VER_NEGATIVE);
 
//要使用AnimateWindow还必须在StdAfx.h添加： 
#undef WINVER
#define WINVER 0x500 //WINVER>=0X500指Win2000以上。
//有些api和常数只有在win2000以上才支持