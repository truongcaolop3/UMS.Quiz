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
    public class ExamDAL : _BaseDAL, ICommonDAL<Exam>
    {
        public ExamDAL(string connectionString) : base(connectionString)
        {
        }
        public int Add(Exam data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"insert into Exam (ExamName,StartTime,EndTime)
                            values(@ExamName,@StartTime,@EndTime);
                            select @@identity;";
                var parameters = new
                {
                    ExamName = data.ExamName,
                    StartTime = data.StartTime,
                    EndTime = data.EndTime,
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
                var sql = @"SELECT COUNT(*) FROM Exam 
                WHERE 
                    (@searchValue IS NULL OR @searchValue = '') OR 
                    (ExamName LIKE '%' + @ExamName + '%') OR 
                    (StartTime = @StartTime) OR 
                    (EndTime = @EndTime)";
                var parameters = new
                {
                    searchValue = searchValue ?? "",
                };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return count;
        }

        public int Count(string searchValue = "", string termID = "")
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from Exam where ExamID = @ExamID";
                var parameters = new
                {
                    TopicTemplateID = id
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public Exam? Get(int id)
        {
            Exam? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select from Exam where ExamID = @ExamID";
                var parameters = new { ExamID = id };
                data = connection.QueryFirstOrDefault<Exam>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
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
                var sql = @"IF EXISTS (SELECT * FROM Exam WHERE ExamID = @ExamID)
                    SELECT 1
                ELSE
                    SELECT 0";

                var parameters = new { ExamID = id };

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ExamID", id);
                    result = (int)command.ExecuteScalar() == 1;
                }

                connection.Close();
            }

            return result;
        }

        public IList<Exam> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Exam> data = new List<Exam>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"with cte as
                            (
                             select	*, row_number() over (order by QuestionText) as RowNumber
                             from	Exam 
                             WHERE 
                                (ExamName LIKE '%' + @ExamName + '%') OR 
                                (StartTime = @StartTime) OR 
                                (EndTime = @EndTime)
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
                data = connection.Query<Exam>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();
                connection.Close();
            }
            return data;
        }

        public IList<Exam> List(int page = 1, int pageSize = 0, string searchValue = "", string termId = "")
        {
            throw new NotImplementedException();
        }

        public bool Update(Exam data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"update Exam 
                           set ExamName = @ExamName,
                                StartTime = @StartTime,
                                EndTime = @EndTime,
                            where ExamID = @ExamID";
                var parameters = new
                {
                    ExamName = data.ExamName,
                    StartTime = data.StartTime,
                    EndTime = data.EndTime,
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }
    }
}
