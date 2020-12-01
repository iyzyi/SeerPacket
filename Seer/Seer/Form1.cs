using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            this.listView1.Items.Add(li);

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
    }
}