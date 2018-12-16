#!/usr/bin/env python  
#encoding: utf-8
'''
编写人：李旭
编写时间：2015.2.17
备注：来源
python在windows的cmd中打印彩色文字
http://blog.csdn.net/five3/article/details/7630295
在基础上修改
'''
import ctypes

STD_INPUT_HANDLE = -10
STD_OUTPUT_HANDLE= -11
STD_ERROR_HANDLE = -12

FOREGROUND_BLACK = 0x0
FOREGROUND_BLUE = 0x01 # text color contains blue. 蓝色
FOREGROUND_GREEN= 0x02 # text color contains green. 绿色
FOREGROUND_RED = 0x04 # text color contains red. 红色
FOREGROUND_INTENSITY = 0x08 # text color is intensified.

BACKGROUND_BLUE = 0x10 # background color contains blue.
BACKGROUND_GREEN= 0x20 # background color contains green.
BACKGROUND_RED = 0x40 # background color contains red.
BACKGROUND_INTENSITY = 0x80 # background color is intensified.

class Color:
    ''' See http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winprog/winprog/windows_api_reference.asp
    for information on Windows APIs.'''
    std_out_handle = ctypes.windll.kernel32.GetStdHandle(STD_OUTPUT_HANDLE)
    
    def set_cmd_color(self, color, handle=std_out_handle):
        """(color) -> bit
        Example: set_cmd_color(FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE | FOREGROUND_INTENSITY)
        """
        bool = ctypes.windll.kernel32.SetConsoleTextAttribute(handle, color)
        return bool
    
    def reset_color(self):
        self.set_cmd_color(FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE)
    
    def print_red_text(self, print_text):
        self.set_cmd_color(FOREGROUND_RED | FOREGROUND_INTENSITY)
        print print_text
        self.reset_color()
        
    def print_green_text(self, print_text):
        self.set_cmd_color(FOREGROUND_GREEN | FOREGROUND_INTENSITY)
        print print_text
        self.reset_color()
    
    def print_blue_text(self, print_text): 
        self.set_cmd_color(FOREGROUND_BLUE | FOREGROUND_INTENSITY)
        print print_text
        self.reset_color()
          
    def print_red_text_with_blue_bg(self, print_text):
        self.set_cmd_color(FOREGROUND_RED | FOREGROUND_INTENSITY| BACKGROUND_BLUE | BACKGROUND_INTENSITY)
        print print_text
        self.reset_color()    

    #设置当前字体颜色为红色
    def setFontColor(self,ColorName):
        if ColorName == 'red':
            self.set_cmd_color(FOREGROUND_RED|FOREGROUND_INTENSITY);
        elif ColorName == 'green':
            self.set_cmd_color(FOREGROUND_GREEN | FOREGROUND_INTENSITY)
        elif ColorName == 'blue':
            self.set_cmd_color(FOREGROUND_BLUE | FOREGROUND_INTENSITY)
        else:
            print('设置字体颜色错误，未定义的颜色值');
    #恢复字体颜色为默认
    def resetColor(self):
         self.set_cmd_color(FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE)
         
            
        
if __name__ == "__main__":
    clr = Color()

    
    clr.print_red_text('red')
    clr.print_green_text('green')
    clr.print_blue_text('blue')
    clr.print_red_text_with_blue_bg('background')

    clr.setFontColor('red');

    print('放假的我真懒啊');
    clr.resetColor();
    print('颜色恢复了');
    raw_input();
