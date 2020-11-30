using System;
using System.Text;
using WindowsInput;
using System.Runtime.InteropServices;

namespace PasswordFinder
{
    partial class Program
    {
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        static int SW_SHOWNORMAL = 1;
        static int SW_SHOWMINIMIZED = 2;
        static int SW_SHOWMAXIMIZED = 3;

        static InputSimulator Sendkey = new InputSimulator();
    }

}
