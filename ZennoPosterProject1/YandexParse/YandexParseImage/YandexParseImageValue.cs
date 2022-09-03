using System;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace ZennoPosterYandexParseImage
{
    class YandexParseImageValue
    {
        protected int CountParseImage { get; set; }
        protected string ParseFilter { get; set; }

        readonly IZennoPosterProjectModel project;
        public YandexParseImageValue(IZennoPosterProjectModel project)
        {
            this.project = project;

            CountParseImage = Convert.ToInt32(project.Variables["set_CountParseImage"].Value);
            ParseFilter = project.Variables["set_ParseFilter"].Value;
        }
    }
}
