/////////////////////////////////////
// �����ȼ�
/////////////////////////////////////

/* *************************************************************
*   ����ͨ�������WM_HOTKEY��Ϣ,��Ϊ����û��.ֻ���ֹ�
* ���.
***************************************************************/

//һ ������λ�ü��� "afx_msg LONG OnHotKey(WPARAM wParam,LPARAM lParam);  //�ֹ����"

	// Generated message map functions
	//{{AFX_MSG(CMy00001Dlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnButton2();
	afx_msg void OnButton3();
	//}}AFX_MSG

	afx_msg LONG OnHotKey(WPARAM wParam,LPARAM lParam);  //�ֹ����
	DECLARE_MESSAGE_MAP()
//------------------------------------------------------------------------------------------------------------------	

//�� ������λ�ü��� "ON_MESSAGE(WM_HOTKEY,OnHotKey) //�ֶ�����"	

BEGIN_MESSAGE_MAP(CMy00001Dlg, CDialog)
	//{{AFX_MSG_MAP(CMy00001Dlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON2, OnButton2)
	ON_BN_CLICKED(IDC_BUTTON3, OnButton3)
	
	ON_MESSAGE(WM_HOTKEY,OnHotKey) //�ֶ�����

	//}}AFX_MSG_MAP
END_MESSAGE_MAP()
//------------------------------------------------------------------------------------------------------------------	

//�� Ϊ�Ի���������´������

void CMy00001Dlg::OnButton2() 
{
	// TODO: Add your control notification handler code here

	ASSERT(NULL != GetSafeHwnd());
	//Register�ȼ�
	int nRet = RegisterHotKey(GetSafeHwnd(),5000,MOD_CONTROL,'D'); //�ȼ� ctrl + d
	if(!nRet)
		AfxMessageBox(_T("RegisterHotKey 0 false"));
	nRet = RegisterHotKey(GetSafeHwnd(),5001,MOD_ALT,'M'); //�ȼ� alt + m
	if(!nRet)
		AfxMessageBox(_T("RegisterHotKey 1 false"));



}

void CMy00001Dlg::OnButton3() 
{
	// TODO: Add your control notification handler code here
	int nRet = UnregisterHotKey(GetSafeHwnd(),5000); 
	if(!nRet)
		AfxMessageBox(_T("UnregisterHotKey 0 false"));
	nRet = UnregisterHotKey(GetSafeHwnd(), 5001); 
	if(!nRet)
		AfxMessageBox(_T("UnregisterHotKey 1 false"));



}

LONG CMy00001Dlg::OnHotKey(WPARAM wParam,LPARAM lParam)         
{

	UINT fuModifiers = (UINT) LOWORD(lParam);  // key-modifier flags 
	UINT uVirtKey = (UINT) HIWORD(lParam);     // virtual-key code 

	//�ж���Ӧ��ʲô�ȼ�
	if( MOD_CONTROL == fuModifiers && 'D' == uVirtKey )
	{
		AfxMessageBox(_T("�㰴������ϼ� ctrl + d"));  
	}
	else if( MOD_ALT == fuModifiers && 'M' == uVirtKey )
	{
		AfxMessageBox(_T("�㰴������ϼ� alt + m"));  
	}
	else
		AfxMessageBox(_T("�㰴����δ֪�ȼ�"));  
	                     
	return 0;        
} 
