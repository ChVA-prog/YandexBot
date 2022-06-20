using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterProject1;

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
        Random random = new Random();
        public void GoToSearchQuery()
        { 
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);
            
            string RandomSearchQueries = new YandexWalkSettings(instance, Project).GetRandomSearchQueries();

            GoToYandex();

            HtmlElement HtmlElementInputSearch = instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementInputSearch, 0);
            HtmlElement HtmlElementSearchButton = instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementSearchButton, 0);

            Project.SendInfoToLog("Вводим поисковый запрос в строку",true);

            swipeAndClick.SetText(HtmlElementInputSearch, RandomSearchQueries);

            if (String.IsNullOrEmpty(HtmlElementInputSearch.GetAttribute("value")) || String.IsNullOrWhiteSpace(HtmlElementInputSearch.GetAttribute("value")))
            {
                Project.SendErrorToLog("Поисковый запрос не ввелся в строку",true);
                new AdditionalMethods(instance, Project).ErrorExit();
            }

            Project.SendInfoToLog("Кликаем по кнопке найти",true);

            swipeAndClick.ClickToElement(HtmlElementSearchButton);
            new AdditionalMethods(instance,Project).WaitDownloading();
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
            HtmlElement HtmElementCookies = instance.ActiveTab.FindElementByXPath("//button[contains(@Class, 'sc-pNWxx sc-jrsJCI dryRrI kcLcLu')]", 0);

            if (!HtmElementReklama.IsVoid)
            {
                swipeAndClick.ClickToElement(HtmElementCookies);
            }
        }//Закрытие рекламы

        public void GoToYandex()
        {           
            Project.SendInfoToLog("Переходим на страницу яндекса", true);
            instance.ActiveTab.Navigate(new YandexWalkSettings(instance,Project).GetRandomYandexHost());
            new AdditionalMethods(instance, Project).WaitDownloading();
        }//Переход в яндекс
    }
}
