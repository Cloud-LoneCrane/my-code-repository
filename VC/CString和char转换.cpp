   
//一   CString转char*  
    CString str=_T("Hello I'm a hero.");
    char buf[25];
    strcpy(buf,(LPCTSTR)str);
    cout<<buf<<endl;
    
//二   char* 转CString 
    char*buf="Hello World.";
    CString str;
    str.Format("%s",buf);
    cout<<(LPCTSTR)str<<endl;
    
//http://www.vckbase.com/document/viewdoc/?id=1094
    