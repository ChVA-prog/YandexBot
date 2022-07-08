using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using ZennoPosterDataBaseAndProfile;
using ZennoPosterProxy;
using System;
using System.Threading;
using ZennoPosterEmulation;

namespace ZennoPosterProject1
{
    public class AdditionalMethods       
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;
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


        public HtmlElement hep { get; set; }
        public void WaitHtmlElement(string he)
        {
            Random random = new Random();
            instance.ActiveTab.WaitDownloading();          

            hep = instance.ActiveTab.FindElementByXPath(he, 0);
            while (hep.IsVoid)
            {
                project.SendInfoToLog("Ждем появления HtmlElement", true);
                Thread.Sleep(random.Next(4000, 6000));
                hep = instance.ActiveTab.FindElementByXPath(he, 0);
            }
            Thread.Sleep(random.Next(2000, 4000));
        }       
    }
}
