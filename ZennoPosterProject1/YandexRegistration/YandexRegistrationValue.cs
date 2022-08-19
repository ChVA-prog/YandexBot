using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ZennoPosterYandexRegistration
{
    class YandexRegistrationValue
    {
        readonly IZennoPosterProjectModel project;

        protected string HtmlElementEnterId { get; set; }
        protected string HtmlElementCreatId { get; set; }
        protected string HtmlElementSetPhoneNumber { get; set; }
        protected string HtmlElementConfirmPhoneNumber { get; set; }
        protected string HtmlElementInputCodeActivation { get; set; }
        protected string HtmlElementInputFirstName { get; set; }
        protected string HtmlElementInputLastName { get; set; }
        protected string HtmlElementConfirmFirstNameAndLastName { get; set; }
        protected string HtmlElementCheckBox { get; set; }
        protected string HtmlElementConfirmUserAgreement { get; set; }
        protected string HtmlElementAccountMenu { get; set; }
        protected string HtmlElementSettings { get; set; }
        protected string HtmlElementAccountSettings { get; set; }
        protected string HtmlElementCreateLogin { get; set; }
        protected string HtmlElementSetLogin { get; set; }
        protected string HtmlElementApprovedLogin { get; set; }
        protected string HtmlElementSetPassword { get; set; }
        protected string HtmlElementApprovedPassword { get; set; }
        protected string HtmlElementMailAndPhone { get; set; }
        protected string HtmlElementChangeMailAndPhoneList { get; set; }
        protected string HtmlElementOffInputSms { get; set; }
        protected string HtmlElementGoYandexFromAccountSettings { get; set; }
        protected string HtmlElementHumberSettings { get; set; }
        protected string HtmlElementWhyDeletePhoneNumber { get; set; }
        protected string HtmlElementNextPageDeleteNumber { get; set; }
        protected string HtmlElementSecurityQuestionMenu { get; set; }
        protected string HtmlElementResponceSecurityQuestion { get; set; }
        protected string HtmlElementSaveSecurityQuestion { get; set; }
        protected string HtmlElementCheckNeedWritePassword { get; set; }
        protected string HtmlElementInputPasswordSecurityQuestion { get; set; }
        protected string HtmlElementConfirmPasswordSecurityQuestion { get; set; }
        protected string HtmlElementDeletePhoneNumber { get; set; }
        protected string HtmlElementSendSmsForDeletePhoneNumber { get; set; }
        protected string HtmlElementInputSmsCodeDeletePhoneNumber { get; set; }
        protected string HtmlElementInputPasswordForDeletePhoneNumber { get; set; }
        protected string HtmlElementConfirmDeletePhoneNumber { get; set; }
        protected string HtmlElementBackAccountSettings { get; set; }
        protected string HtmlElementSearchResultsCard { get; set; }
        protected string AccountAvatarFolder { get; set; }
        protected List<string> AdressList { get; set; }
        protected string HtmlElementButtonIdForSettingsAccount { get; set; }
        protected string HtmlElementAddAccountPhoto { get; set; }
        protected string HtmlElementDownloadAccountPhoto { get; set; }
        protected string HtmlElementSaveAccountPhoto { get; set; }                         
        protected string HtmlElementAdditionalPersonalInfo { get; set; }
        protected string HtmlElementInputName { get; set; }
        protected string HtmlElementInputBirthdayDay { get; set; }
        protected string HtmlElementInputBirthdayMonth { get; set; }
        protected string HtmlElementInputBirthdayYear { get; set; }
        protected string HtmlElementChangeGender { get; set; }
        protected string HtmlElementInputMySity { get; set; }
        protected string HtmlElementSavePersonalSettings { get; set; }
        protected string HtmlElementAdditionalPersonalAdress { get; set; }
        protected string HtmlElementChangePersonalAdress { get; set; }
        protected string HtmlElementSavePersonalAdress { get; set; }
        protected string HtmlElementAdditionalHomeAdress { get; set; }
        protected string HtmlElementInputHomeAndWorkAdress { get; set; }
        protected string HtmlElementSaveHomeAndWorkAdress { get; set; }
        protected string EmailListPath { get; set; }

        public YandexRegistrationValue(IZennoPosterProjectModel project)
        {
            this.project = project;

            HtmlElementEnterId = project.Variables["set_HtmlElementEnterId"].Value;
            HtmlElementCreatId = project.Variables["set_HtmlElementCreatId"].Value;
            HtmlElementSetPhoneNumber = project.Variables["set_HtmlElementSetPhoneNumber"].Value;
            HtmlElementConfirmPhoneNumber = project.Variables["set_HtmlElementConfirmPhoneNumber"].Value;
            HtmlElementInputCodeActivation = project.Variables["set_HtmlElementInputCodeActivation"].Value;
            HtmlElementInputFirstName = project.Variables["set_HtmlElementInputFirstName"].Value;
            HtmlElementInputLastName = project.Variables["set_HtmlElementInputLastName"].Value;
            HtmlElementConfirmFirstNameAndLastName = project.Variables["set_HtmlElementConfirmFirstNameAndLastName"].Value;
            HtmlElementCheckBox = project.Variables["set_HtmlElementCheckBox"].Value;
            HtmlElementConfirmUserAgreement = project.Variables["set_HtmlElementConfirmUserAgreement"].Value;
            HtmlElementAccountMenu = project.Variables["set_HtmlElementAccountMenu"].Value;
            HtmlElementSettings = project.Variables["set_HtmlElementSettings"].Value;
            HtmlElementAccountSettings = project.Variables["set_HtmlElementAccountSettings"].Value;
            HtmlElementCreateLogin = project.Variables["set_HtmlElementCreateLogin"].Value;
            HtmlElementSetLogin = project.Variables["set_HtmlElementSetLogin"].Value;
            HtmlElementApprovedLogin = project.Variables["set_HtmlElementApprovedLogin"].Value;
            HtmlElementSetPassword = project.Variables["set_HtmlElementSetPassword"].Value;
            HtmlElementApprovedPassword = project.Variables["set_HtmlElementApprovedPassword"].Value;
            HtmlElementMailAndPhone = project.Variables["set_HtmlElementMailAndPhone"].Value;
            HtmlElementChangeMailAndPhoneList = project.Variables["set_HtmlElementChangeMailAndPhoneList"].Value;
            HtmlElementOffInputSms = project.Variables["set_HtmlElementOffInputSms"].Value;
            HtmlElementGoYandexFromAccountSettings = project.Variables["set_HtmlElementGoYandexFromAccountSettings"].Value;
            HtmlElementHumberSettings = project.Variables["set_HtmlElementHumberSettings"].Value;
            HtmlElementWhyDeletePhoneNumber = project.Variables["set_HtmlElementWhyDeletePhoneNumber"].Value;
            HtmlElementNextPageDeleteNumber = project.Variables["set_HtmlElementNextPageDeleteNumber"].Value;
            HtmlElementSecurityQuestionMenu = project.Variables["set_HtmlElementSecurityQuestionMenu"].Value;
            HtmlElementResponceSecurityQuestion = project.Variables["set_HtmlElementResponceSecurityQuestion"].Value;
            HtmlElementSaveSecurityQuestion = project.Variables["set_HtmlElementSaveSecurityQuestion"].Value;
            HtmlElementCheckNeedWritePassword = project.Variables["set_HtmlElementCheckNeedWritePassword"].Value;
            HtmlElementInputPasswordSecurityQuestion = project.Variables["set_HtmlElementInputPasswordSecurityQuestion"].Value;
            HtmlElementConfirmPasswordSecurityQuestion = project.Variables["set_HtmlElementConfirmPasswordSecurityQuestion"].Value;
            HtmlElementDeletePhoneNumber = project.Variables["set_HtmlElementDeletePhoneNumber"].Value;
            HtmlElementSendSmsForDeletePhoneNumber = project.Variables["set_HtmlElementSendSmsForDeletePhoneNumber"].Value;
            HtmlElementInputSmsCodeDeletePhoneNumber = project.Variables["set_HtmlElementInputSmsCodeDeletePhoneNumber"].Value;
            HtmlElementInputPasswordForDeletePhoneNumber = project.Variables["set_HtmlElementInputPasswordForDeletePhoneNumber"].Value;
            HtmlElementConfirmDeletePhoneNumber = project.Variables["set_HtmlElementConfirmDeletePhoneNumber"].Value;
            HtmlElementBackAccountSettings = project.Variables["set_HtmlElementBackAccountSettings"].Value;
            HtmlElementSearchResultsCard = project.Variables["set_HtmlElementSearchResultsCard"].Value;
            AccountAvatarFolder = project.Variables["set_AccountAvatarFolder"].Value;
            AdressList = project.Variables["set_AdressList"].Value.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            EmailListPath = project.Variables["set_EmailList"].Value;

            HtmlElementButtonIdForSettingsAccount = project.Variables["set_HtmlElementButtonIdForSettingsAccount"].Value;
            HtmlElementAddAccountPhoto = project.Variables["set_HtmlElementAddAccountPhoto"].Value;
            HtmlElementDownloadAccountPhoto = project.Variables["set_HtmlElementDownloadAccountPhoto"].Value;
            HtmlElementSaveAccountPhoto = project.Variables["set_HtmlElementSaveAccountPhoto"].Value;
            HtmlElementAdditionalPersonalInfo = project.Variables["set_HtmlElementAdditionalPersonalInfo"].Value;
            HtmlElementInputName = project.Variables["set_HtmlElementInputName"].Value;
            HtmlElementInputBirthdayDay = project.Variables["set_HtmlElementInputBirthdayDay"].Value;
            HtmlElementInputBirthdayMonth = project.Variables["set_HtmlElementInputBirthdayMonth"].Value;
            HtmlElementInputBirthdayYear = project.Variables["set_HtmlElementInputBirthdayYear"].Value;
            HtmlElementChangeGender = project.Variables["set_HtmlElementChangeGender"].Value;
            HtmlElementInputMySity = project.Variables["set_HtmlElementInputMySity"].Value;
            HtmlElementSavePersonalSettings = project.Variables["set_HtmlElementSavePersonalSettings"].Value;
            HtmlElementAdditionalPersonalAdress = project.Variables["set_HtmlElementAdditionalPersonalAdress"].Value;
            HtmlElementChangePersonalAdress = project.Variables["set_HtmlElementChangePersonalAdress"].Value;
            HtmlElementSavePersonalAdress = project.Variables["set_HtmlElementSavePersonalAdress"].Value;
            HtmlElementAdditionalHomeAdress = project.Variables["set_HtmlElementAdditionalHomeAdress"].Value;
            HtmlElementInputHomeAndWorkAdress = project.Variables["set_HtmlElementInputHomeAndWorkAdress"].Value;
            HtmlElementSaveHomeAndWorkAdress = project.Variables["set_HtmlElementSaveHomeAndWorkAdress"].Value;
        }
    }
}
