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
    public class ExamQuestionDAL :_BaseDAL, ICommonDAL<ExamQuestions>
    {
        public ExamQuestionDAL(string connectionString) : base(connectionString)
        {
        }
        public int Add(ExamQuestions data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"insert into ExamQuestions (ExamQuestionName,ExamTime,Status,QuestionType,QuestionText,QuestionPoint)
                            values(@ExamQuestionName,@ExamTime,@Status,QuestionType,QuestionText,QuestionPoint);
                            select @@identity;";
                var parameters = new
                {
                    ExamQuestionName = data.ExamQuestionName,
                    ExamTime = data.ExamTime,
                    Status = data.Status,
                    QuestionType = data.QuestionType,
                    QuestionText = data.QuestionText,
                    QuestionPoint = data.QuestionPoint,
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
                var sql = @"SELECT COUNT(*) FROM ExamQuestions 
                WHERE 
                    (@searchValue IS NULL OR @searchValue = '') OR 
                    (ExamQuestionName LIKE '%' + @ExamQuestionName + '%') OR 
                    (ExamTime = @ExamTime) OR 
                    (Status = @Status) OR 
                    (QuestionType = @QuestionType) OR 
                    (QuestionText = @QuestionText) OR 
                    (QuestionPoint = @QuestionPoint)";
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
                var sql = @"delete from ExamQuestions where ExamQuestionID = @ExamQuestionID";
                var parameters = new
                {
                    TopicTemplateID = id
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public ExamQuestions? Get(int id)
        {
            ExamQuestions? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select from ExamQuestions where ExamQuestionID = @ExamQuestionID";
                var parameters = new { ExamQuestionID = id };
                data = connection.QueryFirstOrDefault<ExamQuestions>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
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
                var sql = @"IF EXISTS (SELECT * FROM ExamQuestions WHERE ExamQuestionID = @ExamQuestionID)
                    SELECT 1
                ELSE
                    SELECT 0";

                var parameters = new { ExamQuestionID = id };

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ExamQuestionID", id);
                    result = (int)command.ExecuteScalar() == 1;
                }

                connection.Close();
            }

            return result;
        }

        public IList<ExamQuestions> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<ExamQuestions> data = new List<ExamQuestions>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"with cte as
                            (
                             select	*, row_number() over (order by QuestionText) as RowNumber
                             from	ExamQuestions 
                             WHERE 
                                (ExamQuestionName LIKE '%' + @ExamQuestionName + '%') OR 
                                (ExamTime = @ExamTime) OR 
                                (Status = @Status) OR 
                                (QuestionType = @QuestionType) OR 
                                (QuestionText = @QuestionText) OR 
                                (QuestionPoint = @QuestionPoint)
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
                data = connection.Query<ExamQuestions>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();
                connection.Close();
            }
            return data;
        }

        public IList<ExamQuestions> List(int page = 1, int pageSize = 0, string searchValue = "", string termId = "")
        {
            throw new NotImplementedException();
        }

        public bool Update(ExamQuestions data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"update ExamQuestions 
                           set ExamQuestionName = @ExamQuestionName,
                                ExamTime = @ExamTime,
                                Status = @Status,
                                QuestionType = @QuestionType,
                                QuestionText = @QuestionText,
                                QuestionPoint = @QuestionPoint,
                            where ExamQuestionID = @ExamQuestionID";
                var parameters = new
                {
                    ExamQuestionName = data.ExamQuestionName ?? "",
                    ExamTime = data.ExamTime,
                    Status = data.Status,
                    QuestionType = data.QuestionType,
                    QuestionText = data.QuestionText,
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }
    }
}
