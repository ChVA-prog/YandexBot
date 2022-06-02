using System;
using System.Collections.Generic;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;

namespace ZennoPosterNitkaDogWalk
{
    class SiteNavigateMethods
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;

        public SiteNavigateMethods(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.project = project;
        }


        public void HomePageWalk()
        {
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            Random random = new Random();

            HtmlElementCollection hec = instance.ActiveTab.FindElementsByXPath("//span[contains(@class, 'elementor-button-text')]");
            int CountElement = hec.Count;
            List<int> vs = new List<int>();

            for (int i = 0; i < CountElement; i++)
            {
                int rnd = random.Next(0, CountElement);
                if (vs.Contains(rnd))
                {
                    i--;
                    continue;
                }
                vs.Add(rnd);
            }
            foreach (int item in vs)
            {
                HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[contains(@class, 'elementor-button-text')]", item);
                swipeAndClick.SwipeToElement(he);
            }

        }
    }
}
