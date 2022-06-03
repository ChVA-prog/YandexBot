using System;
using ZennoLab.CommandCenter;
using ZennoLab.InterfacesLibrary.ProjectModel;
using System.Data.SQLite;

namespace ZennoPosterDataBaseAndProfile
{
    class DB
    {
        readonly Instance instance;
        readonly IZennoPosterProjectModel project;
  
        public DB(Instance instance, IZennoPosterProjectModel project)
        {
            this.instance = instance;
            this.project = project;           
        }

        public SQLiteConnection OpenConnectDb()
        {
            string PathToDb = DataBaseAndProfileValue.PathToDB;
            string Connection = @"Data Source=" + PathToDb + "; Pooling=true; FailIfMissing=false; Version=3";

            SQLiteConnection sqliteConnection = new SQLiteConnection(Connection);

            sqliteConnection.Open();

            return sqliteConnection;
        }//Подключение к БД     
    }
}
