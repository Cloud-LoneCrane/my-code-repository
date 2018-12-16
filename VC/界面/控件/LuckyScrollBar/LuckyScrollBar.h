

#if !defined(AFX_MYSCROLLBAR_H__0E01FD4D_3308_4A36_B758_67F6843274E0__INCLUDED_)
#define AFX_MYSCROLLBAR_H__0E01FD4D_3308_4A36_B758_67F6843274E0__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// LuckyScrollBar.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CLuckyScrollBar window

class CLuckyScrollBar : public CScrollBar
{
// Construction
public:
	CLuckyScrollBar();

// Attributes
public:
	COLORREF   rgb;
	int CtrlTipID;
protected:
	bool bMouseDown;
	CBrush *m_pBrush;

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CLuckyScrollBar)
	//}}AFX_VIRTUAL

// Implementation
public:
	void SetColor(COLORREF color);
	virtual ~CLuckyScrollBar();

	// Generated message map functions
protected:
	//{{AFX_MSG(CLuckyScrollBar)
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
	afx_msg HBRUSH CtlColor(CDC* pDC, UINT nCtlColor);
	//}}AFX_MSG

	DECLARE_MESSAGE_MAP()
	
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MYSCROLLBAR_H__0E01FD4D_3308_4A36_B758_67F6843274E0__INCLUDED_)