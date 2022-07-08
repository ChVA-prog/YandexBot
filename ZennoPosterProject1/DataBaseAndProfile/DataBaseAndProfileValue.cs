using System;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace ZennoPosterDataBaseAndProfile
{
     class DataBaseAndProfileValue
    {
        readonly IZennoPosterProjectModel Project;

        public int CountFreeProfileInDB { set; get; }
        public string PathToDB { set; get; }
        public string PathToFolderProfile { get; set; }
        public int CountSessionLimit { get; set; }

        public DataBaseAndProfileValue(IZennoPosterProjectModel project)
        {
            this.Project = project;

            CountFreeProfileInDB = Convert.ToInt32(Project.Variables["set_CountFreeProfileInDB"].Value);
            PathToDB = Project.Variables["set_PathToDB"].Value;
            PathToFolderProfile = Project.Variables["set_PathToFolderProfile"].Value;
            CountSessionLimit = Convert.ToInt32(Project.Variables["set_CountSessionDayLimit"].Value);
        }
     }
}
