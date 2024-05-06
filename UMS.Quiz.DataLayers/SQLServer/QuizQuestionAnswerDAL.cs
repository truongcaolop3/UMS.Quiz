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
    public class QuizQuestionAnswerDAL : _BaseDAL, ICommonDAL<QuizQuestionAnswer>
    {
        public QuizQuestionAnswerDAL(string connectionString) : base(connectionString)
        {
        }
        public int Add(QuizQuestionAnswer data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"insert into QuizQuestionAnswer(AnswerText,IsCorrect,PercenterValue)
                            values(@AnswerText,@IsCorrect,@PercenterValue,);
                            select @@identity;";
                var parameters = new
                {
                    AnswerText = data.AnswerText,
                    IsCorrect = data.IsCorrect,
                    PercenterValue = data.PercenterValue
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
                var sql = @"select count(*) from QuizQuestionAnswer 
                            where (@searchValue = N'') or (AnswerText like @AnswerText) or (IsCorrect like @IsCorrect)  or (PercenterValue like @PercenterValue)";
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
                var sql = @"delete from QuizQuestionAnswer where QuizQuestionAnswerID = @QuizQuestionAnswerID";
                var parameters = new
                {
                    QuizQuestionAnswerID = id
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public QuizQuestionAnswer? Get(int id)
        {
            QuizQuestionAnswer? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select from QuizQuestionAnswer where QuizQuestionAnswerID = @QuizQuestionAnswerID";
                var parameters = new { QuizQuestionAnswerID = id };
                data = connection.QueryFirstOrDefault<QuizQuestionAnswer>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
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
                var sql = @"IF EXISTS (SELECT * FROM QuizQuestionAnswer WHERE QuizQuestionAnswerID = @QuizQuestionAnswerID)
                    SELECT 1
                ELSE
                    SELECT 0";

                var parameters = new { QuizQuestionAnswerID = id };

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@QuizQuestionAnswerID", id);
                    result = (int)command.ExecuteScalar() == 1;
                }

                connection.Close();
            }

            return result;
        }

        public IList<QuizQuestionAnswer> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<QuizQuestionAnswer> data = new List<QuizQuestionAnswer>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"with cte as
                            (
                             select	*, row_number() over (order by QuestionText) as RowNumber
                             from	QuizQuestionAnswer 
                             where	(@searchValue = N'') or (AnswerText like @AnswerText) or (IsCorrect like @IsCorrect)  or (PercenterValue like @PercenterValue)
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
                data = connection.Query<QuizQuestionAnswer>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();
                connection.Close();
            }
            return data;
        }

        public bool Update(QuizQuestionAnswer data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"update QuizQuestionAnswer 
                           set AnswerText = @AnswerText,
                                IsCorrect = @IsCorrect,
                                PercenterValue = @PercenterValue,
                            where QuizQuestionAnswerID = @QuizQuestionAnswerID";
                var parameters = new
                {
                    AnswerText = data.AnswerText ?? "",
                    IsCorrect = data.IsCorrect,
                    PercenterValue = data.PercenterValue,
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }
    }
}
