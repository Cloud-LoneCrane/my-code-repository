#import "c:\Program Files\Common Files\System\ADO\msado15.dll" \
   no_namespace rename("EOF", "adoEOF")
#include <stdio.h>

void main(void)
{
	_ConnectionPtr m_pConnection;
	_CommandPtr m_pCommand;

   CoInitialize(NULL);
   HRESULT hr;
try
{
hr = m_pConnection.CreateInstance("ADODB.Connection");///����Connection����
if(SUCCEEDED(hr))
{
m_pConnection->ConnectionTimeout = 0;
hr = m_pConnection->Open( "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=e:\\Student.mdb", 
                                                 "", "", adModeUnknown);

m_pCommand.CreateInstance(__uuidof(Command));
m_pCommand->CommandTimeout = 5;
m_pCommand->ActiveConnection = m_pConnection;
}
}
catch(_com_error e)///��׽�쳣
{
	printf("err:%s",e.ErrorMessage());
}

		_RecordsetPtr m_pRecordset;
        m_pRecordset.CreateInstance(__uuidof(Recordset)); //������¼��ʵ��

        try
        {
        m_pRecordset->Open("SELECT * FROM studentInfo",// ��ѯDemoTable���������ֶ�
        m_pConnection.GetInterfacePtr(),  // ��ȡ��ӿ��IDispatchָ��
        adOpenDynamic,
        adLockOptimistic,
        adCmdText);
        }
        catch(_com_error *e)
        {
              //  AfxMessageBox(e->ErrorMessage());
			printf("Recordset: %s",e->ErrorMessage());
        }
		
		 _variant_t var; 
      
        
        while(!m_pRecordset->adoEOF)
        {
			var = m_pRecordset->GetCollect("stuName");
            if(var.vt != VT_NULL)
				printf("%s\n",(LPCSTR)_bstr_t(var));
            
			m_pRecordset->MoveNext();
		}
   CoUninitialize();
}
