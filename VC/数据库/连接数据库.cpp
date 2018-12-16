#import "c:\program files\common files\system\ado\msado15.dll" \
	no_namespace \
rename ("EOF", "adoEOF")


	_ConnectionPtr m_pConnection;
	_CommandPtr m_pCommand;

   CoInitialize(NULL);
   HRESULT hr;
	try
	{
	hr = m_pConnection.CreateInstance("ADODB.Connection");///创建Connection对象
	if(SUCCEEDED(hr))
	{
	m_pConnection->ConnectionTimeout = 0;
	hr = m_pConnection->Open( "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.\\wbgl.mdb; Jet OLEDB:DataBase password=jfmoonsun"
							,"", "", adModeUnknown);
	m_pCommand.CreateInstance(__uuidof(Command));
	m_pCommand->CommandTimeout = 5;
	m_pCommand->ActiveConnection = m_pConnection;
	}
	}
	catch(_com_error e)///捕捉异常
	{
 		printf("Error:%s",e.ErrorMessage());
	}

	_RecordsetPtr m_pRecordset;
//    m_pRecordset.CreateInstance(__uuidof(Recordset)); 
	//创建记录集实例
	m_pRecordset.CreateInstance("ADODB.Recordset");

	// Define string variables.
	_bstr_t strSQL;

	//会员
	strSQL=("SELECT * FROM zhxx");

	try
	{
		m_pRecordset->Open(strSQL,
			m_pConnection.GetInterfacePtr(),  // 获取库接库的IDispatch指针
			adOpenDynamic,
			adLockOptimistic,
			adCmdText);
	}
	catch(_com_error *e)
	{
		printf("Recordset: %s",e->ErrorMessage());
		//	  printf("Recordset: %s",e->Description());
	}

	_variant_t var;     	
	
// 	__int64 i64Var;
// 	double dteVar;
// 	int iVar;
// 	char szVar[12];

	while(!m_pRecordset->adoEOF)
	{
	//------------------------------------------------------------------
//1
		var = m_pRecordset->GetCollect("bh");		
		Convert(&zhxxVar.lBh,sizeof(zhxxVar.lBh),var);
		
		var = m_pRecordset->GetCollect("qydm");		
		Convert(&zhxxVar.szQydm,sizeof(zhxxVar.szQydm),var);

		var = m_pRecordset->GetCollect("djdm");		
		Convert(&zhxxVar.szDjdm,sizeof(zhxxVar.szDjdm),var);

		var = m_pRecordset->GetCollect("xm");		
		Convert(&zhxxVar.szXm,sizeof(zhxxVar.szXm),var);

		var = m_pRecordset->GetCollect("zh");		
		Convert(&zhxxVar.szZh,sizeof(zhxxVar.szZh),var);
//2
		var = m_pRecordset->GetCollect("zjmc");		
		Convert(&zhxxVar.szZjmc,sizeof(zhxxVar.szZjmc),var);
		
		var = m_pRecordset->GetCollect("zjh");		
		Convert(&zhxxVar.szZjh,sizeof(zhxxVar.szZjh),var);

		var = m_pRecordset->GetCollect("kl");		
		Convert(&zhxxVar.szKl,sizeof(zhxxVar.szKl),var);

		var = m_pRecordset->GetCollect("ljyfe");		
		Convert(&zhxxVar.i64Ljyfe,sizeof(zhxxVar.i64Ljyfe),var);

		var = m_pRecordset->GetCollect("ye");		
		Convert(&zhxxVar.i64Ye,sizeof(zhxxVar.i64Ye),var);
//3
		var = m_pRecordset->GetCollect("zse");		
		Convert(&zhxxVar.i64Zse,sizeof(zhxxVar.i64Zse),var);
		
		var = m_pRecordset->GetCollect("ljzse");		
		Convert(&zhxxVar.i64Ljzse,sizeof(zhxxVar.i64Ljzse),var);
		
		var = m_pRecordset->GetCollect("khe");		
		Convert(&zhxxVar.i64Khe,sizeof(zhxxVar.i64Khe),var);
		
		var = m_pRecordset->GetCollect("khsj");		
		Convert(&zhxxVar.dKhsj,sizeof(zhxxVar.dKhsj),var);
		
		var = m_pRecordset->GetCollect("yxx");		
		Convert(&zhxxVar.sYxx,sizeof(zhxxVar.sYxx),var);
//4
		var = m_pRecordset->GetCollect("zxzt");		
		Convert(&zhxxVar.sZxzt,sizeof(zhxxVar.sZxzt),var);
		
		var = m_pRecordset->GetCollect("zhdlsj");		
		Convert(&zhxxVar.dZhdlsj,sizeof(zhxxVar.dZhdlsj),var);
		
		var = m_pRecordset->GetCollect("jysj");		
		Convert(&zhxxVar.dJysj,sizeof(zhxxVar.dJysj),var);
		
		var = m_pRecordset->GetCollect("czydh");		
		Convert(&zhxxVar.szCzydh,sizeof(zhxxVar.szCzydh),var);
		
		var = m_pRecordset->GetCollect("bz");		
		Convert(&zhxxVar.szBz,sizeof(zhxxVar.szBz),var);
//5
		var = m_pRecordset->GetCollect("lb");		
		Convert(&zhxxVar.lLb,sizeof(zhxxVar.lLb),var);
		
		var = m_pRecordset->GetCollect("czjf");		
		Convert(&zhxxVar.lCzjf,sizeof(zhxxVar.lCzjf),var);
		
		var = m_pRecordset->GetCollect("gjhy");		
		Convert(&zhxxVar.lGjhy,sizeof(zhxxVar.lGjhy),var);
		
		var = m_pRecordset->GetCollect("firstday");		
		Convert(&zhxxVar.dFirstday,sizeof(zhxxVar.dFirstday),var);
		
		var = m_pRecordset->GetCollect("lastday");		
 		Convert(&zhxxVar.dLastday,sizeof(zhxxVar.dLastday),var);
// 6
		var = m_pRecordset->GetCollect("js");		
		Convert(&zhxxVar.lJs,sizeof(zhxxVar.lJs),var);
		
		var = m_pRecordset->GetCollect("bybh");		
		Convert(&zhxxVar.szBybh,sizeof(zhxxVar.szBybh),var);
		
		var = m_pRecordset->GetCollect("xssc");		
		Convert(&zhxxVar.lXssc,sizeof(zhxxVar.lXssc),var);
		
		var = m_pRecordset->GetCollect("bygs");		
		Convert(&zhxxVar.lBygs,sizeof(zhxxVar.lBygs),var);
		
		var = m_pRecordset->GetCollect("sjje");		
		Convert(&zhxxVar.i64Sjje,sizeof(zhxxVar.i64Sjje),var);

	//------------------------------------------------------------------
		m_pRecordset->MoveNext();
	}
	
	m_pRecordset->Close();
	m_pConnection->Close();

   CoUninitialize();

