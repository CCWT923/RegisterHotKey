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
        #region Windows API
        public const int WM_HOTKEY = 0x0312;
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hwnd, int id);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowText);
        public enum ModifierKey
        {
            Alt = 0x0001,
            Control = 0x0002,
            Shift = 0x0004,
            Windows = 0x0008
        }
        #endregion
        IntPtr thisWindow;
        private void Form1_Load(object sender, EventArgs e)
        {
            thisWindow = FindWindow(null, this.Text);
            bool success = false;
            if(thisWindow != IntPtr.Zero)
            {
                Console.WriteLine(thisWindow);
                //若要注册多个修饰键，可以使用位或运算符
                success = RegisterHotKey(thisWindow, 1, (uint)ModifierKey.Shift | (uint)ModifierKey.Control, (uint)Keys.Q);
                Lbl_Info.Text = success ? "注册成功。" : "注册失败！";
            }
            else
            {
                Lbl_Info.Text = "Error!";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(thisWindow, 1);
        }

        protected override void WndProc(ref Message keyPressed)
        {
            if(keyPressed.Msg == WM_HOTKEY)
            {
                Lbl_Info.Text = DateTime.Now.ToString("hh:mm:ss") + "，热键被按下";
            }
            base.WndProc(ref keyPressed);
        }
    }
}
