///////////////////////////////////////////////////////////////////
//	׷��ϵͳ�˵�
///////////////////////////////////////////////////////////////////

#define IDI_PECULIARMENU 140

//һ�����
	CMenu* m_pMenu;
	m_pMenu = GetSystemMenu(FALSE);
	m_pMenu->AppendMenu(MF_STRING,IDI_PECULIARMENU,"ϵͳ�˵�");
	
//����void CPeculiarMenuDlg::OnSysCommand(UINT nID, LPARAM lParam)�����
else if (nID == IDI_PECULIARMENU)
	{
			MessageBox("ϵͳ�˵�","��ʾ",MB_OK|MB_ICONINFORMATION);
	}