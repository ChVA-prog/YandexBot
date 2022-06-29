using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Threading;



namespace ZennoPosterEmulation
{
    class SwipeAndClick : EmulationValue
    {
        readonly Instance instance;
        readonly IZennoPosterProjectModel project;
        public SwipeAndClick(Instance instance, IZennoPosterProjectModel project) : base(project)
        {
            this.instance = instance;
            this.project = project;
        }

        public void SwipeToElement(HtmlElement HtmlElem)
        {
            CreateTouchAndSwipeParametr emulation = new CreateTouchAndSwipeParametr(project);       

            instance.ActiveTab.Touch.SetTouchEmulationParameters(emulation.CreateTouchParametrs());

            int CounterAttemptSwipe = 0;
            do
            {
                Thread.Sleep(3000);
                if(CounterAttemptSwipe == 10)
                {
                    project.SendWarningToLog("Сделали 10 попыток найти HtmlElement, пропускаем его",true);
                    break;
                }

                instance.ActiveTab.Touch.SwipeIntoView(HtmlElem);

                if(String.IsNullOrEmpty(HtmlElem.GetAttribute("topInTab")))
                {
                    project.SendWarningToLog("Не получили атрибут topInTab элемента, пробуем еще раз.",true);
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
            CreateTouchAndSwipeParametr emulation = new CreateTouchAndSwipeParametr(project);

            if (HtmlElem.IsVoid)
            {
                project.SendErrorToLog("HTML элемент для клика не найден",true);
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
            SwipeAndClickToElement(HtmlElem);

            char[] InputText = text.ToCharArray();

            foreach (char InputChar in InputText)
            {
                instance.SendText(Convert.ToString(InputChar), 0);
                Thread.Sleep(LatencyKeySetText);
            }
        }//Ввод текста
    }
}