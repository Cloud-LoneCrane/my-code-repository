//������С����ť
void CMainFrame::OnMenudismin() 
{
	// TODO: Add your command handler code here
	//��ô��ڷ��
	Style = ::GetWindowLong(m_hWnd,GWL_STYLE);
	//�����µķ��
	Style &= ~(WS_MINIMIZEBOX);
	::SetWindowLong(m_hWnd,GWL_STYLE,Style);
	GetWindowRect(&Rect);
	//�ػ����ڱ߿�
	::SetWindowPos(m_hWnd,HWND_TOP,Rect.left,Rect.top,Rect.Width(),Rect.Height(),SWP_DRAWFRAME);
}
//������󻯰�ť
void CMainFrame::OnMenudismax() 
{
	// TODO: Add your command handler code here
	//��ô��ڷ��
	Style = ::GetWindowLong(m_hWnd,GWL_STYLE);
	//�����µķ��
	Style &= ~(WS_MAXIMIZEBOX);
	::SetWindowLong(m_hWnd,GWL_STYLE,Style);
	GetWindowRect(&Rect);
	//�ػ����ڱ߿�
	::SetWindowPos(m_hWnd,HWND_TOP,Rect.left,Rect.top,Rect.Width(),Rect.Height(),SWP_DRAWFRAME);
}
//���ùرհ�ť
void CMainFrame::OnMenudisclose() 
{
	// TODO: Add your command handler code here
	//���ϵͳ�˵�
	CMenu *pMenu = GetSystemMenu(false);
	//��ùرհ�ťID
	UINT ID = pMenu->GetMenuItemID(pMenu->GetMenuItemCount()-1);
	//ʹ�رհ�ť��Ч
	pMenu->EnableMenuItem(ID,MF_GRAYED);
}
//ʹ��С����ť��Ч
void CMainFrame::OnMenuablemin() 
{
	// TODO: Add your command handler code here
	//��ô��ڷ��
	Style = ::GetWindowLong(m_hWnd,GWL_STYLE);
	//�����µķ��
	Style |= WS_MINIMIZEBOX;
	::SetWindowLong(m_hWnd,GWL_STYLE,Style);
	GetWindowRect(&Rect);
	//�ػ����ڱ߿�
	::SetWindowPos(m_hWnd,HWND_TOP,Rect.left,Rect.top,Rect.Width(),Rect.Height(),SWP_DRAWFRAME);
}
//ʹ��󻯰�ť��Ч
void CMainFrame::OnMenuablemax() 
{
	// TODO: Add your command handler code here
	//��ô��ڷ��
	Style = ::GetWindowLong(m_hWnd,GWL_STYLE);
	//�����µķ��
	Style |= WS_MAXIMIZEBOX;
	::SetWindowLong(m_hWnd,GWL_STYLE,Style);
	GetWindowRect(&Rect);
	//�ػ����ڱ߿�
	::SetWindowPos(m_hWnd,HWND_TOP,Rect.left,Rect.top,Rect.Width(),Rect.Height(),SWP_DRAWFRAME);
}
//ʹ�رհ�ť��Ч
void CMainFrame::OnMenuableclose() 
{
	// TODO: Add your command handler code here
	//���ϵͳ�˵�
	CMenu *pMenu = GetSystemMenu(false);
	//��ùرհ�ťID
	UINT ID = pMenu->GetMenuItemID(pMenu->GetMenuItemCount()-1);
	//ʹ�رհ�ť����
	pMenu->EnableMenuItem(ID,MF_ENABLED);
}
