using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Quiz.DomainModels;

namespace UMS.Quiz.DataLayers.SQLServer
{
    public class TermDAL : _BaseDAL,ICommonDAL<Terms>
    {
        public TermDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Terms data)
        {
            using (var connection = OpenConnection())
            //insert into Knowledges(KnowledgeName, TermID)
            //            values(@KnowledgeName, @TermID)
            {
                var sql = @"insert into Terms(TermID, TermName)
                            values(@TermID, @TermName)
                            ";

                var parameters = new
                {
                    TermID = data.TermID,
                    TermName = data.TermName,
                };
                var result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
                return result;
            }
        }

        public int Count(string searchValue = "")
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue = "", string termID = "", int AccountId = 0)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Terms? Get(int id)
        {
            Terms? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select * from Terms as k
                                   join Account as t on t.TermID = k.TermID
                                where TermID = @TermID";
                var parameters = new { TermID = id, };
                data = connection.QueryFirstOrDefault<Terms>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public Terms? Get(string id)
        {
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT t.TermID, t.TermName 
                            FROM Terms AS t
                            WHERE t.TermID = @TermID";
                var parameters = new { TermID = id, };
                var data = connection.QueryFirstOrDefault<Terms>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();

                return data;
            }
        }

        public bool IsUsed(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Terms> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            throw new NotImplementedException();
        }

        public IList<Terms> List(int page = 1, int pageSize = 0, string searchValue = "", string termId = "")
        {
            throw new NotImplementedException();
        }

        public IList<Terms> List(int page = 1, int pageSize = 0, string searchValue = "", string termId = "", int AccountId = 0)
        {
            throw new NotImplementedException();
        }

        public bool Update(Terms data)
        {
            throw new NotImplementedException();
        }

        //Terms term(string TermID, string TermName);
    }
}
