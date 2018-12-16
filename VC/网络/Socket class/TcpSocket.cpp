// TcpSocket.cpp : implementation file
//

#include "stdafx.h"
#include "TcpSocket.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CTcpSocket



//DataArrival OnDataRecive;


CTcpSocket::CTcpSocket()
{
//	m_pClient=NULL;

	//----------------------------------
	//��ʼ���ص�����
	pConnect=NULL;
	pDataArrival=NULL;
	pClose=NULL;

}

CTcpSocket::~CTcpSocket()
{
}


// Do not edit the following lines, which are needed by ClassWizard.
#if 0
BEGIN_MESSAGE_MAP(CTcpSocket, CAsyncSocket)
	//{{AFX_MSG_MAP(CTcpSocket)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()
#endif	// 0

/////////////////////////////////////////////////////////////////////////////
// CTcpSocket member functions


/************************************************************************/
/* ֪ͨ��Ϣ����                                                         */
/************************************************************************/
void CTcpSocket::OnAccept(int nErrorCode) 
{
	/*
	m_pClient=new CAsyncSocket;
	this->Accept(*m_pClient);

	m_pClient->GetPeerName(m_RemoteHost,m_RemotePort);
*/	

	CAsyncSocket::OnAccept(nErrorCode);
}

void CTcpSocket::OnClose(int nErrorCode) 
{
	if (!nErrorCode)
	{
		Close();
	}

	m_bConnected = FALSE;
	
	if(pClose!=NULL) pClose();

	CAsyncSocket::OnClose(nErrorCode);
}

void CTcpSocket::OnConnect(int nErrorCode) 
{
	/*
	//-------------------------------------------
	//��������д��ʽ
	if (!nErrorCode)
	{
		 m_bConnected=TRUE;
	}
	else
	{
		m_bConnected=FALSE;
	}
*/
	//--------------------------------------------
	//�򵥵���д��ʽ
	m_bConnected=(0==nErrorCode);

	if (NULL != pConnect) 
		pConnect();

	CAsyncSocket::OnConnect(nErrorCode);
}


void CTcpSocket::OnReceive(int nErrorCode) 
{
	CString strIP;
	unsigned int iPort;

	char buff[4096]={0};
	int nRead;
	nRead = Receive(buff, 4096); 
	
	switch(nRead)
	{
		case 0:
		case SOCKET_ERROR:
			Close();
			break;
		default:
			GetPeerName(strIP,iPort);
			m_RemoteHost = strIP;
			m_RemotePort = iPort;

			GetSockName(m_LocalIP,m_LocalPort);

			CString strTmp = buff;

			TRACE(strTmp);
			break;
		}

		//���������յ��¼�
	if (pDataArrival != NULL)
		pDataArrival(buff,strlen(buff));

		CAsyncSocket::OnReceive(nErrorCode);
}



void CTcpSocket::OnSend(int nErrorCode) 
{
	
	CAsyncSocket::OnSend(nErrorCode);
}

/************************************************************************/
/* ����                                                                 */
/************************************************************************/
/** �Ƿ�ر� */
BOOL CTcpSocket::IsConnected()
{
	return m_bConnected;
}