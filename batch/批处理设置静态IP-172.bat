::【批处理设置静态IP】**********  复制以下内容保存为bat
netsh interface ip  set address name="本地连接" source = static addr = 172.16.1.199  mask = 255.255.0.0     
netsh interface ip set address  name="本地连接" gateway = 172.16.1.1 gwmetric =0
