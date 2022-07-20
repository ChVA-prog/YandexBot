using System;
using System.Collections.Generic;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterProject1;

namespace ZennoPosterYandexWalk
{   
    class YandexWalkSettings : YandexWalkValue
    {
        readonly IZennoPosterProjectModel Project;
        readonly Instance instance;

        Random random = new Random();

        public YandexWalkSettings(Instance _instance, IZennoPosterProjectModel _project) : base(_project)
        {
            this.instance = _instance;
            this.Project = _project;
        }
        public void GoOrLearnCard(List<int> SearchCardList, string NextPageHtmlElementSearchResultsCard)
        {            
            YandexNavigate yandexNavigate = new YandexNavigate(instance, Project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, Project);
            SearchCardList.Sort();
            Program.logger.Debug("Начинаем перебор карточек, решаем в какие будем переходить а какие будем изучать.");
            foreach (int i in SearchCardList)
            {
                Program.logger.Debug("Получили карточку под номером: " + SearchCardList[i]);
                HtmlElement LearnElement = instance.ActiveTab.FindElementByXPath(NextPageHtmlElementSearchResultsCard, i);

                if (random.Next(0, 100) < CountGetCard.ParseRangeValueInt().ValueRandom)
                {
                    Program.logger.Debug("Будем переходить в поисковую карточку под номером: " + SearchCardList[i]);
                    Project.SendInfoToLog("Будем переходить в карточку.");
                    if (yandexNavigate.GoSearchCard(LearnElement))
                    {
                        yandexNavigate.CloseUnnecessaryWindows();
                        continue;
                    }
                }
                else
                {
                    Program.logger.Debug("Изучаем карточку под номером: " + SearchCardList[i]);
                    Project.SendInfoToLog("Просто изучаем карточку.", true);

                    swipeAndClick.SwipeToElement(LearnElement);
                    Thread.Sleep(random.Next(2000, 4000));
                }                           
            }
            Program.logger.Debug("Закончили перебор карточек на странице.");
        }//Решаем переходим в карточку или просто изучаем
        public bool CheckMyUrl(string url)
        {          
            
            try
            {
                Program.logger.Debug("Перебираем список запрещенных сайтов.");
                foreach (string site in MyUrlList)
                {
                    if (site.Contains(Convert.ToString(url)))
                    {
                        Program.logger.Debug("{0} содержится в {1} в списке MyUrlList",url,site);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Project.SendWarningToLog("Не удалось сделать проверку URL",true);
                Program.logger.Warn("Не удалось сделать проверку URL");
                return false;
            }
            return false;
        }//Проверяем не содержит ли карточка для перехода мой URL      
        public string GetRandomYandexHost()
        {
            Program.logger.Debug("Получаем рандомный хост яндекса для перехода.");
            string[] YandexHosts = Project.Variables["set_YandexHosts"].Value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string RandomHost = YandexHosts[random.Next(0, YandexHosts.Length)];
            Program.logger.Debug("Получили хост: " + RandomHost);
            return RandomHost;
        }//Получение рандомного хоста яндекса
        public string GetRandomSearchQueries()
        {
            Program.logger.Debug("Получаем рандомный поисковый запрос.");
            string[] YandexSearchQueries = Project.Variables["set_SearchQueries"].Value.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            string RandomYandexSearchQueries = YandexSearchQueries[random.Next(0, YandexSearchQueries.Length)];
            Program.logger.Debug("Получили рандомный поисковый запрос: " + RandomYandexSearchQueries);
            return RandomYandexSearchQueries;
        }//Получение рандомного поискового запроса
        public int GetRandomPageCountSearch()
        {
            Program.logger.Debug("Получаем рандомное количество страниц которые будем изучать.");
            int CountPage = ZennoPosterEmulation.Extension.ParseRangeValueInt(PageCountSearch).ValueRandom;
            Program.logger.Debug("Будем изучать {0} страниц.", CountPage);
            return CountPage;
        }//Получение рандомного количества изучаемых страниц
        public List<int> CountLearnCard(int CountCard)
        {
            Program.logger.Debug("Получаем рандомные карточки с которыми будем работать.");
            List<int> SearchCardList = new List<int>();
            for (int i = 0; i < CountCard; i++)
            {
                int rnd = random.Next(0, CountCard);
                if (SearchCardList.Contains(rnd))
                {
                    i--;
                    continue;
                }
                SearchCardList.Add(rnd);
                Program.logger.Debug("Добавили карточку номер {0} в список для работы.", rnd);
            }
            return SearchCardList;
        } //Получение рандомного количиства карточек с которыми будем работать
    }
}
