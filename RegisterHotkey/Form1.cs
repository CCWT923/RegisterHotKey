using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace RegisterHotkey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        IntPtr thisWindow;
        private void Form1_Load(object sender, EventArgs e)
        {
            thisWindow = HotKeyManager.FindWindow(null, this.Text);
            bool success = false;
            if(thisWindow != IntPtr.Zero)
            {
                //若要注册多个修饰键，可以使用位或运算符
                success = HotKeyManager.RegisterHotKey(thisWindow, 1, (uint)HotKeyManager.ModifierKey.Shift | (uint)HotKeyManager.ModifierKey.Control, (uint)Keys.Q);
                Lbl_Info.Text = "注册" + (success ? "成功。" : "失败！");
            }
            else
            {
                Lbl_Info.Text = "Error!";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            HotKeyManager.UnregisterHotKey(thisWindow, 1);
        }

        protected override void WndProc(ref Message keyPressed)
        {
            
            if(keyPressed.Msg == HotKeyManager.WM_HOTKEY)
            {
                Lbl_Info.Text = DateTime.Now.ToString("hh:mm:ss") + "，热键被按下";
            }
            //移动窗口
            if(keyPressed.Msg == 0x0003)
            {
                
            }
            //获得焦点
            if(keyPressed.Msg == 0x0007)
            {
                Lbl_Info.BackColor = Color.Green;
            }
            //失去焦点
            if(keyPressed.Msg == 0x0008)
            {
                Lbl_Info.BackColor = SystemColors.Control;
            }
            base.WndProc(ref keyPressed);
        }
    }
}
