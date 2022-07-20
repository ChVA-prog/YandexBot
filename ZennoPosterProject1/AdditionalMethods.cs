using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using DataBaseProfileAndProxy;
using System;
using System.Threading;
using ZennoPosterEmulation;
using NLog;
using NLog.Config;
using NLog.Targets;
using System.Text;

namespace ZennoPosterProject1
{
    public class AdditionalMethods       
    {
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
            Random random = new Random();
            instance.ActiveTab.WaitDownloading();
            Thread.Sleep(random.Next(10000,15000));
            instance.ActiveTab.WaitDownloading();
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
            var name = DateTime.Now.ToString("dd.MM.yyyy");
            var path = project.Directory + "/Logs";


            if (LogManager.Configuration == null)
            {
                LogManager.Configuration = new LoggingConfiguration();
            }

            if (LogManager.Configuration.FindRuleByName(name) != null || LogManager.Configuration.FindTargetByName(name) != null);

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
