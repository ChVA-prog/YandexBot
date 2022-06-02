using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using System.Threading;
using System;
using ZennoPosterEmulation;
using ZennoPosterYandexWalk;

namespace ZennoPosterSiteWalk
{
    class SiteNavigate
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;

        public SiteNavigate(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.project = project;
        }


        public void GetRandomSite()
        {
            Random random = new Random();
            SiteWalkSettings siteWalkSettings = new SiteWalkSettings(instance,project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            YandexWalkSettings yandexWalkSettings = new YandexWalkSettings(instance, project);
            siteWalkSettings.ShuffleListSitesForVisit();
           
            foreach (string url in SiteWalkSettings.SiteVisitUrl)
            {
                int CountGetUrl = SiteWalkValue.CountSitesToVisit.ParseRangeValueInt().ValueRandom;

                instance.ActiveTab.Navigate(url);
                instance.ActiveTab.WaitDownloading();
                
                for (int i = 0; i < CountGetUrl; i++)
                {
                    HtmlElementCollection LinksInSite = instance.ActiveTab.FindElementsByXPath("//span[contains(@class, 'elementor-button-text')] ");
                    HtmlElement LinkInSite = instance.ActiveTab.FindElementByXPath
                        ("//span[contains(@class, 'elementor-button-text')]", random.Next(0, LinksInSite.Count));

                    project.SendInfoToLog(Convert.ToString(LinksInSite.Count));
                    if (LinkInSite == null || LinkInSite.IsVoid)
                    {
                        break;
                    }
                    swipeAndClick.SwipeToElement(LinkInSite);
                    Thread.Sleep(random.Next(2000, 5000));
                    yandexWalkSettings.CloseUnnecessaryWindows();
                    

                }

               
            }


        }
    }
}
