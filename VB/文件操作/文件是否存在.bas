'��VB������ж��ļ����ļ����Ƿ���� 

   if dir("c:wpswinwps.exe")<>"" then 
		shell "c:wpswinwps.exe" 
   end if 
    
  ' ������жϵ��ļ��������ļ�,�����������޷��жϳ���,��ʱ����Ҫ���Ϻ���Ŀ�ѡ��Ŀ,���� 
   ' �ж�C�̸�Ŀ¼���Ƿ��������ļ�command.com�����������Դ���룺 
    if dir("c:command.com,vbhidden")<>"" then 
			msgbox"Found c:command.com" 
   end if 
    
   ' �ж��ļ����Ƿ���ڣ�����������䣺 
  '  dir(�ļ���·��, vbDirectory) <>"" 
 '   ���磬Ҫ�ж��ļ���c:aaa�Ƿ����ڣ���������£� 
    if Dir("c:aaa", vbDirectory) <>"" then 
    ��msgbox"�ļ��У�c:aaa ���ڣ�" 
    end if 
     
