//��   10.   ��μ���ָ���Ĵ����¼�   ��   
          ����֪����Windows   9x/2000��SetForegroundWindow�������û����ڲ�����������ʱ�ǲ���ǿ��ĳ������Ϊǰ�����ڵģ����Ǽ���ڲ�����FlashWindowEx������֪ͨ�û������Ǻܶ�ʵ�����Ҫ�󽫼���ڵ�ͬʱ������������ڵ��߳���Ϊǰ��״̬����������������ǿ���ʹ��USER32.DLL�еļ���δ����API������   
  void   SwitchToThisWindow   (   
  HWND   hWnd,       //   ������Ĵ��ھ��   
  BOOL   bRestore   //   �������С������ָ�����   
  );   
  ���붯̬�������������   
  typedef   void   (WINAPI   *PROCSWITCHTOTHISWINDOW)   (HWND,   BOOL);   
  PROCSWITCHTOTHISWINDOW   SwitchToThisWindow;   
  HMODULE   hUser32   =   GetModuleHandle("user32");   
  SwitchToThisWindow   =   (   PROCSWITCHTOTHISWINDOW)   
  GetProcAddress(hUser32,   "SwitchToThisWindow");       
  ������ֻҪ���κ��ִ洰�ڵľ����������������ɣ��ڶ�������ָ��������ڼ�С�����Ƿ�ָ���ԭ״̬��   
  SwitchToThisWindow(hWnd,   TRUE);   
  //===========================================================================   
 // ��   11.   ��λ�ȡWindows��������ڵľ��   ��   
        �ڱ�̹����У����ǳ�����Ҫ��ȡWindows��������ڵľ����HWND����΢����MSDN���ṩ��һ�������ĺ�����   
  HWND   hwndShell   =   FindWindow("Program",NULL);   
        ��������������������ǵ�Ҫ�󣬵��������ṩһ�����õķ�������User32.dll�е�GetShellWindow�����������ԭ�ͺܼ򵥣�   
  HWND   GetShellWindow;   
        ֻ����������Ǹ�δ������API����������Ҫʹ�������붯̬����User32.dll���������£�   
  typedef   HWND   (WINAPI   *PROCGETSHELLWND)();   
  PROCGETSHELLWND   GetShellWindow;   
  HMODULE   hUser32   =   GetModuleHandle("user32");   
  GetShellWindow   =   (PROCGETSHELLWND)   
  GetProcAddress(hUser32,"GetShellWindow");   
  //===========================================================================   
  ��   12.   �Ա�̵ķ�ʽʹ���Դ���   ��   
  SetSystemPowerState(   
  BOOL   fSuspend,   //   Windows98   Ignore   it.   
  BOOL   fForce         //   FALSE   for   safe   
  )   ;   
  //===========================================================================   
  ��   13.   �رյ�ǰ���е�Ӧ�ó���   ��   
  int   nApp   =   BSM_APPLICATIONS;   
  unsigned   long     bsm_app   =   (unsigned   long   )app;   
  BroadcastSystemMessage(BSF_POSTMESSAGE,   &bsm_app,   WM_CLOSE,   NULL,   NULL);   
  //===========================================================================   
  ��   17.   ����Win9x�µĹػ��Ի���   ��   
  typedef   long   (CALLBACK   *SHOWSUTDOWNDIALOG)(HWND);   
  SHOWSUTDOWNDIALOG   ShowShutDownDialog;   
  void   __fastcall   TForm1::Button1Click(TObject   *Sender)   
  {   
          ShowShutDownDialog   =   (SHOWSUTDOWNDIALOG)GetProcAddress(   
                          GetModuleHandle("Shell32.dll"),   "ShowShutDownDialog");   
          if(ShowShutDownDialog)   
          ShowShutDownDialog(0);   
  }
