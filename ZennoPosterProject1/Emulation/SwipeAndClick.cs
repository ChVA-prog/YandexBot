using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Threading;



namespace ZennoPosterEmulation
{
    class SwipeAndClick
    {

        readonly Instance instance;
        readonly IZennoPosterProjectModel Project;
        public SwipeAndClick(Instance _instance, IZennoPosterProjectModel _project)
        {
            this.instance = _instance;
            this.Project = _project;
        }
        public void SwipeToElement(HtmlElement HtmlElem)
        {
            CreateTouchAndSwipeParametr emulation = new CreateTouchAndSwipeParametr();       

            if (HtmlElem.IsVoid)
            {
                Project.SendErrorToLog("HTML элемент для свайпа не найден", true);
                return;
            }

            instance.ActiveTab.Touch.SetTouchEmulationParameters(emulation.CreateTouchParametrs());

            int CounterAttemptSwipe = 0;
            do
            {
                Thread.Sleep(3000);
                if(CounterAttemptSwipe == 10)
                {
                    Project.SendWarningToLog("Сделали 10 попыток найти элемент, пропускаем его");
                    break;
                }

                instance.ActiveTab.Touch.SwipeIntoView(HtmlElem);

                if(String.IsNullOrEmpty(HtmlElem.GetAttribute("topInTab")))
                {
                    Project.SendWarningToLog("Не получили атрибут topInTab элемента, пробуем еще раз.");
                    CounterAttemptSwipe++;
                    continue;
                }

                EmulationValue.ElementPosition = Convert.ToInt32(HtmlElem.GetAttribute("topInTab"));
                EmulationValue.InstanceHeight = Convert.ToInt32(instance.ActiveTab.MainDocument.EvaluateScript("return window.innerHeight"));
                CounterAttemptSwipe++;
            }
            while (EmulationValue.ElementPosition > EmulationValue.InstanceHeight || EmulationValue.ElementPosition < 0);

            
        }//Свайп до элемента

        public void ClickToElement(HtmlElement HtmlElem)
        {
            CreateTouchAndSwipeParametr emulation = new CreateTouchAndSwipeParametr();

            if (HtmlElem.IsVoid)
            {
                Project.SendErrorToLog("HTML элемент для клика не найден");
                return;
            }

            instance.ActiveTab.Touch.SetTouchEmulationParameters(emulation.CreateTouchParametrs());
            instance.ActiveTab.Touch.Touch(HtmlElem);
        }//Клик по элементу

        public void SwipeAndClickToElement(HtmlElement HtmlElem)
        {                    
            SwipeToElement(HtmlElem);                          
            ClickToElement(HtmlElem);
        }//Свайп до элемента и клик по нему

        int LatencyKeySetText { get { return EmulationValue.LatencyKey.ParseRangeValueInt().ValueRandom; } }
        public void SetText(HtmlElement HtmlElem, string text)
        {
            ClickToElement(HtmlElem);

            char[] InputText = text.ToCharArray();

            foreach (char InputChar in InputText)
            {
                instance.SendText(Convert.ToString(InputChar), 0);
                Thread.Sleep(LatencyKeySetText);
            }
        }//Ввод текста
    }
}