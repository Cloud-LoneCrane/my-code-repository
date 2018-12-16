///////////////////////////////////////////////////////////////////
//	追加系统菜单
///////////////////////////////////////////////////////////////////

#define IDI_PECULIARMENU 140

//一、添加
	CMenu* m_pMenu;
	m_pMenu = GetSystemMenu(FALSE);
	m_pMenu->AppendMenu(MF_STRING,IDI_PECULIARMENU,"系统菜单");
	
//二、void CPeculiarMenuDlg::OnSysCommand(UINT nID, LPARAM lParam)中添加
else if (nID == IDI_PECULIARMENU)
	{
			MessageBox("系统菜单","提示",MB_OK|MB_ICONINFORMATION);
	}