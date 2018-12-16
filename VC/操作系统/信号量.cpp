

//信号量
	HANDLE hSem = CreateSemaphore(NULL, 1, 1, _T("jiftle.posts.sem.activate"));
	if(!hSem || GetLastError() == ERROR_ALREADY_EXISTS){
		MessageBeep(0);  //解决咚咚响
		return FALSE;
	}
