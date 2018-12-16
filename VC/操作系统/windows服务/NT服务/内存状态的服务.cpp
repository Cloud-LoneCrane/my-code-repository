////////////////////////////////////////////////////

#include <windows.h>
#include <stdio.h>

#define SLEEP_TIME	5000
#define LOGFILE	"C:\\memstatus.txt" //日志文件名

SERVICE_STATUS ServiceStatus; //服务状态
SERVICE_STATUS_HANDLE	hStatus; //服务句柄

//服务的主函数
void ServiceMain(int argc, char** argv); 
//服务的控制函数（ControlHandler 函数处理 SCM 控制请求） 
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

/***********************************************************************
 *ServiceMain 的代码。该函数是服务的入口点。它运行在一个单独的线程当中，
 * 这个线程是由控制分派器创建的。ServiceMain 应该尽可能早早为服务注册控
 * 制处理器。这要通过调用 RegisterServiceCtrlHadler函数来实现。你要
 * 将两个参数传递给此函数：服务名和指向 ControlHandlerfunction 的指针。
 * 　　它指示控制分派器调用 ControlHandler 函数处理 SCM 控制请求。注册完
 * 控制处理器之后，获得状态句柄（hStatus）。通过调用 
 * SetServiceStatus 函数，用 hStatus 向 SCM 报告服务的状态。
 * Listing 1 展示了如何指定服务特征和其当前状态来初始化 ServiceStatus 
 * 结构，ServiceStatus 结构的每个域都有其用途：
 * 
 * dwServiceType：指示服务类型，创建 Win32 服务。赋值 SERVICE_WIN32； 
 * dwCurrentState：指定服务的当前状态。因为服务的初始化在这里没有完成，
 * 所以这里的状态为 SERVICE_START_PENDING； 
 * dwControlsAccepted：这个域通知 SCM 服务接受哪个域。本
 * 文例子是允许 STOP 和 SHUTDOWN 请求。处理控制请求将在第三步讨论； 
 * dwWin32ExitCode 和 dwServiceSpecificExitCode：这两个域在你终止服务并报
 * 告退出细节时很有用。初始化服务时并不退出，因此，它们的值为 0； 
 * dwCheckPoint 和 dwWaitHint：这两个域表示初始化某个服务进程时要
 * 30秒以上。本文例子服务的初始化过程很短，所以这两个域的值都为 0。 
 * 　　调用 SetServiceStatus 函数向 SCM 报告服务的状态时。要提供
 * hStatus 句柄和 ServiceStatus 结构。注意 ServiceStatus 一个全局变量，所
 * 以你可以跨多个函数使用它。ServiceMain 函数中，你给结构的几个域赋值，它
 * 们在服务运行的整个过程中都保持不变，比如：dwServiceType。
 * 　　在报告了服务状态之后，你可以调用 InitService 函数来完成初始化。这
 * 个函数只是添加一个说明性字符串到日志文件。
 ************************************************************************/
