#include<windows.h>   //GetStdHandle和SetConsoleTextAttribute在头文件windows.h中     
#include<iostream>     
using namespace std; //使用命名空间
 
void SetColor(unsigned short ForeColor=FOREGROUND_RED,unsigned short BackGroundColor=0)                                           
{ //设置参数默认值  
	HANDLE hCon = GetStdHandle(STD_OUTPUT_HANDLE);    //本例以输出为例
    SetConsoleTextAttribute(hCon,ForeColor|BackGroundColor); 
//	SetConsoleTextAttribute(hCon,ForeColor|BackGroundColor|FOREGROUND_INTENSITY); //字体加亮
}; 
void main()
{   
	SetColor(FOREGROUND_RED,1);
	std::cout<<"Hello world!"<<endl;
	
	SetColor(FOREGROUND_GREEN,0);
	std::cout<<"Hello world!"<<endl;
      
}