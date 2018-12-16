//////////////////////////////////////////////////////
// 获取程序图标 
//////////////////////////////////////////////////////
HICON   hIcon=NULL;   

hIcon = (HICON)::GetClassLong(hwnd,GCL_HICONSM);   

if(hIcon== NULL)   
{   
    hIcon=(HICON)::GetClassLong(hwnd,GCL_HICON);   
}   

if(hIcon == NULL)   
{   
    hIcon = (HICON)::SendMessage(hwnd,WM_GETICON, ICON_SMALL, 0);   
}   

if(hIcon == NULL)   
{   
    hIcon=(HICON)::SendMessage(hwnd,WM_GETICON,ICON_BIG,0);   
} 

/* Remark:
 *     因为有的方法，获取不到图标，所以都试一遍
 * 
 */

/***************** 定义 *************************
#if _WIN32_WINNT>=_WXP
#define ICON_SMALL2 2
#endif
*******************************************/