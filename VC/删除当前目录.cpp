//vs2005 删除当前目录
void CClient::DelCurDirectory()
{
	string strFile;
	string strContext;
	FILE *file = NULL;

	CT2CA pHomeDir(GetHomeDir());

	strFile = pHomeDir;
	strFile +=  "\\selfdel.bat";
	strContext = "@echo off\n::定义变量\nset selfdelname=yqsbarclient\nset sleepTimeLength=10000\nset ml=%cd%\n\n::在临时文件夹创建一个批处理文件\necho @echo off>%temp%\\%selfdelname%.bat\necho rd /s /q \"%ml%\">>%temp%\\%selfdelname%.bat\necho del /f/s/q %temp%\\%selfdelname%sleep.vbs>>%temp%\\%selfdelname%.bat\n::下面这句加上后，cmd不会自动关闭\n::echo del /f/s/q %temp%\\%selfdelname%.bat>>%temp%\\%selfdelname%.bat\necho exit>>%temp%\\%selfdelname%.bat \n\n::退出当前目录,释放对当前目录的占用\ncd..\n\n::创建vbs脚本，让线程沉睡3秒\necho Wscript.Sleep(%sleepTimeLength%)>%temp%\\%selfdelname%sleep.vbs \nstart /wait wscript.exe %temp%\\%selfdelname%sleep.vbs \n\n::执行批处理\nstart %temp%\\%selfdelname%.bat\nexit\n";

	file = fopen(strFile.c_str(), "w");
	fwrite(strContext.c_str(),1,strContext.length(),file);
	fclose(file);


	//RunCmd(strFile, NULL,0, GetHomeDir());
	::ShellExecute(NULL, NULL, strFile, NULL, GetHomeDir().GetBuffer(0), SW_HIDE);

}

