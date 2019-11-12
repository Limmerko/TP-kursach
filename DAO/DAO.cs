using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Computer_Store.DAO
{
    public class DAO
    {
        private string connectionString = System.Configuration.ConfigurationManager.
            ConnectionStrings[@"ComputerStoreDB"].ConnectionString;
        protected SqlConnection connection { get; set; }
        public void connect()
        {
            Logger.log.Info("Устанsовка соединения с БД");
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                Logger.log.Info("Установка соединения с БД выполнена");
            }
            catch (Exception e)
            {
                Logger.log.Error("Произошла ошибка во время подключения к БД" + e.Message);
            }
        }
        public void disconnect()
        {
            Logger.log.Info("Закрытие соединения с БД");
            connection.Close();
        }
    }
}