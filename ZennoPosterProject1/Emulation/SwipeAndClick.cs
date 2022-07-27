using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Threading;
using ZennoPosterProject1;



namespace ZennoPosterEmulation
{
    class SwipeAndClick 
    {
        private  int ElementPosition { get; set; }
        private  int InstanceHeight { get; set; }
        public int LatencyKeySetText { get { return EmulationValue.LatencyKey.ParseRangeValueInt().ValueRandom; } }
        readonly Instance instance;
        readonly IZennoPosterProjectModel project;
        CreateTouchAndSwipeParametr CreatTuchParametrs;

        public SwipeAndClick(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.project = project;
            this.CreatTuchParametrs = new CreateTouchAndSwipeParametr(project);
        }
        
        public void SetText(HtmlElement HtmlElem, string text, bool True_SwipeAndClick_False_Click)
        {
            project.SendInfoToLog("Вводим текст: " + text);
            Program.logger.Debug("Получили HtmlElement для ввода текста: {0}",text);

            try
            {
                if (True_SwipeAndClick_False_Click)
                {                   
                    SwipeAndClickToElement(HtmlElem);
                }
                else
                {
                    ClickToElement(HtmlElem);
                }
                
                char[] InputText = text.ToCharArray();

                foreach (char InputChar in InputText)
                {
                    instance.SendText(Convert.ToString(InputChar), 0);
                    Thread.Sleep(LatencyKeySetText);
                }

                Program.logger.Debug("Ввод текста завершен.");

            }
            catch (Exception ex)
            {
                project.SendWarningToLog("Ввод текста не был совершен так как не был сделан свайп или клик. " + ex.Message);
                Program.logger.Warn("Ввод текста не был совершен так как не был сделан свайп или клик. " + ex.Message);
            }
        }//Ввод текста
        public void SwipeAndClickToElement(HtmlElement HtmlElem)
        {
            Program.logger.Debug("Получили HtmlElement для спайпа и клика. ");

            try
            {
                SwipeToElement(HtmlElem);
                ClickToElement(HtmlElem);
            }

            catch (Exception ex)
            {
                Program.logger.Warn("Не удалось сделать свайп или клик: " + ex.Message);
                project.SendWarningToLog("Не удалось сделать свайп или клик: " + ex.Message,true);
            }                     
        }//Свайп до элемента и клик по нему       
        public void SwipeToElement(HtmlElement HtmlElem)
        {
            Program.logger.Debug("Получили HtmlElement для свайпа.");
            instance.ActiveTab.Touch.SetTouchEmulationParameters(CreatTuchParametrs.CreateTouchParametrs());
            int CounterAttemptSwipe = 0;

            do
            {
                SwipeMoreTime:
                Thread.Sleep(3000);

                if(CounterAttemptSwipe == 10)
                {
                    project.SendWarningToLog("Сделали 10 попыток найти HtmlElement для свайпа, пропускаем его",true);
                    break;                    
                }

                instance.ActiveTab.Touch.SwipeIntoView(HtmlElem);

                if(String.IsNullOrEmpty(HtmlElem.GetAttribute("topInTab")))
                {
                    CounterAttemptSwipe++;
                    goto SwipeMoreTime;
                }

                ElementPosition = Convert.ToInt32(HtmlElem.GetAttribute("topInTab"));
                InstanceHeight = Convert.ToInt32(instance.ActiveTab.MainDocument.EvaluateScript("return window.innerHeight"));
                CounterAttemptSwipe++;               
                Program.logger.Debug("Свайп выполнен.");
            }
            while (ElementPosition > InstanceHeight || ElementPosition < 0);

        }//Свайп до элемента
        public void ClickToElement(HtmlElement HtmlElem)
        {
            Program.logger.Debug("Получили HtmlElement для клика.");

            if (HtmlElem.IsVoid)
            {
                project.SendErrorToLog("HtmlElement элемент для клика не найден.", true);
                throw new Exception("HtmlElement элемент для клика не найден");
            }

            instance.ActiveTab.Touch.SetTouchEmulationParameters(CreatTuchParametrs.CreateTouchParametrs());
            instance.ActiveTab.Touch.Touch(HtmlElem);
            Program.logger.Debug("Клик выполнен.");
        }//Клик по элементу
    }
}