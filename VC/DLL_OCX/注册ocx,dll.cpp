////////////////////////////////////////////
//	◊¢≤·ocx,dll(com¿‡–Õ)
///////////////////////////////////////////

	// this wouldn't work for a dynamically linked Regular DLL
	HINSTANCE h = ::LoadLibrary(strDllPath);
	if(h == NULL) {
		CString msg;
		msg.Format("Failed to find server %s", strDllPath);
		AfxMessageBox(msg);
		return FALSE;
	}
	FARPROC pFunc = ::GetProcAddress((HMODULE) h, "DllRegisterServer");
	if(pFunc == NULL) {
		AfxMessageBox("Failed to find DllRegisterServer function");
		return FALSE;
	}
	(*pFunc)();	// call the function to register the server
	AfxMessageBox("Server registered OK");
	return FALSE;