//���ı�д�������
//-------------------------------------------------------------------
CString strSource; 
//strSource�б����ı�����

if(::OpenClipboard())
{
	HGLOBAL clipbuffer; //��windows ������������͡����Ǹ��������ʾһ���ڴ���������ָ�롣�ڶ��ڴ����Ĳ����У�һ����ָ�룬�����handle���Եõ�ָ�룬Ȼ��Ϳ��Զ��ڴ������в�����
	char * buffer;
	::EmptyClipboard(); //��ռ�����
	clipbuffer= ::GlobalAlloc(GMEM_DDESHARE,source.GetLength()+1); //�����ڴ�
	buffer = (char*)GlobalLock(clipbuffer);//�����ڴ�,����ַ� ��ָ��
	strcpy(buffer,LPCSTR(source));//���ڴ��и����ַ���
	::GlobalUnlock(clipbuffer); //�����ڴ�
	::SetClipboardData(CT_TEXT,clipbuffer); //�������ݵ���������
	::CloseClipboard(); //�رռ�����
}
//-------------------------------------------------------------------


//�����������ϵ��ı�
//-------------------------------------------------------------------
char* buffer= NULL;
//�򿪼�����
if(::OpenClipboard())
{
	HANDLE hData = ::GetClipData(CF_TEXT); //��ü������ı����ݵ��ڴ���
	buffer = (char*)::GlobalLock(hData); //���ת��Ϊ�ַ�ָ��
	::GlobalUnlock(hData); //�����ڴ��,��������������
	::CloseClipboard(); //�رռ�����
} 
//-------------------------------------------------------------------
