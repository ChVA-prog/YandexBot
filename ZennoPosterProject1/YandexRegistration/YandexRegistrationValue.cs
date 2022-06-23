using ZennoLab.InterfacesLibrary.ProjectModel;

namespace ZennoPosterYandexRegistration
{
    class YandexRegistrationValue
    {
        readonly IZennoPosterProjectModel project;

        public static string HtmlElementEnterId { get; set; }
        public static string HtmlElementCreatId { get; set; }
        public static string HtmlElementSetPhoneNumber { get; set; }
        public static string HtmlElementConfirmPhoneNumber { get; set; }
        public static string HtmlElementInputCodeActivation { get; set; }
        public static string HtmlElementInputFirstName { get; set; }
        public static string HtmlElementInputLastName { get; set; }
        public static string HtmlElementConfirmFirstNameAndLastName { get; set; }
        public static string HtmlElementCheckBox { get; set; }
        public static string HtmlElementConfirmUserAgreement { get; set; }
        public static string HtmlElementAccountMenu { get; set; }
        public static string HtmlElementSettings { get; set; }
        public static string HtmlElementAccountSettings { get; set; }
        public static string HtmlElementCreateLogin { get; set; }
        public static string HtmlElementSetLogin { get; set; }
        public static string HtmlElementApprovedLogin { get; set; }
        public static string HtmlElementSetPassword { get; set; }
        public static string HtmlElementApprovedPassword { get; set; }
        public static string HtmlElementMailAndPhone { get; set; }
        public static string HtmlElementChangeMailAndPhoneList { get; set; }
        public static string HtmlElementOffInputSms { get; set; }
        public static string HtmlElementGoYandexFromAccountSettings { get;set;}
        public static string YandexLogin { get; set; }
        public static string YandexPassword { get; set; }
        public static string  HtmlElementHumberSettings { get; set; }
        public static string  HtmlElementWhyDeletePhoneNumber { get; set; }
        public static string  HtmlElementNextPageDeleteNumber { get; set; }
        public static string  HtmlElementSecurityQuestionMenu { get; set; }
        public static string  HtmlElementResponceSecurityQuestion { get; set; }
        public static string  HtmlElementSaveSecurityQuestion { get; set; }
        public static string  HtmlElementCheckNeedWritePassword { get; set; }
        public static string  HtmlElementInputPasswordSecurityQuestion { get; set; }
        public static string  HtmlElementConfirmPasswordSecurityQuestion { get; set; }
        public static string  HtmlElementDeletePhoneNumber { get; set; }
        public static string  HtmlElementSendSmsForDeletePhoneNumber { get; set; }
        public static string  HtmlElementInputSmsCodeDeletePhoneNumber { get; set; }
        public static string  HtmlElementInputPasswordForDeletePhoneNumber { get; set; }
        public static string HtmlElementConfirmDeletePhoneNumber { get; set; }
        public static string HtmlElementBackAccountSettings { get; set; }

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
        } 
    }
}
