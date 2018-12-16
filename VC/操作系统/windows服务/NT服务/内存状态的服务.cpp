////////////////////////////////////////////////////

#include <windows.h>
#include <stdio.h>

#define SLEEP_TIME	5000
#define LOGFILE	"C:\\memstatus.txt" //��־�ļ���

SERVICE_STATUS ServiceStatus; //����״̬
SERVICE_STATUS_HANDLE	hStatus; //������

//�����������
void ServiceMain(int argc, char** argv); 
//����Ŀ��ƺ�����ControlHandler �������� SCM �������� 
void ControlHandler(DWORD request); 

int InitService();

int WriteToLog(char* str)
{
	FILE* log;
	log = fopen(LOGFILE, "a+");
	if (log == NULL)
		return -1;
	fprintf(log, "%s\n", str);
	fclose(log);
	return 0;

}

void main()
{
	//������ڱ���������ֺ���ں�����
	SERVICE_TABLE_ENTRY ServiceTable[2];
	ServiceTable[0].lpServiceName = "MemoryStatus";
	ServiceTable[0].lpServiceProc = (LPSERVICE_MAIN_FUNCTION)ServiceMain;

	//NULL��ʾ��β
	ServiceTable[1].lpServiceName = NULL;
	ServiceTable[1].lpServiceProc = NULL;

	// ��������Ŀ��Ʒ��ɻ��߳�
	StartServiceCtrlDispatcher(ServiceTable); 


}

// �����ʼ��
int InitService() 
{ 
	int result;
	result = WriteToLog("Monitoring started.");
	return(result); 
}

// Control Handler
void ControlHandler(DWORD request) 
{ 
	switch(request) 
	{ 
	case SERVICE_CONTROL_STOP: 
		OutputDebugString("Monitoring stopped.");
		WriteToLog("Monitoring stopped.");

		ServiceStatus.dwWin32ExitCode = 0; 
		ServiceStatus.dwCurrentState = SERVICE_STOPPED; 
		SetServiceStatus (hStatus, &ServiceStatus);
		return; 

	case SERVICE_CONTROL_SHUTDOWN: 
		OutputDebugString("Monitoring stopped.");
		WriteToLog("Monitoring stopped.");

		ServiceStatus.dwWin32ExitCode = 0; 
		ServiceStatus.dwCurrentState = SERVICE_STOPPED; 
		SetServiceStatus (hStatus, &ServiceStatus);
		return; 

	default:
		break;
	} 

	// Report current status
	SetServiceStatus (hStatus, &ServiceStatus);

	return; 
}

void ServiceMain(int argc, char** argv) 
{ 
	int error; 

	//�����״̬��Ϣ
	ServiceStatus.dwServiceType =	SERVICE_WIN32; //����
	ServiceStatus.dwCurrentState = 	SERVICE_START_PENDING; //��ǰ״̬
	ServiceStatus.dwControlsAccepted   =  SERVICE_ACCEPT_STOP |	SERVICE_ACCEPT_SHUTDOWN;
	ServiceStatus.dwWin32ExitCode = 0; //�˳���
	ServiceStatus.dwServiceSpecificExitCode = 0; 
	ServiceStatus.dwCheckPoint = 0; 
	ServiceStatus.dwWaitHint = 0; 

	//ע����ƴ���������
	hStatus = RegisterServiceCtrlHandler(
		"MemoryStatus", 
		(LPHANDLER_FUNCTION)ControlHandler); 
	if (hStatus == (SERVICE_STATUS_HANDLE)0) 
	{ 
		// Registering Control Handler failed
		return; 
	}  

	// Initialize Service 
	error = InitService(); 
	if (error) 
	{
		// Initialization failed
		ServiceStatus.dwCurrentState = SERVICE_STOPPED; 
		ServiceStatus.dwWin32ExitCode = -1; 

		//���·����״̬��Ϣ
		SetServiceStatus(hStatus, &ServiceStatus); 
		return; 
	} 

	// We report the running status to SCM.
	//���·����״̬��Ϣ
	ServiceStatus.dwCurrentState = 
		SERVICE_RUNNING; 
	SetServiceStatus (hStatus, &ServiceStatus);

	//�ڴ�״̬����������
	MEMORYSTATUS memory; 
	// The worker loop of a service
	while (ServiceStatus.dwCurrentState == 
		SERVICE_RUNNING)
	{
		char buffer[16];
		GlobalMemoryStatus(&memory); //���ȫ���ڴ�״̬
		sprintf(buffer, "%d", memory.dwAvailPhys);

		OutputDebugString(buffer); //���
		int result = WriteToLog(buffer);  //д�뵽�ļ�
		if (result)
		{
			ServiceStatus.dwCurrentState = 
				SERVICE_STOPPED; 
			ServiceStatus.dwWin32ExitCode      = -1; 
			SetServiceStatus(hStatus, 
				&ServiceStatus);
			return;
		}
		Sleep(SLEEP_TIME);
	}
	return; 
}

/***********************************************************************
 *ServiceMain �Ĵ��롣�ú����Ƿ������ڵ㡣��������һ���������̵߳��У�
 * ����߳����ɿ��Ʒ����������ġ�ServiceMain Ӧ�þ���������Ϊ����ע���
 * �ƴ���������Ҫͨ������ RegisterServiceCtrlHadler������ʵ�֡���Ҫ
 * �������������ݸ��˺�������������ָ�� ControlHandlerfunction ��ָ�롣
 * ������ָʾ���Ʒ��������� ControlHandler �������� SCM ��������ע����
 * ���ƴ�����֮�󣬻��״̬�����hStatus����ͨ������ 
 * SetServiceStatus �������� hStatus �� SCM ��������״̬��
 * Listing 1 չʾ�����ָ�������������䵱ǰ״̬����ʼ�� ServiceStatus 
 * �ṹ��ServiceStatus �ṹ��ÿ����������;��
 * 
 * dwServiceType��ָʾ�������ͣ����� Win32 ���񡣸�ֵ SERVICE_WIN32�� 
 * dwCurrentState��ָ������ĵ�ǰ״̬����Ϊ����ĳ�ʼ��������û����ɣ�
 * ���������״̬Ϊ SERVICE_START_PENDING�� 
 * dwControlsAccepted�������֪ͨ SCM ��������ĸ��򡣱�
 * ������������ STOP �� SHUTDOWN ���󡣴�����������ڵ��������ۣ� 
 * dwWin32ExitCode �� dwServiceSpecificExitCode����������������ֹ���񲢱�
 * ���˳�ϸ��ʱ�����á���ʼ������ʱ�����˳�����ˣ����ǵ�ֵΪ 0�� 
 * dwCheckPoint �� dwWaitHint�����������ʾ��ʼ��ĳ���������ʱҪ
 * 30�����ϡ��������ӷ���ĳ�ʼ�����̺̣ܶ��������������ֵ��Ϊ 0�� 
 * �������� SetServiceStatus ������ SCM ��������״̬ʱ��Ҫ�ṩ
 * hStatus ����� ServiceStatus �ṹ��ע�� ServiceStatus һ��ȫ�ֱ�������
 * ������Կ�������ʹ������ServiceMain �����У�����ṹ�ļ�����ֵ����
 * ���ڷ������е����������ж����ֲ��䣬���磺dwServiceType��
 * �����ڱ����˷���״̬֮������Ե��� InitService ��������ɳ�ʼ������
 * ������ֻ�����һ��˵�����ַ�������־�ļ���
 ************************************************************************/
