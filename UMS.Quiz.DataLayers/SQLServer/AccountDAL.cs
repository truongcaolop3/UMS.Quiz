using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Quiz.DomainModels;

namespace UMS.Quiz.DataLayers.SQLServer
{
    public class AccountDAL : _BaseDAL , ICommonDAL<Account>, IAccountDAL
    {
        public AccountDAL(string connectionString) : base(connectionString)
        {
        }
        public int Add(Account data)
        {
            int id = 0;
            //using (var connection = OpenConnection())
            //{
            //    var sql = @"insert into Exam (ExamName,StartTime,EndTime)
            //                values(@ExamName,@StartTime,@EndTime);
            //                select @@identity;";
            //    var parameters = new
            //    {
            //        ExamName = data.ExamName,
            //        StartTime = data.StartTime,
            //        EndTime = data.EndTime,
            //    };
            //    id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
            //    connection.Close();
            //}
            return id;
        }

        public int Count(string searchValue = "")
        {
            int count = 0;
            //if (!string.IsNullOrEmpty(searchValue))
            //    searchValue = "%" + searchValue + "%";
            //using (var connection = OpenConnection())
            //{
            //    var sql = @"SELECT COUNT(*) FROM Exam 
            //    WHERE 
            //        (@searchValue IS NULL OR @searchValue = '') OR 
            //        (ExamName LIKE '%' + @ExamName + '%') OR 
            //        (StartTime = @StartTime) OR 
            //        (EndTime = @EndTime)";
            //    var parameters = new
            //    {
            //        searchValue = searchValue ?? "",
            //    };
            //    count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
            //    connection.Close();
            //}
            return count;
        }

        public int Count(string searchValue = "", string termID = "")
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            bool result = false;
            //using (var connection = OpenConnection())
            //{
            //    var sql = @"delete from Exam where ExamID = @ExamID";
            //    var parameters = new
            //    {
            //        TopicTemplateID = id
            //    };
            //    result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
            //    connection.Close();
            //}
            return result;
        }

        public Account? Get(int id)
        {
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT * FROM Account WHERE AccountId = @AccountId";
                var parameters = new { AccountId = id };
                var data = connection.QuerySingleOrDefault<Account>(sql: sql, param: parameters, commandType: CommandType.Text);
                
                return data;
            }
        }

        public bool IsUsed(int id)
        {
            bool result = false;
            ////using (var connection = OpenConnection())
            ////{
            ////    var sql = @"if exists(select * from Knowledges where KnowledgeId = @KnowledgeId)
            ////                    select 1
            ////                else 
            ////                    select 0";
            ////    var parameters = new { KnowledgeId = id };
            ////    result = connection.executescalar<bool>(sql: sql, param: parameters, commandtype: system.data.commandtype.text);
            ////    connection.close();
            ////}

            //using (var connection = OpenConnection())
            //{
            //    var sql = @"IF EXISTS (SELECT * FROM Exam WHERE ExamID = @ExamID)
            //        SELECT 1
            //    ELSE
            //        SELECT 0";

            //    var parameters = new { ExamID = id };

            //    using (var command = new SqlCommand(sql, connection))
            //    {
            //        command.Parameters.AddWithValue("@ExamID", id);
            //        result = (int)command.ExecuteScalar() == 1;
            //    }

            //    connection.Close();
            //}

            return result;
        }

        public IList<Account> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Account> data = new List<Account>();
            //if (!string.IsNullOrEmpty(searchValue))
            //    searchValue = "%" + searchValue + "%";
            //using (var connection = OpenConnection())
            //{
            //    var sql = @"with cte as
            //                (
            //                 select	*, row_number() over (order by QuestionText) as RowNumber
            //                 from	Exam 
            //                 WHERE 
            //                    (ExamName LIKE '%' + @ExamName + '%') OR 
            //                    (StartTime = @StartTime) OR 
            //                    (EndTime = @EndTime)
            //                )
            //                select * from cte
            //                where  (@pageSize = 0) 
            //                 or (RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
            //                order by RowNumber";
            //    var parameters = new
            //    {
            //        page = page,
            //        pageSize = pageSize,
            //        searchValue = searchValue ?? ""
            //    };
            //    data = connection.Query<Account>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();
            //    connection.Close();
            //}
            return data;
        }

        public IList<Account> List(int page = 1, int pageSize = 0, string searchValue = "", string termId = "")
        {
            throw new NotImplementedException();
        }

        public Account Login(string userName, string password, string role)
        {
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT TOP (1)  a.AccountId,
                                            a.ID,
                                            a.Password,
                                            a.FullName,  
                                            a.BirthDay,
                                            a.Email,
                                            a.Role,
                                            a.TermId
                            FROM Account AS a
                            WHERE a.ID = @UserName 
                                AND a.Password = @Password 
                                AND a.Role = @Role";
                var parameters = new
                {
                    UserName = userName,
                    Password = password,
                    Role = role
                };
                var result = connection.QuerySingleOrDefault<Account>(sql: sql, param: parameters, commandType: CommandType.Text);

                return result!;
            }
        }

        public bool Update(Account data)
        {
            using (var connection = OpenConnection())
            {
                var sql = @"UPDATE Account 
                            SET TermId = @TermId
                            WHERE AccountId = @AccountId";
                var parameters = new
                {
                    TermId = data.TermId,
                    AccountId = data.AccountId,
                };
                var result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;

                return result;
            }
        }
    }
}
