
//�������
void COrientationDlg::OnLButtonDown(UINT nFlags, CPoint point) 
{
	// TODO: Add your message handler code here and/or call default
	
	SetCapture();
	CDialog::OnLButtonDown(nFlags, point);
}

//�ͷ����
void COrientationDlg::OnLButtonUp(UINT nFlags, CPoint point) 
{
	// TODO: Add your message handler code here and/or call default
	CPoint pos;
	::GetCursorPos(&pos);
	CString strText;
	strText.Format("(%d,%d)",pos.x,pos.y);
	GetDlgItem(IDC_EDIT_POS)->SetWindowText(strText);

	ReleaseCapture();
	
	CDialog::OnLButtonUp(nFlags, point);
}

