/* 
 * ���ܵ���ʹ�ã���Ҫ�������ؼ�һ��ʹ��
 * ��Ҫ��SpinCtrl�ĸ����ڴ���	ON_NOTIFY(UDN_DELTAPOS, IDC_SPIN, OnDeltaposSpin)
 * 
 */

    //��ʼ��
	UDACCEL up;
	up.nInc = 5;
	up.nSec = 1;

	CSpinButtonCtrl* pSpin=NULL;
	pSpin = (CSpinButtonCtrl*)GetDlgItem(IDC_SPIN);
	pSpin->SetBuddy((CWnd*)GetDlgItem(IDC_EDIT_frequency)); //���ú����󶨵Ŀؼ�
	pSpin->SetRange(0,100);
	pSpin->SetAccel(5,&up);
	
void CAlertDlg::OnDeltaposSpin(NMHDR* pNMHDR, LRESULT* pResult) 
{
	NM_UPDOWN* pNMUpDown = (NM_UPDOWN*)pNMHDR;
	// TODO: Add your control notification handler code here
	
	CString strTmp="";
    //����ֵ
	strTmp.Format("%d",pNMUpDown->iPos);	
	CWnd *p=((CSpinButtonCtrl*)GetDlgItem(IDC_SPIN))->GetBuddy();
	if(p)
		p->SetWindowText(strTmp);

	*pResult = 0;
}