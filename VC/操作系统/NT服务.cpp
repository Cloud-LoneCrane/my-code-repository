/////////////////////////////////////////////
// 	NT����
/////////////////////////////////////////////
#include <stdio.h>
#include <windows.h>

SERVICE_STATUS m_ServiceStatus;
SERVICE_STATUS_HANDLE m_ServiceStatusHandle;
BOOL bRunning=true;

void WINAPI ServiceMain(DWORD argc, LPTSTR *argv);//����������
void WINAPI ServiceCtrlHandler(DWORD Opcode);//������ƺ���
void WINAPI CmdStart(void);//Ҫ�����ĳ�����
BOOL InstallService();   //��װ����ĺ���
BOOL DeleteService();    //ɾ������ĺ���

int main(int argc, char* argv[])
{
	printf("%d	%s",argc,argv[1]);
	printf("\twindows based service demo\n");

    if(argc!=3)
	{
		printf("usage: %s -install[remove]",argv[0]);
		return 0;
	}
		if(strcmp(argv[1],"-install")==0) // install
		{
			if(InstallService()) //��װ����
				printf("\n\nService Installed Sucessfully\n");
			else
				printf("\n\nError Installing Service\n");
		}
	else if(strcmp(argv[1],"-remove")==0)    // remove
		{
			if(DeleteService())	//ж�ط���
				printf("\n\nService remove sucessfully\n");
			else
				printf("\n\nError removing Service\n");
		} 
		else
		{
			printf("\nusage: %s -install[remove]\n",argv[0]);
			return 0;
		}
//�ڽ���㺯������Ҫ���ServiceMain�ĳ�ʼ����
//׼ȷ��˵�ǳ�ʼ��һ��SERVICE_TABLE_ENTRY�ṹ���飬
//����ṹ��¼���������������������������з��������
//�ͷ���Ľ���㺯��
SERVICE_TABLE_ENTRY 
DispatchTable[]={{"WindowsMgr",ServiceMain},{NULL,NULL}}; //������ĳ�ʼ��
//����NULLָ������Ľ��������ȱ� ��

//��ʼ�������
	  StartServiceCtrlDispatcher(DispatchTable); 
		return 0;
}

void WINAPI ServiceMain(DWORD argc, LPTSTR *argv)
{
	m_ServiceStatus.dwServiceType = SERVICE_WIN32;
	m_ServiceStatus.dwCurrentState = SERVICE_START_PENDING; 
	m_ServiceStatus.dwControlsAccepted = SERVICE_ACCEPT_STOP;
	m_ServiceStatus.dwWin32ExitCode = 0; 
	m_ServiceStatus.dwServiceSpecificExitCode = 0; 
	m_ServiceStatus.dwCheckPoint = 0; 
	m_ServiceStatus.dwWaitHint = 0;
	m_ServiceStatusHandle = RegisterServiceCtrlHandler("WindowsMgr",ServiceCtrlHandler);
	if (m_ServiceStatusHandle == (SERVICE_STATUS_HANDLE)0)return; 
	m_ServiceStatus.dwCurrentState = SERVICE_RUNNING; //���÷���״̬
	m_ServiceStatus.dwCheckPoint = 0; 
	m_ServiceStatus.dwWaitHint = 0; 
//SERVICE_STATUS�ṹ�����߸���Ա�����Ƿ�ӳ���������״̬��
//������Щ��Ա����������ṹ�����ݵ�SetServiceStatus֮ǰ��ȷ������
SetServiceStatus (m_ServiceStatusHandle, &m_ServiceStatus);
 	bRunning=true;
	//*
    CmdStart(); //�������ǵķ������
	//*
	return; 
}
void WINAPI ServiceCtrlHandler(DWORD Opcode)//������ƺ���
{
	switch(Opcode) 
	{ 
	case SERVICE_CONTROL_PAUSE:    // we accept the command to pause it
		m_ServiceStatus.dwCurrentState = SERVICE_PAUSED; 
		break; 
	case SERVICE_CONTROL_CONTINUE:  // we got the command to continue
		m_ServiceStatus.dwCurrentState = SERVICE_RUNNING; 
		break; 
	case SERVICE_CONTROL_STOP:   // we got the command to stop this service
		m_ServiceStatus.dwWin32ExitCode = 0; 
		m_ServiceStatus.dwCurrentState = SERVICE_STOPPED; 
		m_ServiceStatus.dwCheckPoint = 0; 
		m_ServiceStatus.dwWaitHint = 0; 
		SetServiceStatus (m_ServiceStatusHandle,&m_ServiceStatus);
		bRunning=false;
		break;
	case SERVICE_CONTROL_INTERROGATE: // 
		break; 
	} 
	return; 
}
BOOL InstallService()   //��װ������
{
	char strDir[1024];
	SC_HANDLE schSCManager,schService;
	GetCurrentDirectory(1024,strDir);
	GetModuleFileName(NULL,strDir,sizeof(strDir));

	char chSysPath[1024];
	GetSystemDirectory(chSysPath,sizeof(chSysPath));

	strcat(chSysPath,"\\WindowsMgr.exe");
if(!CopyFile(strDir,chSysPath,FALSE))printf("Copy file OK\n");
// �����ǵķ�������Ƶ�ϵͳ��Ŀ¼
	strcpy(strDir,chSysPath);
	//����һ����������ƹ�����������
schSCManager = OpenSCManager(NULL,NULL,SC_MANAGER_ALL_ACCESS); 
	if (schSCManager == NULL) 
	{
		printf("open scmanger failed,maybe you do not have the privilage to do this\n");
		return false;
	}

	LPCTSTR lpszBinaryPathName=strDir;
	//���������Ϣ��ӵ�SCM�����ݿ�
	schService = CreateService(schSCManager,"WindowsMgr","Windows Manger Control", 
		SERVICE_ALL_ACCESS, // desired access 
		SERVICE_WIN32_OWN_PROCESS, // service type 
		SERVICE_AUTO_START, // start type 
		SERVICE_ERROR_NORMAL, // error control type 
		lpszBinaryPathName, // service's binary 
		NULL, // no load ordering group 
		NULL, // no tag identifier 
		NULL, // no dependencies 
		NULL, // LocalSystem account 
		NULL); // no password 

	if (schService == NULL) 
	{
		printf("faint,we failed just because we invoke createservices failed\n");
		return false; 
	}
	CloseServiceHandle(schService); 
	return true;
}

//ж�ط���
BOOL DeleteService()
{
	SC_HANDLE schSCManager;
	SC_HANDLE hService;
	schSCManager = OpenSCManager(NULL,NULL,SC_MANAGER_ALL_ACCESS);

    char chSysPath[1024];
	GetSystemDirectory(chSysPath,sizeof(chSysPath)); //���ϵͳĿ¼
    strcat(chSysPath,"\\WindowsMgr.exe");

	if (schSCManager == NULL) 
	{
		printf("faint,open scmanger failed\n");
		return false; 
	}
	hService=OpenService(schSCManager,"WindowsMgr",SERVICE_ALL_ACCESS);
	if (hService == NULL) 
	{
		printf("faint,open services failt\n");
		return false;
	}
    if(DeleteFile(chSysPath)==0)
		{
			printf("Dell file Failure !\n");               
			return false;
		}
	else printf("Delete file OK!\n");

	//ɾ������
	if(DeleteService(hService)==0)
		return false;
    
	if(CloseServiceHandle(hService)==0)
		return false;
	else
		return true;
}

void WINAPI CmdStart(void)
{
//--------------------------------
//�����Ҫ���ɷ��������ĳ��������ӵ�����
//��ô��Ĵ���Ϳ�����ΪNT����������
//--------------------------------
}

////////////////////////////////////////////////////

#include <windows.h>
#include <stdio.h>

#define SLEEP_TIME	5000
#define LOGFILE	"C:\\memstatus.txt" //��־�ļ���

SERVICE_STATUS ServiceStatus; //����״̬
SERVICE_STATUS_HANDLE	hStatus; //������

void ServiceMain(int argc, char** argv); 
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



