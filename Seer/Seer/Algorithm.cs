using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seer
{
    class Algorithm
    {
        #region 初始化封包加解密的密钥
        public static byte[] Key;

        public static void InitKey(string KeyStr)
        {
            Key = System.Text.Encoding.UTF8.GetBytes(KeyStr);   //https://blog.csdn.net/tom_221x/article/details/71643015
            Console.WriteLine("初始化密钥：{0}\n", System.Text.Encoding.UTF8.GetString(Key));
        }
        #endregion

        #region 解密算法
        static public byte[] Decrypt(byte[] cipher)
        {
            int result = Key[(cipher.Length - 1) % Key.Length] * 13 % (cipher.Length);
            cipher = Misc.ArrayMerge(Misc.ArraySlice(cipher, cipher.Length - result, cipher.Length), Misc.ArraySlice(cipher, 0, cipher.Length - result));

            byte[] plain = new byte[cipher.Length - 1];

            for (int i = 0; i < cipher.Length - 1; i++)
            {
                plain[i] = (byte)((cipher[i] >> 5) | (cipher[i + 1] << 3));
            }

            int j = 0;
            bool NeedBecomeZero = false;
            for (int i = 0; i < plain.Length; i++)
            {
                if (j == 1 && NeedBecomeZero)
                {
                    j = 0;
                    NeedBecomeZero = false;
                }
                if (j == Key.Length)
                {
                    j = 0;
                    NeedBecomeZero = true;
                }
                plain[i] = (byte)(plain[i] ^ Key[j]);
                j++;
            }
            return plain;
        }
        #endregion

        #region 加密算法
        static public byte[] Encrypt(byte[] plain)
        {
            byte[] cipher = new byte[plain.Length + 1];

            int j = 0;
            bool NeedBecomeZero = false;
            for (int i = 0; i < plain.Length; i++)
            {
                if (j == 1 && NeedBecomeZero)
                {
                    j = 0;
                    NeedBecomeZero = false;
                }
                if (j == Key.Length)
                {
                    j = 0;
                    NeedBecomeZero = true;
                }
                cipher[i] = (byte)(plain[i] ^ Key[j]);
                j++;
            }
            cipher[cipher.Length - 1] = 0;

            for (int i = cipher.Length - 1; i > 0; i--)
            {
                cipher[i] = (byte)((cipher[i] << 5) | (cipher[i - 1] >> 3));
            }
            cipher[0] = (byte)((cipher[0] << 5) | 3);

            int result = Key[(plain.Length) % Key.Length] * 13 % (cipher.Length);
            cipher = Misc.ArrayMerge(Misc.ArraySlice(cipher, result, cipher.Length), Misc.ArraySlice(cipher, 0, result));

            return cipher;
        }
        #endregion

        #region 序列号相关算法
        public static int MSerial(int a, int b, int c, int d)
        {
            return a + c + (int)(a / (-3)) + b % 17 + d % 23 + 120;
        }
        #endregion
    }
}
