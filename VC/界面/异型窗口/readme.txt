使用该异型窗口类,需要:
1 建立对话框资源IDD_SHARPED_DLG,style只保留客户区
2 在.h文件中添加对主程序.h头文件的调用,目的是包含一个资源的定义IDD_SHAEPED_DLG
2 使用如下方式调用即可

void CBkgndDlg::OnButton1() 
{
//显示第一段文字
	CShapedDialog *pDlg = new CShapedDialog(this);
	
	pDlg->SetTextColor(RGB(255,0,0),0);
	pDlg->SetCoolText(_T("Gossip Girl by jiftle"));
	pDlg->SetFont("楷体_GB2312",50);
	pDlg->SetClientRect(100,100,300,300);
	pDlg->SetCenterWindow(FALSE);
	pDlg->Create(IDD_SHARPED_DLG,this);
	pDlg->ShowWindow(SW_SHOW);
	
//显示第二段文字
	CShapedDialog *pDlg1 = new CShapedDialog(this);
	
	pDlg1->SetTextColor(RGB(0,255,0),0);
	pDlg1->SetCoolText(_T("Gossip Girl by jiftle"));
	pDlg1->SetFont("楷体_GB2312",50);
	pDlg1->SetClientRect(100,400,300,300);
	pDlg1->SetCenterWindow(FALSE);
	pDlg1->Create(IDD_SHARPED_DLG,this);
	pDlg1->ShowWindow(SW_SHOW);	

}
