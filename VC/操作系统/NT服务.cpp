/////////////////////////////////////////////
// 	NT服务
/////////////////////////////////////////////
#include <stdio.h>
#include <windows.h>

SERVICE_STATUS m_ServiceStatus;
SERVICE_STATUS_HANDLE m_ServiceStatusHandle;
BOOL bRunning=true;

void WINAPI ServiceMain(DWORD argc, LPTSTR *argv);//服务主函数
void WINAPI ServiceCtrlHandler(DWORD Opcode);//服务控制函数
void WINAPI CmdStart(void);//要启动的程序函数
BOOL InstallService();   //安装服务的函数
BOOL DeleteService();    //删除服务的函数

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
			if(InstallService()) //安装服务
				printf("\n\nService Installed Sucessfully\n");
			else
				printf("\n\nError Installing Service\n");
		}
	else if(strcmp(argv[1],"-remove")==0)    // remove
		{
			if(DeleteService())	//卸载服务
				printf("\n\nService remove sucessfully\n");
			else
				printf("\n\nError removing Service\n");
		} 
		else
		{
			printf("\nusage: %s -install[remove]\n",argv[0]);
			return 0;
		}
//在进入点函数里面要完成ServiceMain的初始化，
//准确点说是初始化一个SERVICE_TABLE_ENTRY结构数组，
//这个结构记录了这个服务程序里面所包含的所有服务的名称
//和服务的进入点函数
SERVICE_TABLE_ENTRY 
DispatchTable[]={{"WindowsMgr",ServiceMain},{NULL,NULL}}; //对数组的初始化
//最后的NULL指明数组的结束（调度表 ）

//开始服务调度
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
	m_ServiceStatus.dwCurrentState = SERVICE_RUNNING; //设置服务状态
	m_ServiceStatus.dwCheckPoint = 0; 
	m_ServiceStatus.dwWaitHint = 0; 
//SERVICE_STATUS结构含有七个成员，它们反映服务的现行状态。
//所有这些成员必须在这个结构被传递到SetServiceStatus之前正确的设置
SetServiceStatus (m_ServiceStatusHandle, &m_ServiceStatus);
 	bRunning=true;
	//*
    CmdStart(); //启动我们的服务程序
	//*
	return; 
}
void WINAPI ServiceCtrlHandler(DWORD Opcode)//服务控制函数
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
BOOL InstallService()   //安装服务函数
{
	char strDir[1024];
	SC_HANDLE schSCManager,schService;
	GetCurrentDirectory(1024,strDir);
	GetModuleFileName(NULL,strDir,sizeof(strDir));

	char chSysPath[1024];
	GetSystemDirectory(chSysPath,sizeof(chSysPath));

	strcat(chSysPath,"\\WindowsMgr.exe");
if(!CopyFile(strDir,chSysPath,FALSE))printf("Copy file OK\n");
// 把我们的服务程序复制到系统根目录
	strcpy(strDir,chSysPath);
	//创建一个到服务控制管理器的连接
schSCManager = OpenSCManager(NULL,NULL,SC_MANAGER_ALL_ACCESS); 
	if (schSCManager == NULL) 
	{
		printf("open scmanger failed,maybe you do not have the privilage to do this\n");
		return false;
	}

	LPCTSTR lpszBinaryPathName=strDir;
	//将服务的信息添加到SCM的数据库
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

//卸载服务
BOOL DeleteService()
{
	SC_HANDLE schSCManager;
	SC_HANDLE hService;
	schSCManager = OpenSCManager(NULL,NULL,SC_MANAGER_ALL_ACCESS);

    char chSysPath[1024];
	GetSystemDirectory(chSysPath,sizeof(chSysPath)); //获得系统目录
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

	//删除服务
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
//把你的要做成服务启动的程序代码添加到这里
//那么你的代码就可以作为NT服务启动了
//--------------------------------
}

////////////////////////////////////////////////////

#include <windows.h>
#include <stdio.h>

#define SLEEP_TIME	5000
#define LOGFILE	"C:\\memstatus.txt" //日志文件名

SERVICE_STATUS ServiceStatus; //服务状态
SERVICE_STATUS_HANDLE	hStatus; //服务句柄

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
	//服务入口表（服务的名字和入口函数）
	SERVICE_TABLE_ENTRY ServiceTable[2];
	ServiceTable[0].lpServiceName = "MemoryStatus";
	ServiceTable[0].lpServiceProc = (LPSERVICE_MAIN_FUNCTION)ServiceMain;

	//NULL表示结尾
	ServiceTable[1].lpServiceName = NULL;
	ServiceTable[1].lpServiceProc = NULL;

	// 启动服务的控制分派机线程
	StartServiceCtrlDispatcher(ServiceTable); 


}

// 服务初始化
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

	//服务的状态信息
	ServiceStatus.dwServiceType =	SERVICE_WIN32; //类型
	ServiceStatus.dwCurrentState = 	SERVICE_START_PENDING; //当前状态
	ServiceStatus.dwControlsAccepted   =  SERVICE_ACCEPT_STOP |	SERVICE_ACCEPT_SHUTDOWN;
	ServiceStatus.dwWin32ExitCode = 0; //退出码
	ServiceStatus.dwServiceSpecificExitCode = 0; 
	ServiceStatus.dwCheckPoint = 0; 
	ServiceStatus.dwWaitHint = 0; 

	//注册控制处理器函数
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

		//更新服务的状态信息
		SetServiceStatus(hStatus, &ServiceStatus); 
		return; 
	} 

	// We report the running status to SCM.
	//更新服务的状态信息
	ServiceStatus.dwCurrentState = 
		SERVICE_RUNNING; 
	SetServiceStatus (hStatus, &ServiceStatus);

	//内存状态物理和虚拟的
	MEMORYSTATUS memory; 
	// The worker loop of a service
	while (ServiceStatus.dwCurrentState == 
		SERVICE_RUNNING)
	{
		char buffer[16];
		GlobalMemoryStatus(&memory); //获得全局内存状态
		sprintf(buffer, "%d", memory.dwAvailPhys);

		OutputDebugString(buffer); //输出
		int result = WriteToLog(buffer);  //写入到文件
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



