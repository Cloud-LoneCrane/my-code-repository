//////////////////////////////////////////////////////
// ��ȡ����ͼ�� 
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
 *     ��Ϊ�еķ�������ȡ����ͼ�꣬���Զ���һ��
 * 
 */

/***************** ���� *************************
#if _WIN32_WINNT>=_WXP
#define ICON_SMALL2 2
#endif
*******************************************/