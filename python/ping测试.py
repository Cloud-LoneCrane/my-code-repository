#!/usr/bin/python
#-*- coding:gb18030 -*-
'''
��д�ˣ�jiftle
��дʱ��: 2015.2.17
#�ж��ļ��е�ip�Ƿ���pingͨ�����ҽ�ͨ�벻ͨ��ip�ֱ�д�������ļ���
#�ļ��е�ipһ��һ��
#QQ:136354553
'''
import time,os

#�ȴ��û��������
hostipFileName = raw_input('��������Ҫping����ip���ļ���:');

print(hostipFileName);

start_Time=int(time.time()) #��¼��ʼʱ��
def ping_Test():
    ips=open(hostipFileName,'r')
    ip_True = open(hostipFileName + '_pingTrue.txt','w')
    ip_False = open(hostipFileName + '_pingFalse.txt','w')
    count_True,count_False=0,0
    for ip in ips.readlines():
        ip = ip.replace('\n','')  #�滻�����з�
        return1=os.system('ping -n 2 -w 1 %s'%ip) #ÿ��ip ping2�Σ��ȴ�ʱ��Ϊ1s
        if return1:
            print 'ping %s is fail'%ip
            ip_False.write(ip)  #��ping��ͨ��д��ip_False.txt��
            count_False += 1
        else:
            print 'ping %s is ok'%ip
            ip_True.write(ip)  #��pingͨ��ipд��ip_True.txt��
            count_True += 1
    ip_True.close()
    ip_False.close()
    ips.close()
    end_Time = int(time.time())  #��¼����ʱ��
    print "time(��)��",end_Time - start_Time,"s"  #��ӡ�������õ�ʱ��
    print "pingͨ����",count_True,"   ping��ͨ��ip����",count_False
    raw_input();
ping_Test()