MsgWait for Visual C++

Here is the MsgWait sample code for use with VC++.  A DoEvents function is also shown that processes Windows messages.  If messages are not processed, no events will be received.
void DoEvents();
void MsgWait(long ms);

 

' *********************************************************
' * DoEvents
' *
' * Processes all Windows messages in the message queue
' * for the current thread
' * 
' * Input: none
' * Output: none
' *
' * Notes: Resolution in NT 3.5 and above is 10 ms
' * Copyright (c) 2003, Epson America, Inc. FAR Division
' *********************************************************
void DoEvents()
{
  MSG msg;
  long sts;

 

  do {
    if (sts = PeekMessage(&msg, (HWND) NULL, 0, 0, PM_REMOVE)) {
      TranslateMessage(&msg);
      DispatchMessage(&msg);
    }
  } while (sts);
}

 

' *********************************************************
' * MsgWait
' *
' * Sleeps for a specified time but allows
' * events to process immediately
' *
' * Input: ms - milliseconds to wait
' * Output: none
' *
' * Notes: Resolution in NT 3.5 and above is 10 ms
' * Copyright (c) 2003, Epson America, Inc. FAR Division
' *********************************************************
void MsgWait(long ms)
{
  long start, timeRemaining, timeNow;

 

  start = GetTickCount();
  timeRemaining = ms;
  do {
    // Sleep until timeout or event occurs
    MsgWaitForMultipleObjects(0, 0, 0, timeRemaining, QS_ALLINPUT);
    timeNow = GetTickCount();
    if (timeNow - start >= timeRemaining)
      return;
    else if (timeNow < start)
      // Handle GetTickCount 49.7 day wrap around
      start = timeNow;
    timeRemaining = timeRemaining - (timeNow - start);
    start = timeNow;
    DoEvents();
  } while(1);
}


