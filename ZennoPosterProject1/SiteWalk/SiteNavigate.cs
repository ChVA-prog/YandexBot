using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using System.Threading;
using System;
using ZennoPosterEmulation;
using ZennoPosterYandexWalk;

namespace ZennoPosterSiteWalk
{
    class SiteNavigate : SiteWalkValue
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;

        public SiteNavigate(Instance instance, IZennoPosterProjectModel project) : base (instance,project)
        {
            this.instance = instance;
            this.project = project;
        }
        public void GetRandomSite()
        {
            Random random = new Random();
            SiteWalkSettings siteWalkSettings = new SiteWalkSettings(instance,project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            YandexNavigate yandexNavigate = new YandexNavigate(instance, project);

            siteWalkSettings.ShuffleListSitesForVisit();
           
            foreach (string url in SiteWalkSettings.SiteVisitUrl)
            {
                int CountGetUrl = CountSitesToVisit.ParseRangeValueInt().ValueRandom;

                instance.ActiveTab.Navigate(url);
                instance.ActiveTab.WaitDownloading();
                
                for (int i = 0; i < CountGetUrl; i++)
                {
                    HtmlElementCollection LinksInSite = instance.ActiveTab.FindElementsByXPath("//p");
                    HtmlElement LinkInSite = instance.ActiveTab.FindElementByXPath
                        ("//p", random.Next(0, LinksInSite.Count));

                    if (LinkInSite == null || LinkInSite.IsVoid)
                    {
                        break;
                    }
                    swipeAndClick.SwipeToElement(LinkInSite);
                    Thread.Sleep(random.Next(2000, 5000));
                    yandexNavigate.CloseUnnecessaryWindows();
                }
            }
        }//Переход на рандомный сайт из списка и его изучение //TODO
    }
}
