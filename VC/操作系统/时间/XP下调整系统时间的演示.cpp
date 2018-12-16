/** XP�µ���ϵͳʱ�����ʾ */
BOOL CSyncTimeDlg::SetSysTime()
{
   HANDLE hToken; 
   TOKEN_PRIVILEGES tkp; 

   // Get a token for this process. 
   if (!OpenProcessToken(GetCurrentProcess(), 
        TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, &hToken)) 
      return( FALSE ); 

   // Get the LUID for the shutdown privilege. 
   LookupPrivilegeValue(NULL, SE_SYSTEMTIME_NAME, //ע�⣬�ĵľ�����
        &tkp.Privileges[0].Luid); 
        //����ǹػ�,�Ͱ�SE_SYSTEMTIME_NAME ��Ϊ SE_SHUTDOWN_NAME

   tkp.PrivilegeCount = 1;  // one privilege to set    
   tkp.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED; 

   // Get the shutdown privilege for this process. 
   AdjustTokenPrivileges(hToken, FALSE, &tkp, 0, 
        (PTOKEN_PRIVILEGES)NULL, 0); 

   if (GetLastError() != ERROR_SUCCESS) 
      return FALSE; 

    //ȡ�ñ�ҪȨ�޺�����ϵͳʱ��
    SYSTEMTIME systm;
    ::GetLocalTime(&systm);    //�õ���ǰ�ı���ʱ��
    systm.wMonth += 1;        //��Ϊ��ʾ���������·ݺ�Сʱ����1
    systm.wHour += 1;

    if(0 != ::SetLocalTime(&systm))
    {
        return TRUE;
    }
    else
    {
        return FALSE;
    }
}