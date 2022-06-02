using System;
using System.Collections.Generic;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;

namespace ZennoPosterYandexWalk
{
    class YandexNavigate
    {
        readonly Instance instance;
        readonly IZennoPosterProjectModel Project;

        public YandexNavigate(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.Project = project;
        }
        
        public void GoToSearchQuery()
        { 
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);
            new YandexWalkSettings(instance, Project);
            RandomGeneration randomGeneration = new RandomGeneration(instance, Project);

            string RandomSearchQueries = randomGeneration.GetRandomSearchQueries();

            GoToYandex();

            HtmlElement HtmlElementInputSearch = instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementInputSearch, 0);
            HtmlElement HtmlElementSearchButton = instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementSearchButton, 0);

            Project.SendInfoToLog("Вводим поисковый запрос");

            swipeAndClick.SetText(HtmlElementInputSearch, RandomSearchQueries);

            if (HtmlElementInputSearch.GetAttribute("value") != RandomSearchQueries)
            {
                Project.SendInfoToLog("Поисковый запрос не ввелся в строку");
            }
            Project.SendInfoToLog("Кликаем по кнопке найти");

            swipeAndClick.SwipeAndClickToElement(HtmlElementSearchButton);

            CloseAdvertisement();

        }//Ввод поискового запроса и переход по нему

        private void CloseAdvertisement()
        {
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);
            Thread.Sleep(new Random().Next(3000, 5000));
            HtmlElement HtmElementReklama = instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementCloseAdvertisement, 0);

            if (!HtmElementReklama.IsVoid)
            {
                swipeAndClick.ClickToElement(HtmElementReklama);
            }
        }//Закрытие рекламы

        private void GoToYandex()
        {
            
            Project.SendInfoToLog("Переходим на страницу яндекс");
            instance.ActiveTab.Navigate(new RandomGeneration(instance, Project).GetRandomYandexHost());
            instance.ActiveTab.WaitDownloading();
        }//Переход в яндекс
    }
}
