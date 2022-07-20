using System;
using System.Data.SQLite;
using ZennoPosterProject1;

namespace DataBaseProfileAndProxy
{
    class DB
    {
        public static string PathToDB { set; get; }

        public SQLiteConnection OpenConnectDb() 
        {
            try
            {
                Program.logger.Debug("Открываем соединение с БД");

                string Connection = @"Data Source=" + PathToDB + "; Pooling=true; FailIfMissing=false; Version=3";

                SQLiteConnection sqliteConnection = new SQLiteConnection(Connection);
                sqliteConnection.Open();

                Program.logger.Debug("Соединение с БД успешно открыто.");
                return sqliteConnection;
            }
            catch (Exception ex)
            {
                Program.logger.Error("Не удалось открыть соединение с БД. " + ex.Message);
                throw new Exception("Не удалось открыть соединение с БД. " + ex.Message);
            }
        }//Подключение к БД     
    }
}
