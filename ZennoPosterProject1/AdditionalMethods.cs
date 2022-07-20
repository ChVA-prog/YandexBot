using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using System;
using System.Threading;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Text;
using System.Runtime.InteropServices;

namespace ZennoPosterProject1
{ 
    public class AdditionalMethods       
    {
        public static object LockList = new object();
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;

        public HtmlElement HtmlElementWhichWait { get; set; }

        public AdditionalMethods(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.project = project;
        }

        public void WaitDownloading()
        {
            Program.logger.Debug("Зашли в метод длительного ожидания загрузки страницы.");
            Random random = new Random();
            instance.ActiveTab.WaitDownloading();
            Thread.Sleep(random.Next(10000,15000));
            instance.ActiveTab.WaitDownloading();
            Program.logger.Debug("Закончили ожидать загрузку страницы.");
        }//Ожидание загрузки страницы
        public void WaitHtmlElement(string he)
        {
            Random random = new Random();
            instance.ActiveTab.WaitDownloading();

            HtmlElementWhichWait = instance.ActiveTab.FindElementByXPath(he, 0);
            while (HtmlElementWhichWait.IsVoid)
            {
                project.SendInfoToLog("Ждем появления HtmlElement", true);
                Thread.Sleep(random.Next(4000, 6000));
                HtmlElementWhichWait = instance.ActiveTab.FindElementByXPath(he, 0);
            }
            Thread.Sleep(random.Next(2000, 4000));
        }
        public void NLogCofig()
        {
            lock (LockList)
            {
                var name = DateTime.Now.ToString("dd.MM.yyyy");
                var path = project.Directory + "/Logs";


                if (LogManager.Configuration == null)
                {
                    LogManager.Configuration = new LoggingConfiguration();
                }

                if (LogManager.Configuration.FindRuleByName(name) != null || LogManager.Configuration.FindTargetByName(name) != null) ;

                var target = new FileTarget();
                target.Layout = "${time} | ${threadid} | ${callsite} | ${level} | ${message} ";
                target.FileName = $"{path}/{name}.csv";
                target.KeepFileOpen = false;
                target.Encoding = Encoding.UTF8;
                target.Name = name;

                LogManager.Configuration.AddRule(LogLevel.Trace, LogLevel.Fatal, target, name);
                LogManager.ReconfigExistingLoggers();
                Program.logger.Debug("Настроили файл конфиг для NLog");
            }
        }
    }
    public class WaitUser
    {
        public static class SWP
        {
            public static readonly int
            NOSIZE = 0x0001,
            NOMOVE = 0x0002,
            NOZORDER = 0x0004,
            NOREDRAW = 0x0008,
            NOACTIVATE = 0x0010,
            DRAWFRAME = 0x0020,
            FRAMECHANGED = 0x0020,
            SHOWWINDOW = 0x0040,
            HIDEWINDOW = 0x0080,
            NOCOPYBITS = 0x0100,
            NOOWNERZORDER = 0x0200,
            NOREPOSITION = 0x0200,
            NOSENDCHANGING = 0x0400,
            DEFERERASE = 0x2000,
            ASYNCWINDOWPOS = 0x4000;
        }

        public static IntPtr HWND_TOPMOST = new IntPtr(-1);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd,
            IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly,
        string lpWindowName);

        /// <summary>
        /// Lock this object to mark part of code for single thread execution
        /// </summary>
        public static object SyncObject = new object();

        public static void ShowOnTopUserAction(string uniqueTitle, int waitSec, Instance instance, IZennoPosterProjectModel project)
        {
            IntPtr hWnd = WaitUser.FindWindowByCaption(IntPtr.Zero, uniqueTitle);
            new Thread(() => {
                System.Threading.Thread.Sleep(1000);
                WaitUser.SetForegroundWindow(hWnd);
                project.SendInfoToLog("Выполнение SetForeground для тайтла: " + uniqueTitle, true);
                WaitUser.SetWindowPos(hWnd, WaitUser.HWND_TOPMOST, 0, 0, 0, 0, WaitUser.SWP.NOMOVE | WaitUser.SWP.NOSIZE);
            }).Start();

            instance.WaitForUserAction(waitSec);
        }
    }
}
