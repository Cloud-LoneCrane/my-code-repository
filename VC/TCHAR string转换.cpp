

CString cs ("Hello");

// Convert a TCHAR string to a LPCSTR
CT2CA pszConvertedAnsiString (cs);

// construct a std::string using the LPCSTR input
std::string strStd (pszConvertedAnsiString);
