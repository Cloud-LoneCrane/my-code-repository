#include "iostream"
#include "windows.h"
using namespace std;   

void show_file(char path[], int level = 0)
{
    char find_path[128];
    sprintf(find_path, "%s*", path);

    WIN32_FIND_DATA FindFileData;
    HANDLE hFind;
    BOOL bContinue = TRUE;

    hFind = FindFirstFile(find_path, &FindFileData);

    if (hFind == INVALID_HANDLE_VALUE) 
        return;

    while (bContinue)
    {
        if (stricmp(FindFileData.cFileName, "..") && stricmp(FindFileData.cFileName, "."))
        {
            for (int i = 0; i < level; ++i)
                cout<<"    ";
            cout<<FindFileData.cFileName<<endl;

            if (FindFileData.dwFileAttributes == FILE_ATTRIBUTE_DIRECTORY)
            {
                sprintf(find_path, "%s%s\\", path, FindFileData.cFileName);
                show_file(find_path, level + 1);
            }
        }
        bContinue = FindNextFile(hFind, &FindFileData);
    }
	
}

int main()   
{   
    char szPath[] = "E:\\Á·Ï°\\VC\\console\\";
	
    show_file(szPath);
	
    return 0;
}   

