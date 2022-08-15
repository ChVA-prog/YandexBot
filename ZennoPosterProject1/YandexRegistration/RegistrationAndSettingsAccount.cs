using System;
using System.Linq;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterProject1;
using ZennoPosterYandexWalk;
using ZennoPosterYandexRegistrationSmsServiceSmsHubOrg;
using System.IO;
using System.Collections.Generic;

namespace ZennoPosterYandexRegistration
{
    class RegistrationAndSettingsAccount : YandexRegistrationValue
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;
        GetNumber getNumber;

        public string YandexLogin { get; set; }
        public string YandexPassword { get; set; }


        public RegistrationAndSettingsAccount(Instance instance, IZennoPosterProjectModel project) : base(project)
        {
            this.instance = instance;
            this.project = project;
            getNumber = new GetNumber(project);
        }

        public void RegisterAccountAndSetPassword()
        {

            YandexNavigate yandexNavigate = new YandexNavigate(instance, project);

            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);
            Random random = new Random();
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);

            yandexNavigate.GoToYandex();
            additionalMethods.FuckCapcha();
            try
            {
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementEnterId, 0));
                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementCreatId, 0));
                
            escho:
                getNumber.GetNumberAndId();
                swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementSetPhoneNumber, 0), getNumber.PhoneNumber.Substring(1), true);
                swipeAndClick.ClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementConfirmPhoneNumber, 0));
                
                try
                {
                    getNumber.GetSmsCode(true);
                }
                catch (Exception)
                {
                    swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath("//a[contains(@class, 'PreviousStepButton')]", 0));
                    Thread.Sleep(random.Next(2000, 4000));
                    swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath("//a[contains(@class, 'PreviousStepButton')]", 0));
                    Thread.Sleep(random.Next(2000, 4000));
                    swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementCreatId, 0));
                    goto escho;
                }



                swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputCodeActivation, 0), getNumber.CodeActivation, true);

                Thread.Sleep(random.Next(3000, 5000));
                HtmlElement formal2 = instance.ActiveTab.FindElementByXPath("//h1[contains(text(),'Немного формальностей')]", 0);
                if (formal2.IsVoid)
                {
                    swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputFirstName, 0), project.Profile.Name, true);
                    swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputLastName, 0), project.Profile.Surname, true);
                    swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementConfirmFirstNameAndLastName, 0));
                }

                Thread.Sleep(random.Next(3000, 5000));

                HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Создать новый аккаунт')]", 0);
                if (!he.IsVoid)
                {
                    swipeAndClick.SwipeAndClickToElement(he);
                }

                ;
                swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement(HtmlElementCheckBox, 0));
                swipeAndClick.ClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementConfirmUserAgreement, 0));
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
                additionalMethods.WaitDownloading();
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementAccountMenu, 0));
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementSettings, 0));
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementAccountSettings, 0));

                if (instance.ActiveTab.URL.Contains("nosync"))
                {

                }
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementCreateLogin, 0));




                ;
                if (String.IsNullOrEmpty(additionalMethods.WaitHtmlElement(HtmlElementSetLogin, 0).GetAttribute("value")))
                {
                    swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementSetLogin, 0), project.Profile.NickName, true);
                    YandexLogin = project.Profile.NickName;
                }
                else
                {
                    YandexLogin = instance.ActiveTab.FindElementByXPath(HtmlElementSetLogin, 0).GetAttribute("value");
                }

                HtmlElement LoginIsBusy = instance.ActiveTab.FindElementByXPath("//div[contains(text(),'логин занят')]", 0);
                if (!LoginIsBusy.IsVoid)
                {
                    swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementSetLogin, 0), new Random().Next(1000, 5000).ToString(), true);
                    YandexLogin = instance.ActiveTab.FindElementByXPath(HtmlElementSetLogin, 0).GetAttribute("value");
                }

                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementApprovedLogin, 0));
                swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementSetPassword, 0), project.Profile.Password, true);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementApprovedPassword, 0));
                YandexPassword = project.Profile.Password;
                ;
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementMailAndPhone, 0));
                ;
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementChangeMailAndPhoneList, 0));
                ;
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementOffInputSms, 0));
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

            try
            {
                ;
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementHumberSettings, 0)); // Настройки номера
                ;
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementWhyDeletePhoneNumber,0));  //как удалить
                ;
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementNextPageDeleteNumber, 0)); // Далее
                additionalMethods.WaitDownloading();
                ;
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementSecurityQuestionMenu, 0)); //Выпадающее меню конктрольный вопрос

                HtmlElement he = instance.ActiveTab.FindElementByXPath(HtmlElementSecurityQuestionMenu, 0);
                var top = Convert.ToInt32(he.GetAttribute("topInTab"));
                var left = Convert.ToInt32(he.GetAttribute("leftInTab"));
                Random random = new Random();
                Thread.Sleep(random.Next(4000, 10000));
                instance.ActiveTab.Touch.Touch(left + random.Next(30, 100), top + random.Next(100, 200));  // Выбор контрольного вопроса

                ;
                swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementResponceSecurityQuestion, 0), project.Profile.Password, true); //Поле ввода ответа на вопрос
                ;
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementSaveSecurityQuestion, 0)); //Сохранить контрльный вопррос
                additionalMethods.WaitDownloading();

                HtmlElement CheckHe = instance.ActiveTab.FindElementByXPath(HtmlElementCheckNeedWritePassword, 0); //Введите парроль еще раз (проверрка)
                if (!CheckHe.IsVoid)
                {
                    ;
                    swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputPasswordSecurityQuestion, 0), project.Profile.Password, true);
                    ;
                    swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementConfirmPasswordSecurityQuestion, 0)); //Подтвердить пароль
                }

                ;
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementHumberSettings, 0)); // Настройки номера
                ;
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementDeletePhoneNumber, 0)); // Удалить номер
                ;
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementSendSmsForDeletePhoneNumber, 0)); // Отправить смс
                getNumber.GetSmsCode(false);
                ;
                swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputSmsCodeDeletePhoneNumber, 0), getNumber.CodeActivation, true); // Ввести смс код
                ;
                swipeAndClick.SetText(additionalMethods.WaitHtmlElement(HtmlElementInputPasswordForDeletePhoneNumber, 0), project.Profile.Password, true); // Ввести парроль аккаунта
                ;
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement(HtmlElementConfirmDeletePhoneNumber, 0)); // Подтвердить
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось удалить номер из аккаунта: " + ex.Message);
            }
        }//Отвязка номера и установка контрольного вопроса
        public void SettingsAccount()
        {
            AdditionalMethods additionalMethods = new AdditionalMethods(instance, project);
            SwipeAndClick swipeAndClick = new SwipeAndClick(instance, project);
            Random random = new Random();

            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//a[contains(@class, 'PageHeaderLogo-id')]", 0));// кнопка id, для входа в настройки акка
            swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement("//span[contains(@class, 'avatar-mask')]", 0));// кнопка добавить фото


            List<string> AccountFolder = (from a in Directory.GetFiles(AccountAvatarFolder) select Path.GetFileName(a)).ToList();                                                                                                                     // 
            instance.SetFilesForUpload(project.Directory + String.Format(@"\AccountPhoto\{0}", AccountFolder[random.Next(0, AccountFolder.Count)]));
            swipeAndClick.ClickToElement(additionalMethods.WaitHtmlElement("//input[contains(@name, 'attachment')]", 0));// Загрузить фото

            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//span[starts-with(text(),'Сохранить')]", 0));// сохранить фото

            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//div[contains(@class, 'AdditionalPersonalInfo-change')]", 0));// Изменить персональную информацию

            swipeAndClick.SetText(additionalMethods.WaitHtmlElement("//input[contains(@id, 'firstname')]", 0), project.Profile.Name, true);// Указать имя

            
            swipeAndClick.SetText(additionalMethods.WaitHtmlElement("//input[contains(@id, 'lastname')]", 0), project.Profile.Surname, true); // Указать фамилию

            if (project.Profile.BornDay > 28)
            {
                project.Profile.BornDay = 28;
            }

            swipeAndClick.SetText(additionalMethods.WaitHtmlElement("//input[contains(@id, 'birthday-day')]", 0), project.Profile.BornDay.ToString(), true);

            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//select[contains(@name, 'month')]", 0));//указать месяц рождения
            var SetBirthdayMonthTop = Convert.ToInt32(additionalMethods.WaitHtmlElement("//select[contains(@name, 'month')]", 0).GetAttribute("topInTab"));
            var SetBirthdayMonthLeft = Convert.ToInt32(additionalMethods.WaitHtmlElement("//select[contains(@name, 'month')]", 0).GetAttribute("leftInTab"));
            Thread.Sleep(random.Next(2000, 4000));
            instance.ActiveTab.Touch.Touch(SetBirthdayMonthLeft + random.Next(30, 100), SetBirthdayMonthTop + random.Next(100, 200));

            swipeAndClick.SetText(additionalMethods.WaitHtmlElement("//input[contains(@id, 'birthday-year')]", 0), project.Profile.BornYear.ToString(), true);

            if (project.Profile.Sex.ToString().Contains("Female"))
            {
                HtmlElement Gender = instance.ActiveTab.FindElementByXPath("//div[starts-with(text(),'Женский')]", 0);//Выбираем женский пол если акк женский
                swipeAndClick.SwipeAndClickToElement(Gender);
            }

            swipeAndClick.SetText(additionalMethods.WaitHtmlElement("//input[contains(@id, 'city')]", 0), project.Profile.CurrentRegion, true);

            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//span[starts-with(text(),'Сохранить')]", 0));//СОхраняем персональные настройки

            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//span[contains(@class, 'AdditionalPersonalInfo-link')]", 0));//указать публичный адрес

            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//span[contains(@class, 'PublicId-suggestValue')]", 0));//выбираем предложенный адрес

            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//span[starts-with(text(),'Сохранить')]", 0));//сохраняем публичный адрес

            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//div[contains(@class, 'Addresses-block')]", 0));//Добавить домашний адрес

            swipeAndClick.SetText(additionalMethods.WaitHtmlElement("//input[contains(@id, 'addressLine')]", 0), AdressList[random.Next(0, AdressList.Count)], true);//Домашний адрес
            var SetHomeAdressTop = Convert.ToInt32(additionalMethods.WaitHtmlElement("//input[contains(@id, 'addressLine')]", 0).GetAttribute("topInTab"));
            var SetHomeAdressLeft = Convert.ToInt32(additionalMethods.WaitHtmlElement("//input[contains(@id, 'addressLine')]", 0).GetAttribute("leftInTab"));
            instance.ActiveTab.Touch.Touch(SetHomeAdressLeft + random.Next(30, 100), SetHomeAdressTop + random.Next(50, 60));

            HtmlElement SetWorkAdress = instance.ActiveTab.FindElementByXPath("//input[contains(@id, 'addressLine')]", 1);//Работа
            swipeAndClick.SetText(SetWorkAdress, AdressList[random.Next(0, AdressList.Count)], true);
            var SetWorkAdressTop = Convert.ToInt32(SetWorkAdress.GetAttribute("topInTab"));
            var SetWorkAdressLeft = Convert.ToInt32(SetWorkAdress.GetAttribute("leftInTab"));
            instance.ActiveTab.Touch.Touch(SetWorkAdressLeft + random.Next(30, 100), SetWorkAdressTop + random.Next(50, 60));


            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//span[starts-with(text(),'Сохранить')]", 0));//Сохранить адреса

            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//div[contains(@class, 'user-pic user-pic_has-plus_ user-account__pic')]", 0)); //Иконка профиля
            swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//span[starts-with(text(),'Почта')]", 0));//Почта

            HtmlElementCollection htmlElements = instance.ActiveTab.FindElementsByXPath("//div[contains(@class, 'messagesMessage-firstline')]");//Письма

            for (int i = 0; i < htmlElements.Count; i++)
            {
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//span[contains(@class, 'messagesMessage-firstline')]", i));//Письмо
                swipeAndClick.SwipeAndClickToElement(additionalMethods.WaitHtmlElement("//div[contains(@class, 'ico_rounded')]", 0));//Назад
                Thread.Sleep(random.Next(1000,2000));
            }

            instance.ActiveTab.Navigate("vk.com", instance.ActiveTab.URL);
            Thread.Sleep(random.Next(1000, 2000));
            

        }//Настрока данных аккаунта
    }
}
