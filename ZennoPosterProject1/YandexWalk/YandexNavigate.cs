﻿using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterProject1;
using ZennoPosterSiteWalk;

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
            string RandomSearchQueries = new YandexWalkSettings(instance, Project).GetRandomSearchQueries();

            GoToYandex();

            HtmlElement HtmlElementInputSearch = instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementInputSearch, 0);
            HtmlElement HtmlElementSearchButton = instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementSearchButton, 0);

            Project.SendInfoToLog("Вводим поисковый запрос в строку",true);
            swipeAndClick.SetText(HtmlElementInputSearch, RandomSearchQueries);

            if (String.IsNullOrEmpty(HtmlElementInputSearch.GetAttribute("value")))
            {
                throw new Exception("Поисковый запрос не ввелся в строку");
            }

            Project.SendInfoToLog("Кликаем по кнопке найти",true);

            //Сделать проверку нажатия кнопки найти 

            swipeAndClick.ClickToElement(HtmlElementSearchButton);
            new AdditionalMethods(instance,Project).WaitDownloading();
            CloseYandexTrash();
        }//Ввод поискового запроса и переход по нему



        public void GoToYandex()
        {           
            Project.SendInfoToLog("Переходим на страницу яндекса", true);
            instance.ActiveTab.Navigate(new YandexWalkSettings(instance,Project).GetRandomYandexHost());
            new AdditionalMethods(instance, Project).WaitDownloading();
        }//Переход в яндекс
        public void CloseUnnecessaryWindows()
        {
            if (instance.AllTabs.Length > 1)
            {
                instance.ActiveTab.Close();
                Project.SendInfoToLog("Закрыли лишнюю вкладку.", true);
            }
            else
            {
                if (!instance.ActiveTab.URL.Contains("/search/") || instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementSearchResultsCard, 0).IsVoid)
                {
                    instance.ActiveTab.MainDocument.EvaluateScript("javascript:history.back()");
                }
            }
        }//Закрываем лишнюю вкладку
        public bool GoSearchCard(HtmlElement LearnElement)
        {
            SiteWalk siteWalk = new SiteWalk(instance, Project);
            YandexWalkSettings yandexWalkSettings = new YandexWalkSettings(instance, Project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);
            Random random = new Random();

            HtmlElement GetSearchCard = LearnElement.FindChildByXPath(YandexWalkValue.HtmlElementUrlSearchCard, 0);

            string CurenSite = GetSearchCard.InnerHtml;
            string ClearCurenSite = CurenSite.GetUrlToDomain();

            if (String.IsNullOrEmpty(ClearCurenSite) || String.IsNullOrWhiteSpace(ClearCurenSite))
            {
                return true;
            }
            else
            {
                Project.SendInfoToLog("Делаем проверку на запрещенные для перехода сайты", true);
                if (yandexWalkSettings.CheckMyUrl(CurenSite))
                {
                    return true;
                }
            }

            Project.SendInfoToLog("Переходим в карточку " + ClearCurenSite, true);
            swipeAndClick.SwipeAndClickToElement(LearnElement);

            new AdditionalMethods(instance, Project).WaitDownloading();
            CloseYandexTrash();
            Project.SendInfoToLog("Изучаем сайт: " + ClearCurenSite, true);
            siteWalk.SiteRandomWalk();
            Thread.Sleep(random.Next(4000, 8000));

            return false;
        }//Переходим в карточку
        public void CloseYandexTrash()
        {
            Random random = new Random();
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance,Project);
            instance.ActiveTab.WaitDownloading();
            Thread.Sleep(random.Next(2000, 5000));
            instance.ActiveTab.WaitDownloading();

            HtmlElement alisa = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Закрыть')]", 0); //Алиса
            HtmlElement kinopoisk = instance.ActiveTab.FindElementByXPath("//a[starts-with(text(),'Остаться')]", 0); //Кинопоиск
            HtmlElement dzen = instance.ActiveTab.FindElementByXPath("//span[contains(@class, 'maba')]", 0); //Дзен
            HtmlElement Yamerket = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Продолжить на сайте')]", 0); //Яндекс маркет
            HtmlElement YandexBrowser = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Позже')] | //span[starts-with(text(),'Не сейчас')]", 0); //Яндекс браузер
            HtmlElement Yamerket2 = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Скрыть')]", 0); //Яндекс маркет2
           

            if (!alisa.IsVoid)
            {
                swipeAndClick.SwipeAndClickToElement(alisa);
            }
            if (!kinopoisk.IsVoid)
            {
                swipeAndClick.SwipeAndClickToElement(kinopoisk);
            }
            if (!dzen.IsVoid)
            {
                swipeAndClick.SwipeAndClickToElement(dzen);
            }
            if (!Yamerket.IsVoid)
            {
                swipeAndClick.SwipeAndClickToElement(Yamerket);
            }
            if (!YandexBrowser.IsVoid)
            {
                swipeAndClick.SwipeAndClickToElement(YandexBrowser);
            }
            if (!Yamerket2.IsVoid)
            {
                swipeAndClick.SwipeAndClickToElement(Yamerket2);
            }
        }
    }
}
