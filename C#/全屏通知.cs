using System.Runtime.InteropServices;
using System.Windows;

//使用说明：
//===============================================================
// 定义
// int uCallBackMsg = 0;
// bool RunningFullScreenApp = false;
// //注册
// private void RegisterAppBar(bool registered)
// {
//     APPBARDATA abd = new APPBARDATA();
//     abd.cbSize = (uint)Marshal.SizeOf(abd);
//     abd.hWnd = this.Handle;

//     if (!registered)
//     {
//         //register
//         uCallBackMsg = APIWrapper.RegisterWindowMessage("APPBARMSG_CSDN_HELPER");
//         abd.uCallbackMessage = (uint)uCallBackMsg;
//         uint ret = APIWrapper.SHAppBarMessage((int)ABMsg.ABM_NEW, ref abd);
//     }
//     else
//     {
//         APIWrapper.SHAppBarMessage((int)ABMsg.ABM_REMOVE, ref abd);
//     }
// }
// //窗口加载时注册
// private void Form1_Load(object sender, EventArgs e)
// {
//     this.RegisterAppBar(false);
// }
// //窗口关闭时卸载
//private void Form1_FormClosed(object sender, FormClosingEventArgs e)
// {
//     this.RegisterAppBar(true);
// }
// //消息处理方法
//protected override void WndProc(ref System.Windows.Forms.Message m)
//       {
//           if (m.Msg == uCallBackMsg)
//           {
//               switch (m.WParam.ToInt32())
//               {
//                   case (int)ABNotify.ABN_FULLSCREENAPP:
//                       {
//                           if ((int)m.LParam == 1)
//                               this.RunningFullScreenApp = true;
//                           else
//                               this.RunningFullScreenApp = false;
//                           break;
//                       }
//                   default:
//                       break;
//               }
//           }

//           base.WndProc(ref m);
//       }
//===============================================================
namespace Jiftle.Windows
{
    #region Declares

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public override string ToString()
        {
            return "{left=" + left.ToString() + ", " + "top=" + top.ToString() + ", " +
            "right=" + right.ToString() + ", " + "bottom=" + bottom.ToString() + "}";
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct APPBARDATA
    {
        public uint cbSize;
        public IntPtr hWnd;
        public uint uCallbackMessage;
        public uint uEdge;
        public RECT rc;
        public IntPtr lParam;
    }

    public enum ABMsg : int
    {
        ABM_NEW = 0,
        ABM_REMOVE,
        ABM_QUERYPOS,
        ABM_SETPOS,
        ABM_GETSTATE,
        ABM_GETTASKBARPOS,
        ABM_ACTIVATE,
        ABM_GETAUTOHIDEBAR,
        ABM_SETAUTOHIDEBAR,
        ABM_WINDOWPOSCHANGED,
        ABM_SETSTATE
    }

    public enum ABNotify : int
    {
        ABN_STATECHANGE = 0,
        ABN_POSCHANGED,
        ABN_FULLSCREENAPP,
        ABN_WINDOWARRANGE
    }

    public enum ABEdge : int
    {
        ABE_LEFT = 0,
        ABE_TOP,
        ABE_RIGHT,
        ABE_BOTTOM
    }

    #endregion
    
    //api wrapper class
    public class APIWrapper
    {
        #region Method

            [DllImport("SHELL32", CallingConvention = CallingConvention.StdCall)]
            public static extern uint SHAppBarMessage(int dwMessage, ref APPBARDATA pData);

            [DllImport("User32.dll", CharSet = CharSet.Auto)]
            public static extern int RegisterWindowMessage(string msg);

        #endregion
    }

    

}