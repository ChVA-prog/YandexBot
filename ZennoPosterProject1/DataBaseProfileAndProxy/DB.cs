using System.Data.SQLite;

namespace DataBaseProfileAndProxy
{
    class DB
    {
        public static string PathToDB { set; get; }

        public SQLiteConnection OpenConnectDb() 
        {
            string Connection = @"Data Source=" + PathToDB + "; Pooling=true; FailIfMissing=false; Version=3";

            SQLiteConnection sqliteConnection = new SQLiteConnection(Connection);
            sqliteConnection.Open();

            return sqliteConnection;
        }//Подключение к БД     
    }
}
