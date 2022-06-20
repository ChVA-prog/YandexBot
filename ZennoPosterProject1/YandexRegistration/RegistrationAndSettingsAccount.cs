using System;
using System.Collections.Generic;
using System.Linq;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterSiteWalk;
using ZennoPosterProject1;
using ZennoPosterYandexWalk;
using ZennoPosterYandexRegistrationSmsServiceSmsHubOrg;

namespace ZennoPosterYandexRegistration
{
    class RegistrationAndSettingsAccount
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;
        public RegistrationAndSettingsAccount(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.project = project;
        }
        public void RegisterAccountAndSetPassword()
        {
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance,project);
            YandexNavigate yandexNavigate = new YandexNavigate(instance, project);
            GetNumber getNumber = new GetNumber(instance, project);
            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);

            yandexNavigate.GoToYandex();
            additionalMethods.WaitDownloading();
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementEnterId, 0));
            additionalMethods.WaitDownloading();
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementCreatId, 0));
            additionalMethods.WaitDownloading();
            getNumber.GetNumberAndId();
            swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementSetPhoneNumber, 0), SmshubValue.PhoneNumber);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementConfirmPhoneNumber, 0));
            additionalMethods.WaitDownloading();
            getNumber.GetSmsCode();
            swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementInputCodeActivation, 0), SmshubValue.CodeActivation);
            additionalMethods.WaitDownloading();
            swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementInputFirstName, 0), project.Profile.Name);
            swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementInputLastName, 0), project.Profile.Surname);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementConfirmFirstNameAndLastName, 0));
            additionalMethods.WaitDownloading();
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementCheckBox, 0));
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementConfirmUserAgreement, 0));
            additionalMethods.WaitDownloading();
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementAccountMenu, 0));
            additionalMethods.WaitDownloading();
            additionalMethods.WaitDownloading();
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementSettings, 0));
            additionalMethods.WaitDownloading();
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementAccountSettings, 0));
            additionalMethods.WaitDownloading();
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementCreateLogin, 0));
            additionalMethods.WaitDownloading();
            if (String.IsNullOrEmpty(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementSetLogin, 0).GetAttribute("value")))
            {
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementSetLogin, 0), project.Profile.NickName);
                YandexRegistrationValue.YandexLogin = project.Profile.NickName;
            }
            else
            {                
                YandexRegistrationValue.YandexLogin = instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementSetLogin, 0).GetAttribute("value");
            }
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementApprovedLogin, 0));
            additionalMethods.WaitDownloading();
            swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementSetPassword, 0), project.Profile.Password);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementApprovedPassword, 0));
            YandexRegistrationValue.YandexPassword = project.Profile.Password;
            additionalMethods.WaitDownloading();
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementMailAndPhone, 0));
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementChangeMailAndPhoneList, 0));
            additionalMethods.WaitDownloading();
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementOffInputSms, 0));
            additionalMethods.WaitDownloading();
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementGoYandexFromAccountSettings, 0));
            additionalMethods.WaitDownloading();
            swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementInputSearch, 0), "Вк");
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementSearchButton, 0));
            additionalMethods.WaitDownloading();
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementSearchResultsCard, 0));
            additionalMethods.WaitDownloading();
            instance.AllTabs.First().Close();
            additionalMethods.WaitDownloading();
            instance.CloseAllTabs();
        }//Регистрация аккаунта и установка логина с паролем
    }
}
