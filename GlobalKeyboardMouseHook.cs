using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace folder_Protector
{
    public class GlobalKeyboardMouseHook : IDisposable
    {
        private static int _keyboardHookId = 0;
        private static int _mouseHookId = 0;
        private static HookProc _keyboardProc;
        private static HookProc _mouseProc;
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE_LL = 14;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_RBUTTONUP = 0x0205;

        public GlobalKeyboardMouseHook()
        {
            _keyboardProc = new HookProc(KeyboardHookCallback);
            _mouseProc = new HookProc(MouseHookCallback);
            _keyboardHookId = SetHook(WH_KEYBOARD_LL, _keyboardProc);
            _mouseHookId = SetHook(WH_MOUSE_LL, _mouseProc);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(_keyboardHookId);
            UnhookWindowsHookEx(_mouseHookId);
        }

        private static int SetHook(int idHook, HookProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(idHook, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (Control.ModifierKeys == Keys.Control && (vkCode == (int)Keys.C || vkCode == (int)Keys.V || vkCode == (int)Keys.X)||vkCode==(int)Keys.Delete)
                {
                    return (IntPtr)1; // Return a non-zero value to block the key press
                }
            }
            return CallNextHookEx(_keyboardHookId, nCode, wParam, lParam);
        }

        private static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_RBUTTONDOWN || wParam == (IntPtr)WM_RBUTTONUP))
            {
                return (IntPtr)1; // Return a non-zero value to block the right-click
            }
            return CallNextHookEx(_mouseHookId, nCode, wParam, lParam);
        }

        #region DLL Imports

        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(int hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(int hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion
    }
}
