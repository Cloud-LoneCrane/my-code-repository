
int intTmp=-1;

intTmp=GetSystemMetrics(SM_CLEANBOOT);
switch(intTmp)
{
	case 0:
		//normal mode
	case 1:
		//safe mode
	case 2:
		//safe mode with net
	default:
		//unknown
}
