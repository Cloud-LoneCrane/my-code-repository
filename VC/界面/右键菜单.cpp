
//当在窗体上右击时，框架会调用CWnd::OnContextMenu()
void CPopManuDlg::OnContextMenu(CWnd* pWnd, CPoint point) 
{
	CMenu m_popmenu;
	m_popmenu.LoadMenu(IDR_POPMENU);
	CMenu* m_submenu = m_popmenu.GetSubMenu(0);
	ASSERT(m_submenu);

	//TrackPopupMenu 弹出弹出菜单
	m_submenu->TrackPopupMenu(TPM_LEFTBUTTON |TPM_LEFTALIGN ,point.x,point.y,this);

	m_popmenu.DestroyMenu();
}

//当在窗体上右击时，框架会调用CWnd::OnContextMenu()
void CGDIPClockDlg::OnContextMenu(CWnd*, CPoint point)
{
	if (point.x == -1 && point.y == -1){
		//keystroke invocation
		CRect rect;
		GetClientRect(rect);
		ClientToScreen(rect);

		point = rect.TopLeft();
		point.Offset(5, 5);
	}

	CMenu menu;
	VERIFY(menu.LoadMenu(IDR_MENU1));

	CMenu* pPopup = menu.GetSubMenu(0);
	ASSERT(pPopup != NULL);
	CWnd* pWndPopupOwner = this;

	while (pWndPopupOwner->GetStyle() & WS_CHILD)
		pWndPopupOwner = pWndPopupOwner->GetParent();

	pPopup->TrackPopupMenu(TPM_LEFTALIGN | TPM_RIGHTBUTTON, point.x, point.y,
		pWndPopupOwner);
}