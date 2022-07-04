using System.Data.SQLite;
using ZennoLab.InterfacesLibrary.ProjectModel;

namespace ZennoPosterDataBaseAndProfile
{
    class DB : DataBaseAndProfileValue
    {
        readonly IZennoPosterProjectModel project;
        public DB(IZennoPosterProjectModel project) : base(project)
        {

        }
        public SQLiteConnection OpenConnectDb() 
        {
            string Connection = @"Data Source=" + PathToDB + "; Pooling=true; FailIfMissing=false; Version=3";

            SQLiteConnection sqliteConnection = new SQLiteConnection(Connection);
            sqliteConnection.Open();

            return sqliteConnection;
        }//Подключение к БД     
    }
}
