   
//һ   CStringתchar*  
    CString str=_T("Hello I'm a hero.");
    char buf[25];
    strcpy(buf,(LPCTSTR)str);
    cout<<buf<<endl;
    
//��   char* תCString 
    char*buf="Hello World.";
    CString str;
    str.Format("%s",buf);
    cout<<(LPCTSTR)str<<endl;
    
//http://www.vckbase.com/document/viewdoc/?id=1094
    