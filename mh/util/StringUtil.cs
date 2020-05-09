using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace mh
{
    class StringUtil
    {

        //byte[]转换为struct
        public static object BytesToStruct(byte[] bytes, Type type)
        {
            int size = Marshal.SizeOf(type);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes, 0, buffer, size);
                return Marshal.PtrToStructure(buffer, type);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
        //struct转换为byte[]
        public static byte[] StructToBytes(object structObj, int size = 0)
        {
            if (size == 0)
            {
                size = Marshal.SizeOf(structObj); //得到结构体大小
            }
            IntPtr buffer = Marshal.AllocHGlobal(size);  //开辟内存空间
            try
            {
                Marshal.StructureToPtr(structObj, buffer, false);   //填充内存空间
                byte[] bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, size);   //填充数组
                return bytes;
            }
            catch (Exception)
            {

                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);   //释放内存
            }
        }

        /// <summary>
        /// 字节追加
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static byte[] AppendBytes(params byte[][] parms)
        {
            List<byte> byteSource = new List<byte>();
            for (int i = 0; i < parms.Length; i++)
            {
                byteSource.AddRange(parms[i]);
            }
            return byteSource.ToArray();

        }

        /// <summary>
        /// 查找字符集
        /// </summary>
        /// <param name="old"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static int FindBytes(byte[] old, byte[] v, int start)
        {
            for (int i = start; i < old.Length; i++)
            {
                if (old[i] == v[0])
                {
                    bool ret = true;
                    for (int j = 0; j < v.Length; j++)
                    {
                        if (v[j] != old[i + j])
                        {
                            ret = false;
                        }
                    }
                    if (ret)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        public static byte[] ReplaceBytes(byte[] data, int s, int num, byte[] ndata)
        {

            //获取前半段数据
            byte[] sd = data.Skip(0).Take(s - 1).ToArray();
            byte[] se = data.Skip(s + num - 1).Take(data.Length - (s + num - 1)).ToArray();
            byte[] ret = new byte[sd.Length + ndata.Length + se.Length];
            sd.CopyTo(ret, 0);
            ndata.CopyTo(ret, sd.Length);
            se.CopyTo(ret, ndata.Length + sd.Length);

            return ret;
        }

        /// <summary>
        /// 获取16进制数组中的汉字
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static string GetByteHexData(byte[] data, int offset)
        {
            data = data.Skip(offset).Take(data.Length - offset).ToArray();
            return StringUtil.GetChsFromHex(StringUtil.byteToHexStr(data));
        }
        /// <summary>
        /// 查找字节在数组的位置
        /// </summary>
        /// <param name="data"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static int FindByte(byte[] data, int start, byte z)
        {
            int idx = -1;
            for (int i = start; i < data.Length; i++)
            {
                if (data[i] == z)
                {

                    return i;
                }
            }
            return idx;
        }
        /// <summary>
        /// 查找最后字节出现的位置
        /// </summary>
        /// <param name="data"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static int FindByteLastOf(byte[] data, byte z)
        {
            int idx = -1;
            for (int i = data.Length - 1; i > 0; i--)
            {
                if (data[i] == z)
                {

                    return i;
                }
            }
            return idx;
        }
        /// <summary>
        /// 获取指针数据
        /// </summary>
        /// <param name="adr">指针地址</param>
        /// <param name="size">数据大小</param>
        /// <returns></returns>
        public static byte[] GetPtrData(IntPtr addr, int size)
        {
            byte[] data = new byte[size];
            Marshal.Copy(addr, data, 0, size);
            return data;
        }

        /// <summary>
        /// 指针到整数
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        public static int GetPtrToInt(IntPtr addr)
        {
            byte[] data = new byte[4];
            Marshal.Copy(addr, data, 0, 4);
            return BitConverter.ToInt32(data, 0);
        }
        /// <summary>
        /// 整数转换16进制地址
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string FormatIntToHex(int v)
        {

            string ret = String.Format("{0:X}", v);
            int len = 8 - ret.Length;
            if (len > 0)
            {
                for (int i = 0; i < len; i++)
                {
                    ret = "0" + ret;
                }
            }
            string two = ret.Substring(ret.Length - 2, 2);
            ret = ret.Substring(0, 6);
            two = two + ret.Substring(ret.Length - 2, 2);
            ret = ret.Substring(0, 4);
            two = two + ret.Substring(ret.Length - 2, 2);
            ret = ret.Substring(0, 2);
            two = two + ret.Substring(0, 2);

            return two;
        }
        /// <summary>
        /// 从汉字转换到16进制
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetHexFromChs(string s)
        {
            if ((s.Length % 2) != 0)
            {
                s += " ";//空格
                         //throw new ArgumentException("s is not valid chinese string!");
            }

            System.Text.Encoding chs = System.Text.Encoding.GetEncoding("gb2312");

            byte[] bytes = chs.GetBytes(s);

            string str = "";

            for (int i = 0; i < bytes.Length; i++)
            {
                str += string.Format("{0:X}", bytes[i]);
            }

            return str;
        }
        /// <summary>
        /// 从16进制转换成汉字
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static string GetChsFromHex(string hex)
        {
            if (hex == null)
                throw new ArgumentNullException("hex");
            if (hex.Length % 2 != 0)
            {
                hex += "20";//空格
            }
            // 需要将 hex 转换成 byte 数组。
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    // 每两个字符是一个 byte。
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                        System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    // Rethrow an exception with custom message.
                    throw new ArgumentException("hex is not a valid hex number!", "hex");
                }
            }

            // 获得 GB2312，Chinese Simplified。
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding("gb2312");


            return chs.GetString(bytes);
        }
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }



        //将16进制的字符串转为byte[]
        public static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Trim().Replace(" ", "");
            if (hexString.Length % 2 != 0)
            {
                hexString += "0";
            }
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
            {
                int idx = i * 2;
                string t = hexString.Substring(i * 2, 2);
                if (i == 72)
                {

                }
                returnBytes[i] = Convert.ToByte(t, 16);
            }

            return returnBytes;
        }
        /// <summary>
        /// 格式化字节数组到16进制格式
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string FormatBytesToHex(byte[] data)
        {
            //处理16进制数据
            StringBuilder line = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                line.Append(FormatIntToHexStyle(data[i], true));
            }
            return line.ToString();
        }
        /// <summary>
        /// 格式化16进制样式 
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static string FormatHexStyle(string hex)
        {
            byte[] d = strToToHexByte(hex);
            return FormatBytesToHex(d);
        }
        /// <summary>
        /// 转换整数的16进制字节数组
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string FormatIntToHexStyle(int v, bool addSpace)
        {
            string ret = "";
            string space = " ";
            if (addSpace == false) { space = ""; }
            if (v < 16)
            {
                ret = "0" + String.Format("{0:X}", v) + space;
            }
            else if (v <= 255)
            {
                ret = String.Format("{0:X}", v) + space;

            }
            else
            {
                byte[] b = BitConverter.GetBytes(v);
                ret += FormatIntToHexStyle(b[0], addSpace);
                ret += FormatIntToHexStyle(b[1], addSpace);
                ret += FormatIntToHexStyle(b[2], addSpace);
                ret += FormatIntToHexStyle(b[3], addSpace);
            }
            return ret;
        }
        /// <summary>
        /// GB2312转换成UTF8
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string gb2312_utf8(string text)
        {
            //声明字符集   
            System.Text.Encoding utf8, gb2312;
            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            byte[] gb;
            gb = gb2312.GetBytes(text);
            gb = System.Text.Encoding.Convert(gb2312, utf8, gb);
            //返回转换后的字符   
            return utf8.GetString(gb);
        }
        /// <summary>
        /// 八位字符串取反
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string OtcBitReverse(string body)
        {
            string[] arry = new string[4];
            switch (body.Length)
            {
                case 8:
                    arry[0] = body.Substring(6, 2);
                    arry[1] = body.Substring(4, 2);
                    arry[2] = body.Substring(2, 2);
                    arry[3] = body.Substring(0, 2);
                    break;
                case 7:
                    body = "0" + body;
                    arry[0] = body.Substring(6, 2);
                    arry[1] = body.Substring(4, 2);
                    arry[2] = body.Substring(2, 2);
                    arry[3] = body.Substring(0, 2);
                    break;
                case 6:
                    arry[0] = body.Substring(4, 2);
                    arry[1] = body.Substring(2, 2);
                    arry[2] = body.Substring(0, 2);
                    return arry[0] + arry[1] + arry[2] + "00";
                case 5:
                    body = "0" + body;
                    arry[0] = body.Substring(4, 2);
                    arry[1] = body.Substring(2, 2);
                    arry[2] = body.Substring(0, 2);
                    return arry[0] + arry[1] + arry[2] + "00";
                case 4:
                    arry[0] = body.Substring(2, 2);
                    arry[1] = body.Substring(0, 2);
                    return arry[0] + arry[1] + "0000";
                case 3:
                    body = "0" + body;
                    arry[0] = body.Substring(2, 2);
                    arry[1] = body.Substring(0, 2);
                    return arry[0] + arry[1] + "0000";
                case 2:
                    return body + "000000";
                case 1:
                    return "0" + body + "000000";
                case 0:
                    return "00000000";
            }
            return arry[0] + arry[1] + arry[2] + arry[3];
        }
        /// <summary>
        /// 过滤控件输入的非法字符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void FilertTxtNumber(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键
            if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数
            if (e.KeyChar > 0x20)
            {
                try
                {
                    double.Parse(((TextBox)sender).Text + e.KeyChar.ToString());
                }
                catch
                {
                    e.KeyChar = (char)0;   //处理非法字符
                }
            }
        }
        /// <summary>
        /// 整数到16进制
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string IntToHex(int v)
        {
            return v.ToString("X2").PadLeft(8, '0');
        }

    }


}
