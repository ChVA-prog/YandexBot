using ZennoLab.InterfacesLibrary.ProjectModel;

namespace ZennoPosterYandexRegistrationSmsServiceSmsHubOrg
{
    class SmshubValue
    {
        public static string ApiKeySmshub { get; set; }
        public static string SmshubOperator { get; set; }
        public static string Prefix { get; set; }

        readonly IZennoPosterProjectModel project;
        public SmshubValue(IZennoPosterProjectModel project)
        {
            this.project = project;

            ApiKeySmshub = project.Variables["set_ApiKeySmshub"].Value;
            SmshubOperator = project.Variables["set_SmshubOperator"].Value;
            Prefix = project.Variables["set_Prefix"].Value;
        }


    }
}
