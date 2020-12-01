using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Seer
{
    class Hook
    {
        //根据DLL中的回调函数的原型声明一个委托类型并实例化
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Delegate(int socket, IntPtr buf, int len);
        static Delegate pRecvCallBack = new Delegate(RecvCallBack);
        static Delegate pSendCallBack = new Delegate(SendCallBack);

        [DllImport("hook.dll")]
        public static extern bool Inline_InstallHook_Recv();
        [DllImport("hook.dll")]
        public static extern bool Inline_InstallHook_Send();
        [DllImport("hook.dll")]
        public static extern void SetRecvCallBack(Delegate pFun);
        [DllImport("hook.dll")]
        public static extern void SetSendCallBack(Delegate pFun);
        [DllImport("hook.dll")]
        public static extern int RealSend(int socket, IntPtr buffer, int length, int flags);   //本函数等效于HOOK前的send函数
        
        //初始化
        public static void InitHook()
        {
            //设置回调函数。将RecvCallBack、SendCallBack的函数地址pRecvCallBack、pSendCallBack传入HOOK.DLL
            SetRecvCallBack(pRecvCallBack);
            SetSendCallBack(pSendCallBack);

            //安装Hook
            Inline_InstallHook_Recv();
            Inline_InstallHook_Send();
        }

        //排他锁
        private static object RecvLock = new object();
        private static object SendLock = new object();

        //接收封包 回调函数
        public static int RecvCallBack(int socket, IntPtr buf, int len)
        {
            lock (RecvLock)
            {
                byte[] temp = new byte[len];
                Marshal.Copy(buf, temp, 0, len);

                Packet.ProcessingRecvPacket(socket, temp, len);
                return 0;
            }
        }

        //发送封包 回调函数
        public static int SendCallBack(int socket, IntPtr buf, int len)
        {
            lock (SendLock)
            {
                byte[] temp = new byte[len];
                Marshal.Copy(buf, temp, 0, len);

                int res = Packet.ProcessingSendPacket(socket, temp, len);
                return res;
            }
        }
    }
}
