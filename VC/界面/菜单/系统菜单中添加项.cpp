/************************************************************************
��ϵͳ�˵�����Ӳ˵����������
һ ���Ȼ��ϵͳ�˵���ָ�룻
�� ����CMenu�ķ����������˵��
�� ��OnSysCommand(UINT nID, LPARAM lParam)�д���˵������Ӧ��
************************************************************************/

#define IDM_TEST 0x1000 //����˵����ID,0xFFF0

//��ò˵�ָ�벢������
CMenu* pSysMenu = GetSystemMenu(FALSE);
pSysMenu->AppendMenu(MF_STRING,IDM_TEST,_T("����"));

//����˵���Ӧ
void OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else if((nID & 0xFFF0)==IDM_TEST)
	{
		MessageBox.Show(_T("����"));
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}