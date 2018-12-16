using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace Jiftle
{
    public class Funcs
    {
#region MessageBox

        static string strCaption = "提示";

        public static void jMsgInfo(string strTip)
        {
            MessageBox.Show(strTip.PadRight(15), strCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void jMsgWarn(string strTip)
        {
            MessageBox.Show(strTip.PadRight(15), strCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void jMsgErr(string strTip)
        {
            MessageBox.Show(strTip.PadRight(15), strCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

#endregion

        #region RequiredFieldValidator

        public static bool IsEmpty(Control ctrlTmp,string strCtrlName)
        {
            if (ctrlTmp.Text.Length == 0)
            {
                jMsgWarn(string.Format("{0}不能为空！",strCtrlName));
                return true;
            }
             
            else
            {
                return false;
            }
        }


        #endregion

    }

}
