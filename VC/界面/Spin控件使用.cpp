/* 
 * 不能单独使用，需要和其他控件一起使用
 * 需要在SpinCtrl的父窗口处理	ON_NOTIFY(UDN_DELTAPOS, IDC_SPIN, OnDeltaposSpin)
 * 
 */

    //初始化
	UDACCEL up;
	up.nInc = 5;
	up.nSec = 1;

	CSpinButtonCtrl* pSpin=NULL;
	pSpin = (CSpinButtonCtrl*)GetDlgItem(IDC_SPIN);
	pSpin->SetBuddy((CWnd*)GetDlgItem(IDC_EDIT_frequency)); //设置和它绑定的控件
	pSpin->SetRange(0,100);
	pSpin->SetAccel(5,&up);
	
void CAlertDlg::OnDeltaposSpin(NMHDR* pNMHDR, LRESULT* pResult) 
{
	NM_UPDOWN* pNMUpDown = (NM_UPDOWN*)pNMHDR;
	// TODO: Add your control notification handler code here
	
	CString strTmp="";
    //更新值
	strTmp.Format("%d",pNMUpDown->iPos);	
	CWnd *p=((CSpinButtonCtrl*)GetDlgItem(IDC_SPIN))->GetBuddy();
	if(p)
		p->SetWindowText(strTmp);

	*pResult = 0;
}