

//�ź���
	HANDLE hSem = CreateSemaphore(NULL, 1, 1, _T("jiftle.posts.sem.activate"));
	if(!hSem || GetLastError() == ERROR_ALREADY_EXISTS){
		MessageBeep(0);  //���������
		return FALSE;
	}
