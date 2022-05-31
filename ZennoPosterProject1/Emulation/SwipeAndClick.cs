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
            EmulationValue emulation = new EmulationValue(instance,Project);

            instance.ActiveTab.WaitDownloading();
            
            if (HtmlElem.IsVoid)
            {
                Project.SendErrorToLog("HTML элемент не найден",true);
                return;
            }

            instance.ActiveTab.Touch.SetTouchEmulationParameters(emulation.CreateTouchParametrs());
            instance.ActiveTab.Touch.SwipeIntoView(HtmlElem);
        }

        public void ClickToElement(HtmlElement HtmlElem)
        {
            EmulationValue emulation = new EmulationValue(instance, Project);
           
            instance.ActiveTab.WaitDownloading();
            
            if (HtmlElem.IsVoid)
            {
                Project.SendErrorToLog("HTML элемент не найден");
                return;
            }

            instance.ActiveTab.Touch.SetTouchEmulationParameters(emulation.CreateTouchParametrs());                       
            instance.ActiveTab.Touch.Touch(HtmlElem);
            instance.ActiveTab.WaitDownloading();
        }

        public void SwipeAndClickToElement(HtmlElement HtmlElem)
        {         
            instance.ActiveTab.WaitDownloading();
            
            if (HtmlElem.IsVoid)
            {
                Project.SendErrorToLog("HTML элемент не найден");
                return;
            }

            int ElementPosition = Convert.ToInt32(HtmlElem.GetAttribute("topInTab"));
            int InstanceHeight = Convert.ToInt32(instance.ActiveTab.MainDocument.EvaluateScript("return window.innerHeight"));

            if (ElementPosition > InstanceHeight && ElementPosition > 0)
            {
                SwipeToElement(HtmlElem);
            }

            ClickToElement(HtmlElem);
            instance.ActiveTab.WaitDownloading();

        }


        int LatencyKeySetText {get{return EmulationValue.LatencyKey.ParseRangeValueInt().ValueRandom;}}

        public void SetText(HtmlElement HtmlElem, string text)
        {          
            instance.ActiveTab.WaitDownloading();
            
            if (HtmlElem.IsVoid)
            {
                Project.SendErrorToLog("HTML элемент не найден");
                return;
            }
                         
            SwipeAndClickToElement(HtmlElem);

            char[] InputText = text.ToCharArray();

            foreach(char InputChar in InputText)
            {
                instance.SendText(Convert.ToString(InputChar), 0);
                Thread.Sleep(LatencyKeySetText);
            }


        }
    }
}
