//禁用最小化按钮
void CMainFrame::OnMenudismin() 
{
	// TODO: Add your command handler code here
	//获得窗口风格
	Style = ::GetWindowLong(m_hWnd,GWL_STYLE);
	//设置新的风格
	Style &= ~(WS_MINIMIZEBOX);
	::SetWindowLong(m_hWnd,GWL_STYLE,Style);
	GetWindowRect(&Rect);
	//重画窗口边框
	::SetWindowPos(m_hWnd,HWND_TOP,Rect.left,Rect.top,Rect.Width(),Rect.Height(),SWP_DRAWFRAME);
}
//禁用最大化按钮
void CMainFrame::OnMenudismax() 
{
	// TODO: Add your command handler code here
	//获得窗口风格
	Style = ::GetWindowLong(m_hWnd,GWL_STYLE);
	//设置新的风格
	Style &= ~(WS_MAXIMIZEBOX);
	::SetWindowLong(m_hWnd,GWL_STYLE,Style);
	GetWindowRect(&Rect);
	//重画窗口边框
	::SetWindowPos(m_hWnd,HWND_TOP,Rect.left,Rect.top,Rect.Width(),Rect.Height(),SWP_DRAWFRAME);
}
//禁用关闭按钮
void CMainFrame::OnMenudisclose() 
{
	// TODO: Add your command handler code here
	//获得系统菜单
	CMenu *pMenu = GetSystemMenu(false);
	//获得关闭按钮ID
	UINT ID = pMenu->GetMenuItemID(pMenu->GetMenuItemCount()-1);
	//使关闭按钮无效
	pMenu->EnableMenuItem(ID,MF_GRAYED);
}
//使最小化按钮有效
void CMainFrame::OnMenuablemin() 
{
	// TODO: Add your command handler code here
	//获得窗口风格
	Style = ::GetWindowLong(m_hWnd,GWL_STYLE);
	//设置新的风格
	Style |= WS_MINIMIZEBOX;
	::SetWindowLong(m_hWnd,GWL_STYLE,Style);
	GetWindowRect(&Rect);
	//重画窗口边框
	::SetWindowPos(m_hWnd,HWND_TOP,Rect.left,Rect.top,Rect.Width(),Rect.Height(),SWP_DRAWFRAME);
}
//使最大化按钮有效
void CMainFrame::OnMenuablemax() 
{
	// TODO: Add your command handler code here
	//获得窗口风格
	Style = ::GetWindowLong(m_hWnd,GWL_STYLE);
	//设置新的风格
	Style |= WS_MAXIMIZEBOX;
	::SetWindowLong(m_hWnd,GWL_STYLE,Style);
	GetWindowRect(&Rect);
	//重画窗口边框
	::SetWindowPos(m_hWnd,HWND_TOP,Rect.left,Rect.top,Rect.Width(),Rect.Height(),SWP_DRAWFRAME);
}
//使关闭按钮有效
void CMainFrame::OnMenuableclose() 
{
	// TODO: Add your command handler code here
	//获得系统菜单
	CMenu *pMenu = GetSystemMenu(false);
	//获得关闭按钮ID
	UINT ID = pMenu->GetMenuItemID(pMenu->GetMenuItemCount()-1);
	//使关闭按钮可用
	pMenu->EnableMenuItem(ID,MF_ENABLED);
}
