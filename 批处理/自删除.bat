::============================================
::��ɾ��
::��ע��
::      ɾ���Ƚ�Σ�գ�����ע�͵�start %temp%\\%selfdelname%.bat
::  ��Ҫɾ��ʱ��ȥ��ע�ͼ���
::============================================
@echo off

::�������
set selfdelname=yqsbarclient
set sleepTimeLength=1000
set ml=%cd%

::����ʱ�ļ��д���һ���������ļ�
echo @echo off>%temp%\\%selfdelname%.bat
echo rd /s /q "%ml%">>%temp%\\%selfdelname%.bat
echo del /f/s/q %temp%\\%selfdelname%sleep.vbs>>%temp%\\%selfdelname%.bat
::���������Ϻ�cmd�����Զ��ر�
::echo del /f/s/q %temp%\\%selfdelname%.bat>>%temp%\\%selfdelname%.bat
echo exit>>%temp%\\%selfdelname%.bat 

::�˳���ǰĿ¼,�ͷŶԵ�ǰĿ¼��ռ��
cd..

::����vbs�ű������̳߳�˯3��
echo Wscript.Sleep(%sleepTimeLength%)>%temp%\\%selfdelname%sleep.vbs 
start /wait wscript.exe %temp%\\%selfdelname%sleep.vbs 

::ִ��������
::start %temp%\\%selfdelname%.bat
exit 
