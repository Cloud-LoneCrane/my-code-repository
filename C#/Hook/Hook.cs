using System;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Jiftle.Windows
{
    //==============================================================================
    // 使用说明：
    //private Hook m_HookMain = new Hook();

    //private void Form1_Load(object sender, EventArgs e)
    //{
    //    m_HookMain.InstallHook();
    //    m_HookMain.OnKeyDown += new KeyEventHandler(this.HookMain_OnKeyDown);
    //    m_HookMain.OnKeyPress += new KeyPressEventHandler(this.HookMain_OnKeyPress);
    //    m_HookMain.OnKeyUp += new KeyEventHandler(this.HookMain_OnKeyUp);

    //}

    //private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    //{
    //    m_HookMain.UnInstallHook();
    //}

    //private void HookMain_OnKeyDown(object sender, KeyEventArgs e)
    //{
    //    Debug.Print(e.KeyCode.ToString());
    //}

    //public void HookMain_OnKeyUp(object sender, KeyEventArgs e)
    //{
    //    Debug.Print(e.KeyCode.ToString());
    //}

    //public void HookMain_OnKeyPress(object sender, KeyPressEventArgs e)
    //{
    //    Debug.Print(e.KeyChar.ToString());
    //}
    //==============================================================================


    class Hook
    {  

        #region Declares

        /// <summary>
        /// 键盘钩子句柄
        /// </summary>
        private IntPtr m_pKeyboardHook = IntPtr.Zero;

        /// <summary>
        /// 键盘钩子委托实例
        /// </summary>
        /// <remarks>
        /// 不要试图省略此变量,否则将会导致
        /// 激活 CallbackOnCollectedDelegate 托管调试助手 (MDA)。 
        /// 详细请参见MSDN中关于 CallbackOnCollectedDelegate 的描述
        /// </remarks>
        private HookProc m_KeyboardHookProcedure;


        #endregion

        #region Event

            public event KeyEventHandler OnKeyDown;
            public event KeyEventHandler OnKeyUp;
            public event KeyPressEventHandler OnKeyPress;

        #endregion

        #region Method

        public bool InstallHook()
        {
            IntPtr pInstance = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().ManifestModule);

            if (this.m_pKeyboardHook == IntPtr.Zero)
            {
                this.m_KeyboardHookProcedure = new HookProc(this.LowLevelKeyboardProc);
                this.m_pKeyboardHook = APIWrapper.SetWindowsHookEx(enmHookType.WH_KEYBOARD_LL,
                    this.m_KeyboardHookProcedure, pInstance, 0);
                if (this.m_pKeyboardHook == IntPtr.Zero)
                {
                    int nErrCode = Marshal.GetLastWin32Error();

                    this.UnInstallHook();
                    return false;
                }
            }

            return true;
        }


        public bool UnInstallHook()
        {
            bool result = true;
            if (this.m_pKeyboardHook != IntPtr.Zero)
            {
                result = (APIWrapper.UnhookWindowsHookEx(this.m_pKeyboardHook) && result);
                this.m_pKeyboardHook = IntPtr.Zero;
            }
            return result;
        }

        /// <summary>
        /// 键盘钩子处理函数
        /// </summary>
        /// <remarks>此版本的键盘事件处理不是很好,还有待修正.</remarks>
        private int LowLevelKeyboardProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode == 0)
            {
                //获得信息
                KeyboardLLHookStruct KeyboardInfo =
                (KeyboardLLHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardLLHookStruct));
                uint VKCode = KeyboardInfo.VKCode;

                if (this.OnKeyDown != null && (wParam == (int)enmKeyboardMessageType.WM_SYSKEYDOWN ||
                    wParam == (int)enmKeyboardMessageType.WM_KEYDOWN))
                {
                    Keys keyData = (Keys)KeyboardInfo.VKCode;
                    KeyEventArgs keyEvent = new KeyEventArgs(keyData);
                    this.OnKeyDown(this, keyEvent);
                }

                if (this.OnKeyPress != null && wParam == (Int32)enmKeyboardMessageType.WM_KEYUP)
                {
                    byte[] inBuffer = new byte[2];

                    /*
                     * 当ToAscii返回1个字符表示为按键，
                     * 为0表示转换失败
                     * 为2表示转换了2个字符，在KeyPressEventArgs中只有一个Char信息，所以此中情况将忽略。
                     * 一般在特殊键盘输入（如德语、法语等的注音）时发生。
                     */
                    if (APIWrapper.ToAscii(KeyboardInfo.VKCode,
                            KeyboardInfo.ScanCode,
                            this.m_KeyState,
                            inBuffer,
                            KeyboardInfo.Flags) == 1)
                    {
                        KeyPressEventArgs keyPressEvent = new KeyPressEventArgs((char)inBuffer[0]);
                        this.OnKeyPress(this, keyPressEvent);
                    }
                }

                if (this.OnKeyUp != null && (wParam == (Int32)enmKeyboardMessageType.WM_KEYUP ||
                    wParam == (Int32)enmKeyboardMessageType.WM_SYSKEYUP))
                {
                    Keys keyData = (Keys)KeyboardInfo.VKCode;
                    KeyEventArgs keyEvent = new KeyEventArgs(keyData);
                    this.OnKeyUp(this, keyEvent);
                }


            }

            return APIWrapper.CallNextHookEx(this.m_pKeyboardHook, nCode, wParam, lParam);
        }

        /// <summary>
        /// 按键状态数组
        /// </summary>
        private readonly byte[] m_KeyState = new byte[256];

        public Hook()
        {
            APIWrapper.GetKeyboardState(this.m_KeyState);
        }

        #endregion

    }

}
