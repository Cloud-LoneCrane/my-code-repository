/// <summary>
/// 执行应用程序
/// </summary>
/// <param name="workingDirectory">工作目录</param>
/// <param name="exePath">应用程序路径</param>
/// <param name="strArg">参数</param>
/// <param name="strErr"></param>
/// <returns>调用例子:string strErr="";	bool blnResult = ExecExe(null,"msiexec", "/i ///D:\\mysql-connector-odbc-5.1.5-win32.msi /quiet",ref strErr);
///if(!blnResult) MessageBox.Show("mysql ODBC安装失败","安装程序");
///</returns>
public static bool ExecExe(string workingDirectory,string exePath,string strArguments,ref string strErr)
{
	System.Diagnostics.Process p = new System.Diagnostics.Process();
	p.StartInfo.FileName = exePath;
	p.StartInfo.Arguments = strArguments;
	p.StartInfo.UseShellExecute = false;
	p.StartInfo.RedirectStandardError = true;
	p.StartInfo.RedirectStandardOutput = true;

	p.Start();
	p.WaitForExit(); //组件无限期的等待关联进程退出

	String strOut;
	strOut = null;
	strOut = p.StandardOutput.ReadToEnd();
	strErr = p.StandardError.ReadToEnd();

	if(0!=p.ExitCode)
	{
		strErr = "ExitCode:" + p.ExitCode;
		return false;
	}
	
	if((strOut.Length!=0)||(strErr.Length!=0))
	{
		strErr = "";
		Console.WriteLine(strOut);
		return false;
	}
	else
	{
		Console.WriteLine(strOut);
		return true;
	}
}