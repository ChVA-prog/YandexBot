using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Threading;
using System.Data.SQLite;
using ZennoPosterDataBaseAndProfile;
using System.Net;
using System.IO;
using ZennoPosterProject1;

namespace ZennoPosterYandexRegistrationSmsServiceSmsHubOrg
{
    class SmshubValue
    {
        public static string ApiKeySmshub { get; set; }
        public static string SmshubOperator { get; set; }
        public static string PhoneNumber { get; set; }
        public static string IdActivation { get; set; }
        public static string CodeActivation { get; set; }

        readonly IZennoPosterProjectModel project;
        public SmshubValue(IZennoPosterProjectModel project)
        {
            this.project = project;

            ApiKeySmshub = project.Variables["set_ApiKeySmshub"].Value;
            SmshubOperator = project.Variables["set_SmshubOperator"].Value;
        }


    }
}
