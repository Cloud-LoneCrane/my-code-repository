ʹ�ø����ʹ�����,��Ҫ:
1 �����Ի�����ԴIDD_SHARPED_DLG,styleֻ�����ͻ���
2 ��.h�ļ�����Ӷ�������.hͷ�ļ��ĵ���,Ŀ���ǰ���һ����Դ�Ķ���IDD_SHAEPED_DLG
2 ʹ�����·�ʽ���ü���

void CBkgndDlg::OnButton1() 
{
//��ʾ��һ������
	CShapedDialog *pDlg = new CShapedDialog(this);
	
	pDlg->SetTextColor(RGB(255,0,0),0);
	pDlg->SetCoolText(_T("Gossip Girl by jiftle"));
	pDlg->SetFont("����_GB2312",50);
	pDlg->SetClientRect(100,100,300,300);
	pDlg->SetCenterWindow(FALSE);
	pDlg->Create(IDD_SHARPED_DLG,this);
	pDlg->ShowWindow(SW_SHOW);
	
//��ʾ�ڶ�������
	CShapedDialog *pDlg1 = new CShapedDialog(this);
	
	pDlg1->SetTextColor(RGB(0,255,0),0);
	pDlg1->SetCoolText(_T("Gossip Girl by jiftle"));
	pDlg1->SetFont("����_GB2312",50);
	pDlg1->SetClientRect(100,400,300,300);
	pDlg1->SetCenterWindow(FALSE);
	pDlg1->Create(IDD_SHARPED_DLG,this);
	pDlg1->ShowWindow(SW_SHOW);	

}
