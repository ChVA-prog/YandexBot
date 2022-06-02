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
        public int CountLearnCard { get; set; }

        public YandexWalk(Instance _instance, IZennoPosterProjectModel _project)
        {
            this.instance = _instance;
            this.Project = _project;
        }
        
        public void GoYandexWalk()
        {
            YandexWalkSettings yandexWalkSettings = new YandexWalkSettings(instance, Project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);
            YandexNavigate yandexNavigate = new YandexNavigate(instance, Project);
            RandomGeneration randomGeneration = new RandomGeneration(instance, Project);
            new YandexWalkValue(instance, Project);
                                                      
            int CounterPage = 1;
            int CountPage = randomGeneration.GetRandomPageCountSearch();
           
            yandexNavigate.GoToSearchQuery();
            Project.SendInfoToLog("Будем изучать " + CountPage + " страниц",true);

            do
            {
                if (CounterPage != 1)
                {
                    YandexWalkValue.HtmlElementSearchResultsCard = YandexWalkValue.HtmlElementPageNumber.Replace("num", CounterPage.ToString());
                }

                Project.SendInfoToLog("Номер страницы " + CounterPage, true);

                CountLearnCard = instance.ActiveTab.FindElementsByXPath(YandexWalkValue.HtmlElementSearchResultsCard).Count.CalcPercentLearnCard
                    (YandexWalkValue.CountLearnCard.ParseRangeValueInt().ValueRandom);

                Project.SendInfoToLog("Будем изучать " + CountLearnCard + " карточек", true);

                List<int> SearchCardList = yandexWalkSettings.CountLearnCard(CountLearnCard);

                yandexWalkSettings.GoOrLearnCard(SearchCardList);

                Project.SendInfoToLog("Переходим на следующую страницу", true);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementNextPageButton, 0));

                CounterPage++;
            }
            while (CountPage > CounterPage);
                      
        } //Запуск бродилки по яндексу

    }
}
