﻿using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterProject1;
using ZennoPosterSiteWalk;
using System.Collections.Generic;

namespace ZennoPosterYandexWalk
{
    class YandexNavigate : YandexWalkValue
    {
        readonly Instance instance;
        readonly IZennoPosterProjectModel Project;

        public YandexNavigate(Instance instance, IZennoPosterProjectModel project) : base(project)
        {
            this.instance = instance;
            this.Project = project;
        }
        public void GoToYandex()
        {
            Program.logger.Info("Переходим в яндекс.");
            Project.SendInfoToLog("Переходим в яндекс", true);
            instance.ActiveTab.Navigate(new YandexWalkSettings(instance, Project).GetRandomYandexHost());
            new AdditionalMethods(instance, Project).WaitDownloading();
            CloseYandexTrash();
            Program.logger.Debug("Успешно перешли в яндекс.");
        }//Переход в яндекс
        public void GoToSearchQuery()
        {
            Program.logger.Debug("Начинаем процесс ввода поискового запроса в строку.");
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);
            string RandomSearchQueries = new YandexWalkSettings(instance, Project).GetRandomSearchQueries();
            GoToYandex();           
            Project.SendInfoToLog("Вводим поисковый запрос.",true);
            Program.logger.Info("Вводим поисковый запрос в строку: " + RandomSearchQueries);
            swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementInputSearchIn, 0), RandomSearchQueries,false);

            if (String.IsNullOrEmpty(instance.ActiveTab.FindElementByXPath(HtmlElementInputSearchIn, 0).GetAttribute("value")))
            {
                Program.logger.Warn("Поисковый запрос не ввелся в строку. Пробуем еще раз.");
                Project.SendWarningToLog("Поисковый запрос не ввелся в строку. Пробуем еще раз.");
                instance.CloseAllTabs();
                GoToSearchQuery();
            }

            Program.logger.Debug("Кликаем по кнопке найти.");
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementSearchButtonIn, 0));
            new AdditionalMethods(instance,Project).WaitDownloading();

            if (instance.ActiveTab.FindElementByXPath(HtmlElementNextPageButton, 0).IsVoid)
            {
                Project.SendWarningToLog("Кнопка \"Найти\" не нажалась. Пробуем еще раз.");
                Program.logger.Warn("Кнопка \"Найти\" не нажалась. Пробуем еще раз.");
                instance.CloseAllTabs();
                GoToSearchQuery();
            }

            Program.logger.Debug("Успешно ввели поисковый запрос и перешли по нему.");
            CloseYandexTrash();          
        }//Ввод поискового запроса и переход по нему
        public bool GoSearchCard(HtmlElement LearnElement)
        {
            Program.logger.Debug("Начинаем работу с карточкой для перехода.");
            SiteWalk siteWalk = new SiteWalk(instance, Project);
            YandexWalkSettings yandexWalkSettings = new YandexWalkSettings(instance, Project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);
            Random random = new Random();

            string CurenSite = LearnElement.FindChildByXPath(HtmlElementUrlSearchCard, 0).InnerHtml;
            string ClearCurenSite = CurenSite.GetUrlToDomain();
            Program.logger.Debug("CurenSite = {0}, ClearCurenSite = {1}", CurenSite, ClearCurenSite);

            if (String.IsNullOrEmpty(ClearCurenSite) || String.IsNullOrWhiteSpace(ClearCurenSite))
            {
                Program.logger.Warn("ClearCurenSite пустой, возвращаем true");
                return true;
            }

            else
            {
                Project.SendInfoToLog("Делаем проверку на запрещенные для перехода сайты.", true);
                Program.logger.Info("Делаем проверку на запрещенные для перехода сайты.");

                if (yandexWalkSettings.CheckMyUrl(CurenSite))
                {
                    Program.logger.Debug("Сайт запрещен для перехода, возвращаем true.");
                    return true;
                }
            }

            Project.SendInfoToLog("Переходим в карточку " + ClearCurenSite, true);
            Program.logger.Info("Переходим в карточку " + ClearCurenSite);
            swipeAndClick.SwipeAndClickToElement(LearnElement);
            new AdditionalMethods(instance, Project).WaitDownloading();
            CloseYandexTrash();
            Program.logger.Debug("Url текущей вкладки: " + instance.ActiveTab.URL);
            Project.SendInfoToLog("Изучаем сайт: " + ClearCurenSite, true);
            Program.logger.Info("Изучаем сайт: " + ClearCurenSite);
            siteWalk.SiteRandomWalk();
            Thread.Sleep(random.Next(4000, 8000));
            Program.logger.Info("Закончили работу с карточкой поисковой выдачи.");
            return false;
        }//Переходим в карточку
        public void CloseUnnecessaryWindows()
        {
            Program.logger.Debug("Начинаем закрытие лишней вкладки : " + instance.ActiveTab.Name +" | " + instance.ActiveTab.URL);

            if (instance.AllTabs.Length > 1)
            {
                instance.ActiveTab.Close();
                Program.logger.Info("Закрыли лишнюю вкладку. Количество открытых вкладок: " + instance.AllTabs.Length + "Url текущей вкладки: " + instance.ActiveTab.URL);
                if (instance.AllTabs.Length > 1)
                {
                    Program.logger.Warn("Вкладка не закрылась, пробуем еще раз.");
                    CloseUnnecessaryWindows();
                }
            }

            else
            {
                Program.logger.Debug("Количество активных вкладок: {0} | Url текущей вкладки: {1}.", instance.AllTabs.Length, instance.ActiveTab.URL);
                if (!instance.ActiveTab.URL.Contains("/search/") || instance.ActiveTab.FindElementByXPath(HtmlElementSearchResultsCard, 0).IsVoid)
                {
                    Program.logger.Debug("Делаем возврат на прошлую страницу.");
                    instance.ActiveTab.MainDocument.EvaluateScript("javascript:history.back()");
                    Program.logger.Debug("Url текущей вкладки: " + instance.ActiveTab.URL);
                }
            }
        }//Закрываем лишнюю вкладку        
        public void CloseYandexTrash()
        {
            Program.logger.Debug("Начинаем процесс закрытия яндексовского мусора.");
            Random random = new Random();
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance,Project);
            instance.ActiveTab.WaitDownloading();
            Thread.Sleep(random.Next(2000, 5000));
            instance.ActiveTab.WaitDownloading();
            new AdditionalMethods(instance,Project).FuckCapcha();
            HtmlElement alisa = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Закрыть')]", 0); //Алиса
            HtmlElement kinopoisk = instance.ActiveTab.FindElementByXPath("//a[starts-with(text(),'Остаться')]", 0); //Кинопоиск
            HtmlElement dzen = instance.ActiveTab.FindElementByXPath("//div[contains(text(),'Дзен')] | //div[starts-with(text(),'Пора переходить')]", 0); //Дзен
            HtmlElement Yamerket = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Продолжить на сайте')]", 0); //Яндекс маркет
            HtmlElement YandexBrowser = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Позже')] | //span[starts-with(text(),'Не сейчас')]", 0); //Яндекс браузер
            HtmlElement Yamerket2 = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Скрыть')]", 0); //Яндекс маркет2

            HtmlElement[] YandexTrash = new HtmlElement[] {alisa ,kinopoisk,Yamerket,YandexBrowser,Yamerket2 };

            foreach (var item in YandexTrash)
            {
                if (!item.IsVoid)
                {
                    Program.logger.Debug("Закрываем мусор яндекса.");
                    swipeAndClick.SwipeAndClickToElement(item);
                }
            }

            if (!dzen.IsVoid)
            {
                Program.logger.Debug("Закрываем баннер дзена.");
                var top = Convert.ToInt32(instance.ActiveTab.MainDocument.EvaluateScript("return window.innerHeight"));
                var left = Convert.ToInt32(instance.ActiveTab.MainDocument.EvaluateScript("return window.innerWidth"));
                instance.ActiveTab.Touch.Touch(left - random.Next(10, 20), (top - top) + random.Next(5, 20));
            }

            Program.logger.Info("Закончили процесс закрытия яндексовского мусора.");
        }//Закрываем яндексовский мусор
    }
}
