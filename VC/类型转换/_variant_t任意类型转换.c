
/*   
 *   ���ø�ʽ
 *   ::CopyMemory(sendBuf,&zhxxVar,sizeof(zhxxVar));
 *   
 *   */

//ת����������
void Convert(
			 PVOID Destination,   // copy destination
			 SIZE_T Length,        // size of memory block
			 _variant_t var //����
			 )
{

	short sVar; //������
	long lVar;//������
	double dVar;//ʱ��������
	__int64 i64Var;//����
	char szVar[512];//_BSTR_T�����ַ���

	//���ݱ��������ͣ���ת���ɳ�������������
	switch (var.vt)
	{
		case 2: //������
			sVar=short(var);
			::CopyMemory(Destination,&sVar,Length);
			break;
		case 3: //������
			lVar=long(var);
			::CopyMemory(Destination,&lVar,Length);
			break;
		case 6:	//����
			i64Var= CY(var).int64;
			::CopyMemory(Destination,&i64Var,Length);
			break;
		case 7:	//ʱ��������
			dVar=double(var);
			::CopyMemory(Destination,&dVar,Length);
			break;
		case 8:	//_BSTR_T�����ַ���
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