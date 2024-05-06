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
    public class ExamDetailCandidatesDAL:_BaseDAL, ICommonDAL<ExamDetailCandidates>
    {
        public ExamDetailCandidatesDAL(string connectionString) : base(connectionString)
        {
        }
        public int Add(ExamDetailCandidates data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"insert into ExamDetailCandidates (StudentID,StudentName,BirthDay,Email,AccountStudent,PassworkStudent)
                            values(@StudentID,@StudentName,@BirthDay,@Email,@AccountStudent,@PassworkStudent);
                            select @@identity;";
                var parameters = new
                {
                    StudentID = data.StudentID,
                    StudentName = data.StudentName,
                    BirthDay = data.BirthDay,
                    Email = data.Email,
                    AccountStudent = data.AccountStudent,
                    PassworkStudent = data.PassworkStudent,
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
                var sql = @"SELECT COUNT(*) FROM ExamDetailCandidates 
                WHERE 
                    (@searchValue IS NULL OR @searchValue = '') OR 
                    (StudentID LIKE '%' + @StudentID + '%') OR 
                    (StudentName LIKE '%' + @StudentName + '%') OR 
                    (BirthDay = @BirthDay) OR 
                    (Email = @Email) OR
                    (AccountStudent = @AccountStudent) OR 
                    (PassworkStudent = @PassworkStudent)";
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
                var sql = @"delete from ExamDetailCandidates where ExamDetailCandidatesID = @ExamDetailCandidatesID";
                var parameters = new
                {
                    TopicTemplateID = id
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public ExamDetailCandidates? Get(int id)
        {
            ExamDetailCandidates? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select from ExamDetailCandidates where ExamDetailCandidatesID = @ExamDetailCandidatesID";
                var parameters = new { ExamDetailCandidatesID = id };
                data = connection.QueryFirstOrDefault<ExamDetailCandidates>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
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
                var sql = @"IF EXISTS (SELECT * FROM ExamDetailCandidates WHERE ExamDetailCandidatesID = @ExamDetailCandidatesID)
                    SELECT 1
                ELSE
                    SELECT 0";

                var parameters = new { ExamDetailCandidatesID = id };

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ExamDetailCandidatesID", id);
                    result = (int)command.ExecuteScalar() == 1;
                }

                connection.Close();
            }

            return result;
        }

        public IList<ExamDetailCandidates> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<ExamDetailCandidates> data = new List<ExamDetailCandidates>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"with cte as
                            (
                             select	*, row_number() over (order by QuestionText) as RowNumber
                             from	ExamDetailCandidates 
                             WHERE 
                                (@searchValue IS NULL OR @searchValue = '') OR 
                                (StudentID LIKE '%' + @StudentID + '%') OR 
                                (StudentName LIKE '%' + @StudentName + '%') OR 
                                (BirthDay = @BirthDay) OR 
                                (Email = @Email) OR
                                (AccountStudent = @AccountStudent) OR 
                                (PassworkStudent = @PassworkStudent)
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
                data = connection.Query<ExamDetailCandidates>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();
                connection.Close();
            }
            return data;
        }

        public bool Update(ExamDetailCandidates data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"update ExamDetailCandidates 
                           set StudentID = @StudentID,
                                StudentName = @StudentName,
                                BirthDay = @BirthDay,
                                Email = @Email,
                                AccountStudent = @AccountStudent,
                                PassworkStudent = @PassworkStudent,       
                            where ExamDetailCandidatesID = @ExamDetailCandidatesID";
                var parameters = new
                {
                    StudentID = data.StudentID,
                    StudentName = data.StudentName,
                    BirthDay = data.BirthDay,
                    Email = data.Email,
                    AccountStudent = data.AccountStudent,
                    PassworkStudent = data.PassworkStudent,
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }
    }
}
