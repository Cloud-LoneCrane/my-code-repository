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
portname= "//./" +portname;//端口名字有默认的
				//前缀和http://www.vckbase.com/document/viewdoc/?id=1734上描述的不一样?

hComm = CreateFile(portname,
                      GENERIC_READ | GENERIC_WRITE,//允许读和写
                      0,//独占方式
                      0,
                      OPEN_EXISTING,
                      0,//同步方式
                      0);
		if(hComm==INVALID_HANDLE_VALUE){
		//MessageBox("Cannot open Communication Port.Please\nQuit the program and Re-start your PC.","Com Port Error",MB_OK+MB_ICONERROR);
		return false;}
		else
			return true;

}

/** 配置端口信息 */
BOOL CSerialCom::ConfigPort(DWORD BaudRate,BYTE ByteSize,DWORD fParity,BYTE  Parity,BYTE StopBits)
{
	if((m_bPortReady = GetCommState(hComm, &m_dcb))==0){//获得当前通信设备的控制信息
		MessageBox("GetCommState Error","Error",MB_OK+MB_ICONERROR);
		CloseHandle(hComm);
		return false;
	}
m_dcb.BaudRate =BaudRate;//Baud Rate
m_dcb.ByteSize = ByteSize;//Data bit,either 7 or 8
m_dcb.Parity =Parity ;//Parity,must between 0 to 4 指定奇偶校验是否允许 
m_dcb.StopBits =StopBits; //Stop bit must between 0 to 2
m_dcb.fBinary=TRUE;//Binary must be TRUE in Win32
m_dcb.fDsrSensitivity=false;
m_dcb.fParity=fParity;
m_dcb.fOutX=false;//// DSR output flow control 指定CTS是否用于检测发送控制.(数据装备好） 当为TRUE是CTS为OFF，发送将被挂起。 
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

m_bPortReady = SetCommState(hComm, &m_dcb);//设置端口控制信息
if(m_bPortReady ==0){
		//MessageBox("SetCommState Error","Error",MB_OK+MB_ICONERROR);
		CloseHandle(hComm);
	return false;}
return true;
}

/** 设置端口超时 */
BOOL CSerialCom::SetCommunicationTimeouts(DWORD ReadIntervalTimeout,
										  DWORD ReadTotalTimeoutMultiplier, 
										  DWORD ReadTotalTimeoutConstant,
										  DWORD WriteTotalTimeoutMultiplier,
										  DWORD WriteTotalTimeoutConstant)
{
	//设置通信设备的超时参数
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

/** 写入数据 */
BOOL CSerialCom::WriteByte(BYTE bybyte)
{
	iBytesWritten=0;
if(WriteFile(hComm,&bybyte,1,&iBytesWritten,NULL)==0)
return false;
else return true;
}

/** 读取数据 */
BOOL CSerialCom::ReadByte(BYTE	&resp)
{
BYTE rx;
resp=0;

DWORD dwBytesTransferred=0;

if (ReadFile (hComm,//串口句柄
	&rx,//缓冲区
	1,//缓冲区的字节数
	&dwBytesTransferred,//读到的字节数
	0
	))
{
			 if (dwBytesTransferred == 1){
				 resp=rx;
				 return true;}}
			  
	return false;
}

/** 关闭端口 */
void CSerialCom::ClosePort()
{
CloseHandle(hComm);
return;
}
