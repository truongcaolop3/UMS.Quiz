using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UMS.Quiz.DomainModels;

namespace UMS.Quiz.DataLayers.SQLServer
{
    public class TopicTemplateDAL :_BaseDAL, ITopicTemplateDAL
    {
        public TopicTemplateDAL(string connectionString) : base(connectionString)
        {
        }
        public int Add(TopicTemplate data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"insert into TopicTemplate(TopicTemplateName,ExamTime,PointGet,QuantityGet,All,AllQuantityGet)
                            values(@TopicTemplateName,@ExamTime,@PointGet,@QuantityGet,@AllQuantityGet);
                            select @@identity;";
                var parameters = new
                {
                    TopicTemplateName = data.TopicTemplateName,
                    ExamTime = data.ExamTime,
                    PointGet = data.PointGet,
                    QuantityGet = data.QuantityGet,
                    AllQuantityGet = data.AllQuantityGet,
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
                var sql = @"SELECT COUNT(*) FROM TopicTemplate 
                WHERE 
                    (@searchValue IS NULL OR @searchValue = '') OR 
                    (TopicTemplateName LIKE '%' + @TopicTemplateName + '%') OR 
                    (ExamTime = @ExamTime) OR 
                    (PointGet = @PointGet) OR 
                    (QuantityGet = @QuantityGet) OR 
                    (AllQuantityGet = @AllQuantityGet)";
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
                var sql = @"SELECT COUNT(*) FROM TopicTemplate 
                            WHERE 
                            (@searchValue IS NULL OR @searchValue = '') OR 
                            (TopicTemplateName LIKE '%' + @TopicTemplateName + '%') OR 
                            (ExamTime = @ExamTime) OR 
                            (PointGet = @PointGet) OR 
                            (QuantityGet = @QuantityGet) OR 
                            (AllQuantityGet = @AllQuantityGet)";
                var parameters = new
                {
                    searchValue = searchValue ?? "",
                    TermId = termID,
                    AccountId = AccountId
                };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
            }
            return count;
        }

        public int Count(string searchValue = "", string TermID = "",  int KnowledgeId = 0, int AccountId = 0, int ExamTime = 0)
        {
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT COUNT(DISTINCT tt.TopicTemplateID)
                            FROM TopicTemplate tt
                            INNER JOIN TopicTemplateKnowledges ttk ON ttk.TopicTemplateID = tt.TopicTemplateID
                            INNER JOIN Knowledges k ON ttk.KnowledgeID = k.KnowledgeId
                            WHERE ((@SearchValue = N'') OR (tt.TopicTemplateName LIKE @SearchValue))
                                AND (@ExamTime = 0 OR tt.ExamTime = @ExamTime)
                                AND (@AccountId = 0 OR tt.AccountID = @AccountId)
                                AND (@TermID = '' OR k.TermID = @TermID)";
                var parameters = new
                {
                    SearchValue = searchValue ?? "",
                    TermID = TermID,
                    KnowledgeId = KnowledgeId,
                    AccountId = AccountId,
                    ExamTime = ExamTime
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
                var sql = @"delete from TopicTemplate where TopicTemplateID = @TopicTemplateID";
                var parameters = new
                {
                    TopicTemplateID = id
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public TopicTemplate? Get(int id)
        {
            TopicTemplate? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select from TopicTemplate where TopicTemplateID = @TopicTemplateID";
                var parameters = new { TopicTemplateID = id };
                data = connection.QueryFirstOrDefault<TopicTemplate>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public TopicTemplate? Get(string id)
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
                var sql = @"IF EXISTS (SELECT * FROM TopicTemplate WHERE TopicTemplateID = @TopicTemplateID)
                    SELECT 1
                ELSE
                    SELECT 0";

                var parameters = new { TopicTemplateID = id };

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@TopicTemplateID", id);
                    result = (int)command.ExecuteScalar() == 1;
                }

                connection.Close();
            }

            return result;
        }

        public IList<TopicTemplate> List(int page = 1, int pageSize = 0, string searchValue = "", string TermID = "", int KnowledgeId = 0, int AccountId = 0, int ExamTime = 0)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }

            using (var connection = OpenConnection())
            {
                var sql = @"
                    ;WITH TopicTemplateCTE AS
                    (
                        SELECT
                            tt.TopicTemplateID,
                            tt.TopicTemplateName,
                            tt.ExamTime,
                            tt.PointGet,
                            tt.QuantityGet,
                            tt.AllQuantityGet,
                            k.KnowledgeId,
                            k.knowledgeName,
                            k.TermID,
                            t.TermName,
                            ROW_NUMBER() OVER (PARTITION BY tt.TopicTemplateID ORDER BY tt.TopicTemplateID) as RowNumber
                        FROM TopicTemplate tt
                        INNER JOIN TopicTemplateKnowledges ttk ON ttk.TopicTemplateID = tt.TopicTemplateID
                        INNER JOIN Knowledges k ON ttk.KnowledgeID = k.KnowledgeId
                        INNER JOIN Terms t ON t.TermID = k.TermID
                        WHERE ((@SearchValue = N'') OR (tt.TopicTemplateName LIKE @SearchValue) OR (tt.ExamTime LIKE @SearchValue) OR (tt.QuantityGet LIKE @SearchValue))
                            AND (@ExamTime = 0 OR tt.ExamTime = @ExamTime)
                            AND (@AccountId = 0 OR tt.AccountID = @AccountId)
                            AND (@TermID = '' OR k.TermID = @TermID)
                    )
                    SELECT * FROM TopicTemplateCTE
                    WHERE RowNumber = 1
                        AND (@PageSize = 0 OR RowNumber BETWEEN (@Page - 1) * @PageSize + 1 AND @Page * @PageSize)
                    ORDER BY RowNumber";

                var parameters = new
                {
                    Page = page,
                    PageSize = pageSize,
                    SearchValue = searchValue,
                    TermID = TermID,
                    AccountId = AccountId,
                    ExamTime = ExamTime,
                };

                var topicTemplateDictionary = new Dictionary<int, TopicTemplate>();

                var data = connection.Query<TopicTemplate, Knowledges, TopicTemplate>(sql,
                    (topicTemplate, knowledge) =>
                    {
                        if (!topicTemplateDictionary.TryGetValue(topicTemplate.TopicTemplateID, out var currentTopicTemplate))
                        {
                            currentTopicTemplate = topicTemplate;
                            topicTemplateDictionary.Add(currentTopicTemplate.TopicTemplateID, currentTopicTemplate);
                        }
                        if (knowledge != null)
                        {
                            if (currentTopicTemplate.TopicTemplateKnowledges.All(ttk => ttk.KnowledgeID != knowledge.KnowledgeId))
                            {
                                currentTopicTemplate.TopicTemplateKnowledges.Add(new TopicTemplateKnowledge
                                {
                                    TopicTemplateID = currentTopicTemplate.TopicTemplateID,
                                    Knowledge = knowledge
                                });
                            }
                        }

                        return currentTopicTemplate;
                    },
                    splitOn: "KnowledgeId",
                    param: parameters,
                    commandType: System.Data.CommandType.Text).ToList();

                return data;
            }
        }

        public IList<TopicTemplate> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            throw new NotImplementedException();
        }

        public IList<TopicTemplate> List(int page = 1, int pageSize = 0, string searchValue = "", string termId = "", int AccountId = 0)
        {
            throw new NotImplementedException();
        }

        
        public bool Update(TopicTemplate data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"update TopicTemplate 
                           set TopicTemplateName = @TopicTemplateName,
                                ExamTime = @ExamTime,
                                PointGet = @PointGet,
                                QuantityGet = @QuantityGet
                            where TopicTemplateID = @TopicTemplateID";
                var parameters = new
                {
                    TopicTemplateName = data.TopicTemplateName ?? "",
                    ExamTime = data.ExamTime,
                    PointGet = data.PointGet,
                    QuantityGet = data.QuantityGet,
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }
    }
}
