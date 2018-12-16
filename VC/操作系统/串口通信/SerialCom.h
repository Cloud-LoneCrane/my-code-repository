#if !defined(AFX_SerialCom_H__0DCE7CC1_2426_4BDF_9AFC_410B32D9FE74__INCLUDED_)
#define AFX_SerialCom_H__0DCE7CC1_2426_4BDF_9AFC_410B32D9FE74__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// SerialCom.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CSerialCom window
//////////////////////////////////////////////////////////////////////
// SerialCom.h: implementation of the CSerialCom class.

// Written by Shibu K.V (shibukv@erdcitvm.org)
// Copyright (c) 2002
//
// To use CSerialCom, follow these steps:
//   - Copy the files SerialCom.h & SerialCom.cpp and paste it in your
//		Projects Directory.
//   - Take Project tab from your VC++ IDE,Take Add to Project->Files.Select files 
//		SerialCom.h & SerialCom.cpp and click ok
//	 -	Add the line #include "SerialCom.h" at the top of your Dialog's Header File.
//   - Create an instance of CSerialCom in your dialogs header File.Say
//		CSerialCom port;
 
// Warning: this code hasn't been subject to a heavy testing, so
// use it on your own risk. The author accepts no liability for the 
// possible damage caused by this code.
//
// Version 1.0  7 Sept 2002.

//////////////////////////////////////////////////////////////////////

class CSerialCom : public CWnd
{
// Construction
public:
	CSerialCom();

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSerialCom)
	//}}AFX_VIRTUAL

// Implementation
public:
	void ClosePort();//关闭端口
	BOOL ReadByte(BYTE &resp);//读取字节
	BOOL WriteByte(BYTE bybyte);//写入字节
	BOOL OpenPort(CString portname);//打开端口
	BOOL SetCommunicationTimeouts(DWORD ReadIntervalTimeout,//读取超时
		DWORD ReadTotalTimeoutMultiplier,
		DWORD ReadTotalTimeoutConstant,
		DWORD WriteTotalTimeoutMultiplier,
		DWORD WriteTotalTimeoutConstant);
	//配置端口
	BOOL ConfigPort(DWORD BaudRate,BYTE ByteSize,DWORD fParity,BYTE  Parity,BYTE StopBits);

	/************************************************************************/
	/* 端口信息                                                             */
	/************************************************************************/
	HANDLE hComm;//端口句柄
	DCB      m_dcb;//dcb 串口设备的设置信息
	COMMTIMEOUTS m_CommTimeouts; //com接口超时
	BOOL     m_bPortReady;//端口读取成功
	BOOL     bWriteRC;//是否可以写
	BOOL     bReadRC;
	DWORD iBytesWritten;//写入字节数
	DWORD iBytesRead;//读出字节数
	DWORD dwBytesRead;//读出字节数
	virtual ~CSerialCom();

	// Generated message map functions
protected:
	//{{AFX_MSG(CSerialCom)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SerialCom_H__0DCE7CC1_2426_4BDF_9AFC_410B32D9FE74__INCLUDED_)
