using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh.mhxy
{
    class Common
    {
        /// <summary>
        /// 获得角色名称
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static string GetMHName(int hwnd)
        {
            //加载角色名称
            string str = LoadDll.GetWindowsName((IntPtr)hwnd).Replace(" ", "");
            int start = str.LastIndexOf("-") + 1;
            if (start == 0)
            {
                return "mh";
            }
            int end = str.LastIndexOf("[");
            str = str.Substring(start, end - start);
            return str;
        }
        /// <summary>
        /// 获取梦幻西游的全部句柄信息
        /// </summary>
        /// <returns></returns>
        public static Dictionary<IntPtr, string> GetMHWindows()
        {
            Dictionary<IntPtr, string> wins = new Dictionary<IntPtr, string>();
            IntPtr ws = LoadDll.FindWindow(Global.mh_class, null);
            if (ws.ToInt32() > 0)
            {
                wins[ws] = LoadDll.GetWindowsName(ws);
                while (ws.ToInt32() > 0)
                {
                    ws = LoadDll.FindWindowEx(IntPtr.Zero, ws, Global.mh_class, null);
                    if (ws.ToInt32() > 0)
                    {
                        wins[ws] = LoadDll.GetWindowsName(ws);
                    }
                }

            }
            return wins;
        }

     
    }
}
