/************************************************************************/
/* ������ͼƬ�Ĺ�����                                                   */
/************************************************************************/
/************************************************************
һ ����CToolBar�Ķ���ʹ��Create��CreateEx����;
�� ����CImageList��������ͼƬ��
�� ����CToolBar�İ�ť������ID�����ð�ť��ͼƬ��
�� �������沼����ʾ��������
�� ��ӹ���������Ӧ������
************************************************************/
//�������
CToolBar m_wndToolBar;
CImageList m_imgList;

#define IDB_001 101 //��ť��ID

//����ͼ��Ͱ�ť
m_imgList.Create(32,32,ILC_COLOR32|ILC_MASK,1,1);
m_imgList.Add(AfxGetApp()->LoadIcon(IDR_MAINFRAME));
m_imgList.Add(AfxGetApp()->LoadIcon(IDR_MAINFRAME));
m_imgList.Add(AfxGetApp()->LoadIcon(IDR_MAINFRAME));
m_imgList.Add(AfxGetApp()->LoadIcon(IDR_MAINFRAME));
m_imgList.Add(AfxGetApp()->LoadIcon(IDR_MAINFRAME));

UINT Array[5];
for (int i=0;i<5;i++)
{
	Array[i]=i+IDB_001;
}

m_wndToolBar.Create(this,WS_CHILD|WS_VISIBLE|CBRS_TOP,AFX_IDW_TOOLBAR);
m_wndToolBar.SetButtons(Array,5);
	
m_wndToolBar.GetToolBarCtrl().SetImageList(&m_imgList);
m_wndToolBar.SetButtonText(0,_T("��ťһ"));
m_wndToolBar.SetButtonText(1,_T("��ť��"));
m_wndToolBar.SetButtonText(2,_T("��ť��"));
m_wndToolBar.SetButtonText(3,_T("��ť��"));
m_wndToolBar.SetButtonText(4,_T("��ť��"));
		
//m_wndToolBar.SetButtonInfo(0,IDB_001,)

m_wndToolBar.GetToolBarCtrl().SetButtonWidth(50,70);

m_wndToolBar.SetSizes(CSize(70,50),CSize(28,28));

//�������沼�֣���AFX_IDW_CONTROLBAR_FIRST��AFX_IDW_CONTROLBAR_LAST��
//�ؼ�������������ռλ��
RepositionBars(AFX_IDW_CONTROLBAR_FIRST,AFX_IDW_CONTROLBAR_LAST,0);

//============================================================
	// Generated message map functions
	//{{AFX_MSG(CTBarDlg)
	virtual BOOL OnInitDialog();	
		afx_msg void OnToolBarHandler_IDB_001(); //��Ӵ�����
		afx_msg void OnToolBarHandler_IDB_002();
		//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
	
//������ť����Ӧ�����Ĺ���
BEGIN_MESSAGE_MAP(CTBarDlg, CDialog)
//{{AFX_MSG_MAP(CTBarDlg)
ON_WM_SYSCOMMAND()
ON_WM_PAINT()
ON_WM_QUERYDRAGICON()
ON_COMMAND(IDB_001, OnToolBarHandler_IDB_001) //��ӹ��� ����ֻ����һһ��Ӧ�Ĺ�ϵ
ON_COMMAND(IDB_001, OnToolBarHandler_IDB_001+1) 
//}}AFX_MSG_MAP
END_MESSAGE_MAP()


//.cpp�ж���
void OnToolBarHandler_IDB_001() 
{
	// TODO: Add your command handler code here
	AfxMessageBox("��ť��Ӧ");
}

void OnToolBarHandler_IDB_002() 
{
	// TODO: Add your command handler code here
	AfxMessageBox("��ť��Ӧ");
}