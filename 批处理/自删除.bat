::============================================
::自删除
::备注：
::      删除比较危险，所以注释掉start %temp%\\%selfdelname%.bat
::  需要删除时，去掉注释即可
::============================================
@echo off

::定义变量
set selfdelname=yqsbarclient
set sleepTimeLength=1000
set ml=%cd%

::在临时文件夹创建一个批处理文件
echo @echo off>%temp%\\%selfdelname%.bat
echo rd /s /q "%ml%">>%temp%\\%selfdelname%.bat
echo del /f/s/q %temp%\\%selfdelname%sleep.vbs>>%temp%\\%selfdelname%.bat
::下面这句加上后，cmd不会自动关闭
::echo del /f/s/q %temp%\\%selfdelname%.bat>>%temp%\\%selfdelname%.bat
echo exit>>%temp%\\%selfdelname%.bat 

::退出当前目录,释放对当前目录的占用
cd..

::创建vbs脚本，让线程沉睡3秒
echo Wscript.Sleep(%sleepTimeLength%)>%temp%\\%selfdelname%sleep.vbs 
start /wait wscript.exe %temp%\\%selfdelname%sleep.vbs 

::执行批处理
::start %temp%\\%selfdelname%.bat
exit 
