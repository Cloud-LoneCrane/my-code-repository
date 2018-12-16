ʹ��CSerialCom class���Խ��д���ͨ��;
ʹ�÷���:
 1 �ڹ���.h�ļ���,���#include "SerialCom.h"
 2 ���ʵ���λ����Ӻ������� UINT ComPortThread()
 3 ���ʵ���ʱ��,����AfxBeginThread()��������ͨ���߳�
 
��ע:
----------------------------------------------------
	������������:
		2 48 48 48 48 51 49 57 49 56 49 13 10 3 
		��ʼλ	����λ	�س�����	ֹͣλ
		2	...	13 10	3
----------------------------------------------------
//
AfxBeginThread(ComPortThread, (PVOID)this);

/** com�˿��߳� */
UINT ComPortThread(PVOID r)
{
#define	BUF_LEN		256 //����������
#define START_BYTE	0x02 //��ʼ�ֽ�
#define END_BYTE	0x03 //�����ֽ�
#define NUM_ENDING	"\r\n" 
	
	CBarclientDlg*	dlg = (CBarclientDlg*)r;
	CSerialCom		sc;
	BYTE			buf[BUF_LEN] = { 0 };//�����ֽڻ�����
	int				nIndex = 0;
	PCHAR			pNumStr, pEndNumStr;
	
	if(sc.OpenPort("com1")) //�򿪶˿�,���ö˿�
	{
		if(sc.ConfigPort(9600, 8, 0, NOPARITY, ONESTOPBIT) && 
			sc.SetCommunicationTimeouts(0, 100, 0, 0, 0)) //
		{
			nIndex = 0;
			while(dlg->m_bComPortRunning) //�����Ƿ�����
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
						pNumStr = (PCHAR)buf;//�ֽ�����ֱ��ת��Ϊ�ַ���
						pEndNumStr = strstr(pNumStr, NUM_ENDING);//���Ѿ���ȡ���������в��һس�����
						if(pEndNumStr)
						{//����ҵ�\r\n
							pEndNumStr[0] = 0; //ָ�����ַ����״γ��ֵ�λ��
							
                            CString strComBuf=pNumStr;//��ȡ��ȡ�����ַ���
                            
							//must be sendmessge!!! ���뷢��Message������PostMessage
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
		
		sc.ClosePort();//�رն˿�
	}
	
	return 0;
}
