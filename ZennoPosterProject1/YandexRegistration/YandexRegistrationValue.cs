using System;
using System.Collections.Generic;
using System.Linq;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using ZennoPosterEmulation;
using System.Threading;
using ZennoPosterSiteWalk;
using ZennoPosterProject1;

namespace ZennoPosterProject1.YandexRegistration
{
    class YandexRegistrationValue
    {
        readonly IZennoPosterProjectModel project;
        readonly Instance instance;

        public static string HtmlElementButtonEnter { get; set; }
        public static string HtmlElementCreateId { get; set; }
        public static string HtmElementInputMobileNumber { get; set; }
        public static string HtmlElementConfirmPhoneNumber { get; set; }

        public YandexRegistrationValue(Instance instance,IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.project = project;
            
            
            HtmlElementButtonEnter = project.Variables["set_HtmlElementButtonEnter"].Value;
            HtmlElementCreateId = project.Variables["set_HtmlElementCreateId"].Value;
            HtmElementInputMobileNumber = project.Variables["set_HtmElementInputMobileNumber"].Value;
            HtmlElementConfirmPhoneNumber = project.Variables["set_HtmlElementConfirmPhoneNumber"].Value;
        }

        
    }
}
