//●   10.   如何激活指定的窗口事件   ●   
          我们知道，Windows   9x/2000中SetForegroundWindow函数当用户正在操作其他窗口时是不能强制某个窗口为前景窗口的，而是激活窗口并调用FlashWindowEx函数来通知用户。但是很多实际情况要求将激活窗口的同时将创建这个窗口的线程置为前景状态。碰到这种情况我们可以使用USER32.DLL中的几个未公开API函数。   
  void   SwitchToThisWindow   (   
  HWND   hWnd,       //   被激活的窗口句柄   
  BOOL   bRestore   //   如果被极小化，则恢复窗口   
  );   
  必须动态加载这个函数。   
  typedef   void   (WINAPI   *PROCSWITCHTOTHISWINDOW)   (HWND,   BOOL);   
  PROCSWITCHTOTHISWINDOW   SwitchToThisWindow;   
  HMODULE   hUser32   =   GetModuleHandle("user32");   
  SwitchToThisWindow   =   (   PROCSWITCHTOTHISWINDOW)   
  GetProcAddress(hUser32,   "SwitchToThisWindow");       
  接下来只要用任何现存窗口的句柄调用这个函数即可，第二个参数指定如果窗口极小化，是否恢复其原状态。   
  SwitchToThisWindow(hWnd,   TRUE);   
  //===========================================================================   
 // ●   11.   如何获取Windows外壳主窗口的句柄   ●   
        在编程过程中，我们常常需要获取Windows外壳主窗口的句柄（HWND），微软在MSDN中提供了一个这样的函数：   
  HWND   hwndShell   =   FindWindow("Program",NULL);   
        用这个函数可以满足我们的要求，但本文再提供一个更好的方法，用User32.dll中的GetShellWindow，这个函数的原型很简单：   
  HWND   GetShellWindow;   
        只是这个函数是个未公开的API函数，所以要使用它必须动态加载User32.dll。方法如下：   
  typedef   HWND   (WINAPI   *PROCGETSHELLWND)();   
  PROCGETSHELLWND   GetShellWindow;   
  HMODULE   hUser32   =   GetModuleHandle("user32");   
  GetShellWindow   =   (PROCGETSHELLWND)   
  GetProcAddress(hUser32,"GetShellWindow");   
  //===========================================================================   
  ●   12.   以编程的方式使电脑待机   ●   
  SetSystemPowerState(   
  BOOL   fSuspend,   //   Windows98   Ignore   it.   
  BOOL   fForce         //   FALSE   for   safe   
  )   ;   
  //===========================================================================   
  ●   13.   关闭当前所有的应用程序   ●   
  int   nApp   =   BSM_APPLICATIONS;   
  unsigned   long     bsm_app   =   (unsigned   long   )app;   
  BroadcastSystemMessage(BSF_POSTMESSAGE,   &bsm_app,   WM_CLOSE,   NULL,   NULL);   
  //===========================================================================   
  ●   17.   调出Win9x下的关机对话框   ●   
  typedef   long   (CALLBACK   *SHOWSUTDOWNDIALOG)(HWND);   
  SHOWSUTDOWNDIALOG   ShowShutDownDialog;   
  void   __fastcall   TForm1::Button1Click(TObject   *Sender)   
  {   
          ShowShutDownDialog   =   (SHOWSUTDOWNDIALOG)GetProcAddress(   
                          GetModuleHandle("Shell32.dll"),   "ShowShutDownDialog");   
          if(ShowShutDownDialog)   
          ShowShutDownDialog(0);   
  }
