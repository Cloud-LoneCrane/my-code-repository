#!/usr/bin/python
#-*- coding:gb18030 -*-
'''
编写人：jiftle
编写时间: 2015.2.17
#判断文件中的ip是否能ping通，并且将通与不通的ip分别写到两个文件中
#文件中的ip一行一个
#QQ:136354553
'''
import time,os

#等待用户输入参数
hostipFileName = raw_input('请输入需要ping测试ip的文件名:');

print(hostipFileName);

start_Time=int(time.time()) #记录开始时间
def ping_Test():
    ips=open(hostipFileName,'r')
    ip_True = open(hostipFileName + '_pingTrue.txt','w')
    ip_False = open(hostipFileName + '_pingFalse.txt','w')
    count_True,count_False=0,0
    for ip in ips.readlines():
        ip = ip.replace('\n','')  #替换掉换行符
        return1=os.system('ping -n 2 -w 1 %s'%ip) #每个ip ping2次，等待时间为1s
        if return1:
            print 'ping %s is fail'%ip
            ip_False.write(ip)  #把ping不通的写到ip_False.txt中
            count_False += 1
        else:
            print 'ping %s is ok'%ip
            ip_True.write(ip)  #把ping通的ip写到ip_True.txt中
            count_True += 1
    ip_True.close()
    ip_False.close()
    ips.close()
    end_Time = int(time.time())  #记录结束时间
    print "time(秒)：",end_Time - start_Time,"s"  #打印并计算用的时间
    print "ping通数：",count_True,"   ping不通的ip数：",count_False
    raw_input();
ping_Test()