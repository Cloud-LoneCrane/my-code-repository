
/*   
 *   调用格式
 *   ::CopyMemory(sendBuf,&zhxxVar,sizeof(zhxxVar));
 *   
 *   */

//转换数据类型
void Convert(
			 PVOID Destination,   // copy destination
			 SIZE_T Length,        // size of memory block
			 _variant_t var //变量
			 )
{

	short sVar; //短整型
	long lVar;//长整型
	double dVar;//时间日期型
	__int64 i64Var;//货币
	char szVar[512];//_BSTR_T类型字符串

	//根据变量的类型，先转换成常见的数据类型
	switch (var.vt)
	{
		case 2: //短整型
			sVar=short(var);
			::CopyMemory(Destination,&sVar,Length);
			break;
		case 3: //长整型
			lVar=long(var);
			::CopyMemory(Destination,&lVar,Length);
			break;
		case 6:	//货币
			i64Var= CY(var).int64;
			::CopyMemory(Destination,&i64Var,Length);
			break;
		case 7:	//时间日期型
			dVar=double(var);
			::CopyMemory(Destination,&dVar,Length);
			break;
		case 8:	//_BSTR_T类型字符串
			{
				strcpy(szVar,(LPCSTR)_bstr_t(var));
			::CopyMemory(Destination,&szVar,Length);
			
			char* p=(char*)Destination;
			for (unsigned int i=0;i<Length;i++)
			{
				if(p[i]=='\0')
				{
					memset(&p[i],0,Length-i);
					break;
				}

			}
			}
			break;
		default:
			break;
	}
}