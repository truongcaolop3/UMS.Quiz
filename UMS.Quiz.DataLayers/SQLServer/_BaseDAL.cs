﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Quiz.DataLayers.SQLServer
{
    public abstract class _BaseDAL
    {
        protected string _connectionString = "";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public _BaseDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Tạo và mở kết nối đến CSDL
        /// </summary>
        /// <returns></returns>
        protected SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }
    }
}
