// ShapedDialog.cpp : implementation file
//

#include "stdafx.h"
#include "CoolText.h"
#include "ShapedDialog.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CShapedDialog dialog


CShapedDialog::CShapedDialog(CWnd* pParent /*=NULL*/)
	: CDialog(CShapedDialog::IDD, pParent)
{
	//{{AFX_DATA_INIT(CShapedDialog)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT

	m_strText = "Shaped Window writed by jiftle 2010-10-09";
	
	m_color = RGB(0,0,0);
	m_transparent = 100;
	m_strFontName = _T("����");
	m_iFontSize = 24;
	m_bCenter = TRUE;

}


void CShapedDialog::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CShapedDialog)
		// NOTE: the ClassWizard will add DDX and DDV calls here
	//}}AFX_DATA_MAP
}


BEGIN_MESSAGE_MAP(CShapedDialog, CDialog)
	//{{AFX_MSG_MAP(CShapedDialog)
	ON_WM_CTLCOLOR()
	ON_WM_NCHITTEST()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CShapedDialog message handlers
BOOL CShapedDialog::OnInitDialog()
{
	Init();
		CDialog::OnInitDialog();
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CShapedDialog::SetCoolText(CString Text)
{
	if (Text.GetLength() == 0) return;

	m_strText = Text;

}




HBRUSH CShapedDialog::OnCtlColor(CDC* pDC, CWnd* pWnd, UINT nCtlColor) 
{
	HBRUSH hbr;
//	HBRUSH hbr = CDialog::OnCtlColor(pDC, pWnd, nCtlColor);
	hbr = ::CreateSolidBrush(m_color);
	return hbr;
}

UINT CShapedDialog::OnNcHitTest(CPoint point) 
{
	//return CDialog::OnNcHitTest(point);
	return HTCAPTION; //�϶�����
}


//����Ҫ����
BOOL NeedWrap(CDC*pDC,int LineWidth,const CString& str) 
{
	CString strTmp,strSub;
	CSize sz1,sz2;
	int iPos=0,iCount=0; 
	strTmp=str;
	
	if(strTmp.IsEmpty()) return FALSE;
	
	iCount=strTmp.GetLength();//�ֵĸ���
	sz1=pDC->GetTextExtent(strTmp);
	if (sz1.cx>LineWidth)
	{//һ����ʾ��������,�軻��
		iPos=iCount/2;
		strSub=strTmp.Left(iPos);
		sz2=pDC->GetTextExtent(strSub);
		return TRUE;
	}
	else
	{
		return FALSE;
	}
}


//����
int WrapPos(CDC*pDC,int LineWidth,const CString &str)
{
	CString strText=str;
	if(strText.IsEmpty()) return -1;
	
	int iNoWrapPos=-1,iWrapPos=-1; //��Ҫ���е�λ�úͲ�Ҫ���е�λ��
	BOOL bFound=FALSE; //�ҵ����е�λ��

	int iCount;
	CString strTmp;
	
	iCount=strText.GetLength();

	for (int i=1;i<iCount;i++)
	{
		if (bFound) break;

		strTmp=strText.Left(i);
		
		if (NeedWrap(pDC,LineWidth,strTmp))
		{
		
			iWrapPos = i;		
			bFound = TRUE;
		}
		else
		{
			iNoWrapPos=i;
		}
	}
	
	if (iNoWrapPos == -1)
	{
		return -1;
	}
	else
	{
		return iNoWrapPos;
	}
}


//������ʾ�ı�
void DrawTextWrap(CDC *pDC,int LineWidth,const CString &str)
{
	CString strText=str;
	if (strText.IsEmpty()) return;
	
	BOOL bShown=FALSE; //�ı������ϱ��
	CString strTmp;
	int iTop=0; //��һ�е�����߶�

	LineWidth = LineWidth - 10;
	strTmp=strText;

	while(!bShown)
	{
		int iPos=0,iCount=0;
		
		iCount=strTmp.GetLength();
		iPos=WrapPos(pDC,LineWidth,strTmp); //WrapPos ���ص����±�
		
		if (iPos == -1) 
			break;
		
		CString strOutText=strTmp.Left(iPos+1);
		pDC->TextOut(0,iTop,strOutText); //�������
		TRACE(strOutText + _T("\r\n"));

		iTop += pDC->GetTextExtent(strOutText).cy + 5; //������֮ǰ���¿�϶

		if (iPos != iCount-1)
		{
			//���������ʾ����������
			strTmp=strTmp.Mid(iPos+1,(iCount-1)-(iPos+1)+1);
		}
		else
			bShown= TRUE;
	}

}

void CShapedDialog::GenRgn(CDC *pDC, CString strTmp)
{
	//����һ������DC
	CDC dcTmp;
	dcTmp.CreateCompatibleDC(pDC);
	
	//��������
	CRgn rgn;
	
	//����
	CFont font;
	font.CreateFont(m_iFontSize,
		0,
		0,
		0, 
		0,
		FALSE,
		FALSE, 
		FALSE,
		DEFAULT_CHARSET, 
		OUT_DEFAULT_PRECIS,
		CLIP_DEFAULT_PRECIS,
		DEFAULT_QUALITY, 
		DEFAULT_PITCH,
		m_strFontName);

	//��ɫ
	COLORREF color=RGB(255,0,0);
	
	dcTmp.BeginPath(); //��ʼ·��
	
	//=================================
	//׼������
	dcTmp.SetBkMode(TRANSPARENT);
	
	dcTmp.SelectObject(font);
	dcTmp.SetTextColor(color);
//	dcTmp.TextOut(0,0,strTmp);
	
	CRect rcClient;
	GetClientRect(&rcClient);

	//���豸�������ϻ��Ƴ�����
	DrawTextWrap(&dcTmp,rcClient.Width(),strTmp); 

	dcTmp.EndPath(); //����·��
	
	rgn.CreateFromPath(&dcTmp);//����·������һ������
	
	::SetWindowRgn(this->GetSafeHwnd(),rgn,TRUE); //���ô�������
	
}



void CShapedDialog::SetTextColor(COLORREF color, short Transparent)
{
	m_color=color;
	
	if (0<=Transparent && Transparent>=100)
	{
		m_transparent=Transparent;
	}
	
}
//=============================================================
// ���ܣ����ô���͸����
// ������
//	HWND	hWnd  �������Ҫ�ı�͸���ȵĴ��壩
//	int Layer	N ͨ���ȣ��ٷֱȣ�NΪ͸����(1-100)
// ���ø�ʽ��
//	SetTransparent(this->GetSafeHwnd(),N);   // NΪ͸����(1-100)
//    
//���ȫ�־��
//	CMainFrame *pMain=(CMainFrame *)AfxGetApp()->m_pMainWnd;
//	SetTransparent(pMain->GetSafeHwnd(),90); 
//=============================================================
BOOL SetTransparent(HWND hWnd, int LayerN) 
{ 
	HMODULE hModule = GetModuleHandle("User32.DLL"); 
	if(hModule==NULL) 
	{ 
		return FALSE; 
	} 
	
	if(LayerN<0) LayerN = 0; 
	if(LayerN>100) LayerN =100; 
	
	typedef BOOL (WINAPI* SETLAYEREDWND)(HWND,COLORREF,BYTE,DWORD); 
	SETLAYEREDWND SetLayeredWindowPtr = NULL; 
	SetLayeredWindowPtr = (SETLAYEREDWND)GetProcAddress(hModule, 
		"SetLayeredWindowAttributes");    
	
	if(SetLayeredWindowPtr) 
	{   //���ô�����չ���
		LONG lStyle = GetWindowLong(hWnd,GWL_EXSTYLE)|0x00080000; 
		SetWindowLong(hWnd, GWL_EXSTYLE, lStyle); 
		
		SetLayeredWindowPtr(hWnd, 
			RGB(0,0,0), 
			BYTE((255 * LayerN)/100), 2); 
	} 
	
	return true; 
} 

void CShapedDialog::Init()
{
	int cx,cy;
	cx=cy = 0;
	
	cx = ::GetSystemMetrics(SM_CXSCREEN);
	cy = ::GetSystemMetrics(SM_CYSCREEN);
	
	MoveWindow(&m_rcClient,TRUE);
	GenRgn(GetDC(),m_strText);
	SetTransparent(this->GetSafeHwnd(),m_transparent);

	if (m_bCenter)
	{
		CenterWindow();
	}
	
}

void CShapedDialog::SetFont(const CString FontName, int size)
{
	m_strFontName = FontName;
	m_iFontSize = size;
}

void CShapedDialog::SetClientRect(int left, int top, int width, int height)
{
	m_rcClient.left = left;
	m_rcClient.top = top;
	m_rcClient.right = m_rcClient.left+width;
	m_rcClient.bottom=m_rcClient.top+height;
}

void CShapedDialog::SetCenterWindow(BOOL bEnabled)
{
	m_bCenter = bEnabled;
}
