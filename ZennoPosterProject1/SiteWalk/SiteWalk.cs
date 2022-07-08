using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.CommandCenter;
using System;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterProject1;


namespace ZennoPosterSiteWalk
{
    class SiteWalk
    {
        readonly IZennoPosterProjectModel Project;
        readonly Instance instance;

        public SiteWalk(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.Project = project;           
        }
        public void SiteRandomWalk()
        {
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance,Project);
            Random random = new Random();
            string HtmlElement = "//p";

            HtmlElementCollection hec = instance.ActiveTab.FindElementsByXPath(HtmlElement);

            int CountElement = hec.Count;

            if (CountElement == 0)
            {
                HtmlElement = "//a";
                hec = instance.ActiveTab.FindElementsByXPath(HtmlElement);
                CountElement = hec.Count;
            }

            while (CountElement > 30)
            {
                CountElement = CountElement / 2;
            }
            
            int CounElementToLearn = random.Next(0, CountElement);
            for (int i = 0; i <= CounElementToLearn; i++)
            {
                HtmlElement he = instance.ActiveTab.FindElementByXPath(HtmlElement, i);
                swipeAndClick.SwipeToElement(he);
                Thread.Sleep(random.Next(3000, 5000));
            }
        }//Рандомное изучение сайта
    }
}
