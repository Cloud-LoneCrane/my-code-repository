#include<windows.h>   //GetStdHandle��SetConsoleTextAttribute��ͷ�ļ�windows.h��     
#include<iostream>     
using namespace std; //ʹ�������ռ�
 
void SetColor(unsigned short ForeColor=FOREGROUND_RED,unsigned short BackGroundColor=0)                                           
{ //���ò���Ĭ��ֵ  
	HANDLE hCon = GetStdHandle(STD_OUTPUT_HANDLE);    //���������Ϊ��
    SetConsoleTextAttribute(hCon,ForeColor|BackGroundColor); 
//	SetConsoleTextAttribute(hCon,ForeColor|BackGroundColor|FOREGROUND_INTENSITY); //�������
}; 
void main()
{   
	SetColor(FOREGROUND_RED,1);
	std::cout<<"Hello world!"<<endl;
	
	SetColor(FOREGROUND_GREEN,0);
	std::cout<<"Hello world!"<<endl;
      
}