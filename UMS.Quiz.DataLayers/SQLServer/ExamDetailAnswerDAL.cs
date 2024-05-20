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
    public class ExamDetailAnswerDAL : _BaseDAL , ICommonDAL<ExamDetailAnswer>
    {
        public ExamDetailAnswerDAL(string connectionString) : base(connectionString)
        {
        }
        public int Add(ExamDetailAnswer data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"insert into ExamDetailAnswer (Point,AllPoint)
                            values(@Point,@AllPoint);
                            select @@identity;";
                var parameters = new
                {
                    Point = data.Point,
                    AllPoint = data.AllPoint,
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
                var sql = @"SELECT COUNT(*) FROM ExamDetailAnswer 
                WHERE 
                    (@searchValue IS NULL OR @searchValue = '') OR 
                    (Point = @Point) OR 
                    (AllPoint = @AllPoint)";
                var parameters = new
                {
                    searchValue = searchValue ?? "",
                };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return count;
        }

        public int Count(string searchValue = "", string termID = "", int AccountId = 0)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from ExamDetailAnswer where ExamDetailAnswerID = @ExamDetailAnswerID";
                var parameters = new
                {
                    TopicTemplateID = id
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public ExamDetailAnswer? Get(int id)
        {
            ExamDetailAnswer? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select from ExamDetailAnswer where ExamDetailAnswerID = @ExamDetailAnswerID";
                var parameters = new { ExamDetailAnswerID = id };
                data = connection.QueryFirstOrDefault<ExamDetailAnswer>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public ExamDetailAnswer? Get(string id)
        {
            throw new NotImplementedException();
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
                var sql = @"IF EXISTS (SELECT * FROM ExamDetailAnswer WHERE ExamDetailAnswerID = @ExamDetailAnswerID)
                    SELECT 1
                ELSE
                    SELECT 0";

                var parameters = new { ExamDetailAnswerID = id };

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ExamDetailAnswerID", id);
                    result = (int)command.ExecuteScalar() == 1;
                }

                connection.Close();
            }

            return result;
        }

        public IList<ExamDetailAnswer> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<ExamDetailAnswer> data = new List<ExamDetailAnswer>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"with cte as
                            (
                             select	*, row_number() over (order by QuestionText) as RowNumber
                             from	ExamDetailAnswer 
                             WHERE 
                                (@searchValue IS NULL OR @searchValue = '') OR 
                                (Point = @Point) OR 
                                (AllPoint = @AllPoint)
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
                data = connection.Query<ExamDetailAnswer>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();
                connection.Close();
            }
            return data;
        }

        public IList<ExamDetailAnswer> List(int page = 1, int pageSize = 0, string searchValue = "", string termId = "")
        {
            throw new NotImplementedException();
        }

        public IList<ExamDetailAnswer> List(int page = 1, int pageSize = 0, string searchValue = "", string termId = "", int AccountId = 0)
        {
            throw new NotImplementedException();
        }

        public bool Update(ExamDetailAnswer data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"update ExamDetailAnswer 
                           set Point = @Point,
                                AllPoint = @AllPoint,";
                var parameters = new
                {
                    Point = data.Point,
                    AllPoint = data.AllPoint,
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }
    }
}
