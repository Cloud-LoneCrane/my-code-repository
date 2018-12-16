/////////////////////////////////////
// 设置热键
/////////////////////////////////////

/* *************************************************************
*   不能通过向导添加WM_HOTKEY消息,因为里面没有.只能手工
* 添加.
***************************************************************/

//一 在以下位置加入 "afx_msg LONG OnHotKey(WPARAM wParam,LPARAM lParam);  //手工添加"

	// Generated message map functions
	//{{AFX_MSG(CMy00001Dlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnButton2();
	afx_msg void OnButton3();
	//}}AFX_MSG

	afx_msg LONG OnHotKey(WPARAM wParam,LPARAM lParam);  //手工添加
	DECLARE_MESSAGE_MAP()
//------------------------------------------------------------------------------------------------------------------	

//二 在以下位置加入 "ON_MESSAGE(WM_HOTKEY,OnHotKey) //手动加入"	

BEGIN_MESSAGE_MAP(CMy00001Dlg, CDialog)
	//{{AFX_MSG_MAP(CMy00001Dlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON2, OnButton2)
	ON_BN_CLICKED(IDC_BUTTON3, OnButton3)
	
	ON_MESSAGE(WM_HOTKEY,OnHotKey) //手动加入

	//}}AFX_MSG_MAP
END_MESSAGE_MAP()
//------------------------------------------------------------------------------------------------------------------	

//三 为对话框添加如下处理程序

void CMy00001Dlg::OnButton2() 
{
	// TODO: Add your control notification handler code here

	ASSERT(NULL != GetSafeHwnd());
	//Register热键
	int nRet = RegisterHotKey(GetSafeHwnd(),5000,MOD_CONTROL,'D'); //热键 ctrl + d
	if(!nRet)
		AfxMessageBox(_T("RegisterHotKey 0 false"));
	nRet = RegisterHotKey(GetSafeHwnd(),5001,MOD_ALT,'M'); //热键 alt + m
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

	//判断响应了什么热键
	if( MOD_CONTROL == fuModifiers && 'D' == uVirtKey )
	{
		AfxMessageBox(_T("你按下了组合键 ctrl + d"));  
	}
	else if( MOD_ALT == fuModifiers && 'M' == uVirtKey )
	{
		AfxMessageBox(_T("你按下了组合键 alt + m"));  
	}
	else
		AfxMessageBox(_T("你按下了未知热键"));  
	                     
	return 0;        
} 
