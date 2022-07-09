using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Threading;



namespace ZennoPosterEmulation
{
    class SwipeAndClick 
    {
        private  int ElementPosition { get; set; }
        private  int InstanceHeight { get; set; }
        int LatencyKeySetText { get { return EmulationValue.LatencyKey.ParseRangeValueInt().ValueRandom; } }

        readonly Instance instance;
        readonly IZennoPosterProjectModel project;

        public SwipeAndClick(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.project = project;
        }
        public void SetText(HtmlElement HtmlElem, string text)
        {
            SwipeAndClickToElement(HtmlElem);

            char[] InputText = text.ToCharArray();

            foreach (char InputChar in InputText)
            {
                instance.SendText(Convert.ToString(InputChar), 0);
                Thread.Sleep(LatencyKeySetText);
            }
        }//Ввод текста
        public void SwipeAndClickToElement(HtmlElement HtmlElem)
        {
            SwipeToElement(HtmlElem);
            ClickToElement(HtmlElem);
        }//Свайп до элемента и клик по нему       
        public void SwipeToElement(HtmlElement HtmlElem)
        {
            instance.ActiveTab.Touch.SetTouchEmulationParameters(new CreateTouchAndSwipeParametr(project).CreateTouchParametrs());

            int CounterAttemptSwipe = 0;
            do
            {
                Thread.Sleep(3000);
                if(CounterAttemptSwipe == 10)
                {
                    project.SendWarningToLog("Сделали 10 попыток найти HtmlElement для свайпа, пропускаем его",true);
                    break;
                }

                instance.ActiveTab.Touch.SwipeIntoView(HtmlElem);

                if(String.IsNullOrEmpty(HtmlElem.GetAttribute("topInTab")))
                {
                    project.SendWarningToLog("Не получили атрибут topInTab элемента для свайпа, пробуем еще раз.", true);
                    CounterAttemptSwipe++;
                    continue;
                }

                ElementPosition = Convert.ToInt32(HtmlElem.GetAttribute("topInTab"));
                InstanceHeight = Convert.ToInt32(instance.ActiveTab.MainDocument.EvaluateScript("return window.innerHeight"));
                CounterAttemptSwipe++;
            }
            while (ElementPosition > InstanceHeight || ElementPosition < 0);
            
        }//Свайп до элемента
        private void ClickToElement(HtmlElement HtmlElem)
        {
            if (HtmlElem.IsVoid)
            {
                project.SendErrorToLog("HTML элемент для клика не найден",true);
                return;
            }
            instance.ActiveTab.Touch.SetTouchEmulationParameters(new CreateTouchAndSwipeParametr(project).CreateTouchParametrs());
            instance.ActiveTab.Touch.Touch(HtmlElem);
        }//Клик по элементу
    }
}