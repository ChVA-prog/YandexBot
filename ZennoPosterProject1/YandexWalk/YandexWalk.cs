using System;
using System.Collections.Generic;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;

namespace ZennoPosterYandexWalk
{
    public class YandexWalk
    {
        readonly Instance instance;
        readonly IZennoPosterProjectModel Project;

        public YandexWalk(Instance _instance, IZennoPosterProjectModel _project)
        {
            this.instance = _instance;
            this.Project = _project;
        }
        public int CountLearnCard { get; set; }

        public void GoYandexWalk()
        {
            YandexWalkSettings yandexWalkSettings = new YandexWalkSettings(instance, Project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);
            YandexNavigate yandexNavigate = new YandexNavigate(instance, Project);
            YandexWalkValue yandexWalk = new YandexWalkValue(instance, Project);
            Random random = new Random();
                                           
            int CounterPage = 1;
            int CountPage = yandexWalkSettings.GetRandomPageCountSearch();

            Project.SendInfoToLog("Будем изучать " + CountPage + " страниц");

            yandexNavigate.GoToSearchQuery();


            do
            {               
                if (CounterPage == 1)
                {
                    CountLearnCard = instance.ActiveTab.FindElementsByXPath(YandexWalkValue.HtmlElementSearchResultsCard).Count.CalcPercentLearnCard
                     (YandexWalkValue.CountLearnCard.ParseRangeValueInt().ValueRandom);
                    Project.SendInfoToLog("Будем изучать " + CountLearnCard + " карточек");
                }
                else
                {
                    Project.SendInfoToLog("Номер страницы " + CounterPage);

                    YandexWalkValue.HtmlElementSearchResultsCard = YandexWalkValue.HtmlElementPageNumber.Replace("num", CounterPage.ToString());

                    CountLearnCard = instance.ActiveTab.FindElementsByXPath(YandexWalkValue.HtmlElementSearchResultsCard).Count.CalcPercentLearnCard
                    (YandexWalkValue.CountLearnCard.ParseRangeValueInt().ValueRandom);

                    Project.SendInfoToLog("Будем изучать " + CountLearnCard + " карточек");
                }


                List<int> SearchCardList = new List<int>();

                for (int i = 0; i < CountLearnCard; i++)
                {
                    int rnd = random.Next(0, 9);
                    if (SearchCardList.Contains(rnd))
                    {
                        i--;
                        continue;
                    }
                    SearchCardList.Add(rnd);
                }                

                foreach (int i in SearchCardList)
                {
                    
                    HtmlElement LearnElement = instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementSearchResultsCard, i);

                    if(random.Next(0,100) < YandexWalkValue.CountGetCard.ParseRangeValueInt().ValueRandom)
                    {
                        HtmlElement GetSearchCard = LearnElement.FindChildByXPath(YandexWalkValue.HtmlElementUrlSearchCard, 0);

                        string CurenSite = GetSearchCard.GetAttribute("href");
                        string ClearCurenSite = CurenSite.GetUrlToDomain();

                        if (String.IsNullOrEmpty(ClearCurenSite) || String.IsNullOrWhiteSpace(ClearCurenSite))
                        {

                            continue;
                        }
                        else
                        {
                            Project.SendInfoToLog("Делаем проверку на наличие нашего сайта " + ClearCurenSite);
                            if (yandexWalkSettings.CheckMyUrl(CurenSite))
                                continue;
                        }
                        Project.SendInfoToLog("Переходим в карточку " + ClearCurenSite);
                        swipeAndClick.SwipeAndClickToElement(LearnElement);
                        Thread.Sleep(random.Next(4000,8000));
                    }

                    else
                    {
                        Project.SendInfoToLog("Просто изучаем карточку ");

                        swipeAndClick.SwipeToElement(LearnElement);
                    }





                    if(instance.AllTabs.Length > 1)
                    {
                        Project.SendInfoToLog("Закрываем активную вкладку");
                        instance.ActiveTab.Close();                       
                    }
                    else
                    {
                        if(!instance.ActiveTab.URL.Contains("/search/") || instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementSearchResultsCard,0).IsVoid)
                        {
                            instance.ActiveTab.MainDocument.EvaluateScript("javascript:history.back()");
                        }
                        Thread.Sleep(random.Next(4000,8000));
                    }

                    
                }
                Project.SendInfoToLog("Переходим на следующую страницу");
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementNextPageButton, 0));

                CounterPage++;
            }
            while (CountPage > CounterPage);
                      
        }

    }
}
