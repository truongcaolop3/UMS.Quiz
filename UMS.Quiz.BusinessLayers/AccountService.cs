using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Quiz.DataLayers;
using UMS.Quiz.DataLayers.SQLServer;
using UMS.Quiz.DomainModels;

namespace UMS.Quiz.BusinessLayers
{
    public static class AccountService
    {
        private static readonly IAccountDAL accountDB;

        static AccountService()
        {
            accountDB = new AccountDAL(Configuration.ConnectionString);
        }

        public static Account? Authorize(string userName, string password, string role)
        {
            return accountDB.Login(userName, password, role);
        }
    }
}
