/************************************************************************
向系统菜单中添加菜单项，分三步：
一 首先获得系统菜单的指针；
二 调用CMenu的方法，操作菜单项；
三 在OnSysCommand(UINT nID, LPARAM lParam)中处理菜单项的响应。
************************************************************************/

#define IDM_TEST 0x1000 //定义菜单项的ID,0xFFF0

//获得菜单指针并操作它
CMenu* pSysMenu = GetSystemMenu(FALSE);
pSysMenu->AppendMenu(MF_STRING,IDM_TEST,_T("测试"));

//处理菜单响应
void OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else if((nID & 0xFFF0)==IDM_TEST)
	{
		MessageBox.Show(_T("测试"));
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}