using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Quiz.DomainModels;

namespace UMS.Quiz.DataLayers
{
    /// <summary>
    /// Giao diện giành cho tài khoản
    /// </summary>
    public interface IAccountDAL
    {
        Account Login(string userName, string password, string role);
    }
}
