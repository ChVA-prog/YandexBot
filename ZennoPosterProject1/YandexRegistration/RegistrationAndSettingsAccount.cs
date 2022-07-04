using System;
using System.Linq;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterProject1;
using ZennoPosterYandexWalk;
using ZennoPosterYandexRegistrationSmsServiceSmsHubOrg;

namespace ZennoPosterYandexRegistration
{
    class RegistrationAndSettingsAccount : YandexRegistrationValue
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;
        public RegistrationAndSettingsAccount(Instance instance, IZennoPosterProjectModel project) : base (project)
        {
            this.instance = instance;
            this.project = project;
        }
        public void RegisterAccountAndSetPassword()
        {
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance,project);
            YandexNavigate yandexNavigate = new YandexNavigate(instance, project);
            GetNumber getNumber = new GetNumber(project);
            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);

            yandexNavigate.GoToYandex();
            additionalMethods.CheckIamNotRobot();
            try
            {
                additionalMethods.WaitHtmlElement(HtmlElementEnterId);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementEnterId, 0));
                additionalMethods.WaitHtmlElement(HtmlElementCreatId);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementCreatId, 0));
                additionalMethods.WaitHtmlElement(HtmlElementSetPhoneNumber);
                getNumber.GetNumberAndId();
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementSetPhoneNumber, 0), SmshubValue.PhoneNumber.Substring(1));
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementConfirmPhoneNumber, 0));
                additionalMethods.WaitHtmlElement(HtmlElementInputCodeActivation);
                getNumber.GetSmsCode(true);
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementInputCodeActivation, 0), SmshubValue.CodeActivation);
                additionalMethods.WaitHtmlElement(HtmlElementInputFirstName);
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementInputFirstName, 0), project.Profile.Name);
                additionalMethods.WaitHtmlElement(HtmlElementInputLastName);
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementInputLastName, 0), project.Profile.Surname);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementConfirmFirstNameAndLastName, 0));
                additionalMethods.WaitHtmlElement(HtmlElementCheckBox);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementCheckBox, 0));
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementConfirmUserAgreement, 0));
                additionalMethods.WaitDownloading();
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось зарегестрировать аккаунт: " + ex.Message);               
            }
        }//Регистрация аккаунта
        public void SetLoginAndPasswordAndRemovePhoneNumber()
        {
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);

            try
            {
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementAccountMenu, 0));
                additionalMethods.WaitHtmlElement(HtmlElementSettings);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementSettings, 0));
                additionalMethods.WaitHtmlElement(HtmlElementAccountSettings);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementAccountSettings, 0));
                additionalMethods.WaitHtmlElement(HtmlElementCreateLogin);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementCreateLogin, 0));
                additionalMethods.WaitHtmlElement(HtmlElementSetLogin);
                if (String.IsNullOrEmpty(instance.ActiveTab.FindElementByXPath(HtmlElementSetLogin, 0).GetAttribute("value")))
                {
                    swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementSetLogin, 0), project.Profile.NickName);
                    YandexLogin = project.Profile.NickName;
                }
                else
                {
                    YandexLogin = instance.ActiveTab.FindElementByXPath(HtmlElementSetLogin, 0).GetAttribute("value");
                }
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementApprovedLogin, 0));
                additionalMethods.WaitHtmlElement(HtmlElementSetPassword);
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementSetPassword, 0), project.Profile.Password);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementApprovedPassword, 0));
                YandexPassword = project.Profile.Password;
                additionalMethods.WaitHtmlElement(HtmlElementMailAndPhone);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementMailAndPhone, 0));
                additionalMethods.WaitHtmlElement(HtmlElementChangeMailAndPhoneList);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementChangeMailAndPhoneList, 0));
                additionalMethods.WaitHtmlElement(HtmlElementOffInputSms);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementOffInputSms, 0));
                additionalMethods.WaitDownloading();
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось установить логин или отключить вход по смс: " + ex.Message);
            }
            
        }//Указание логина и пароля для аккаунта
        public void DeletePhoneNumberFromAccount()
        {
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);
            GetNumber getNumber = new GetNumber(project);

            try
            {
                additionalMethods.WaitHtmlElement(HtmlElementHumberSettings);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementHumberSettings, 0)); // Настройки номера
                additionalMethods.WaitHtmlElement(HtmlElementWhyDeletePhoneNumber);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementWhyDeletePhoneNumber, 0));  //как удалить
                additionalMethods.WaitHtmlElement(HtmlElementNextPageDeleteNumber);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementNextPageDeleteNumber, 0)); // Далее
                additionalMethods.WaitDownloading();
                additionalMethods.WaitHtmlElement(HtmlElementSecurityQuestionMenu);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementSecurityQuestionMenu, 0)); //Выпадающее меню конктрольный вопрос

                HtmlElement he = instance.ActiveTab.FindElementByXPath(HtmlElementSecurityQuestionMenu, 0);
                var top = Convert.ToInt32(he.GetAttribute("topInTab"));
                var left = Convert.ToInt32(he.GetAttribute("leftInTab"));
                Random random = new Random();
                Thread.Sleep(random.Next(4000, 10000));
                instance.ActiveTab.Touch.Touch(left + random.Next(30, 100), top + random.Next(100, 200));  // Выбор контрольного вопроса

                additionalMethods.WaitHtmlElement(HtmlElementResponceSecurityQuestion);
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementResponceSecurityQuestion, 0), project.Profile.Password); //Поле ввода ответа на вопрос
                additionalMethods.WaitHtmlElement(HtmlElementSaveSecurityQuestion);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementSaveSecurityQuestion, 0)); //Сохранить контрльный вопррос
                additionalMethods.WaitDownloading();

                HtmlElement CheckHe = instance.ActiveTab.FindElementByXPath(HtmlElementCheckNeedWritePassword, 0); //Введите парроль еще раз (проверрка)
                if (!CheckHe.IsVoid)
                {
                    additionalMethods.WaitHtmlElement(HtmlElementInputPasswordSecurityQuestion);
                    swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementInputPasswordSecurityQuestion, 0), project.Profile.Password);
                    additionalMethods.WaitHtmlElement(HtmlElementConfirmPasswordSecurityQuestion);
                    swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementConfirmPasswordSecurityQuestion, 0)); //Подтвердить пароль
                }

                additionalMethods.WaitHtmlElement(HtmlElementHumberSettings);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementHumberSettings, 0)); // Настройки номера
                additionalMethods.WaitHtmlElement(HtmlElementDeletePhoneNumber);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementDeletePhoneNumber, 0)); // Удалить номер
                additionalMethods.WaitHtmlElement(HtmlElementSendSmsForDeletePhoneNumber);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementSendSmsForDeletePhoneNumber, 0)); // Отправить смс
                getNumber.GetSmsCode(false);
                additionalMethods.WaitHtmlElement(HtmlElementInputSmsCodeDeletePhoneNumber);
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementInputSmsCodeDeletePhoneNumber, 0), SmshubValue.CodeActivation); // Ввести смс код
                additionalMethods.WaitHtmlElement(HtmlElementInputPasswordForDeletePhoneNumber);
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementInputPasswordForDeletePhoneNumber, 0), project.Profile.Password); // Ввести парроль аккаунта
                additionalMethods.WaitHtmlElement(HtmlElementConfirmDeletePhoneNumber);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementConfirmDeletePhoneNumber, 0)); // Подтвердить
                additionalMethods.WaitHtmlElement(HtmlElementGoYandexFromAccountSettings);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementGoYandexFromAccountSettings, 0));
                additionalMethods.WaitHtmlElement(YandexWalkValue.HtmlElementInputSearchIn);
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementInputSearchIn, 0), "Вк");
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(YandexWalkValue.HtmlElementSearchButtonIn, 0));
                additionalMethods.WaitHtmlElement(YandexWalkValue.HtmlElementSearchResultsCard);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath("//span[contains(@class, 'OrganicTitleContentSpan organic__title')]", 0));
                additionalMethods.WaitDownloading();
                instance.AllTabs.First().Close();
                additionalMethods.WaitDownloading();
                instance.CloseAllTabs();
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось удалить номер из аккаунта: " + ex.Message);
            }
        }//Отвязка номера и установка контрольного вопроса
    }
}
