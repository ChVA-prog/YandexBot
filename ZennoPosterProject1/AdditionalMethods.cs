using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using DataBaseProfileAndProxy;
using System;
using System.Threading;
using ZennoPosterEmulation;

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
    }
}
