///////////////////////////////////////////////////////////////
// �Զ��幤����.cpp
///////////////////////////////////////////////////////////////

	//����ͼ���б�,��ͼ���б������ͼ��
	m_Imagelist.Create(32,32,ILC_COLOR24|ILC_MASK,0,1);
	for (int i=0;i<9;i++)
	{
		m_Imagelist.Add(AfxGetApp()->LoadIcon(IDI_ICON1+i));
	}

	//����������
	if (!m_wndToolBar.CreateEx(this, TBSTYLE_FLAT, WS_CHILD | WS_VISIBLE | CBRS_TOP
		| CBRS_GRIPPER | CBRS_TOOLTIPS | CBRS_FLYBY | CBRS_SIZE_DYNAMIC) ||
		!m_wndToolBar.LoadToolBar(IDR_MAINFRAME))
	{
		TRACE0("Failed to create toolbar\n");
		return -1;      // fail to create
	}

	m_wndToolBar.GetToolBarCtrl().SetImageList(&m_Imagelist);
	m_wndToolBar.GetToolBarCtrl().SetButtonSize(CSize(40,40));
	m_wndToolBar.GetToolBarCtrl().SetBitmapSize(CSize(30,30));