using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Seer
{
    #region 结构体：一条完整的封包的数据
    public struct _PacketData
    {
        public int length;                       //包头：封包(包头+包体)长度
        public byte version;                     //包头：版本号
        public int cmdId;                        //包头：命令号
        public int userId;                       //包头：米米号
        public int result;                       //包头：序列号，进行验证封包是否合法
        public byte[] body;                      //包体

        public void display()
        {
            Console.WriteLine("\t长度：{0}", length);
            Console.WriteLine("\t版本号：{0}", version);
            Console.WriteLine("\t命令号：{0}\t{1}", cmdId, Command.GetCommandName(cmdId));
            Console.WriteLine("\t米米号：{0}", userId);
            Console.WriteLine("\t序列号：{0}", result);
            Console.WriteLine("\t包体：[ {0}]", Misc.ByteArray2HexString(body, length - 17));
            Console.WriteLine("");
        }
    }
    #endregion

    class Packet
    {
        #region 全局变量
        public static byte[] RecvBuf = new byte[1048576];       //接收封包的数据缓冲区
        public static int RecvBufLen = 0;                       //接收封包的数据缓冲区长度
        public static int RecvBufIndex = 0;                     //接收封包的数据缓冲区索引

        public static int Result = 0;                           //全局-发送封包的序列号
        public static int Socket = 0;                           //socket通信号
        public static int UserId = 0;                           //登陆的米米号

        public static bool HaveLogin = false;                   //接收到LOGIN_IN包(cmdId为1001)后此项设为true

        public static int RecvPacketNum = 0;                    //接收封包序号
        public static int SendPacketNum = 0;                    //发送封包序号
        #endregion 


        #region 解析一条完整的封包，各项数据放入一个结构体中
        public static void ParsePacket(byte[] packet, ref _PacketData PacketData)
        {
            if (packet.Length >= 17)
            {
                PacketData.length = Misc.GetIntParam(packet, 0);
                PacketData.version = packet[4];
                PacketData.cmdId = Misc.GetIntParam(packet, 5);
                PacketData.userId = Misc.GetIntParam(packet, 9);
                PacketData.result = Misc.GetIntParam(packet, 13);
                PacketData.body = Misc.ArraySlice(packet, 17, PacketData.length);
            }
        }
        #endregion


        #region 将结构体中的各项数据组合成一条完整的封包
        public static byte[] GroupPacket(ref _PacketData PacketData)
        {
            byte[] length = Misc.Int2ByteArray(PacketData.length);
            byte[] cmdId = Misc.Int2ByteArray(PacketData.cmdId);
            byte[] userId = Misc.Int2ByteArray(PacketData.userId);
            byte[] result = Misc.Int2ByteArray(PacketData.result);

            byte[] packet = new byte[PacketData.length];
            length.CopyTo(packet, 0);
            packet[4] = PacketData.version;
            cmdId.CopyTo(packet, 5);
            userId.CopyTo(packet, 9);
            result.CopyTo(packet, 13);
            PacketData.body.CopyTo(packet, 17);

            return packet;
        }
        #endregion


        #region 处理接收封包的数据
        public static void ProcessingRecvPacket(int socket, byte[] buffer, int length)
        {
            _PacketData RecvPacketData = new _PacketData();

            Array.Copy(buffer, 0, RecvBuf, RecvBufLen, length);                     //接收封包的数据追加到接收封包缓冲区的尾部，以解决断包的问题
            RecvBufLen += length;                                                   //更新接收封包缓冲区的长度

            if (Socket != socket)                                   
            {
                #region 直接过滤的接收封包

                if (length > 20)
                    Console.WriteLine("接收封包-解析过滤 : [ {0}...... ]\n", Misc.ByteArray2HexString(RecvBuf, 20));
                else
                    Console.WriteLine("接收封包-解析过滤 : [ {0}]\n", Misc.ByteArray2HexString(RecvBuf, length));
                
                RecvBufIndex += length;                                             //更新接收封包缓冲区的索引

                if (RecvBufIndex == RecvBufLen)
                {
                    //如果接收封包缓冲区索引等于接收封包缓冲区长度
                    //说明刚好取完所有的包，不存在断包的情况，所以此时将二者的值都设为0
                    RecvBufLen = 0;
                    RecvBufIndex = 0;
                }

                #endregion
            }
            else
            {
                while (true)                                                        //从接收封包缓冲区中不停地取出一条条接收封包，直到取完或遇到断包
                {
                    if (RecvBufLen >= 4)
                    {
                        int PacketLen = Misc.GetIntParam(RecvBuf, RecvBufIndex);
                        if (RecvBufIndex + PacketLen <= RecvBufLen)                 //不是断包
                        {
                            #region 从缓冲区中取出一条接收封包，解析并发送(同时会监测cmdId为1001和105的封包)

                            byte[] cipher = Misc.ArraySlice(RecvBuf, RecvBufIndex, RecvBufIndex + PacketLen);   //取出一条接收封包

                            byte[] plain;
                            if (NeedDecrypt(cipher))                                        //解密或者不解密封包
                            {
                                plain = decrypt(cipher);
                            }
                            else
                            {
                                plain = cipher;
                            }

                            ParsePacket(plain, ref RecvPacketData);                 //解析封包
                            RecvPacketNum++;
                            Program.UI.AddList("recv", RecvPacketNum, ref RecvPacketData, plain, cipher);   //更新UI界面的列表


                            #region 登录包(cmdId == 1001)
                            if (RecvPacketData.cmdId == 1001)                       //登陆LOGIN_IN包
                            {
                                Login(ref RecvPacketData);                          //处理登录数据，拿到密钥
                                Socket = socket;                                    //设置全局socket通信号
                                Result = RecvPacketData.result;                     //LOGIN_IN的recv包是含有序列号的
                                UserId = RecvPacketData.userId;                     //米米号
                                HaveLogin = true;                                     //是否登录
                            }
                            #endregion

                            //Listen.listen(ref RecvPacketData);

                            RecvBufIndex += PacketLen;                              //更新接收封包缓冲区的索引

                            #endregion
                        } 
                        else                                                        //断包，等待下一次接收封包的到来
                        {
                            break;
                        }
                    }
                    else                                                            //断包，等待下一次接收封包的到来
                    {
                        break;
                    }

                    #region 取完缓冲区内所有的包后重置RecvBufLen和RecvBufIndex
                    if (RecvBufIndex == RecvBufLen)
                    {
                        //如果接收封包缓冲区索引等于接收封包缓冲区长度
                        //说明刚好取完所有的包，不存在断包的情况，所以此时将二者的值都设为0
                        RecvBufLen = 0;
                        RecvBufIndex = 0;
                    }
                    #endregion
                }
            }
        }
        #endregion


        #region 处理发送封包的数据
        public static int ProcessingSendPacket(int socket, byte[] cipher, int length)
        {
            _PacketData SendPacketData = new _PacketData();
            int res = 0;
            if (cipher.Length < 17 || Misc.ByteArray2HexString(cipher, 2) != "00 00 ")
            {
                #region 直接过滤的发送封包（这个包也得发送出去，但是我们的封包解析程序不会解析这一条封包）

                res = SendPacket.Send(socket, cipher);          //直接发送封包

                if (cipher.Length > 20)
                    Console.WriteLine("发送封包-解析过滤 : [ {0}...... ]\n", Misc.ByteArray2HexString(cipher, 20));
                else
                    Console.WriteLine("发送封包-解析过滤 : [ {0}]\n", Misc.ByteArray2HexString(cipher));

                #endregion
            }
            else
            {
                #region 需要解析的发送封包

                if (!HaveLogin)
                {
                    Socket = socket;                                //通信号
                }

                byte[] plain;
                if (NeedDecrypt(cipher))
                {
                    plain = decrypt(cipher);                        //解密封包
                    ParsePacket(plain, ref SendPacketData);         //解析封包
                    CalculateResult(ref SendPacketData);            //修改序列号
                    plain = GroupPacket(ref SendPacketData);        //组合封包
                    cipher = encrypt(plain);                        //加密封包
                }
                else                                                //无需加密只有一种情况，即处于登录界面
                {                                                   //这种情况下并不需要修改序列号，只解析封包即可
                    plain = cipher;
                    ParsePacket(plain, ref SendPacketData);         //解析封包  

                    #region 登陆前 伪造米米号
                    // 如果 "伪造米米号" 对应的文本框不为空，
                    // 则登录前的封包，米米号修改为该文本框中的米米号。
                    // 这个是用于测试赛尔号的伪造登录。正常游戏则置空即可。
                    if (!HaveLogin && !String.IsNullOrEmpty(Program.UI.textBox11.Text) && 
                        !String.IsNullOrEmpty(Program.UI.textBox12.Text) 
                        && Program.UI.textBox12.Text.Length == 32 && SendPacketData.cmdId == 103)
                    {
                        int iSubUserId = Int32.Parse(Program.UI.textBox11.Text);
                        byte[] subUserId = Misc.Int2ByteArray(iSubUserId);
                        subUserId.CopyTo(cipher, 9);
                        subUserId.CopyTo(plain, 9);

                        byte[] doubleMD5Pwd;
                        doubleMD5Pwd = System.Text.Encoding.UTF8.GetBytes(Program.UI.textBox12.Text);
                        doubleMD5Pwd.CopyTo(cipher, 17);
                        doubleMD5Pwd.CopyTo(plain, 17);

                        ParsePacket(plain, ref SendPacketData);
                    }
                    #endregion

                }

                res = SendPacket.Send(socket, cipher);              //发送封包
                if (HaveLogin)
                {
                    Result = SendPacketData.result;                 //更新全局序列号(登录前的不用更新，也不能更新)
                }
                SendPacketNum++;                                    //发送封包的总序号
                Program.UI.AddList("send", SendPacketNum, ref SendPacketData, plain, cipher);   //UI界面的列表增加这条发送记录

                #endregion 
            }
            return res;
        }
        #endregion


        #region 解密封包
        public static byte[] decrypt(byte[] cipher)
        {
            int CipherLen = Misc.GetIntParam(cipher, 0);            //获取封包密文长度
            byte[] PlainLen = Misc.Int2ByteArray(CipherLen - 1);    //封包明文长度比封包密文长度小一
            byte[] plain;
            cipher = Misc.ArraySlice(cipher, 4, cipher.Length);     //封包的前4个字节（封包长度）不参与解密运算
            plain = Algorithm.Decrypt(cipher);
            plain = Misc.ArrayMerge(PlainLen, plain);               //封包明文的长度与解密后的数据拼接，得到封包明文
            return plain;
        }
        #endregion


        #region 加密封包
        public static byte[] encrypt(byte[] plain)
        {
            int PlainLen = Misc.GetIntParam(plain, 0);              //获取封包明文长度
            byte[] CipherLen = Misc.Int2ByteArray(PlainLen + 1);    //封包密文长度比封包明文长度大一
            byte[] cipher;
            plain = Misc.ArraySlice(plain, 4, plain.Length);        //封包的前4个字节（封包长度）不参与解密运算
            cipher = Algorithm.Encrypt(plain);
            cipher = Misc.ArrayMerge(CipherLen, cipher);            //封包密文的长度与加密后的数据拼接，得到封包密文
            return cipher;
        }
        #endregion


        #region 计算发送封包的序列号
        public static void CalculateResult(ref _PacketData PacketData)
        {
            int result = 0;
            if (PacketData.cmdId > 1000)
            {
                int v6 = 0, v7 = 0;
                while (v7 < PacketData.body.Length)
                {
                    v6 = (v6 ^ PacketData.body[v7]) & 255;
                    v7++;
                }
                result = Algorithm.MSerial(Result, PacketData.body.Length, v6, PacketData.cmdId);
            }
            else
            {
                result = 0;
            }
            PacketData.result = result;
        }
        #endregion


        #region 处理登录数据，拿到密钥
        public static void Login(ref _PacketData RecvPacketData)
        {
            byte[] b = Misc.ArraySlice(RecvPacketData.body, RecvPacketData.body.Length - 4, RecvPacketData.body.Length);        //取LOGIN_IN接收封包的后4个字节
            int d = Misc.GetIntParam(b, 0);
            string s = (d ^ RecvPacketData.userId).ToString();
            b = System.Text.Encoding.UTF8.GetBytes(s);
            s = Misc.GetMD5(b);
            s = s.Substring(0, 10);         //取md5后的前10个字节作为密钥
            Algorithm.InitKey(s);
        }
        #endregion


        #region 判断是否需要加解密封包
        public static bool NeedDecrypt(byte[] packet)
        {
            /* 不需要解密的封包是登录前的，其send的版本号是31，recv的版本号是0
             * 其命令号都是小于一百左右的，所以命令号这个int的前三个字节都是0
             * 所以封包的第4到第7个字节是31 00 00 00或00 00 00 00
             * 以此为依据判断是否加解密封包
             */
            int temp = Misc.GetIntParam(packet, 4);
            if (temp == 0x31000000 || temp == 0x00000000)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}