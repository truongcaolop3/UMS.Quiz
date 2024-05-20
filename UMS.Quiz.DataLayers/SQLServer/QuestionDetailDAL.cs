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
    public class QuestionDetailDAL: _BaseDAL, IQuestionDetailDAL
    {
        public QuestionDetailDAL(string connectionString) : base(connectionString)
        {
        }
        public int Add(QuestionDetail data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"insert into QuestionDetail(QuestionType,QuestionText,QuestionPoint)
                            values(@QuestionType,@QuestionText,@QuestionPoint,);
                            select @@identity;";
                var parameters = new
                {
                    QuestionType = data.QuestionType,
                    QuestionText = data.QuestionText,
                    QuestionPoint = data.QuestionPoint
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
                var sql = @"select count(*) from QuestionDetail 
                            where (@searchValue = N'') or (QuestionType like @QuestionType) or (QuestionText like @QuestionText)  or (QuestionPoint like @QuestionPoint)";
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
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT	COUNT(*)
	                        FROM QuestionDetail qd
	                        INNER JOIN Knowledges k ON qd.KnowledgeId = k.KnowledgeId
	                        INNER JOIN Terms t ON t.TermID = k.TermID
	                        WHERE ((@SearchValue = N'') OR (qd.QuestionText LIKE @SearchValue))
		                        AND (@QuestionType = 0 OR qd.QuestionType = @QuestionType)
		                        AND (@KnowledgeId = 0 OR k.KnowledgeId = @KnowledgeId)
		                        AND (@AccountId = N'' or qd.AccountId = @AccountId)";
                var parameters = new
                {
                    searchValue = searchValue ?? "",
                };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return count;
        }

        public int Count(string searchValue = "", int questionType = 0, int knowledgeId = 0, int AccountId = 0)
        {
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT	COUNT(*)
	                        FROM QuestionDetail qd
	                        INNER JOIN Knowledges k ON qd.KnowledgeId = k.KnowledgeId
	                        INNER JOIN Terms t ON t.TermID = k.TermID
	                        WHERE ((@SearchValue = N'') OR (qd.QuestionText LIKE @SearchValue))
		                        AND (@QuestionType = 0 OR qd.QuestionType = @QuestionType)
		                        AND (@KnowledgeId = 0 OR k.KnowledgeId = @KnowledgeId)
		                        AND (@AccountId = 0 or qd.AccountId = @AccountId)";
                var parameters = new
                {
                    SearchValue = searchValue ?? "",
                    QuestionType = questionType,
                    KnowledgeId = knowledgeId,
                    AccountId = AccountId
                };

                var count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                return count;
            }
        }

        public bool Delete(int id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from QuestionDetail where QuestionDetailID = @QuestionDetailID";
                var parameters = new
                {
                    QuestionDetailID = id
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public QuestionDetail? Get(int id)
        {
            QuestionDetail? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select * from QuestionDetail 
                                where QuestionDetailID = @QuestionDetailID";
                var parameters = new { QuestionDetailID = id, };
                data = connection.QueryFirstOrDefault<QuestionDetail>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public QuestionDetail? Get(string id)
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
                var sql = @"IF EXISTS (SELECT * FROM QuestionDetail WHERE QuestionDetailID = @QuestionDetailID)
                    SELECT 1
                ELSE
                    SELECT 0";

                var parameters = new { QuestionDetailID = id };

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@QuestionDetailID", id);
                    result = (int)command.ExecuteScalar() == 1;
                }

                connection.Close();
            }

            return result;
        }

        public IList<QuestionDetail> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<QuestionDetail> data = new List<QuestionDetail>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @";with cte as
                            (
                             select	*, row_number() over (order by QuestionText) as RowNumber
                             from	QuestionDetail 
                             where	(@searchValue = N'') or (QuestionType like @QuestionType) or (QuestionText like @QuestionText)  or (QuestionPoint like @QuestionPoint)
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
                data = connection.Query<QuestionDetail>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();
                connection.Close();
            }
            return data;
        }

       
        public IList<QuestionDetail> List(int page = 1, int pageSize = 0, string searchValue = "", string termId = "", int AccountId = 0)
        {
            List<QuestionDetail> data = new List<QuestionDetail>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @";WITH questionDetailCTE AS
                            (
                                SELECT	t.TermID, 
		                                t.TermName, 
		                                k.KnowledgeId, 
		                                k.KnowledgeName,
		                                qd.QuestionText,
		                                qd.QuestionType,
		                                qd.QuestionPoint,
		                                qd.AccountId,
		                                ROW_NUMBER() OVER (ORDER BY QuestionDetailID DESC) as RowNumber
	                            FROM QuestionDetail qd
	                            INNER JOIN Knowledges k ON qd.KnowledgeId = k.KnowledgeId
	                            INNER JOIN Terms t ON t.TermID = k.TermID
	                            WHERE ((@SearchValue = N'') OR (qd.QuestionText LIKE @SearchValue))
		                            AND (@QuestionType = 0 OR qd.QuestionType = @QuestionType)
		                            AND (@KnowledgeId = 0 OR k.KnowledgeId = @KnowledgeId)
		                            AND (@AccountId = N'' or qd.AccountId = @AccountId)
                            )
                            SELECT * FROM questionDetailCTE
                            WHERE (@PageSize = 0) 
                                OR (RowNumber BETWEEN (@Page - 1) * @PageSize + 1 and @Page * @PageSize)
                            order by RowNumber";
                var parameters = new
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue ?? "",
                    TermID = termId,
                    AccountId = AccountId   
                };
                data = connection.Query<QuestionDetail>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();
                connection.Close();
            }
            return data;
        }

        public IList<QuestionDetail> List(int page = 1, int pageSize = 0, string searchValue = "", int questionType = 0, int knowledgeId = 0, int AccountId = 0)
        {
            List<QuestionDetail> data = new List<QuestionDetail>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @";WITH questionDetailCTE AS
                            (
                            SELECT	qd.QuestionDetailId,
                                    qd.QuestionText,
		                            qd.QuestionType,
		                            qd.QuestionPoint,
		                            qd.AccountId,
                                    t.TermID AS TermID, 
                                    t.TermName AS TermName, 
                                    k.KnowledgeId AS KnowledgeId, 
                                    k.KnowledgeName AS KnowledgeName,
		                            ROW_NUMBER() OVER (ORDER BY QuestionDetailID DESC) as RowNumber
	                        FROM QuestionDetail qd
	                        INNER JOIN Knowledges k ON qd.KnowledgeId = k.KnowledgeId
	                        INNER JOIN Terms t ON t.TermID = k.TermID
	                        WHERE ((@SearchValue = N'') OR (qd.QuestionText LIKE @SearchValue))
		                        AND (@QuestionType = 0 OR qd.QuestionType = @QuestionType)
		                        AND (@KnowledgeId = 0 OR k.KnowledgeId = @KnowledgeId)
		                        AND (@AccountId = 0 OR qd.AccountId = @AccountId)
                            )
                            SELECT * FROM questionDetailCTE
                            WHERE (@PageSize = 0) 
                                OR (RowNumber BETWEEN (@Page - 1) * @PageSize + 1 and @Page * @PageSize)
                            order by RowNumber";
                var parameters = new
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue,
                    QuestionType = questionType,
                    KnowledgeId = knowledgeId,
                    AccountId = AccountId,
                    
                };
                // data = connection.Query<QuestionDetail>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();
                data = connection.Query<QuestionDetail, Terms, Knowledges, QuestionDetail>(sql, (questionDetail, term, knowledge) =>
                {
                    questionDetail.knowledges = knowledge;
                    questionDetail.knowledges.Terms = term;
                    return questionDetail;
                }, 
                splitOn: "TermID,KnowledgeId",
                param: parameters,
                commandType: System.Data.CommandType.Text).ToList();
            }
            //Console.WriteLine(data[0].QuestionText);
            return data;
        }

        public bool Update(QuestionDetail data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"update QuestionDetail 
                           set QuestionType = @QuestionType,
                                QuestionText = @QuestionText,
                                QuestionPoint = @QuestionPoint,
                            where QuestionDetailID = @QuestionDetailID";
                var parameters = new
                {
                    QuestionType = data.QuestionType,
                    QuestionText = data.QuestionText ?? "",
                    QuestionPoint = data.QuestionPoint,
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }
    }
}
