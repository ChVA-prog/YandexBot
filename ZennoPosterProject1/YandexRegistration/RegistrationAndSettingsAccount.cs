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
            additionalMethods.CheckIamNotRobot();
            
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementEnterId);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementEnterId, 0));
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementCreatId);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementCreatId, 0));
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementSetPhoneNumber);
            getNumber.GetNumberAndId();
            swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementSetPhoneNumber, 0), SmshubValue.PhoneNumber.Substring(1));
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementConfirmPhoneNumber, 0));
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementInputCodeActivation);
            getNumber.GetSmsCode();
            swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementInputCodeActivation, 0), SmshubValue.CodeActivation);
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementInputFirstName);
            swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementInputFirstName, 0), project.Profile.Name);
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementInputLastName);
            swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementInputLastName, 0), project.Profile.Surname);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementConfirmFirstNameAndLastName, 0));
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementCheckBox);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementCheckBox, 0));
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementConfirmUserAgreement, 0));
            additionalMethods.WaitDownloading();           
        }//Регистрация аккаунта

        public void SetLoginAndPasswordAndRemovePhoneNumber()
        {
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementAccountMenu, 0));
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementSettings);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementSettings, 0));
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementAccountSettings);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementAccountSettings, 0));
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementCreateLogin);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementCreateLogin, 0));
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementSetLogin);
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
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementSetPassword);
            swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementSetPassword, 0), project.Profile.Password);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementApprovedPassword, 0));
            YandexRegistrationValue.YandexPassword = project.Profile.Password;
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementMailAndPhone);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementMailAndPhone, 0));
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementChangeMailAndPhoneList);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementChangeMailAndPhoneList, 0));
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementOffInputSms);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementOffInputSms, 0));
            additionalMethods.WaitDownloading();
        }//Указание логина и пароля для аккаунта
        public void DeletePhoneNumberFromAccount()
        {
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);
            GetNumber getNumber = new GetNumber(instance, project);

            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementSettings);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementHumberSettings, 0)); // Настройки номера
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementWhyDeletePhoneNumber);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementWhyDeletePhoneNumber, 0));  //как удалить
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementNextPageDeleteNumber);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementNextPageDeleteNumber, 0)); // Далее
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementSecurityQuestionMenu);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementSecurityQuestionMenu, 0)); //Выпадающее меню конктрольный вопрос


            HtmlElement he = instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementSecurityQuestionMenu, 0);
            var top = Convert.ToInt32(he.GetAttribute("topInTab"));
            var left = Convert.ToInt32(he.GetAttribute("leftInTab"));
            Random random = new Random();
            Thread.Sleep(random.Next(4000,10000));
            instance.ActiveTab.Touch.Touch(left + random.Next(30, 100), top + random.Next(100, 200));  // Выбор контрольного вопроса

            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementResponceSecurityQuestion);
            swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementResponceSecurityQuestion, 0), project.Profile.Password); //Поле ввода ответа на вопрос
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementSaveSecurityQuestion);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementSaveSecurityQuestion, 0)); //Сохранить контрльный вопррос
            additionalMethods.WaitDownloading();
            HtmlElement CheckHe = instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementCheckNeedWritePassword, 0); //Введите парроль еще раз (проверрка)

            if (!CheckHe.IsVoid)
            {
                additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementInputPasswordSecurityQuestion);
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementInputPasswordSecurityQuestion, 0), project.Profile.Password);
                additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementConfirmPasswordSecurityQuestion);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementConfirmPasswordSecurityQuestion, 0)); //Подтвердить пароль
            }
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementHumberSettings);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementHumberSettings, 0)); // Настройки номера
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementDeletePhoneNumber);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementDeletePhoneNumber, 0)); // Удалить номер
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementSendSmsForDeletePhoneNumber);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementSendSmsForDeletePhoneNumber, 0)); // Отправить смс
            getNumber.GetSmsCode();
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementInputSmsCodeDeletePhoneNumber);
            swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementInputSmsCodeDeletePhoneNumber, 0), SmshubValue.CodeActivation); // Ввести смс код
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementInputPasswordForDeletePhoneNumber);
            swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementInputPasswordForDeletePhoneNumber, 0), project.Profile.Password); // Ввести парроль аккаунта
            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementConfirmDeletePhoneNumber);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementConfirmDeletePhoneNumber, 0)); // Подтвердить

            additionalMethods.WaitHtmlElement(YandexRegistrationValue.HtmlElementBackAccountSettings);
            swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementBackAccountSettings, 0)); // Возврат на страницу с найтроками аккаунта


        }//Отвязка номера и установка контрольного вопроса




        //swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexRegistrationValue.HtmlElementGoYandexFromAccountSettings, 0));
        //additionalMethods.WaitHtmlElement(YandexWalkValue.HtmlElementInputSearch);
        //swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementInputSearch, 0), "Вк");
        //swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementSearchButton, 0));
        //additionalMethods.WaitHtmlElement(YandexWalkValue.HtmlElementSearchResultsCard);
        //swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementSearchResultsCard, 0));
        //additionalMethods.WaitDownloading();
        //instance.AllTabs.First().Close();
        // additionalMethods.WaitDownloading();
        //instance.CloseAllTabs();
    }
}
