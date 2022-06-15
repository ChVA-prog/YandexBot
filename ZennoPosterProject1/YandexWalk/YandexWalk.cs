using System.Collections.Generic;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterProject1;

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
            int CountPage = new YandexWalkSettings(instance,Project).GetRandomPageCountSearch();
           
            yandexNavigate.GoToSearchQuery();
            Project.SendInfoToLog("Будем изучать " + CountPage + " страниц",true);

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
                        Project.SendErrorToLog("Не удалось получить карточки поисковой выдачи");
                        new AdditionalMethods(instance, Project).ErrorExit();
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

                CounterPage++;
            }
            while (CountPage > CounterPage);                      
        } //Запуск бродилки по яндексу
    }
}
