using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace ZennoPosterDataBaseAndProfile
{
    class DataBaseAndProfileValue
    {
        readonly IZennoPosterProjectModel Project;

        public static int CountFreeProfileInDB { set; get; }
        public static string PathToDB { set; get; }
        public static string PathToFolderProfile { get; set; }
        public static int CountSession { get; set; }
        public static int CountSessionDay { get; set; }
        public static string PathToProfile { get; set; }


        public DataBaseAndProfileValue(IZennoPosterProjectModel project)
        {
            this.Project = project;

            CountFreeProfileInDB = Convert.ToInt32(Project.Variables["set_CountFreeProfileInDB"].Value);
            PathToDB = Project.Variables["set_PathToDB"].Value;
            PathToFolderProfile = Project.Variables["set_PathToFolderProfile"].Value;
        }
    }
}
