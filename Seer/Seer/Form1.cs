using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace Seer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Hook.InitHook();

            Algorithm.InitKey("!crAckmE4nOthIng:-)");

            // 初始过滤关键字
            //textBox5.Text = "leave_map\r\nenter_map";
        }

        public void AddList(string type, int num, ref _PacketData PacketData, byte[] plain, byte[] cipher)
        {
            ListViewItem li = new ListViewItem(type);
            li.SubItems.Add(num.ToString());
            li.SubItems.Add(PacketData.length.ToString());
            li.SubItems.Add(PacketData.version.ToString());
            li.SubItems.Add(PacketData.userId.ToString());
            li.SubItems.Add(PacketData.cmdId.ToString());
            li.SubItems.Add(Command.GetCommandName(PacketData.cmdId));
            li.SubItems.Add(PacketData.result.ToString());
            li.SubItems.Add(Misc.ByteArray2HexString(PacketData.body));
            li.SubItems.Add(Misc.ByteArray2HexString(plain));
            li.SubItems.Add(Misc.ByteArray2HexString(cipher));

            // 添加到封包log列表中
            PacketLogList.Add(li);
            //Console.WriteLine(PacketLogList.Count);

            // 这段由PacketFilter内部实现
            // 若关键字过滤的文本框中没有字符，则将该条封包log添加到UI的listview框中
            //if (String.IsNullOrEmpty(textBox5.Text))
            //{
            //    this.listView1.Items.Add(li);
            //}

            PacketFilter(li);

            if (type == "send")
            {
                iSendPacketNum++;
            }
            if (type == "recv")
            {
                iRecvPacketNum++;
            }
            label4.Text = String.Format("本次连接中，发送封包{0}条，接收封包{1}条", iSendPacketNum, iRecvPacketNum);


            //this.listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
            //列表刷新时自动拉到最后
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && this.listView1.SelectedIndices.Count == 1)    //右键 && 单选
            {
                this.contextMenuStrip1.Show(this.listView1, e.Location);//弹出菜单
            }
        }

        //点击发送封包
        private void button1_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            SendPacket.SendPacketManually(s);
        }

        private void 复制此包明文ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //https://zhidao.baidu.com/question/537278467.html?qbl=relate_question_0
            string s = this.listView1.SelectedItems[0].SubItems[9].Text;
            Clipboard.SetDataObject(s);
        }

        private void 复制此包密文ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = this.listView1.SelectedItems[0].SubItems[10].Text;
            Clipboard.SetDataObject(s);
        }

        private void 编辑此包数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems[0].SubItems[0].Text == "send")     //只有send封包才可以被编辑
            {
                string s = this.listView1.SelectedItems[0].SubItems[9].Text;
                this.textBox1.Text = s;
            }
        }

        #region 点击喊话按钮
        private void button2_Click(object sender, EventArgs e)
        {
            string content = this.textBox2.Text;
            if (content == "")
            {
                content = "秋风生渭水，落叶满长安。";
            }
            if (content.Length > 30)
            {
                MessageBox.Show("消息最长为30！");
            }
            else
            {
                SendPacket.Chat(content);
            }
        }
        #endregion


        #region 点击刷新
        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearPacketLog();

            webBrowser1.Document.ExecCommand("Refresh", false, null);

            Packet.RecvBufLen = 0;
            Packet.RecvBufIndex = 0;

            Packet.Result = 0;
            Packet.Socket = 0;
            Packet.UserId = 0;

            Packet.HaveLogin = false;

            Packet.RecvPacketNum = 0;
            Packet.SendPacketNum = 0;

            Algorithm.InitKey("!crAckmE4nOthIng:-)");
        }
        #endregion


        #region 复制同一cmdId的发送/接收封包(只复制包体，不复制包头，以便分析)
        private void 复制此类封包ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string cmdId = this.listView1.SelectedItems[0].SubItems[5].Text;        //命令号
            string type = this.listView1.SelectedItems[0].SubItems[0].Text;         //recv或send

            string CutString = "";

            for (int nRowOrder = 0; nRowOrder < listView1.Items.Count; nRowOrder++) //遍历listView1的每一行
            {
                ListViewItem li = listView1.Items[nRowOrder];
                if (li.SubItems[5].Text == cmdId && li.SubItems[0].Text == type)    //相同命令号，且同为recv或send
                {
                    string body = li.SubItems[8].Text;
                    CutString += body + "\n";
                }
            }
            Clipboard.SetDataObject(CutString);
        }
        #endregion


        #region 行走
        private void button3_Click(object sender, EventArgs e)
        {
            int x, y;
            if (textBox3.Text == "")
                x = 490;
            else
                x = Int32.Parse(textBox3.Text);
            if (textBox4.Text == "")
                y = 280;
            else
                y = Int32.Parse(textBox4.Text);

            SendPacket.Walk(x, y);
        }
        #endregion


        #region 只允许输入数字
        private void numKeyPress(object sender, KeyPressEventArgs e)
        {
            //https://jingyan.baidu.com/article/ca41422fddd5201eae99ed28.html
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
        #endregion


        #region 地图
        private void button4_Click(object sender, EventArgs e)
        {
            SendPacket.GotoMap(10);
        }
        #endregion



        #region 清空封包记录

        private void ClearPacketLog()
        {
            PacketLogList.Clear();
            Program.UI.listView1.Items.Clear();

            iSendPacketNum = 0;
            iRecvPacketNum = 0;
            label4.Text = String.Format("本次连接中，发送封包{0}条，接收封包{1}条", iSendPacketNum, iRecvPacketNum);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClearPacketLog();
        }
        #endregion



        #region 过滤封包

        private static object FilterLock = new object();
        private static bool bShowAllPacketLog = true;

        // newLi为null时表示此次过滤不是新插入
        private void PacketFilter(ListViewItem newLi = null)
        {
            lock (FilterLock)
            {
                if (newLi == null || !bShowAllPacketLog)
                {
                    // 清空listview框。
                    Program.UI.listView1.Items.Clear();
                }

                // 从textBox5中获取过滤的关键字，一行一个
                string[] aPacketKeyword = new string[textBox5.Lines.Length];
                for (int i = 0; i < textBox5.Lines.Length; i++)
                {
                    aPacketKeyword[i] = textBox5.Lines[i];
                }

                // 从textBox10中获取排除的关键字，一行一个
                string[] aPacketBanKeyword = new string[textBox10.Lines.Length];
                for (int i = 0; i < textBox10.Lines.Length; i++)
                {
                    aPacketBanKeyword[i] = textBox10.Lines[i];
                }

                // 关键字不为0个，即过滤
                if (aPacketKeyword.Length != 0)
                {
                    // 枚举PacketLogList中的所有封包log, 符合关键字的则添加到listview框中。
                    foreach (ListViewItem li in PacketLogList)
                    {
                        // 排除关键字比过滤关键字的优先级高，优先BAN掉而非保留
                        if (TypeFilter(li) && RangeFilter(li) && !BanFilter(ref aPacketBanKeyword, li))
                        {
                            for (int i = 0; i < aPacketKeyword.Length; i++)
                            {
                                if (String.IsNullOrEmpty(aPacketKeyword[i]))
                                {
                                    continue;
                                }
                                if (li.SubItems[6].Text.ToUpper().Contains(aPacketKeyword[i].ToUpper()))
                                {
                                    // 添加到listview框中
                                    this.listView1.Items.Add(li);
                                    break;
                                }
                            }
                        }
                    }

                    bShowAllPacketLog = false;
                }
                // 不过滤
                else
                {
                    // 如果此次PacketFilter没有过滤的关键字，而且是向listview框中添加一条封包log，那么不要清空listview框再重新全部添加，只需要添加这一条就行。
                    if (newLi != null && bShowAllPacketLog)
                    {
                        if (TypeFilter(newLi) && RangeFilter(newLi) && !BanFilter(ref aPacketBanKeyword, newLi))
                        {
                            // 添加到listview框中
                            this.listView1.Items.Add(newLi);
                        }
                    }
                    else
                    {
                        foreach (ListViewItem li in PacketLogList)
                        {
                            if (TypeFilter(li) && RangeFilter(li) && !BanFilter(ref aPacketBanKeyword, li))
                            {
                                // 添加到listview框中
                                this.listView1.Items.Add(li);
                            }
                        }
                    }

                    // 不过滤就是显示全部封包log
                    bShowAllPacketLog = true;
                }

                if (this.listView1.Items.Count > 0)
                {
                    //this.listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
                    //列表刷新时自动拉到最后
                }
            }

        }

        private bool TypeFilter(ListViewItem li)
        {
            return (li.SubItems[0].Text == "send" && SendCheckBox.Checked == true) ||
                    (li.SubItems[0].Text == "recv" && RecvCheckBox.Checked == true);
        }

        // 过滤封包区间
        private bool RangeFilter(ListViewItem li)
        {
            if (li.SubItems[0].Text == "send" )
            {
                // 同时不为空时，过滤
                if (!String.IsNullOrEmpty(textBox6.Text) && !String.IsNullOrEmpty(textBox7.Text))
                {
                    int num = int.Parse(li.SubItems[1].Text);
                    int r1 = int.Parse(textBox6.Text);
                    int r2 = int.Parse(textBox7.Text);
                    if (r1 < r2 && num >= r1 && num <= r2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                // 不同时不为空时，不过滤
                else
                {
                    return true;
                }
            }

            if (li.SubItems[0].Text == "recv")
            {
                // 同时不为空时，过滤
                if (!String.IsNullOrEmpty(textBox8.Text) && !String.IsNullOrEmpty(textBox9.Text))
                {
                    int num = int.Parse(li.SubItems[1].Text);
                    int r1 = int.Parse(textBox8.Text);
                    int r2 = int.Parse(textBox9.Text);
                    if (r1 < r2 && num >= r1 && num <= r2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                // 不同时不为空时，不过滤
                else
                {
                    return true;
                }
            }

            return false;
        }

        private bool BanFilter(ref string[] aPacketBanKeyword, ListViewItem li)
        {
            if (aPacketBanKeyword.Length != 0)
            {
                for (int i = 0; i < aPacketBanKeyword.Length; i++)
                {
                    if (String.IsNullOrEmpty(aPacketBanKeyword[i]))
                    {
                        continue;
                    }
                    //if (li.SubItems[6].Text.ToUpper() == aPacketBanKeyword[i].ToUpper())
                    if (li.SubItems[6].Text.ToUpper().Contains(aPacketBanKeyword[i].ToUpper()))
                    {
                        // BAN
                        return true;
                    }
                }
            }
            // 不BAN
            return false;;
        }


        private void button6_Click(object sender, EventArgs e)
        {
            PacketFilter();
        }


        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            PacketFilter();
        }

        private void SendCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PacketFilter();
        }

        private void RecvCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PacketFilter();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            PacketFilter();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            PacketFilter();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            PacketFilter();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            PacketFilter();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            PacketFilter();
        }


        #endregion



        private void 添加至排除关键字ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in this.listView1.SelectedItems)
            {
                string s = li.SubItems[6].Text;
                if (String.IsNullOrEmpty(textBox10.Text))
                {
                    textBox10.Text += s;
                }
                else
                {
                    textBox10.Text += "\r\n" + s;
                }
            }
        }

        private void 发送此封包ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in this.listView1.SelectedItems)
            {
                if (li.SubItems[0].Text == "send")     //只有send封包才可以被发送
                {
                    string s = li.SubItems[9].Text;
                    SendPacket.SendPacketManually(s);
                }
            }

            
        }
    }
}