//////////////////////////////////////////
//	多线程
//////////////////////////////////////////

    DWORD dwThreadld,dwThrdParam=1;
    HANDLE hThread;
    hThread=CreateThread(NULL,0,ThreadFunc,&dwThrdParam,0,&dwThreadld);
    if (hThread == NULL)
    {
        printf("CreateThread failed (%ld)\n",GetLastError());
    }
    else
    {
        CloseHandle(hThread);
    }

        
        
//==================================================
// 功能：线程函数
//==================================================
DWORD WINAPI ThreadFunc(LPVOID lpParam)
{
    //............................
    return 1;	
}

/*
Win32 API对多线程编程的支持

　　Win32 提供了一系列的API函数来完成线程的创建、挂起、恢复、终结以及通信等工作。下面将选取其中的一些重要函数进行说明。 

1、HANDLE CreateThread(LPSECURITY_ATTRIBUTES lpThreadAttributes,
                 DWORD dwStackSize,
                 LPTHREAD_START_ROUTINE lpStartAddress,
                 LPVOID lpParameter,
                 DWORD dwCreationFlags,
                 LPDWORD lpThreadId);

该函数在其调用进程的进程空间里创建一个新的线程，并返回已建线程的句柄，其中各参数说明如下：
lpThreadAttributes：指向一个 SECURITY_ATTRIBUTES 结构的指针，该结构决定了线程的安全属性，一般置为 NULL； 
dwStackSize：指定了线程的堆栈深度，一般都设置为0； 
lpStartAddress：表示新线程开始执行时代码所在函数的地址，即线程的起始地址。一般情况为(LPTHREAD_START_ROUTINE)ThreadFunc，ThreadFunc 是线程函数名； 
lpParameter：指定了线程执行时传送给线程的32位参数，即线程函数的参数； 
dwCreationFlags：控制线程创建的附加标志，可以取两种值。如果该参数为0，线程在被创建后就会立即开始执行；如果该参数为CREATE_SUSPENDED,则系统产生线程后，该线程处于挂起状态，并不马上执行，直至函数ResumeThread被调用； 
lpThreadId：该参数返回所创建线程的ID； 
如果创建成功则返回线程的句柄，否则返回NULL。 

2、DWORD SuspendThread(HANDLE hThread);

该函数用于挂起指定的线程，如果函数执行成功，则线程的执行被终止。

 3、DWORD ResumeThread(HANDLE hThread);

该函数用于结束线程的挂起状态，执行线程。

 4、VOID ExitThread(DWORD dwExitCode);

该函数用于线程终结自身的执行，主要在线程的执行函数中被调用。其中参数dwExitCode用来设置线程的退出码。 

5、BOOL TerminateThread(HANDLE hThread,DWORD dwExitCode);

　　一般情况下，线程运行结束之后，线程函数正常返回，但是应用程序可以调用TerminateThread强行终止某一线程的执行。各参数含义如下：
hThread：将被终结的线程的句柄； 
dwExitCode：用于指定线程的退出码。 
　　使用TerminateThread()终止某个线程的执行是不安全的，可能会引起系统不稳定；虽然该函数立即终止线程的执行，但并不释放线程所占用的资源。因此，一般不建议使用该函数。 

6、BOOL PostThreadMessage(DWORD idThread,
			UINT Msg,
			WPARAM wParam,
			LPARAM lParam);

该函数将一条消息放入到指定线程的消息队列中，并且不等到消息被该线程处理时便返回。
idThread：将接收消息的线程的ID； 
Msg：指定用来发送的消息； 
wParam：同消息有关的字参数； 
lParam：同消息有关的长参数； 
调用该函数时，如果即将接收消息的线程没有创建消息循环，则该函数执行失败。
*/
