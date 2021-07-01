using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace Seer
{
    class SendPacket
    {
        #region 客户端发送封包用此函数
        public static int Send(int socket, byte[] packet)
        {
            IntPtr My_Packet = Misc.BytesToIntptr(packet);
            int res = Hook.RealSend(socket, My_Packet, packet.Length, 0);
            Marshal.FreeHGlobal(My_Packet);                                     //回收空间
            return res;
        }
        #endregion


        #region 手动发送封包用此函数
        public static int SendPacketManually(string PlainStr)
        {
            if (!Misc.CheckHexString(PlainStr))
            {
                MessageBox.Show("封包数据格式错误，请检查十六进制字符串的格式是否正确");
                return 0;
            }
            byte[] plain = Misc.HexString2ByteArray(PlainStr);
            _PacketData SendPacketData = new _PacketData();
            Packet.ParsePacket(plain, ref SendPacketData);
            Packet.CalculateResult(ref SendPacketData);                         //更新序列号
            SendPacketData.userId = Packet.UserId;                              //修改为当前登录的米米号
            plain = Packet.GroupPacket(ref SendPacketData);
            byte[] cipher = Packet.encrypt(plain);

            int res = SendPacket.Send(Packet.Socket, cipher);
            Packet.Result = SendPacketData.result;
            Packet.SendPacketNum++;
            Program.UI.AddList("send", Packet.SendPacketNum, ref SendPacketData, plain, cipher);

            return res;
        }
        #endregion


        #region 喊话
        public static int Chat(string content)
        {
            /* 喊话“A”
             * 00 00 00 1B 31 00 00 08 36 29 75 C9 B0 00 00 02 78 00 00 00 00 00 00 00 02 41 30 
             * 喊话“aaa”
             * 00 00 00 1D 31 00 00 08 36 29 75 C9 B0 00 00 02 88 00 00 00 00 00 00 00 04 61 61 61 30 
             * 喊话“暑期福利放送”
             * 00 00 00 2C 31 00 00 08 36 29 75 C9 B0 00 00 01 D4 00 00 00 00 00 00 00 13 E6 9A 91 E6 9C 9F E7 A6 8F E5 88 A9 E6 94 BE E9 80 81 30 
             * 喊话“我*30”，喊话字数上限为30（30个汉字，30个英语字母，等等）
             * 00 00 00 74 31 00 00 08 36 29 75 C9 B0 00 00 02 A5 00 00 00 00 00 00 00 5B E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 E6 88 91 30 
             * */

            _PacketData PacketData = new _PacketData();
            string example = "00 00 00 1B 31 00 00 08 36 29 75 C9 B0 00 00 02 78 00 00 00 00 00 00 00 02 41 30 ";
            byte[] plain = Misc.HexString2ByteArray(example);
            Packet.ParsePacket(plain, ref PacketData);     //版本号和命令号准备就绪

            byte[] a = new byte[7] { 0, 0, 0, 0, 0, 0, 0 };
            byte[] c = System.Text.Encoding.UTF8.GetBytes(content);
            byte[] b = new byte[1] { (byte)(c.Length + 1) };
            byte[] d = new byte[1] { 30 };
            byte[] body = new byte[9 + c.Length];
            a.CopyTo(body, 0);
            b.CopyTo(body, 7);
            c.CopyTo(body, 8);
            d.CopyTo(body, 8 + c.Length);
            PacketData.body = body;                         //包体

            PacketData.length = 17 + PacketData.body.Length;//长度

            //序列号和米米号在调用SendPacketManually()的时候会自动修复

            plain = Packet.GroupPacket(ref PacketData);
            string PlainStr = Misc.ByteArray2HexString(plain);
            return SendPacketManually(PlainStr);
        }
        #endregion


        #region 行走
        public static int Walk(int x, int y)
        {
            /* 行走到大约位于中心的位置：
             * 00 00 00 3F 31 00 00 08 35 29 75 C9 B0 00 00 03 2E 00 00 00 00 00 00 01 DD 00 00 01 62 00 00 00 1E 09 05 01 0A 23 01 03 78 03 79 05 40 7A E3 33 33 33 33 33 04 82 4A 0A 01 04 83 56 04 82 5E 
             * x坐标是0x1DD, y坐标是0x162.
             * 不同的坐标，封包的长度居然不一样。
             * 不过意外发现只需要修改x, y坐标即可实现行走到相应的位置。
             * 其余的数据的含义无需分析出来，哪怕长度并不同，也能正常用。
             * 包体的第二个int是x坐标，范围大约是0~0x3c0;
             * 包体的第三个int是y坐标，范围大约是0~0x280.
             * 坐标可以超出上限，但是会消失在屏幕范围内。
             * 再点一次屏幕，人就回来了。
             */

            _PacketData PacketData = new _PacketData();
            string example = "00 00 00 3F 31 00 00 08 35 29 75 C9 B0 00 00 03 2E 00 00 00 00 00 00 01 DD 00 00 01 62 00 00 00 1E 09 05 01 0A 23 01 03 78 03 79 05 40 7A E3 33 33 33 33 33 04 82 4A 0A 01 04 83 56 04 82 5E ";
            byte[] plain = Misc.HexString2ByteArray(example);
            Packet.ParsePacket(plain, ref PacketData);

            byte[] x_b = Misc.Int2ByteArray(x);
            byte[] y_b = Misc.Int2ByteArray(y);
            x_b.CopyTo(PacketData.body, 4);
            y_b.CopyTo(PacketData.body, 8);

            plain = Packet.GroupPacket(ref PacketData);
            string PlainStr = Misc.ByteArray2HexString(plain);
            return SendPacketManually(PlainStr);
        }
        #endregion


        #region 前往地图, TODO
        public static void GotoMap(int MapId)
        {
            string LeaveMap = "00 00 00 11 31 00 00 07 D2 25 5F EB 30 00 00 02 52 ";        // 没有包体，只有包头
            SendPacketManually(LeaveMap);
            //Console.WriteLine(Listen.bLeaveMap);
            //RecvLeaveMapPacket();       // 异步等待
            //Console.WriteLine(Listen.bLeaveMap);
            //Thread.Sleep(5000);

            RecvLeaveMapPacket(MapId);
        }

        //private static async void RecvLeaveMapPacket(int MapId)
        private static void RecvLeaveMapPacket(int MapId)
        {
            //await Task.Run(() => {
            //    while (!Listen.bLeaveMap)
            //    {
            //        ;
            //    }
            //});

            //MessageBox.Show("ENTER MAP ing");
            Thread.Sleep(100);


            _PacketData PacketData = new _PacketData();
            string EntryMap = "00 00 00 21 31 00 00 07 D1 09 C0 B6 F7 00 00 02 55 00 00 00 00 00 00 00 0A 00 00 02 D5 00 00 00 AC ";
            // 包体是4个int，第一个是0，第二个是地图号，第三第四个分别是x，y坐标
            byte[] plain = Misc.HexString2ByteArray(EntryMap);
            Packet.ParsePacket(plain, ref PacketData);

            byte[] temp = Misc.Int2ByteArray(0);
            byte[] mapid = Misc.Int2ByteArray(MapId);
            //byte[] x = Misc.Int2ByteArray(490);
            //byte[] y = Misc.Int2ByteArray(280);         // 默认传送坐标为（490，280）
            byte[] x = Misc.Int2ByteArray(0x2d5);
            byte[] y = Misc.Int2ByteArray(0xac); 
            byte[] body = new byte[16];
            temp.CopyTo(body, 0);
            mapid.CopyTo(body, 4);
            x.CopyTo(body, 8);
            y.CopyTo(body, 12);
            PacketData.body = body;

            plain = Packet.GroupPacket(ref PacketData);
            string PlainStr = Misc.ByteArray2HexString(plain);
            SendPacketManually(PlainStr);
        }
        #endregion


        #region 通过申请好友，TODO
        //public static int Friend
        #endregion
    }
}
