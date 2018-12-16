使用CSerialCom class可以进行串口通信;
使用方法:
 1 在工程.h文件中,添加#include "SerialCom.h"
 2 在适当的位置添加函数声明 UINT ComPortThread()
 3 在适当的时候,调用AfxBeginThread()启动串口通信线程
 
备注:
----------------------------------------------------
	串口数据例子:
		2 48 48 48 48 51 49 57 49 56 49 13 10 3 
		开始位	数据位	回车换行	停止位
		2	...	13 10	3
----------------------------------------------------
//
AfxBeginThread(ComPortThread, (PVOID)this);

/** com端口线程 */
UINT ComPortThread(PVOID r)
{
#define	BUF_LEN		256 //缓冲区长度
#define START_BYTE	0x02 //开始字节
#define END_BYTE	0x03 //结束字节
#define NUM_ENDING	"\r\n" 
	
	CBarclientDlg*	dlg = (CBarclientDlg*)r;
	CSerialCom		sc;
	BYTE			buf[BUF_LEN] = { 0 };//声明字节缓冲区
	int				nIndex = 0;
	PCHAR			pNumStr, pEndNumStr;
	
	if(sc.OpenPort("com1")) //打开端口,配置端口
	{
		if(sc.ConfigPort(9600, 8, 0, NOPARITY, ONESTOPBIT) && 
			sc.SetCommunicationTimeouts(0, 100, 0, 0, 0)) //
		{
			nIndex = 0;
			while(dlg->m_bComPortRunning) //串口是否运行
			{
				if(sc.ReadByte(buf[nIndex]))
				{
					TRACE("%d\r\n", buf[nIndex]);
					switch(buf[nIndex])
					{
					case START_BYTE:	//start byte
						nIndex = 0;
						break;
					case END_BYTE:		//end byte
						pNumStr = (PCHAR)buf;//字节数组直接转换为字符串
						pEndNumStr = strstr(pNumStr, NUM_ENDING);//在已经读取到的数据中查找回车换行
						if(pEndNumStr)
						{//如果找到\r\n
							pEndNumStr[0] = 0; //指向子字符串首次出现的位置
							
                            CString strComBuf=pNumStr;//获取读取到的字符串
                            
							//must be sendmessge!!! 必须发送Message而不是PostMessage
						//	dlg->SendMessage(WM_COMPORT_NUM, (WPARAM)pNumStr, 0);
						}
						
						memset(buf, 0, BUF_LEN);
						break;
					default:
						nIndex++;
						if(nIndex >= BUF_LEN)
							nIndex = 0;
						break;
					}					
				}
			}
		}
		
		sc.ClosePort();//关闭端口
	}
	
	return 0;
}
