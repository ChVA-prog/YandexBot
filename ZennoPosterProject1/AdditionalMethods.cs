using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using System;
using System.Threading;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using ZennoPosterEmulation;
using ZennoPosterYandexWalk;

namespace ZennoPosterProject1
{ 
    public class AdditionalMethods       
    {
        public static object LockList = new object();
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;
        public HtmlElement HtmlElementWhichWait { get; set; }
        int CounterFuckCapcha = 0;

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
        public HtmlElement WaitHtmlElement(string He, int NumberElement)
        {
            Random random = new Random();
            instance.ActiveTab.WaitDownloading();
            HtmlElementWhichWait = instance.ActiveTab.FindElementByXPath(He, NumberElement);

            while (HtmlElementWhichWait.IsVoid)
            {
                project.SendInfoToLog("Ждем появления HtmlElement", true);
                Thread.Sleep(random.Next(4000, 6000));
                HtmlElementWhichWait = instance.ActiveTab.FindElementByXPath(He, NumberElement);
            }

            Thread.Sleep(random.Next(2000, 4000));
            return HtmlElementWhichWait;
        }
        public bool NLogCofig()
        {
            lock (LockList)
            {
                var name = DateTime.Now.ToString("dd.MM.yyyy");
                var path = project.Directory + "/Logs";

                if (LogManager.Configuration == null)
                {
                    LogManager.Configuration = new LoggingConfiguration();
                }

                if (LogManager.Configuration.FindRuleByName(name) != null || LogManager.Configuration.FindTargetByName(name) != null) return false;
                var target = new FileTarget();
                target.Layout = "${time} | ${threadid} | ${callsite} | ${level} | ${message} ";
                target.FileName = $"{path}/{name}.txt";
                target.KeepFileOpen = false;
                target.Encoding = Encoding.UTF8;
                target.Name = name;

                LogManager.Configuration.AddRule(LogLevel.Trace, LogLevel.Fatal, target, name);
                LogManager.ReconfigExistingLoggers();

                Program.logger.Debug("Настроили файл конфиг для NLog");
                return true;
            }
        }
        public void InstanceScreen()
        {
            if (!Directory.Exists(project.Directory + @"\ErrorScreen\"))
            {
                Directory.CreateDirectory(project.Directory + @"\ErrorScreen\");
            }
            string path = project.Directory +  @"\ErrorScreen\" + DateTime.Now.ToString("dd.MM.yyyy.HH.mm.ss") + ".png";
            File.WriteAllBytes(path, Convert.FromBase64String(instance.ActiveTab.FindElementByTag("html", 0).DrawToBitmap(false)));
        }
        public void FuckCapcha()
        {
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            Random random = new Random();
            YandexWalkValue yandexWalkValue = new YandexWalkValue(project);

            if (!instance.ActiveTab.FindElementByXPath(yandexWalkValue.HtmlElementPageIamNotRobot, 0).IsVoid)
            {
                Program.logger.Debug("Нарвались на проверку робота.");
                project.SendInfoToLog("Налетели на капчу");
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(yandexWalkValue.HtmlElementIAmNotRobot, 0));
                Program.logger.Debug("Нажали я не робот.");
                Thread.Sleep(random.Next(6000, 10000));
            }

            if (!instance.ActiveTab.FindElementByXPath(yandexWalkValue.HtmlElementPageCapcha, 0).IsVoid)
            {
                Program.logger.Debug("Просят ввести капчу.");
                HtmlElement Captcha = instance.ActiveTab.FindElementByXPath(yandexWalkValue.HtmlElementCapchaImage, 0);
                Program.logger.Debug("Делаем запрос с капчей к RuCaptcha.");
                string recognition = ZennoPoster.CaptchaRecognition("RuCaptcha.dll", Captcha.DrawToBitmap(true), "").Split('-')[0];

                if (recognition.Contains("orry") && CounterFuckCapcha !=5)
                {
                    Program.logger.Warn("Не пришел ответ с капчей, пробуем еще раз.");
                    Thread.Sleep(random.Next(1500, 2000));
                    CounterFuckCapcha++;
                    FuckCapcha();
                }

                Program.logger.Debug("Получили ответ: " + recognition);
                char[] InputText = recognition.ToCharArray();

                foreach (char InputChar in InputText)
                {
                    instance.SendText(Convert.ToString(InputChar), 0);
                    Thread.Sleep(swipeAndClick.LatencyKeySetText);
                }

                Program.logger.Debug("Отправляем введенную капчу");               
                swipeAndClick.ClickToElement(instance.ActiveTab.FindElementByXPath(yandexWalkValue.HtmlElementSendCapcha, 0));
                instance.ActiveTab.WaitDownloading();
                Thread.Sleep(random.Next(2000, 5000));

                if (!instance.ActiveTab.FindElementByXPath(yandexWalkValue.HtmlElementCapchaError, 0).IsVoid && CounterFuckCapcha !=5)
                {
                    Program.logger.Warn("Неверно ввели капчу, пробуем еще раз.");
                    new AdditionalMethods(instance, project).InstanceScreen();
                    Thread.Sleep(random.Next(1500, 2000));
                    CounterFuckCapcha++;
                    FuckCapcha();
                }
                Program.logger.Debug("Капча успешно пройдена.");
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
