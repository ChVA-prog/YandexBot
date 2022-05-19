using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoLab.InterfacesLibrary.ProjectModel.Collections;
using ZennoLab.InterfacesLibrary.ProjectModel.Enums;
using ZennoLab.Macros;
using Global.ZennoExtensions;
using ZennoLab.Emulation;
using ZennoLab.CommandCenter.TouchEvents;
using ZennoLab.CommandCenter.FullEmulation;
using ZennoLab.InterfacesLibrary.Enums;


namespace ZennoPosterProject1
{
    class SwipeAndClick
    {
        readonly Instance instance;
        readonly IZennoPosterProjectModel zennoPosterProjectModel;
        public SwipeAndClick(Instance _instance, IZennoPosterProjectModel _zennoPosterProjectModel)
        {
            this.instance = _instance;
            this.zennoPosterProjectModel = _zennoPosterProjectModel;

        }

        public void SwipeToElement(HtmlElement HtmlElem)
        {
            if (instance.ActiveTab.IsBusy)
            {
                zennoPosterProjectModel.SendInfoToLog("Ждем загрузки страницы для свайпа до элемента");
                instance.ActiveTab.WaitDownloading();
            }
            if (HtmlElem.IsVoid)
            {
                zennoPosterProjectModel.SendErrorToLog("HTML элемент не найден");
                return;
            }
            instance.ActiveTab.Touch.SetTouchEmulationParameters(new Emulation(instance, zennoPosterProjectModel).CreateTouchParametrs());
            instance.ActiveTab.Touch.SwipeIntoView(HtmlElem);
        }

        public void ClickToElement(HtmlElement HtmlElem)
        {
            if (instance.ActiveTab.IsBusy)
            {
                zennoPosterProjectModel.SendInfoToLog("Ждем загрузки страницы для клика по элементу");
                instance.ActiveTab.WaitDownloading();
            }
            if (HtmlElem.IsVoid)
            {
                zennoPosterProjectModel.SendErrorToLog("HTML элемент не найден");
                return;
            }
            instance.ActiveTab.Touch.SetTouchEmulationParameters(new Emulation(instance, zennoPosterProjectModel).CreateTouchParametrs());

            instance.ActiveTab.Touch.Touch(HtmlElem);
        }

        public void SwipeAndClickToElement(HtmlElement HtmlElem)
        {
            if (instance.ActiveTab.IsBusy)
            {
                zennoPosterProjectModel.SendInfoToLog("Ждем загрузки страницы для свайпа и клика по элементу");
                instance.ActiveTab.WaitDownloading();
            }
            if (HtmlElem.IsVoid)
            {
                zennoPosterProjectModel.SendErrorToLog("HTML элемент не найден");
                return;
            }
            int ElementPosition = Convert.ToInt32(HtmlElem.GetAttribute("topInTab"));
            int InstanceHeight = Convert.ToInt32(instance.ActiveTab.MainDocument.EvaluateScript("return window.innerHeight"));
            if(ElementPosition > InstanceHeight && ElementPosition > 0)
            {
                SwipeToElement(HtmlElem);
            }
                
            ClickToElement(HtmlElem);
            instance.ActiveTab.WaitDownloading();

        }

    }
}
