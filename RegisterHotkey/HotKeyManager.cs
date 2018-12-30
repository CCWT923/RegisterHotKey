using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace RegisterHotkey
{
    class HotKeyManager
    {
        #region Windows API
        /// <summary>
        /// 热键定义
        /// </summary>
        public const int WM_HOTKEY = 0x0312;
        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="id">热键的ID值，用于跟踪管理此热键</param>
        /// <param name="fsModifiers">修饰键，Alt、Shift、Ctrl和Windows键</param>
        /// <param name="vk">某个字母键</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hwnd, int id, uint fsModifiers, uint vk);

        /// <summary>
        /// 取消注册热键
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="id">注册热键时的ID值</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hwnd, int id);

        /// <summary>
        /// 查找指定类名或标题的窗口句柄
        /// </summary>
        /// <param name="className">窗口类名，不指定则使用null值</param>
        /// <param name="windowText">窗口标题，不指定则使用null值</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowText);
        /// <summary>
        /// 修饰键
        /// </summary>
        public enum ModifierKey
        {
            Alt = 0x0001,
            Control = 0x0002,
            Shift = 0x0004,
            Windows = 0x0008
        }
        #endregion
    }
}
