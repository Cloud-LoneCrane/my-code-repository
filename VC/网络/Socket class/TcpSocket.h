#if !defined(AFX_TCPSOCKET_H__50AD0500_3C9C_4E34_BB43_9F2EC170D5BD__INCLUDED_)
#define AFX_TCPSOCKET_H__50AD0500_3C9C_4E34_BB43_9F2EC170D5BD__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// TcpSocket.h : header file
//

/////////////////////////////////////////////////////////////////////////////
// CTcpSocket command target

class CTcpSocket : public CAsyncSocket
{
// Attributes
public:
	BOOL IsConnected();
	
// Operations
public:
	CTcpSocket();
	virtual ~CTcpSocket();
	
//Events
public:
	void (*pConnect)();
	void (*pDataArrival)(char* buffer,int maxlen); //函数指针
	void (*pClose)();

// Overrides
public:
	BOOL m_bConnected;
	CString m_RemoteHost; //远程主机
	unsigned int m_RemotePort;//远程端口
	CString m_LocalIP;//本地ip
	unsigned int m_LocalPort; //本地端口

//	CAsyncSocket* m_pClient;

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CTcpSocket)
	public:
	virtual void OnAccept(int nErrorCode);
	virtual void OnClose(int nErrorCode);
	virtual void OnConnect(int nErrorCode);
	virtual void OnReceive(int nErrorCode);
	virtual void OnSend(int nErrorCode);
	//}}AFX_VIRTUAL

	// Generated message map functions
	//{{AFX_MSG(CTcpSocket)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_MSG

// Implementation
protected:
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_TCPSOCKET_H__50AD0500_3C9C_4E34_BB43_9F2EC170D5BD__INCLUDED_)
