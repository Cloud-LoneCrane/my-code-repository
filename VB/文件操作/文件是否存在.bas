'在VB中如何判断文件、文件夹是否存在 

   if dir("c:wpswinwps.exe")<>"" then 
		shell "c:wpswinwps.exe" 
   end if 
    
  ' 但如果判断的文件是隐藏文件,上面的语句则无法判断出来,这时就需要加上后面的可选项目,例如 
   ' 判断C盘根目录下是否有隐藏文件command.com，就用下面的源代码： 
    if dir("c:command.com,vbhidden")<>"" then 
			msgbox"Found c:command.com" 
   end if 
    
   ' 判断文件夹是否存在，可用下列语句： 
  '  dir(文件夹路径, vbDirectory) <>"" 
 '   例如，要判断文件夹c:aaa是否正在，则代码如下： 
    if Dir("c:aaa", vbDirectory) <>"" then 
    　msgbox"文件夹：c:aaa 存在！" 
    end if 
     
