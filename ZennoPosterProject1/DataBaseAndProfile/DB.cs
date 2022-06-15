using System.Data.SQLite;

namespace ZennoPosterDataBaseAndProfile
{
    class DB
    {
        public SQLiteConnection OpenConnectDb()
        {
            string Connection = @"Data Source=" + DataBaseAndProfileValue.PathToDB + "; Pooling=true; FailIfMissing=false; Version=3";

            SQLiteConnection sqliteConnection = new SQLiteConnection(Connection);
            sqliteConnection.Open();

            return sqliteConnection;
        }//Подключение к БД     
    }
}
