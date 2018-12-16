// SerialCom.cpp : implementation file
//

#include "stdafx.h"
#include "SerialCom.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif


/////////////////////////////////////////////////////////////////////////////
// SerialCom.cpp: implementation of the CSerialCom class.

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
// http://www.codeproject.com/KB/system/cserialcom.aspx
//////////////////////////////////////////////////////////////////////

// CSerialCom

CSerialCom::CSerialCom()
{
}

CSerialCom::~CSerialCom()
{
}


BEGIN_MESSAGE_MAP(CSerialCom, CWnd)
	//{{AFX_MSG_MAP(CSerialCom)
		// NOTE - the ClassWizard will add and remove mapping macros here.
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()


/////////////////////////////////////////////////////////////////////////////
// CSerialCom message handlers

BOOL CSerialCom::OpenPort(CString portname)
{
portname= "//./" +portname;//�˿�������Ĭ�ϵ�
				//ǰ׺��http://www.vckbase.com/document/viewdoc/?id=1734�������Ĳ�һ��?

hComm = CreateFile(portname,
                      GENERIC_READ | GENERIC_WRITE,//�������д
                      0,//��ռ��ʽ
                      0,
                      OPEN_EXISTING,
                      0,//ͬ����ʽ
                      0);
		if(hComm==INVALID_HANDLE_VALUE){
		//MessageBox("Cannot open Communication Port.Please\nQuit the program and Re-start your PC.","Com Port Error",MB_OK+MB_ICONERROR);
		return false;}
		else
			return true;

}

/** ���ö˿���Ϣ */
BOOL CSerialCom::ConfigPort(DWORD BaudRate,BYTE ByteSize,DWORD fParity,BYTE  Parity,BYTE StopBits)
{
	if((m_bPortReady = GetCommState(hComm, &m_dcb))==0){//��õ�ǰͨ���豸�Ŀ�����Ϣ
		MessageBox("GetCommState Error","Error",MB_OK+MB_ICONERROR);
		CloseHandle(hComm);
		return false;
	}
m_dcb.BaudRate =BaudRate;//Baud Rate
m_dcb.ByteSize = ByteSize;//Data bit,either 7 or 8
m_dcb.Parity =Parity ;//Parity,must between 0 to 4 ָ����żУ���Ƿ����� 
m_dcb.StopBits =StopBits; //Stop bit must between 0 to 2
m_dcb.fBinary=TRUE;//Binary must be TRUE in Win32
m_dcb.fDsrSensitivity=false;
m_dcb.fParity=fParity;
m_dcb.fOutX=false;//// DSR output flow control ָ��CTS�Ƿ����ڼ�ⷢ�Ϳ���.(����װ���ã� ��ΪTRUE��CTSΪOFF�����ͽ������� 
m_dcb.fInX=false;
m_dcb.fNull=false;
m_dcb.fAbortOnError=TRUE;
m_dcb.fOutxCtsFlow=FALSE;
m_dcb.fOutxDsrFlow=false;
m_dcb.fDtrControl=DTR_CONTROL_DISABLE;
m_dcb.fDsrSensitivity=false;
m_dcb.fRtsControl=RTS_CONTROL_DISABLE;
m_dcb.fOutxCtsFlow=false;
m_dcb.fOutxCtsFlow=false;

m_bPortReady = SetCommState(hComm, &m_dcb);//���ö˿ڿ�����Ϣ
if(m_bPortReady ==0){
		//MessageBox("SetCommState Error","Error",MB_OK+MB_ICONERROR);
		CloseHandle(hComm);
	return false;}
return true;
}

/** ���ö˿ڳ�ʱ */
BOOL CSerialCom::SetCommunicationTimeouts(DWORD ReadIntervalTimeout,
										  DWORD ReadTotalTimeoutMultiplier, 
										  DWORD ReadTotalTimeoutConstant,
										  DWORD WriteTotalTimeoutMultiplier,
										  DWORD WriteTotalTimeoutConstant)
{
	//����ͨ���豸�ĳ�ʱ����
if((m_bPortReady = GetCommTimeouts (hComm, &m_CommTimeouts))==0)
   return false;
m_CommTimeouts.ReadIntervalTimeout =ReadIntervalTimeout;
m_CommTimeouts.ReadTotalTimeoutConstant =ReadTotalTimeoutConstant;
m_CommTimeouts.ReadTotalTimeoutMultiplier =ReadTotalTimeoutMultiplier;
m_CommTimeouts.WriteTotalTimeoutConstant = WriteTotalTimeoutConstant;
m_CommTimeouts.WriteTotalTimeoutMultiplier =WriteTotalTimeoutMultiplier;
		m_bPortReady = SetCommTimeouts (hComm, &m_CommTimeouts);
		if(m_bPortReady ==0){
//MessageBox("StCommTimeouts function failed","Com Port Error",MB_OK+MB_ICONERROR);
		CloseHandle(hComm);
		return false;}
		return true;
}

/** д������ */
BOOL CSerialCom::WriteByte(BYTE bybyte)
{
	iBytesWritten=0;
if(WriteFile(hComm,&bybyte,1,&iBytesWritten,NULL)==0)
return false;
else return true;
}

/** ��ȡ���� */
BOOL CSerialCom::ReadByte(BYTE	&resp)
{
BYTE rx;
resp=0;

DWORD dwBytesTransferred=0;

if (ReadFile (hComm,//���ھ��
	&rx,//������
	1,//���������ֽ���
	&dwBytesTransferred,//�������ֽ���
	0
	))
{
			 if (dwBytesTransferred == 1){
				 resp=rx;
				 return true;}}
			  
	return false;
}

/** �رն˿� */
void CSerialCom::ClosePort()
{
CloseHandle(hComm);
return;
}
