#if !defined(AFX_SHAPEDDIALOG_H__0AE22604_1F3C_459C_8B6E_FF1BB540B325__INCLUDED_)
#define AFX_SHAPEDDIALOG_H__0AE22604_1F3C_459C_8B6E_FF1BB540B325__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// ShapedDialog.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CShapedDialog dialog

class CShapedDialog : public CDialog
{
// Construction
public:
	CShapedDialog(CWnd* pParent = NULL);   // standard constructor

// Dialog Data
	//{{AFX_DATA(CShapedDialog)
	enum { IDD = IDD_SHARPED_DLG };
		// NOTE: the ClassWizard will add data members here
	//}}AFX_DATA


// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CShapedDialog)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:

	// Generated message map functions
	//{{AFX_MSG(CShapedDialog)
	virtual BOOL OnInitDialog();
	afx_msg HBRUSH OnCtlColor(CDC* pDC, CWnd* pWnd, UINT nCtlColor);
	afx_msg UINT OnNcHitTest(CPoint point);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

private: 
	void Init();
	CString m_strText;
	COLORREF m_color; //文字颜色
	short m_transparent; //透明度
	CString m_strFontName; //字体名
	int m_iFontSize ;//文字大小 
	RECT m_rcClient; //客户区
	BOOL m_bCenter;

	//functions
	void GenRgn(CDC *pDC, CString strTmp);

public:
	void SetCenterWindow(BOOL bEnabled);
	void SetClientRect(int left,int top,int width,int height);
	void SetFont(const CString FontName,int size);
	void SetTextColor(COLORREF color,short Transparent);
	void SetCoolText(CString Text);
	
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SHAPEDDIALOG_H__0AE22604_1F3C_459C_8B6E_FF1BB540B325__INCLUDED_)
