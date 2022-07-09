using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using System;
using ZennoPosterEmulation;
using System.Threading;


namespace ZennoPosterSiteWalk
{
    class SiteWalk
    {
        readonly IZennoPosterProjectModel Project;
        readonly Instance instance;
        private string HtmlElement { get; set; }
        private int CountElementToSite { get; set; }

        public SiteWalk(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.Project = project;           
        }
        public void SiteRandomWalk()
        {
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance,Project);
            Random random = new Random();
            HtmlElement = "//p";

            HtmlElementCollection SiteHtmlElement = instance.ActiveTab.FindElementsByXPath(HtmlElement);

            CountElementToSite = SiteHtmlElement.Count;

            if (CountElementToSite <= 5)
            {
                HtmlElement = "//a";
                SiteHtmlElement = instance.ActiveTab.FindElementsByXPath(HtmlElement);
                CountElementToSite = SiteHtmlElement.Count;
            }

            while (CountElementToSite > 30)
            {
                CountElementToSite = CountElementToSite / 2;
            }
            
            int CounElementToLearn = random.Next(0, CountElementToSite);
            for (int i = 0; i <= CounElementToLearn; i++)
            {
                HtmlElement he = instance.ActiveTab.FindElementByXPath(HtmlElement, i);
                swipeAndClick.SwipeToElement(he);
                Thread.Sleep(random.Next(3000, 5000));
            }
        }//Рандомное изучение сайта
    }
}
