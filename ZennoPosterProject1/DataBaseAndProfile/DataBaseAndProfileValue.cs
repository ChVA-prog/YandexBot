using System;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace ZennoPosterDataBaseAndProfile
{
    class DataBaseAndProfileValue
    {
        readonly IZennoPosterProjectModel Project;

        protected int CountFreeProfileInDB { set; get; }
        protected string PathToDB { set; get; }
        protected string PathToFolderProfile { get; set; }
        protected int CountSession { get; set; }
        protected int CountSessionDay { get; set; }
        protected string PathToProfile { get; set; }
        protected int CountSessionDayLimit { get; set; }

        public DataBaseAndProfileValue(IZennoPosterProjectModel project)
        {
            this.Project = project;

            CountFreeProfileInDB = Convert.ToInt32(Project.Variables["set_CountFreeProfileInDB"].Value);
            PathToDB = Project.Variables["set_PathToDB"].Value;
            PathToFolderProfile = Project.Variables["set_PathToFolderProfile"].Value;
            CountSessionDayLimit = Convert.ToInt32(Project.Variables["set_CountSessionDayLimit"].Value);
        }
    }
}
