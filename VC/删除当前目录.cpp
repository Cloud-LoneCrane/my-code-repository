//vs2005 ɾ����ǰĿ¼
void CClient::DelCurDirectory()
{
	string strFile;
	string strContext;
	FILE *file = NULL;

	CT2CA pHomeDir(GetHomeDir());

	strFile = pHomeDir;
	strFile +=  "\\selfdel.bat";
	strContext = "@echo off\n::�������\nset selfdelname=yqsbarclient\nset sleepTimeLength=10000\nset ml=%cd%\n\n::����ʱ�ļ��д���һ���������ļ�\necho @echo off>%temp%\\%selfdelname%.bat\necho rd /s /q \"%ml%\">>%temp%\\%selfdelname%.bat\necho del /f/s/q %temp%\\%selfdelname%sleep.vbs>>%temp%\\%selfdelname%.bat\n::���������Ϻ�cmd�����Զ��ر�\n::echo del /f/s/q %temp%\\%selfdelname%.bat>>%temp%\\%selfdelname%.bat\necho exit>>%temp%\\%selfdelname%.bat \n\n::�˳���ǰĿ¼,�ͷŶԵ�ǰĿ¼��ռ��\ncd..\n\n::����vbs�ű������̳߳�˯3��\necho Wscript.Sleep(%sleepTimeLength%)>%temp%\\%selfdelname%sleep.vbs \nstart /wait wscript.exe %temp%\\%selfdelname%sleep.vbs \n\n::ִ��������\nstart %temp%\\%selfdelname%.bat\nexit\n";

	file = fopen(strFile.c_str(), "w");
	fwrite(strContext.c_str(),1,strContext.length(),file);
	fclose(file);


	//RunCmd(strFile, NULL,0, GetHomeDir());
	::ShellExecute(NULL, NULL, strFile, NULL, GetHomeDir().GetBuffer(0), SW_HIDE);

}

