/** XP下调整系统时间的演示 */
BOOL CSyncTimeDlg::SetSysTime()
{
   HANDLE hToken; 
   TOKEN_PRIVILEGES tkp; 

   // Get a token for this process. 
   if (!OpenProcessToken(GetCurrentProcess(), 
        TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, &hToken)) 
      return( FALSE ); 

   // Get the LUID for the shutdown privilege. 
   LookupPrivilegeValue(NULL, SE_SYSTEMTIME_NAME, //注意，改的就是它
        &tkp.Privileges[0].Luid); 
        //如果是关机,就把SE_SYSTEMTIME_NAME 改为 SE_SHUTDOWN_NAME

   tkp.PrivilegeCount = 1;  // one privilege to set    
   tkp.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED; 

   // Get the shutdown privilege for this process. 
   AdjustTokenPrivileges(hToken, FALSE, &tkp, 0, 
        (PTOKEN_PRIVILEGES)NULL, 0); 

   if (GetLastError() != ERROR_SUCCESS) 
      return FALSE; 

    //取得必要权限后，设置系统时间
    SYSTEMTIME systm;
    ::GetLocalTime(&systm);    //得到当前的本地时间
    systm.wMonth += 1;        //作为演示，我们让月份和小时数加1
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