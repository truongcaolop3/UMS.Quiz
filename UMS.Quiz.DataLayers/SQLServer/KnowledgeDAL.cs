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
                //insert into Knowledges(KnowledgeName, TermID)
                //            values(@KnowledgeName, @TermID)
            {
                var sql = @"insert into Knowledges(KnowledgeName, TermID)
                            values(@KnowledgeName, @TermID);
                            select @@identity;";
                var parameters = new
                {
                    KnowledgeName = data.KnowledgeName,
                    TermID = data.TermID,
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
                    searchValue = searchValue ?? ""
                };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return count;
        }

        public int Count(string searchValue = "", string termID = "")
        {
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"select count(*) from Knowledges 
                            where ((@searchValue = N'') or (KnowledgeName like @searchValue)) and TermID = @TermId ";
                var parameters = new
                {
                    searchValue = searchValue ?? "",
                    TermId = termID
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
                var sql = @"DELETE  FROM Knowledges where KnowledgeId = @KnowledgeId";
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
                //as k
                //                   join Account as t on t.TermID = k.TermID
                var sql = @"select * from Knowledges 
                                where KnowledgeId = @KnowledgeId";
                var parameters = new { KnowledgeId = id , };
                data = connection.QueryFirstOrDefault<Knowledges>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public bool IsUsed(int id)
        {
            using (var connection = OpenConnection())
            {
                var sql = @"if exists(select * from Knowledges where KnowledgeId = @KnowledgeId)
                                select 1
                            else 
                                select 0";
                var parameters = new { KnowledgeId = id };
                var result = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);

                return result > 0;
            }

            //using (var connection = OpenConnection())
            //{
            //    var sql = @"IF EXISTS (SELECT * FROM Knowledges WHERE KnowledgeId = @KnowledgeId)
            //        SELECT 1
            //    ELSE
            //        SELECT 0";

            //    var parameters = new { KnowledgeId = id };

            //    using (var command = new SqlCommand(sql, connection))
            //    {
            //        command.Parameters.AddWithValue("@KnowledgeId", id);
            //        result = (int)command.ExecuteScalar() == 1;
            //    }

            //    connection.Close();
            //}
        }

        public IList<Knowledges> List(int page = 1, int pageSize = 0, string searchValue = "" )
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
                             where	(@searchValue = N'') or (KnowledgeName like @searchValue)
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
                    //TermID = TermID
                };
                data = connection.Query<Knowledges>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();
                connection.Close();
            }
            return data;
        }

        public IList<Knowledges> List(int page = 1, int pageSize = 0, string searchValue = "", string termId = "")
        {
            List<Knowledges> data = new List<Knowledges>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @";with cte as
                            (
                                select	*, row_number() over (order by KnowledgeName) as RowNumber
                                from	Knowledges
                                where	((@searchValue = N'') or (KnowledgeName like @searchValue)) and TermID = @TermId
                            )
                            select * from cte
                            where  (@pageSize = 0) 
                                or (RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
                            order by RowNumber";
                var parameters = new
                {
                    page = page,
                    pageSize = pageSize,
                    searchValue = searchValue ?? "",
                    TermID = termId,
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
                    set KnowledgeName = @KnowledgeName
                    where KnowledgeId = @KnowledgeId";
                var parameters = new
                {
                    KnowledgeName = data.KnowledgeName ?? "",
                    KnowledgeId = data.KnowledgeId // Thêm tham số KnowledgeId
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }
    }
}
