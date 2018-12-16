//========================================
// 功能：将time_t 日历时间 转化为 固定格式的时间
//		eg.   0  = 1970-01-01 00:00:00
// 使用说明：包含 time.h , 
//========================================
void Time_tToStr(char* buf,size_t maxsize,time_t t)
{
	struct tm *newtime;
	newtime=localtime(&t);
	strftime(buf,maxsize, "%Y-%m-%d %H:%M:%S", newtime);
}

//========================================
// 功能：将固定格式的时间转化为 time_t 日历时间
//		eg. 1970-01-01 00:00:00  = 0
// 使用说明：包含 time.h , string.h
//========================================
time_t GetTime_t(char* Buf)
{
	int iYear=0,iMonth=0,iDay=0,
		iHour=0,iMinute=0,iSecond=0;

	//获得年,月，日
	char *pTmp,szBuf[21]="";
	_tcscpy(szBuf,Buf);
	pTmp=_tcsstr(szBuf,"-");
	char szYear[5]="",szMonth[3]="",szDay[3]="";
	char* p=szBuf,*p1=szYear;
	while (p != pTmp)
	{	*p1=*p;p++;p1++;
	}

	*pTmp=',';
	pTmp=_tcsstr(szBuf,"-");
	p1=szMonth;p++;
	while (p != pTmp){
		*p1=*p;p++;p1++;
	}

	*pTmp=',';
	pTmp=_tcsstr(szBuf," ");
	p1=szDay;p++;
	while (p != pTmp){
		*p1=*p;p++;p1++;
	}

	//获得时间
	char szHour[3]="",szMinute[3]="",szSecond[3]="";
	*pTmp=',';
	pTmp=_tcsstr(szBuf,":");
	p1=szHour;p++;
	while (p != pTmp){
		*p1=*p;p++;p1++;
	}
	
		*pTmp=',';
	pTmp=_tcsstr(szBuf,":");
	p1=szMinute;p++;
	while (p != pTmp){
		*p1=*p;p++;p1++;
	}
		*pTmp=',';
	pTmp=_tcsstr(szBuf,":");
	p1=szSecond;p++;
	while (p != pTmp && *p != '\0'){
		*p1=*p;p++;p1++;
	}

	iYear=atoi(szYear);iMonth=atoi(szMonth),iDay=atoi(szDay);
	iHour=atoi(szHour),iMinute=atoi(szMinute),iSecond=atoi(szSecond);

	struct tm t;
	time_t t_of_day;
	t.tm_year=iYear-1900;
	t.tm_mon=iMonth-1;
	t.tm_mday=iDay;
	t.tm_hour=iHour;
	t.tm_min=iMinute;
	t.tm_sec=iSecond;
	t.tm_isdst=0;
	t_of_day=mktime(&t);

	return t_of_day;
}

//============================
//    调用格式
//============================
	time_t t;
	//t=GetTime_t("2010-01-28 16:41:31");
	t=GetTime_t("1970-01-01 01:00:00");
	cout<<"time: "<<t<<endl;
		
		