//���õ�ǰ�̵߳����ԣ�����ͬʱ�����˶�����Ե���Դ����

#define LANGUAGE_ENGLISH 1

#if LANGUAGE_ENGLISH
		::SetThreadLocale(MAKELCID(MAKELANGID(LANG_ENGLISH,SUBLANG_DEFAULT),
			SORT_DEFAULT));
#else
		::SetThreadLocale(MAKELCID(MAKELANGID(LANG_CHINESE,SUBLANG_DEFAULT),
			SORT_DEFAULT));
#endif
