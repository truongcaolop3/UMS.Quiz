using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Quiz.DomainModels;
using Dapper;
using Microsoft.Data.SqlClient;

namespace UMS.Quiz.DataLayers.SQLServer
{
    public class KnowledgesDAL : _BaseDAL, ICommonDAL<Knowledges>
    {
        public KnowledgesDAL(string connectionString) : base(connectionString)
        {
        }


        public int Add(Knowledges data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"insert into Knowledges(KnowledgeName)
                            values(@KnowledgeName);
                            select @@identity;";
                var parameters = new
                {
                    KnowledgeName = data.KnowledgeName,
                };
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return id;
        }

        public int Count(string searchValue = "")
        {
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"select count(*) from Knowledges 
                            where (@searchValue = N'') or (KnowledgeName like @searchValue) ";
                var parameters = new
                {
                    searchValue = searchValue ?? "",
                };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return count;
        }

        public bool Delete(int id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from Knowledges where KnowledgeId = @KnowledgeId";
                var parameters = new
                {
                    KnowledgeId = id
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public Knowledges? Get(int id)
        {
            Knowledges? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select * from Knowledges where KnowledgeId = @KnowledgeId";
                var parameters = new { KnowledgeId = id };
                data = connection.QueryFirstOrDefault<Knowledges>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public bool IsUsed(int id)
        {
            bool result = false;
            //using (var connection = OpenConnection())
            //{
            //    var sql = @"if exists(select * from Knowledges where KnowledgeId = @KnowledgeId)
            //                    select 1
            //                else 
            //                    select 0";
            //    var parameters = new { KnowledgeId = id };
            //    result = connection.executescalar<bool>(sql: sql, param: parameters, commandtype: system.data.commandtype.text);
            //    connection.close();
            //}

            using (var connection = OpenConnection())
            {
                var sql = @"IF EXISTS (SELECT * FROM Knowledges WHERE KnowledgeId = @KnowledgeId)
                    SELECT 1
                ELSE
                    SELECT 0";

                var parameters = new { KnowledgeId = id };

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@KnowledgeId", id);
                    result = (int)command.ExecuteScalar() == 1;
                }

                connection.Close();
            }

            return result;
        }

        public IList<Knowledges> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Knowledges> data = new List<Knowledges>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"with cte as
                            (
                             select	*, row_number() over (order by KnowledgeName) as RowNumber
                             from	Knowledges 
                             where	(@searchValue = N'') or (KnowledgeName like @searchValue) or (TermName like @searchValue)
                            )
                            select * from cte
                            where  (@pageSize = 0) 
                             or (RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
                            order by RowNumber";
                var parameters = new
                {
                    page = page,
                    pageSize = pageSize,
                    searchValue = searchValue ?? ""
                };
                data = connection.Query<Knowledges>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();
                connection.Close();
            }
            return data;
        }

        public bool Update(Knowledges data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"update Knowledges 
                            set KnowledgeName = @KnowledgeName,
                            where KnowledgeId = @KnowledgeId";
                var parameters = new
                {
                    KnowledgeName = data.KnowledgeName ?? "",
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }
    }
}
