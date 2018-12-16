#include<stdio.h>
#include<windows.h>
void GetSysInfo();

DWORD GetOS() 
{ 
OSVERSIONINFO os; 


os.dwOSVersionInfoSize=sizeof(OSVERSIONINFO); 
GetVersionEx(&os); 
switch(os.dwPlatformId) 
{ 
case VER_PLATFORM_WIN32_WINDOWS: 
return VER_PLATFORM_WIN32_WINDOWS; 

case VER_PLATFORM_WIN32_NT: 
return VER_PLATFORM_WIN32_NT; 
} 
return 0;
} 
VOID GetSysInfo() 
{ 
TCHAR szBuff[MAX_PATH]; 
TCHAR szTemp[MAX_PATH]; 


wsprintf(szBuff,"\n\n\r<<System Information>>\n\n\r"); 
printf("%s",szBuff);

//计算机名 
DWORD len=sizeof(szTemp); 
GetComputerName(szTemp,&len); 
wsprintf(szBuff,"Computer Name: %s\n\n\r",szTemp); 
printf("%s",szBuff);

//当前操作系统 
switch(GetOS()) 
{ 
case VER_PLATFORM_WIN32_WINDOWS: 
lstrcpy(szTemp,"Windows 9x"); 
break; 
case VER_PLATFORM_WIN32_NT: 
lstrcpy(szTemp,"Windows NT/2000"); 
break; 
} 
wsprintf(szBuff,"Option System: %s\n\n\r",szTemp); 
printf("%s",szBuff);

//内存容量 
MEMORYSTATUS mem; 
mem.dwLength=sizeof(mem); 
GlobalMemoryStatus(&mem); 
wsprintf(szBuff,"Total Memroy: %dM\n\n\r",mem.dwTotalPhys/1024/1024+1); 
printf("%s",szBuff);

//系统目录 
TCHAR szPath[MAX_PATH]; 
GetWindowsDirectory(szTemp,sizeof(szTemp)); 
GetSystemDirectory(szBuff,sizeof(szBuff)); 
wsprintf(szPath,"Windows Directory: %s\n\n\rSystem Directory: %s\n\n\r",szTemp,szBuff); 
printf("%s",szBuff);

//驱动器及分区类型 
TCHAR szFileSys[10]; 

for(int i=0;i<26;++i) 
{ 
wsprintf(szTemp,"%c:\\",'A'+i); 
UINT uType=GetDriveType(szTemp); 
switch(uType) 
{ 
case DRIVE_FIXED: 
GetVolumeInformation(szTemp,NULL,NULL,NULL,NULL,NULL,szFileSys,MAX_PATH); 
wsprintf(szBuff,"Hard Disk: %s (%s)\n\n\r",szTemp,szFileSys); 
printf("%s",szBuff);
break; 
case DRIVE_CDROM: 
wsprintf(szBuff,"CD-ROM Disk: %s\n\n\r",szTemp); 
printf("%s",szBuff);
break; 
case DRIVE_REMOTE: 
GetVolumeInformation(szTemp,NULL,NULL,NULL,NULL,NULL,szFileSys,MAX_PATH); 
wsprintf(szBuff,"NetWork Disk: %s (%s)\n\n\r",szTemp,szFileSys); 
printf("%s",szBuff);
break; 
} 
} 

} 
int main(void )
{
GetSysInfo();
return 0;

}