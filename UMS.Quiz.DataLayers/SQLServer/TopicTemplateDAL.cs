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
    public class TopicTemplateDAL :_BaseDAL, ICommonDAL<TopicTemplate>
    {
        public TopicTemplateDAL(string connectionString) : base(connectionString)
        {
        }
        public int Add(TopicTemplate data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"insert into TopicTemplate(TopicTemplateName,ExamTime,PointGet,QuantityGet)
                            values(@TopicTemplateName,@ExamTime,@PointGet,QuantityGet);
                            select @@identity;";
                var parameters = new
                {
                    TopicTemplateName = data.TopicTemplateName,
                    ExamTime = data.ExamTime,
                    PointGet = data.PointGet,
                    QuantityGet = data.QuantityGet,
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
                    (QuantityGet = @QuantityGet)";
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

        public IList<TopicTemplate> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<TopicTemplate> data = new List<TopicTemplate>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"with cte as
                            (
                             select	*, row_number() over (order by QuestionText) as RowNumber
                             from	TopicTemplate 
                             WHERE 
                                (@searchValue IS NULL OR @searchValue = '') OR 
                                (TopicTemplateName LIKE '%' + @TopicTemplateName + '%') OR 
                                (ExamTime = @ExamTime) OR 
                                (PointGet = @PointGet) OR 
                                (QuantityGet = @QuantityGet)
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
                data = connection.Query<TopicTemplate>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text).ToList();
                connection.Close();
            }
            return data;
        }

        public IList<TopicTemplate> List(int page = 1, int pageSize = 0, string searchValue = "", string termId = "")
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
