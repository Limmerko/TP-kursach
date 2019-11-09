using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Computer_Store.DAO
{
    public class DAO
    {
        private const string ConnectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial 
                Catalog=ComputerStore;Integrated Security=True;
                Connect Timeout=30;Encrypt=False;
                TrustServerCertificate=False;
                ApplicationIntent=ReadWrite;
                MultiSubnetFailover=False";

        protected SqlConnection Connection { get; set; }
        public void Connect()
        {
            Logger.Log.Info("Установка соединения с БД");
            try
            {
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
                Logger.Log.Info("Установка соединения с БД выполнена");
            }
            catch (Exception e)
            {
                Logger.Log.Error("Произошла ошибка во время подключения к БД" + e.Message);
            }
        }
        public void Disconnect()
        {
            Logger.Log.Info("Закрытие соединения с БД");
            Connection.Close();
        }
    }
}