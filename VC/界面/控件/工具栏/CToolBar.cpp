/************************************************************************/
/* 创建带图片的工具栏                                                   */
/************************************************************************/
/************************************************************
一 定义CToolBar的对象，使用Create或CreateEx创建;
二 定义CImageList对象，设置图片；
三 定义CToolBar的按钮索引和ID，设置按钮的图片；
四 调整界面布局显示工具栏；
五 添加工具条的响应函数。
************************************************************/
//定义对象
CToolBar m_wndToolBar;
CImageList m_imgList;

#define IDB_001 101 //按钮的ID

//设置图像和按钮
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
m_wndToolBar.SetButtonText(0,_T("按钮一"));
m_wndToolBar.SetButtonText(1,_T("按钮二"));
m_wndToolBar.SetButtonText(2,_T("按钮三"));
m_wndToolBar.SetButtonText(3,_T("按钮四"));
m_wndToolBar.SetButtonText(4,_T("按钮五"));
		
//m_wndToolBar.SetButtonInfo(0,IDB_001,)

m_wndToolBar.GetToolBarCtrl().SetButtonWidth(50,70);

m_wndToolBar.SetSizes(CSize(70,50),CSize(28,28));

//调整界面布局，让AFX_IDW_CONTROLBAR_FIRST和AFX_IDW_CONTROLBAR_LAST的
//控件不被控制条挤占位置
RepositionBars(AFX_IDW_CONTROLBAR_FIRST,AFX_IDW_CONTROLBAR_LAST,0);

//============================================================
	// Generated message map functions
	//{{AFX_MSG(CTBarDlg)
	virtual BOOL OnInitDialog();	
		afx_msg void OnToolBarHandler_IDB_001(); //添加处理函数
		afx_msg void OnToolBarHandler_IDB_002();
		//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
	
//建立按钮和响应函数的关联
BEGIN_MESSAGE_MAP(CTBarDlg, CDialog)
//{{AFX_MSG_MAP(CTBarDlg)
ON_WM_SYSCOMMAND()
ON_WM_PAINT()
ON_WM_QUERYDRAGICON()
ON_COMMAND(IDB_001, OnToolBarHandler_IDB_001) //添加关联 这里只能是一一对应的关系
ON_COMMAND(IDB_001, OnToolBarHandler_IDB_001+1) 
//}}AFX_MSG_MAP
END_MESSAGE_MAP()


//.cpp中定义
void OnToolBarHandler_IDB_001() 
{
	// TODO: Add your command handler code here
	AfxMessageBox("按钮响应");
}

void OnToolBarHandler_IDB_002() 
{
	// TODO: Add your command handler code here
	AfxMessageBox("按钮响应");
}