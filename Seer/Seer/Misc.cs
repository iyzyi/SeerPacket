using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Seer
{
    class Misc
    {
        #region 字节数组转换为十六进制字符串
        static public string ByteArray2HexString(byte[] ys)
        {
            String hex = "";
            for (int i = 0; i < ys.Length; i++)
            {
                hex += ys[i].ToString("X2") + " ";
            }
            return hex;
        }
        #endregion


        #region 字节数组的前n个数据转换为十六进制字符串
        static public string ByteArray2HexString(byte[] ys, int n)
        {
            String hex = "";
            for (int i = 0; i < n; i++)
            {
                hex += ys[i].ToString("X2") + " ";
            }
            return hex;
        }
        #endregion


        #region 十六进制字符串转换为字节数组
        public static byte[] HexString2ByteArray(string hex)
        {
            hex = hex.Replace(" ", "");
            if (hex.Length % 2 != 0)
            {
                Console.WriteLine("十六进制字符串的长度必须为偶数");
                return null;
            }
            else
            {
                byte[] by = new byte[(int)(hex.Length / 2)];
                for (int i = 0; i < hex.Length; i += 2)
                {
                    string h = hex.Substring(i, 2);
                    by[(int)(i / 2)] = (byte)(Convert.ToInt32(h, 16));
                }
                return by;
            }
        }
        #endregion


        #region 从byte数组的索引位置处取四个byte，然后转换为小端格式，最后转换为int
        static public int GetIntParam(byte[] plain, int index)
        //参数：解密后的封包（即明文），索引位置
        //从byte数组的索引位置处取四个byte，然后转换为小端格式，最后转换为int.
        //用于从封包中提取长度、命令号、米米号、序列号
        {
            byte[] temp = new byte[4];
            Array.Copy(plain, index, temp, 0, 4);   //从plain数组的第index个数据开始，复制4个byte类型数据到temp数组中
            Array.Reverse(temp);                    //大端转换为小端
            return BitConverter.ToInt32(temp, 0);   //长度为4的byte类型数组转换为一个int
        }
        #endregion


        #region int转换为长度为4的byte数组
        public static byte[] Int2ByteArray(int v)
        {
            byte[] b = new byte[4];
            b[3] = (byte)(v);
            b[2] = (byte)(v >> 8);
            b[1] = (byte)(v >> 16);
            b[0] = (byte)(v >> 24);
            return b;
        }
        #endregion


        #region byte数组切片
        public static byte[] ArraySlice(byte[] source, int startIndex, int endIndex)
        //https://blog.csdn.net/jhqin/article/details/6691935?utm_medium=distribute.pc_relevant.none-task-blog-BlogCommendFromMachineLearnPai2-5.nonecase&depth_1-utm_source=distribute.pc_relevant.none-task-blog-BlogCommendFromMachineLearnPai2-5.nonecase
        {
            int length = endIndex - startIndex;

            if (startIndex < 0 || startIndex > source.Length || length < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            byte[] Destination;
            if (endIndex <= source.Length)
            {
                Destination = new byte[length];
                Array.Copy(source, startIndex, Destination, 0, length);
            }
            else
            {
                Destination = new byte[source.Length - startIndex];
                Array.Copy(source, startIndex, Destination, 0, source.Length - startIndex);
            }

            return Destination;
        }
        #endregion


        #region 合并两个byte数组
        public static byte[] ArrayMerge(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            a.CopyTo(c, 0);
            b.CopyTo(c, a.Length);
            return c;
        }
        #endregion


        #region MD5
        public static string GetMD5(byte[] byteOld)
        //https://www.jianshu.com/p/f1f68b5bc234
        {
            MD5 md5 = MD5.Create();
            byte[] byteNew = md5.ComputeHash(byteOld);
            // 将加密结果转换为字符串
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteNew)
            {
                // 将字节转换成16进制表示的字符串，
                sb.Append(b.ToString("x2"));
            }
            // 返回加密的字符串
            return sb.ToString();
        }
        #endregion


        #region byte[]转IntPtr
        public static IntPtr BytesToIntptr(byte[] bytes)
        {
            //https://blog.csdn.net/weixin_34290352/article/details/86118109
            int size = bytes.Length;
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes, 0, buffer, size);
                return buffer;
            }
            finally
            {
                //Console.WriteLine("finally");
                //Marshal.FreeHGlobal(buffer);
            }
        }
        #endregion


        #region 检查十六进制字符串是否具有正确的形式
        public static bool CheckHexString(string str)
        {
            //字符串需形如"1C 09 01 "或"1C 09 01"，最后一个空格可以没有
       
            string pattern = @"^([0-9a-fA-F]{2} )*[0-9a-fA-F]{2} ?$";

            MatchCollection mc = Regex.Matches(str, pattern);

            if (mc.Count == 1)
                return true;
            else
                return false;
        }
        #endregion
    }
}
