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
        private string HtmlElement { get; set; }
        private int CountElementToSite { get; set; }

        public SiteWalk(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.Project = project;           
        }
        public void SiteRandomWalk()
        {
            Program.logger.Debug("Начинаем рандомно изучать сайт");
            Project.SendInfoToLog("Рандомно изучаем сайт");
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance,Project);
            Random random = new Random();
            HtmlElement = "//p";

            HtmlElementCollection SiteHtmlElement = instance.ActiveTab.FindElementsByXPath(HtmlElement);

            CountElementToSite = SiteHtmlElement.Count;

            if (CountElementToSite <= 5)
            {
                Program.logger.Debug("Кол-во элементов //p на странице меньше 5, будем изучать элементы //a");
                HtmlElement = "//a";
                SiteHtmlElement = instance.ActiveTab.FindElementsByXPath(HtmlElement);
                CountElementToSite = SiteHtmlElement.Count;
            }

            while (CountElementToSite > 30)
            {
                Program.logger.Debug("Кол-во изучаемых элементов ({0}) больше 30, делим их на 2", CountElementToSite);
                CountElementToSite = CountElementToSite / 2;
            }
            
            int CounElementToLearn = random.Next(0, CountElementToSite);
            Program.logger.Debug("Будем изучать {0} элементов.", CounElementToLearn);
            for (int i = 0; i <= CounElementToLearn; i++)
            {               
                HtmlElement he = instance.ActiveTab.FindElementByXPath(HtmlElement, i);
                swipeAndClick.SwipeToElement(he);
                Thread.Sleep(random.Next(3000, 5000));
            }
            Program.logger.Debug("Закончили изучение сайта.");
            Project.SendInfoToLog("Закончили изучение сайта");
        }//Рандомное изучение сайта
    }
}
