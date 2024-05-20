using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Quiz.DomainModels;

namespace UMS.Quiz.DataLayers.SQLServer
{
    public class QuizQuestionDAL : _BaseDAL, ICommonDAL<QuizQuestion>
    {
        public QuizQuestionDAL(string connectionString) : base(connectionString)
        {
        }


        public int Add(QuizQuestion data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"insert into QuizQuestions(KnowledgeId, AccountId)
                            values(@KnowledgeId@AccountId);
                            select @@identity;";
                var parameters = new
                {
                    KnowledgeId = data.KnowledgeId,
                    Knowledge =   data.Knowledge,
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
                var sql = @"select count(*) from QuizQuestion 
                            where (@searchValue = N'') or (KnowledgeId like @searchValue)";
                var parameters = new
                {
                    searchValue = searchValue ?? "",
                };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);

            }
            return count;
        }

        public int Count(string searchValue = "", string termID = "", int AccountId = 0)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                //as q
                //Join Knowledges as k ON q.KnowledgeId = k.KnowledgeId
                var sql = @"SELECT COUNT(*)
                            FROM Knowledges
                            WHERE ((@searchValue = N'') OR (KnowledgeName LIKE '%' + @searchValue + '%')) AND TermID = @TermId";
                var parameters = new
                {
                    searchValue = searchValue ?? "",
                    TermId = termID
                };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);

            }
            return count;
        }

        public bool Delete(int id)
        {
            bool result = false;
            //using (var connection = OpenConnection())
            //{
            //    var sql = @"delete from Knowledges where KnowledgeId = @KnowledgeId";
            //    var parameters = new
            //    {
            //        SupplierID = id
            //    };
            //    result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
            //    connection.Close();
            //}
            return result;
        }

        public QuizQuestion? Get(int id)
        {
            QuizQuestion? data = null;
            //using (var connection = OpenConnection())
            //{
            //    var sql = @"select * from QuizQuestions as q
            //                        Join knowledges as k ON k.knowledgeId = q.KnowledgeId
            //                        where QuizQuestionId = @QuizQuestionId";
            //    var parameters = new { QuizQuestionId = id };
            //    data = connection.QueryFirstOrDefault<QuizQuestion>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
            //    connection.Close();
            //}
            return data;
        }

        public QuizQuestion? Get(string id)
        {
            throw new NotImplementedException();
        }

        public bool IsUsed(int id)
        {
            //bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"if exists(select * from QuizQuestion where QuizQuestionId = @QuizQuestionId)
                                select 1
                            else 
                                select 0";
                var parameters = new { KnowledgeId = id };
                var result = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);

                return result > 0;
            }
        }

        public IList<QuizQuestion> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<QuizQuestion> data = new List<QuizQuestion>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"with cte as
                            (
                             select	*, row_number() over (order by KnowledgeName) as RowNumber
                             from	QuizQuestion 
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
                };
                data = connection.Query<QuizQuestion>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();

            }
            return data;
        }

        public IList<QuizQuestion> List(int page = 1, int pageSize = 0, string searchValue = "", string termId = "")
        {
            List<QuizQuestion> data = new List<QuizQuestion>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @";WITH cte AS
                (
                    SELECT *, ROW_NUMBER() OVER (ORDER BY KnowledgeName) AS RowNumber
                    FROM Knowledges
                    WHERE ((@searchValue = N'') OR (KnowledgeName LIKE '%' + @searchValue + '%')) AND TermID = @TermId
                )
                SELECT k.*, q.*
                FROM cte AS k
                LEFT JOIN QuizQuestions AS q ON k.KnowledgeId = q.KnowledgeId
                WHERE k.RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize
                ORDER BY k.RowNumber;

                -- Count total rows that match the criteria

                SELECT COUNT(*)
                FROM Knowledges
                WHERE ((@searchValue = N'') OR (KnowledgeName LIKE '%' + @searchValue + '%')) AND TermID = @TermId
                ";
                // đếm số dòng dữ liệu
                //SELECT COUNT(*)
                //        FROM Knowledges
                //        WHERE((@searchValue = N'') OR(KnowledgeName LIKE '%' + @searchValue + '%')) AND TermID = @TermId
                var parameters = new
                {
                    page = page,
                    pageSize = pageSize,
                    searchValue = searchValue ?? "",
                    TermID = termId,
                };
                data = connection.Query<QuizQuestion>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();

            }
            return data;
        }

        public IList<QuizQuestion> List(int page = 1, int pageSize = 0, string searchValue = "", string termId = "", int AccountId = 0)
        {
            throw new NotImplementedException();
        }

        public bool Update(QuizQuestion data)
        {
            bool result = false;
            //using (var connection = OpenConnection())
            //{
            //    var sql = @"update Knowledges 
            //                set KnowledgeName = @KnowledgeName,
            //                where KnowledgeId = @KnowledgeId";
            //    var parameters = new
            //    {
            //        KnowledgeName = data.KnowledgeName ?? "",
            //    };
            //    result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
            //    connection.Close();
            //}
            return result;
        }
    }
}
