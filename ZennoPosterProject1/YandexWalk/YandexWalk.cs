﻿using System.Collections.Generic;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterProject1;
using System;


namespace ZennoPosterYandexWalk
{
    public class YandexWalk : YandexWalkValue
    {
        private string NextPageHtmlElementSearchResultsCard { get; set; }

        readonly Instance instance;
        readonly IZennoPosterProjectModel Project;
        private int CountLearnCardInPage { get; set; }

        public YandexWalk(Instance _instance, IZennoPosterProjectModel _project) : base(_project)
        {
            this.instance = _instance;
            this.Project = _project;
        }
        
        public void GoYandexWalk()
        {
            Program.logger.Info("Запускаем бродилку по яндексу.");
            Project.SendInfoToLog("Запускаем бродилку по яндексу.", true);
            YandexWalkSettings yandexWalkSettings = new YandexWalkSettings(instance, Project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);
            YandexNavigate yandexNavigate = new YandexNavigate(instance, Project);
            int CounterPage = 1;
            int CountLearnPage = yandexWalkSettings.GetRandomPageCountSearch();

            try
            {
              yandexNavigate.GoToSearchQuery();
            }

            catch (Exception ex)
            {
                Program.logger.Error("Ошибка при переходе в яндекс и вводе запроса: " + ex.Message);
                throw new Exception("Ошибка при переходе в яндекс и вводе запроса: " + ex.Message);              
            }
            
            Project.SendInfoToLog("Будем изучать " + CountLearnPage + " страниц",true);

            do
            {
                Program.logger.Debug("Перешли на страницу номер {0}, работаем с ней.",CounterPage);

                if (CounterPage == 1)
                {
                    Program.logger.Debug("NextPageHtmlElementSearchResultsCard = " + HtmlElementSearchResultsCard);
                    NextPageHtmlElementSearchResultsCard = HtmlElementSearchResultsCard;
                }

                else
                {
                    NextPageHtmlElementSearchResultsCard = HtmlElementPageNumber.Replace("num", CounterPage.ToString());
                    Program.logger.Debug("Меняем путь для изучения карточек.");
                    Program.logger.Debug(NextPageHtmlElementSearchResultsCard + " = " + HtmlElementPageNumber.Replace("num", CounterPage.ToString()));
                }

                Project.SendInfoToLog("Номер страницы " + CounterPage, true);
                int CounterAttemptGetSumCard = 0;

                do
                {
                    if(CounterAttemptGetSumCard == 10)
                    {
                        Program.logger.Error("Сделали 10 попыток получить карточки поисковой выдачи, завершаем работу.");
                        throw new Exception("Не удалось получить карточки поисковой выдачи");
                    }

                    CountLearnCardInPage = instance.ActiveTab.FindElementsByXPath(NextPageHtmlElementSearchResultsCard).Count.CalcPercentLearnCard
                                        (CountLearnCardIn.ParseRangeValueInt().ValueRandom);
                    Thread.Sleep(2000);
                    CounterAttemptGetSumCard++;
                }
                while (CountLearnCardInPage == 0);
                
                Project.SendInfoToLog("Будем изучать " + CountLearnCardInPage + " карточек", true);
                Program.logger.Info("Будем изучать " + CountLearnCardInPage + " карточек на странице номер {0}.", CounterPage);
                List<int> SearchCardList = yandexWalkSettings.CountLearnCard(CountLearnCardInPage);
                yandexWalkSettings.GoOrLearnCard(SearchCardList, NextPageHtmlElementSearchResultsCard);
                Project.SendInfoToLog("Переходим на следующую страницу", true);
                Program.logger.Info("Переходим на следующую страницу");
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementNextPageButton, 0));
                new AdditionalMethods(instance, Project).WaitDownloading();
                yandexNavigate.CloseYandexTrash();
                CounterPage++;
            }

            while (CountLearnPage > CounterPage);

            Program.logger.Debug("CountLearnPage = {0} > CounterPage = {1}", CountLearnPage, CounterPage);
            Program.logger.Info("Закончили бродить по яндексу.");
        } //Запуск бродилки по яндексу
    }
}
