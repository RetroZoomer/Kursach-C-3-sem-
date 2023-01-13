using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace KursachTP.DAO
{
    public class ConnectDAO
    {
        private const string connStr = "server=localhost;user=root;database=dbkp2;password=root";
        public MySqlConnection connection;
        public void Connect()
        {
            connection = new MySqlConnection(connStr);
            connection.Open();
        }

        public void Disconnect()
        {
            connection.Close();
        }
    }
}
