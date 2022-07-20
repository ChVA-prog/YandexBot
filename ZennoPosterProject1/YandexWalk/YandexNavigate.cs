using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterProject1;
using ZennoPosterSiteWalk;

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
            Program.logger.Debug("Переходим в яндекс.");
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
            HtmlElement HtmlElementInputSearch = instance.ActiveTab.FindElementByXPath(HtmlElementInputSearchIn, 0);
            HtmlElement HtmlElementSearchButton = instance.ActiveTab.FindElementByXPath(HtmlElementSearchButtonIn, 0);
            Project.SendInfoToLog("Вводим поисковый запрос.",true);
            Program.logger.Debug("Вводим поисковый запрос в строку: " + RandomSearchQueries);
            swipeAndClick.SetText(HtmlElementInputSearch, RandomSearchQueries);

            if (String.IsNullOrEmpty(HtmlElementInputSearch.GetAttribute("value")))
            {
                Program.logger.Error("Поисковый запрос не ввелся в строку.");
                throw new Exception("Поисковый запрос не ввелся в строку");
            }
            Program.logger.Debug("Кликаем по кнопке найти.");
            //Сделать проверку нажатия кнопки найти 
            swipeAndClick.SwipeAndClickToElement(HtmlElementSearchButton);
            new AdditionalMethods(instance,Project).WaitDownloading();
            CloseYandexTrash();
            Program.logger.Debug("Успешно ввели поисковый запрос и перешли по нему.");
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
                Program.logger.Debug("Делаем проверку на запрещенные для перехода сайты.");
                if (yandexWalkSettings.CheckMyUrl(CurenSite))
                {
                    Program.logger.Debug("Сайт запрещен для перехода, возвращаем true.");
                    return true;
                }
            }

            Project.SendInfoToLog("Переходим в карточку " + ClearCurenSite, true);
            Program.logger.Debug("Переходим в карточку " + ClearCurenSite);
            swipeAndClick.SwipeAndClickToElement(LearnElement);

            new AdditionalMethods(instance, Project).WaitDownloading();
            CloseYandexTrash();
            Project.SendInfoToLog("Изучаем сайт: " + ClearCurenSite, true);
            Program.logger.Debug("Изучаем сайт: " + ClearCurenSite);
            siteWalk.SiteRandomWalk();
            Thread.Sleep(random.Next(4000, 8000));

            Program.logger.Debug("Закончили работу с карточкой поисковой выдачи.");
            return false;
        }//Переходим в карточку
        public void CloseUnnecessaryWindows()
        {
            Program.logger.Debug("Начинаем закрытие лишней вкладки.");
            if (instance.AllTabs.Length > 1)
            {
                instance.GetTabByAddress("popup").Close();
                Program.logger.Debug("Закрыли лишнюю вкладку.");
                Project.SendInfoToLog("Закрыли лишнюю вкладку.", true);
            }
            else
            {
                Program.logger.Debug("Количество активных вкладок ({0}).", instance.AllTabs.Length);
                if (!instance.ActiveTab.URL.Contains("/search/") || instance.ActiveTab.FindElementByXPath(HtmlElementSearchResultsCard, 0).IsVoid)
                {
                    Program.logger.Debug("Делаем возврат на прошлую страницу.");
                    instance.ActiveTab.MainDocument.EvaluateScript("javascript:history.back()");
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

            HtmlElement alisa = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Закрыть')]", 0); //Алиса
            HtmlElement kinopoisk = instance.ActiveTab.FindElementByXPath("//a[starts-with(text(),'Остаться')]", 0); //Кинопоиск
            HtmlElement dzen = instance.ActiveTab.FindElementByXPath("//div[starts-with(text(),'В приложении удобнее')] | //div[starts-with(text(),'Пора переходить')]", 0); //Дзен
            HtmlElement Yamerket = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Продолжить на сайте')]", 0); //Яндекс маркет
            HtmlElement YandexBrowser = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Позже')] | //span[starts-with(text(),'Не сейчас')]", 0); //Яндекс браузер
            HtmlElement Yamerket2 = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Скрыть')]", 0); //Яндекс маркет2
            //КАПЧА
            HtmlElement IAmNotRobot = instance.ActiveTab.FindElementByXPath("//span[contains(text(),'похожи на автоматические')]", 0);          
            HtmlElement InputTextCaptcha = instance.ActiveTab.FindElementByXPath("//span[contains(@class,'Textinput-Box')]", 0);           
            HtmlElement SendCaptcha = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Отправить')]", 0);
            
            if (!IAmNotRobot.IsVoid)
            {
                Program.logger.Debug("Нарвались на проверку робота.");
                Project.SendInfoToLog("Налетели на капчу");
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath("//div[contains(@class,'CheckboxCaptcha')]", 0));
                Program.logger.Debug("Нажали я не робот.");
                Thread.Sleep(random.Next(3000, 6000));
                HtmlElement InpuCaptcha = instance.ActiveTab.FindElementByXPath("//label[contains(text(),'Введите текст с картинки')]", 0);               
                if (!InpuCaptcha.IsVoid)
                {
                    Program.logger.Debug("Просят ввести капчу.");
                    HtmlElement Captcha = instance.ActiveTab.FindElementByXPath("//img[contains(@class,'AdvancedCaptcha')]", 0);
                    Program.logger.Debug("Делаем запрос с капчей к RuCaptcha.");
                    string recognition = ZennoPoster.CaptchaRecognition("RuCaptcha.dll", Captcha.DrawToBitmap(true), "");
                    Program.logger.Debug("Получили ответ: " + recognition);
                    swipeAndClick.SetText(InputTextCaptcha, recognition.Split('-')[0]);
                    Program.logger.Debug("Отправляем введенную капчу");
                    swipeAndClick.SwipeAndClickToElement(SendCaptcha);
                    Thread.Sleep(random.Next(2000, 5000));
                    WaitUser.ShowOnTopUserAction(instance.FormTitle, 500, instance, Project);
                }
            }
            if (!alisa.IsVoid)
            {
                Program.logger.Debug("Закрываем баннер алисы.");
                swipeAndClick.SwipeAndClickToElement(alisa);
            }
            if (!kinopoisk.IsVoid)
            {
                Program.logger.Debug("Закрываем баннер кинопоиска.");
                swipeAndClick.SwipeAndClickToElement(kinopoisk);
            }
            if (!dzen.IsVoid)
            {
                Program.logger.Debug("Закрываем баннер дзена.");
                var top = Convert.ToInt32(instance.ActiveTab.MainDocument.EvaluateScript("return window.innerHeight"));
                var left = Convert.ToInt32(instance.ActiveTab.MainDocument.EvaluateScript("return window.innerWidth"));                
                instance.ActiveTab.Touch.Touch(left - random.Next(10, 20), (top - top) + random.Next(5, 20));  // Выбор контрольного вопроса
            }
            if (!Yamerket.IsVoid)
            {
                Program.logger.Debug("Закрываем баннер яндекс маркета.");
                swipeAndClick.SwipeAndClickToElement(Yamerket);
            }
            if (!YandexBrowser.IsVoid)
            {
                Program.logger.Debug("Закрываем баннер яндекс браузера.");
                swipeAndClick.SwipeAndClickToElement(YandexBrowser);
            }
            if (!Yamerket2.IsVoid)
            {
                Program.logger.Debug("Закрываем баннер яндекс маркета.");
                swipeAndClick.SwipeAndClickToElement(Yamerket2);
            }
            Program.logger.Debug("Закончили процесс закрытия яндексовского мусора.");
        }//Закрываем яндексовский мусор
    }
}
