//////////////////////////////////////////
//	���߳�
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
// ���ܣ��̺߳���
//==================================================
DWORD WINAPI ThreadFunc(LPVOID lpParam)
{
    //............................
    return 1;	
}

/*
Win32 API�Զ��̱߳�̵�֧��

����Win32 �ṩ��һϵ�е�API����������̵߳Ĵ��������𡢻ָ����ս��Լ�ͨ�ŵȹ��������潫ѡȡ���е�һЩ��Ҫ��������˵���� 

1��HANDLE CreateThread(LPSECURITY_ATTRIBUTES lpThreadAttributes,
                 DWORD dwStackSize,
                 LPTHREAD_START_ROUTINE lpStartAddress,
                 LPVOID lpParameter,
                 DWORD dwCreationFlags,
                 LPDWORD lpThreadId);

�ú���������ý��̵Ľ��̿ռ��ﴴ��һ���µ��̣߳��������ѽ��̵߳ľ�������и�����˵�����£�
lpThreadAttributes��ָ��һ�� SECURITY_ATTRIBUTES �ṹ��ָ�룬�ýṹ�������̵߳İ�ȫ���ԣ�һ����Ϊ NULL�� 
dwStackSize��ָ�����̵߳Ķ�ջ��ȣ�һ�㶼����Ϊ0�� 
lpStartAddress����ʾ���߳̿�ʼִ��ʱ�������ں����ĵ�ַ�����̵߳���ʼ��ַ��һ�����Ϊ(LPTHREAD_START_ROUTINE)ThreadFunc��ThreadFunc ���̺߳������� 
lpParameter��ָ�����߳�ִ��ʱ���͸��̵߳�32λ���������̺߳����Ĳ����� 
dwCreationFlags�������̴߳����ĸ��ӱ�־������ȡ����ֵ������ò���Ϊ0���߳��ڱ�������ͻ�������ʼִ�У�����ò���ΪCREATE_SUSPENDED,��ϵͳ�����̺߳󣬸��̴߳��ڹ���״̬����������ִ�У�ֱ������ResumeThread�����ã� 
lpThreadId���ò��������������̵߳�ID�� 
��������ɹ��򷵻��̵߳ľ�������򷵻�NULL�� 

2��DWORD SuspendThread(HANDLE hThread);

�ú������ڹ���ָ�����̣߳��������ִ�гɹ������̵߳�ִ�б���ֹ��

 3��DWORD ResumeThread(HANDLE hThread);

�ú������ڽ����̵߳Ĺ���״̬��ִ���̡߳�

 4��VOID ExitThread(DWORD dwExitCode);

�ú��������߳��ս������ִ�У���Ҫ���̵߳�ִ�к����б����á����в���dwExitCode���������̵߳��˳��롣 

5��BOOL TerminateThread(HANDLE hThread,DWORD dwExitCode);

����һ������£��߳����н���֮���̺߳����������أ�����Ӧ�ó�����Ե���TerminateThreadǿ����ֹĳһ�̵߳�ִ�С��������������£�
hThread�������ս���̵߳ľ���� 
dwExitCode������ָ���̵߳��˳��롣 
����ʹ��TerminateThread()��ֹĳ���̵߳�ִ���ǲ���ȫ�ģ����ܻ�����ϵͳ���ȶ�����Ȼ�ú���������ֹ�̵߳�ִ�У��������ͷ��߳���ռ�õ���Դ����ˣ�һ�㲻����ʹ�øú����� 

6��BOOL PostThreadMessage(DWORD idThread,
			UINT Msg,
			WPARAM wParam,
			LPARAM lParam);

�ú�����һ����Ϣ���뵽ָ���̵߳���Ϣ�����У����Ҳ��ȵ���Ϣ�����̴߳���ʱ�㷵�ء�
idThread����������Ϣ���̵߳�ID�� 
Msg��ָ���������͵���Ϣ�� 
wParam��ͬ��Ϣ�йص��ֲ����� 
lParam��ͬ��Ϣ�йصĳ������� 
���øú���ʱ���������������Ϣ���߳�û�д�����Ϣѭ������ú���ִ��ʧ�ܡ�
*/
