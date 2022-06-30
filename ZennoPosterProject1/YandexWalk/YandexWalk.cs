using System.Collections.Generic;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterProject1;
using System;

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
            Project.SendInfoToLog("Запускаем бродилку по яндексу.", true);
            YandexWalkSettings yandexWalkSettings = new YandexWalkSettings(instance, Project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);
            YandexNavigate yandexNavigate = new YandexNavigate(instance, Project);
                                                      
            int CounterPage = 1;
            int CountLearnPage = new YandexWalkSettings(instance,Project).GetRandomPageCountSearch();            
            try
            {
              yandexNavigate.GoToSearchQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при переходе в яндекс и вводе запроса: " + ex.Message);
            }
            
            Project.SendInfoToLog("Будем изучать " + CountLearnPage + " страниц",true);

            do
            {
                if (CounterPage != 1)
                {
                    YandexWalkValue.HtmlElementSearchResultsCard = YandexWalkValue.HtmlElementPageNumber.Replace("num", CounterPage.ToString());
                }

                Project.SendInfoToLog("Номер страницы " + CounterPage, true);

                int CounterAttemptGetSumCard = 0;

                do
                {
                    if(CounterAttemptGetSumCard == 10)
                    {
                        throw new Exception("Не удалось получить карточки поисковой выдачи");
                    }

                    CountLearnCard = instance.ActiveTab.FindElementsByXPath(YandexWalkValue.HtmlElementSearchResultsCard).Count.CalcPercentLearnCard
                                        (YandexWalkValue.CountLearnCard.ParseRangeValueInt().ValueRandom);
                    Thread.Sleep(2000);
                    CounterAttemptGetSumCard++;
                }
                while (CountLearnCard == 0);
                
                Project.SendInfoToLog("Будем изучать " + CountLearnCard + " карточек", true);

                List<int> SearchCardList = yandexWalkSettings.CountLearnCard(CountLearnCard);

                yandexWalkSettings.GoOrLearnCard(SearchCardList);

                Project.SendInfoToLog("Переходим на следующую страницу", true);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementNextPageButton, 0));
                new AdditionalMethods(instance, Project).WaitDownloading();
                new YandexNavigate(instance, Project).CloseYandexTrash();
                CounterPage++;
            }
            while (CountLearnPage == CounterPage);                      
        } //Запуск бродилки по яндексу
    }
}
