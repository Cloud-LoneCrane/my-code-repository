// MyScrollBar.cpp : implementation file
//// 

#include "stdafx.h"
#include "LuckyScrollBar.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CLuckyScrollBar

CLuckyScrollBar::CLuckyScrollBar()
{
	bMouseDown= false;
	m_pBrush = new CBrush;

	if(3435973836 == rgb)
		rgb = RGB(255,221,238);
	m_pBrush->CreateSolidBrush(rgb);
	
}

CLuckyScrollBar::~CLuckyScrollBar()
{
	bMouseDown= false;
}


BEGIN_MESSAGE_MAP(CLuckyScrollBar, CScrollBar)
	//{{AFX_MSG_MAP(CLuckyScrollBar)
	ON_WM_LBUTTONDOWN()
	ON_WM_LBUTTONUP()
	ON_WM_MOUSEMOVE()
	ON_WM_CTLCOLOR_REFLECT()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CLuckyScrollBar message handlers

void CLuckyScrollBar::OnLButtonDown(UINT nFlags, CPoint point) 
{
	// TODO: Add your message handler code here and/or call default
	bMouseDown= true;
	SetCapture();
	
	//CScrollBar::OnLButtonDown(nFlags, point);
}

void CLuckyScrollBar::OnLButtonUp(UINT nFlags, CPoint point) 
{
	// TODO: Add your message handler code here and/or call default
	bMouseDown= false;
	ReleaseCapture();
	//CScrollBar::OnLButtonUp(nFlags, point);
}

void CLuckyScrollBar::OnMouseMove(UINT nFlags, CPoint point) 
{
	// TODO: Add your message handler code here and/or call default
	if(bMouseDown)
	{
		POINT pos;
		BOOL bFlag=FALSE;

		if(!(bFlag=GetCursorPos(&pos)))
			return;
	
			double iWidth,iPos;
			RECT rect;

			GetWindowRect(&rect);

			iWidth = rect.right - rect.left;

			ScreenToClient(&pos);

			iPos = pos.x;

			double fPercent;
			fPercent = (iPos*100)/(iWidth*100);
	
			int iPrt=int(fPercent * 100);
			if(iPrt<0)
				iPrt=0;
			else if(iPrt>100)
				iPrt=100;


			SetScrollRange(0,100);
			SetScrollPos(iPrt,TRUE);

			CString strTmp;
			strTmp.Format("%d",iPrt);
			strTmp += "%";

			CWnd *pWnd = NULL;
			pWnd = GetParent()->GetDlgItem(CtrlTipID);
			if((!pWnd==NULL))
				pWnd->SetWindowText(strTmp);

	}
	
//	CScrollBar::OnMouseMove(nFlags, point);
}

HBRUSH CLuckyScrollBar::CtlColor(CDC* pDC, UINT nCtlColor) 
{
	return *m_pBrush;

	// TODO: Return a non-NULL brush if the parent's handler should not be called
	//	return NULL;
}

void CLuckyScrollBar::SetColor(COLORREF color)
{
	m_pBrush->DeleteObject();
	m_pBrush->CreateSolidBrush(color);
	Invalidate(); // ÖØ»æ
	
}
