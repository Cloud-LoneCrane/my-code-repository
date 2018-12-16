//给窗口加上 WS_EX_NOACTIVATE 扩展风格（需要_WIN32_WINNT >= 0x0500）。  


#define _WIN32_WINNT 0x0500 

ModifyStyleEx(0,WS_EX_NOACTIVATE);
