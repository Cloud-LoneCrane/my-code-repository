/// <summary>
/// ִ��Ӧ�ó���
/// </summary>
/// <param name="workingDirectory">����Ŀ¼</param>
/// <param name="exePath">Ӧ�ó���·��</param>
/// <param name="strArg">����</param>
/// <param name="strErr"></param>
/// <returns>��������:string strErr="";	bool blnResult = ExecExe(null,"msiexec", "/i ///D:\\mysql-connector-odbc-5.1.5-win32.msi /quiet",ref strErr);
///if(!blnResult) MessageBox.Show("mysql ODBC��װʧ��","��װ����");
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
	p.WaitForExit(); //��������ڵĵȴ����������˳�

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