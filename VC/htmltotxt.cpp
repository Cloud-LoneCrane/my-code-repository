#include<stdio.h>
#include<malloc.h>
#include<string.h>

#define FALSE 0
#define TRUE  1

//////////////////////////////////////////////////////////
//函数声明

char* mid(const char*t1,int m,const char* t2,int n);
int Loc(char t,char *p,int n);
char* getTit(char *t1);
void htm_txt(char *src,char *dest);

////////////////////////////////////////////////////////////

void main()
{
     htm_txt("f:\\001.htm","f:\\result.txt");
     
    
}


//=================================================================
//功能：取两数组中部的函数，解决了相邻两个缓冲区交界处有</BODY>标记的问题。
//=================================================================
char* mid(const char*t1,int m,const char* t2,int n)
{
	char *buf;int i,j;
	if(!(buf=(char*)malloc(13)))
		printf("out of memery...");
    for(i=m-6,j=0;i<m;i++,j++)
      buf[j]=t1[i];
	for(i=0;i<=6&&j<13;i++,j++)
		buf[j]=t2[i];
	   buf[j]='\0';
	   printf("char middle: %s",buf);
	return buf;
}

char* getTit(char *buf1)
{
	char *buf3;int i;
	char *tit,*tit_end,*p,*tit_1,*tit_end_1;
	if(!(buf3=(char *)malloc(100)))
		printf("out of memery...");
           tit=strstr(buf1,"<TITLE");
           tit_end=strstr(buf1,"</TITLE>");
        
           if(tit && tit_end)
		   {
	        for(i=0,p=tit+7;p<tit_end;p++,i++)
               buf3[i]=*p;
            buf3[i]='\0';
			return buf3;
		   }
	  	  tit_1=strstr(buf1,"<title");
          tit_end_1=strstr(buf1,"</title>");
		  if(tit_1 && tit_end_1)
		  {
			for(i=0,p=tit_1+7;p<tit_end_1;p++,i++)
               buf3[i]=*p;
            buf3[i]='\0';
			return buf3;
		  }
}	

void htm_txt(char *src,char*dest)
{
char buf1[1024],buf2[1024],buf3[1024]; 
	FILE *f,*d;int num=1;
	bool startTit=FALSE,startHtm=FALSE,endHtm=FALSE;

	/*打开文件，并将文件内容读进缓冲区。*/
    if(!(f=fopen(src,"r")))
		printf("Can't open source file...");
    if(!(d=fopen(dest,"a")))
        printf("Can't open dest file...");
     if(!fgets(buf1,1024,f))
		printf("fgets 1 error ...\n");
      printf("  buf1: %s\n",buf1);
	printf(" buf1[1022]: %c sizeof(buf1): %d",buf1[1022],sizeof(buf1));
  if(!fgets(buf2,1024,f))
	  printf("fgets 2 error ...\n");
      printf("  buf2: %s\n",buf2);
	  /*缓冲区用 0 填充*/
	memset(buf1,0,1024);memset(buf2,0,1024);memset(buf3,0,1024);

  fprintf(d,"\n%s\n","*******************************************************");
  while(endHtm!=TRUE)
  {
    if(num>1)
	  {memset(buf1,0,1024);
		//fseek(f,1024,SEEK_SET);
		strcpy(buf1,buf2);
		memset(buf2,0,1024);
		
        if(!fgets(buf2,1024,f))
		{
		printf("fgets 2 error ...\n");
		}
		
	   }
    	num++;
  if(!startTit)
  {//获得标题
	  char *buf;
	  buf=getTit(buf1);
	  if(!buf)  printf("can't  get title...\n");
	  else{
      fprintf(d,"%s",buf);
	  startTit=TRUE;
	  }
  }//if(!startTit)

 // if(!startHtm||!endHtm)
endHtm=TRUE;
 }//while
  fclose(f);
  fclose(d);
}