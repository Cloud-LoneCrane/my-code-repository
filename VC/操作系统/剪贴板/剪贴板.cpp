//把文本写入剪贴板
//-------------------------------------------------------------------
CString strSource; 
//strSource中保存文本内容

if(::OpenClipboard())
{
	HGLOBAL clipbuffer; //是windows 定义的数据类型。这是个句柄，标示一个内存区，不是指针。在对内存区的操作中，一般用指针，从这个handle可以得到指针，然后就可以对内存区进行操作了
	char * buffer;
	::EmptyClipboard(); //清空剪贴板
	clipbuffer= ::GlobalAlloc(GMEM_DDESHARE,source.GetLength()+1); //分配内存
	buffer = (char*)GlobalLock(clipbuffer);//锁定内存,获得字符 串指针
	strcpy(buffer,LPCSTR(source));//向内存中复制字符串
	::GlobalUnlock(clipbuffer); //解锁内存
	::SetClipboardData(CT_TEXT,clipbuffer); //放置数据到剪贴板上
	::CloseClipboard(); //关闭剪贴板
}
//-------------------------------------------------------------------


//读出剪贴板上的文本
//-------------------------------------------------------------------
char* buffer= NULL;
//打开剪贴板
if(::OpenClipboard())
{
	HANDLE hData = ::GetClipData(CF_TEXT); //获得剪贴板文本数据的内存句柄
	buffer = (char*)::GlobalLock(hData); //句柄转化为字符指针
	::GlobalUnlock(hData); //解锁内存块,减少锁定计数器
	::CloseClipboard(); //关闭剪贴板
} 
//-------------------------------------------------------------------
