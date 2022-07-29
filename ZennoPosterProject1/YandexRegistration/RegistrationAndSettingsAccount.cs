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


        public RegistrationAndSettingsAccount(Instance instance, IZennoPosterProjectModel project) : base (project)
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
                additionalMethods.WaitHtmlElement(HtmlElementEnterId);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementEnterId, 0));
                additionalMethods.WaitHtmlElement(HtmlElementCreatId);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementCreatId, 0));
                additionalMethods.WaitHtmlElement(HtmlElementSetPhoneNumber);
                getNumber.GetNumberAndId();
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementSetPhoneNumber, 0), getNumber.PhoneNumber.Substring(1),true);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementConfirmPhoneNumber, 0));
                additionalMethods.WaitHtmlElement(HtmlElementInputCodeActivation);
                getNumber.GetSmsCode(true);
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementInputCodeActivation, 0), getNumber.CodeActivation, true);

                Thread.Sleep(random.Next(3000, 5000));
                HtmlElement formal2 = instance.ActiveTab.FindElementByXPath("//h1[contains(text(),'Немного формальностей')]", 0);
                if (formal2.IsVoid)
                {
                    additionalMethods.WaitHtmlElement(HtmlElementInputFirstName);
                    swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementInputFirstName, 0), project.Profile.Name, true);
                    additionalMethods.WaitHtmlElement(HtmlElementInputLastName);
                    swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementInputLastName, 0), project.Profile.Surname, true);
                    swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementConfirmFirstNameAndLastName, 0));
                }

                Thread.Sleep(random.Next(3000,5000));

                HtmlElement he = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Создать новый аккаунт')]",0);
                if (!he.IsVoid)
                {
                    swipeAndClick.SwipeAndClickToElement(he);
                }

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
                additionalMethods.WaitHtmlElement(HtmlElementSetPassword);
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementSetPassword, 0), project.Profile.Password, true);
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
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementResponceSecurityQuestion, 0), project.Profile.Password, true); //Поле ввода ответа на вопрос
                additionalMethods.WaitHtmlElement(HtmlElementSaveSecurityQuestion);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementSaveSecurityQuestion, 0)); //Сохранить контрльный вопррос
                additionalMethods.WaitDownloading();

                HtmlElement CheckHe = instance.ActiveTab.FindElementByXPath(HtmlElementCheckNeedWritePassword, 0); //Введите парроль еще раз (проверрка)
                if (!CheckHe.IsVoid)
                {
                    additionalMethods.WaitHtmlElement(HtmlElementInputPasswordSecurityQuestion);
                    swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementInputPasswordSecurityQuestion, 0), project.Profile.Password, true);
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
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementInputSmsCodeDeletePhoneNumber, 0), getNumber.CodeActivation, true); // Ввести смс код
                additionalMethods.WaitHtmlElement(HtmlElementInputPasswordForDeletePhoneNumber);
                swipeAndClick.SetText(instance.ActiveTab.FindElementByXPath(HtmlElementInputPasswordForDeletePhoneNumber, 0), project.Profile.Password, true); // Ввести парроль аккаунта
                additionalMethods.WaitHtmlElement(HtmlElementConfirmDeletePhoneNumber);
                swipeAndClick.SwipeAndClickToElement(instance.ActiveTab.FindElementByXPath(HtmlElementConfirmDeletePhoneNumber, 0)); // Подтвердить
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

            HtmlElement IdAccountSettings = instance.ActiveTab.FindElementByXPath("//a[contains(@class, 'PageHeaderLogo-id')]", 0);// кнопка id, для входа в настройки акка
            additionalMethods.WaitHtmlElement("//a[contains(@class, 'PageHeaderLogo-id')]");
            swipeAndClick.SwipeAndClickToElement(IdAccountSettings);

            HtmlElement ButtonAddAccountImg = instance.ActiveTab.FindElementByXPath("//a[starts-with(text(),'Добавить фото')]", 0);// кнопка добавить фото
            additionalMethods.WaitHtmlElement("//a[starts-with(text(),'Добавить фото')]");
            swipeAndClick.SwipeAndClickToElement(ButtonAddAccountImg);

            HtmlElement DownloadAccountImg = instance.ActiveTab.FindElementByXPath("//input[contains(@name, 'attachment')]", 0);// Загрузить фото
            List<string> AccountFolder = (from a in Directory.GetFiles(AccountAvatarFolder) select Path.GetFileName(a)).ToList();                                                                                                                     // 
            instance.SetFilesForUpload(project.Directory + String.Format(@"\AccountPhoto\{0}", AccountFolder[random.Next(0, AccountFolder.Count)]));
            swipeAndClick.SwipeAndClickToElement(DownloadAccountImg);
            HtmlElement SaveAccountImg = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Сохранить')]", 0);// сохранить фото
            additionalMethods.WaitHtmlElement("//span[starts-with(text(),'Сохранить')]");
            swipeAndClick.SwipeAndClickToElement(SaveAccountImg);

            HtmlElement ChangePersonalSettings = instance.ActiveTab.FindElementByXPath("//a[starts-with(text(),'Изменить персональную')]", 0);// Изменить персональную информацию
            additionalMethods.WaitHtmlElement("//a[starts-with(text(),'Изменить персональную')]");
            swipeAndClick.SwipeAndClickToElement(ChangePersonalSettings);


            HtmlElement SetName = instance.ActiveTab.FindElementByXPath("//input[contains(@id, 'firstname')]", 0);// Указать имя
            additionalMethods.WaitHtmlElement("//input[contains(@id, 'firstname')]");
            swipeAndClick.SetText(SetName, project.Profile.Name,true);

            HtmlElement SetSurname = instance.ActiveTab.FindElementByXPath("//input[contains(@id, 'lastname')]", 0);// Указать фамилию
            additionalMethods.WaitHtmlElement("//input[contains(@id, 'lastname')]");
            swipeAndClick.SetText(SetSurname, project.Profile.Surname, true);
            
            HtmlElement SetBirthdayDay = instance.ActiveTab.FindElementByXPath("//input[contains(@id, 'birthday-day')]", 0);// Указать день рождения
            additionalMethods.WaitHtmlElement("//input[contains(@id, 'birthday-day')]");
            swipeAndClick.SetText(SetBirthdayDay, project.Profile.BornDay.ToString(), true);

            HtmlElement SetBirthdayMonth = instance.ActiveTab.FindElementByXPath("//select[contains(@name, 'month')]", 0);//указать месяц рождения
            additionalMethods.WaitHtmlElement("//select[contains(@name, 'month')]");
            swipeAndClick.SwipeAndClickToElement(SetBirthdayMonth);
            var SetBirthdayMonthTop = Convert.ToInt32(SetBirthdayMonth.GetAttribute("topInTab"));
            var SetBirthdayMonthLeft = Convert.ToInt32(SetBirthdayMonth.GetAttribute("leftInTab"));
            Thread.Sleep(random.Next(2000, 4000));
            instance.ActiveTab.Touch.Touch(SetBirthdayMonthLeft + random.Next(30, 100), SetBirthdayMonthTop + random.Next(100, 200));

            HtmlElement SetBirthdayYear = instance.ActiveTab.FindElementByXPath("//input[contains(@id, 'birthday-year')]", 0);//указать год рождения
            additionalMethods.WaitHtmlElement("//input[contains(@id, 'birthday-year')]");
            swipeAndClick.SetText(SetBirthdayYear, project.Profile.BornYear.ToString(), true);

            if (project.Profile.Sex.ToString().Contains("Female"))
            {
                HtmlElement Gender = instance.ActiveTab.FindElementByXPath("//div[starts-with(text(),'Женский')]", 0);//Выбираем женский пол если акк женский
                swipeAndClick.SwipeAndClickToElement(Gender);
            }

            HtmlElement City = instance.ActiveTab.FindElementByXPath("//input[contains(@id, 'city')]", 0);//Указываем город
            additionalMethods.WaitHtmlElement("//input[contains(@id, 'city')]");
            swipeAndClick.SetText(City, project.Profile.CurrentRegion, true);

            HtmlElement SaveAccountSettings = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Сохранить')]", 0);//СОхраняем персональные настройки
            additionalMethods.WaitHtmlElement("//span[starts-with(text(),'Сохранить')]");
            swipeAndClick.SwipeAndClickToElement(SaveAccountSettings);

            HtmlElement SetPublicAdress = instance.ActiveTab.FindElementByXPath("//span[contains(@class, 'AdditionalPersonalInfo')]", 0);//указать публичный адрес
            additionalMethods.WaitHtmlElement("//span[contains(@class, 'AdditionalPersonalInfo')]");
            swipeAndClick.SwipeAndClickToElement(SetPublicAdress);


            HtmlElement ChangePublicAdress = instance.ActiveTab.FindElementByXPath("//span[contains(@class,'PublicId-suggestValue')]", 0);//выбираем предложенный адрес
            additionalMethods.WaitHtmlElement("//span[contains(@class, 'PublicId-suggestValue')]");
            swipeAndClick.SwipeAndClickToElement(ChangePublicAdress);

            HtmlElement SavePublicAdress = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Сохранить')]", 0);//сохраняем публичный адрес
            additionalMethods.WaitHtmlElement("//span[starts-with(text(),'Сохранить')]");
            swipeAndClick.SwipeAndClickToElement(SavePublicAdress);

            HtmlElement AddHomeAdress = instance.ActiveTab.FindElementByXPath("//div[starts-with(text(),'Добавить домашний')]", 0);//Добавить домашний адрес
            additionalMethods.WaitHtmlElement("//div[starts-with(text(),'Добавить домашний')]");
            swipeAndClick.SwipeAndClickToElement(AddHomeAdress);

            HtmlElement SetHomeAdress = instance.ActiveTab.FindElementByXPath("//input[contains(@id, 'addressLine')]", 0);//Домашний адрес
            additionalMethods.WaitHtmlElement("//input[contains(@id, 'addressLine')]");
            swipeAndClick.SetText(SetHomeAdress, AdressList[random.Next(0, AdressList.Count)], true);
            var SetHomeAdressTop = Convert.ToInt32(SetHomeAdress.GetAttribute("topInTab"));
            var SetHomeAdressLeft = Convert.ToInt32(SetHomeAdress.GetAttribute("leftInTab"));
            instance.ActiveTab.Touch.Touch(SetHomeAdressLeft + random.Next(30, 100), SetHomeAdressTop + random.Next(50, 60));

            HtmlElement SetWorkAdress = instance.ActiveTab.FindElementByXPath("//input[contains(@id, 'addressLine')]", 1);//Работа
            swipeAndClick.SetText(SetWorkAdress, AdressList[random.Next(0, AdressList.Count)], true);
            var SetWorkAdressTop = Convert.ToInt32(SetWorkAdress.GetAttribute("topInTab"));
            var SetWorkAdressLeft = Convert.ToInt32(SetWorkAdress.GetAttribute("leftInTab"));
            instance.ActiveTab.Touch.Touch(SetWorkAdressLeft + random.Next(30, 100), SetWorkAdressTop + random.Next(50, 60));

            HtmlElement SaveAdress = instance.ActiveTab.FindElementByXPath("//span[starts-with(text(),'Сохранить')]", 0);//Сохранить адреса
            additionalMethods.WaitHtmlElement("//span[starts-with(text(),'Сохранить')]");
            swipeAndClick.SwipeAndClickToElement(SaveAdress);
        }//Настрока данных аккаунта
    }
}
