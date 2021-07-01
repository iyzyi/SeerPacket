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
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制此包明文ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制此包密文ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑此包数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制此类封包ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加至排除关键字ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.发送此封包ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SendCheckBox = new System.Windows.Forms.CheckBox();
            this.RecvCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(0, 24);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(977, 582);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("http://seer.61.com/play.shtml", System.UriKind.Absolute);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 602);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(975, 210);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "类型";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "序号";
            this.columnHeader2.Width = 40;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "长度";
            this.columnHeader3.Width = 40;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "协议号";
            this.columnHeader4.Width = 48;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "米米号";
            this.columnHeader5.Width = 70;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "命令号";
            this.columnHeader6.Width = 48;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "命令";
            this.columnHeader7.Width = 140;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "序列号";
            this.columnHeader8.Width = 48;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "包体";
            this.columnHeader9.Width = 450;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "封包明文";
            this.columnHeader10.Width = 450;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "封包密文";
            this.columnHeader11.Width = 450;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制此包明文ToolStripMenuItem,
            this.复制此包密文ToolStripMenuItem,
            this.编辑此包数据ToolStripMenuItem,
            this.复制此类封包ToolStripMenuItem,
            this.添加至排除关键字ToolStripMenuItem,
            this.发送此封包ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 136);
            // 
            // 复制此包明文ToolStripMenuItem
            // 
            this.复制此包明文ToolStripMenuItem.Name = "复制此包明文ToolStripMenuItem";
            this.复制此包明文ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.复制此包明文ToolStripMenuItem.Text = "复制此包明文";
            this.复制此包明文ToolStripMenuItem.Click += new System.EventHandler(this.复制此包明文ToolStripMenuItem_Click);
            // 
            // 复制此包密文ToolStripMenuItem
            // 
            this.复制此包密文ToolStripMenuItem.Name = "复制此包密文ToolStripMenuItem";
            this.复制此包密文ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.复制此包密文ToolStripMenuItem.Text = "复制此包密文";
            this.复制此包密文ToolStripMenuItem.Click += new System.EventHandler(this.复制此包密文ToolStripMenuItem_Click);
            // 
            // 编辑此包数据ToolStripMenuItem
            // 
            this.编辑此包数据ToolStripMenuItem.Name = "编辑此包数据ToolStripMenuItem";
            this.编辑此包数据ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.编辑此包数据ToolStripMenuItem.Text = "编辑此包数据";
            this.编辑此包数据ToolStripMenuItem.Click += new System.EventHandler(this.编辑此包数据ToolStripMenuItem_Click);
            // 
            // 复制此类封包ToolStripMenuItem
            // 
            this.复制此类封包ToolStripMenuItem.Name = "复制此类封包ToolStripMenuItem";
            this.复制此类封包ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.复制此类封包ToolStripMenuItem.Text = "复制此类封包";
            this.复制此类封包ToolStripMenuItem.Click += new System.EventHandler(this.复制此类封包ToolStripMenuItem_Click);
            // 
            // 添加至排除关键字ToolStripMenuItem
            // 
            this.添加至排除关键字ToolStripMenuItem.Name = "添加至排除关键字ToolStripMenuItem";
            this.添加至排除关键字ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.添加至排除关键字ToolStripMenuItem.Text = "添加至排除关键字";
            this.添加至排除关键字ToolStripMenuItem.Click += new System.EventHandler(this.添加至排除关键字ToolStripMenuItem_Click);
            // 
            // 发送此封包ToolStripMenuItem
            // 
            this.发送此封包ToolStripMenuItem.Name = "发送此封包ToolStripMenuItem";
            this.发送此封包ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.发送此封包ToolStripMenuItem.Text = "发送此封包";
            this.发送此封包ToolStripMenuItem.Click += new System.EventHandler(this.发送此封包ToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1157, 236);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "发送封包";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(983, 32);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(258, 200);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(983, 268);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(258, 63);
            this.textBox2.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1157, 337);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "聊天";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1244, 25);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.刷新ToolStripMenuItem.Text = "刷新";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1157, 366);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "行走";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(1006, 368);
            this.textBox3.MaxLength = 4;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(58, 21);
            this.textBox3.TabIndex = 8;
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numKeyPress);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(1093, 368);
            this.textBox4.MaxLength = 4;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(58, 21);
            this.textBox4.TabIndex = 9;
            this.textBox4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numKeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F);
            this.label1.Location = new System.Drawing.Point(981, 365);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 24);
            this.label1.TabIndex = 10;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 18F);
            this.label2.Location = new System.Drawing.Point(1068, 365);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "Y";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1157, 396);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 12;
            this.button4.Text = "地图";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(981, 477);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 13;
            this.button5.Text = "清空记录";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(980, 581);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(131, 231);
            this.textBox5.TabIndex = 14;
            this.textBox5.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1014, 561);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "过滤关键字";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 816);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1244, 22);
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // SendCheckBox
            // 
            this.SendCheckBox.AutoSize = true;
            this.SendCheckBox.Checked = true;
            this.SendCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SendCheckBox.Location = new System.Drawing.Point(1034, 819);
            this.SendCheckBox.Name = "SendCheckBox";
            this.SendCheckBox.Size = new System.Drawing.Size(96, 16);
            this.SendCheckBox.TabIndex = 18;
            this.SendCheckBox.Text = "显示发送封包";
            this.SendCheckBox.UseVisualStyleBackColor = true;
            this.SendCheckBox.CheckedChanged += new System.EventHandler(this.SendCheckBox_CheckedChanged);
            // 
            // RecvCheckBox
            // 
            this.RecvCheckBox.AutoSize = true;
            this.RecvCheckBox.Checked = true;
            this.RecvCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RecvCheckBox.Location = new System.Drawing.Point(1136, 820);
            this.RecvCheckBox.Name = "RecvCheckBox";
            this.RecvCheckBox.Size = new System.Drawing.Size(96, 16);
            this.RecvCheckBox.TabIndex = 19;
            this.RecvCheckBox.Text = "显示接收封包";
            this.RecvCheckBox.UseVisualStyleBackColor = true;
            this.RecvCheckBox.CheckedChanged += new System.EventHandler(this.RecvCheckBox_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 820);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(221, 12);
            this.label4.TabIndex = 20;
            this.label4.Text = "本次连接中，发送封包0条，接收封包0条";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(984, 533);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(39, 21);
            this.textBox6.TabIndex = 21;
            this.textBox6.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            this.textBox6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numKeyPress);
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(1034, 533);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(39, 21);
            this.textBox7.TabIndex = 22;
            this.textBox7.TextChanged += new System.EventHandler(this.textBox7_TextChanged);
            this.textBox7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numKeyPress);
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(1152, 535);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(39, 21);
            this.textBox8.TabIndex = 23;
            this.textBox8.TextChanged += new System.EventHandler(this.textBox8_TextChanged);
            this.textBox8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numKeyPress);
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(1202, 535);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(39, 21);
            this.textBox9.TabIndex = 24;
            this.textBox9.TextChanged += new System.EventHandler(this.textBox9_TextChanged);
            this.textBox9.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numKeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1023, 538);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1191, 540);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "-";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(979, 513);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 12);
            this.label7.TabIndex = 27;
            this.label7.Text = "显示发送封包序号区间";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1120, 513);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 12);
            this.label8.TabIndex = 28;
            this.label8.Text = "显示接收封包序号区间";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(1112, 581);
            this.textBox10.Multiline = true;
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(131, 231);
            this.textBox10.TabIndex = 29;
            this.textBox10.TextChanged += new System.EventHandler(this.textBox10_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1150, 562);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 30;
            this.label9.Text = "排除关键字";
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(1052, 422);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(190, 21);
            this.textBox11.TabIndex = 31;
            this.textBox11.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numKeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(981, 427);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 32;
            this.label10.Text = "伪造米米号";
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(1034, 450);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(206, 21);
            this.textBox12.TabIndex = 33;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(980, 454);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 34;
            this.label11.Text = "伪造凭证";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1244, 838);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.RecvCheckBox);
            this.Controls.Add(this.SendCheckBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.webBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "赛尔号登录器  by iyzyi";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 复制此包明文ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制此包密文ToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem 编辑此包数据ToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制此类封包ToolStripMenuItem;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label3;
        private StatusStrip statusStrip1;
        private CheckBox SendCheckBox;
        private CheckBox RecvCheckBox;
        private Label label4;

        // 封包log的列表，可将其显示到UI列表中。
        private List<ListViewItem> PacketLogList = new List<ListViewItem>();
        private int iSendPacketNum = 0;
        private int iRecvPacketNum = 0;
        private TextBox textBox6;
        private TextBox textBox7;
        private TextBox textBox8;
        private TextBox textBox9;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox textBox10;
        private Label label9;
        private ToolStripMenuItem 添加至排除关键字ToolStripMenuItem;
        private ToolStripMenuItem 发送此封包ToolStripMenuItem;
        public TextBox textBox11;
        private Label label10;
        public TextBox textBox12;
        private Label label11;
    }
}