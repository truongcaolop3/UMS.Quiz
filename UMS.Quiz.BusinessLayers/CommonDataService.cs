﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Quiz.DataLayers;
using UMS.Quiz.DataLayers.SQLServer;
using System.Configuration;
using UMS.Quiz.DomainModels;



namespace UMS.Quiz.BusinessLayers
{
    /// <summary>
    /// Cung cấp các chức năng xử lý dữ liệu chung
    /// Khối kiến thức, thư viện trắc nghiệm, câu hỏi trắc nghiệm, khối kiến thức, bộ đề thi, tổ chức thi, danh sách sinh viên thi....
    /// </summary>
    public static class CommonDataService
    {
        private static readonly ICommonDAL<Knowledges> KnowledgesDB;
        private static readonly ICommonDAL<QuizQuestion> QuizQuestionDB;
        private static readonly ICommonDAL<QuestionDetail> QuestionDetailDB;
        private static readonly ICommonDAL<QuizQuestionAnswer> QuizQuestionAnswerDB;
        private static readonly ICommonDAL<TopicTemplate> TopicTemplateDB;

        /// <summary>
        /// Ctor
        /// </summary>
        static CommonDataService()
        {
            string connectionString = Configuration.ConnectionString;

            KnowledgesDB = new KnowledgesDAL(connectionString);
            QuizQuestionDB = new QuizQuestionDAL(connectionString);
            QuestionDetailDB = new QuestionDetailDAL(connectionString);
            QuizQuestionAnswerDB = new QuizQuestionAnswerDAL(connectionString);
            TopicTemplateDB = new TopicTemplateDAL(connectionString);
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách khối kiến thức
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Knowledges> ListOfKnowledges(out int rowCount , int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = KnowledgesDB.Count(searchValue);
            return KnowledgesDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của một khối kiến thức theo mã học phần
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Knowledges? GetKnowledges(int id)
        {
            return KnowledgesDB.Get(id);
        }
        /// <summary>
        /// Bổ sung khối kiến thức
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddKnowledges(Knowledges data)
        {
            return KnowledgesDB.Add(data);
        }
        /// <summary>
        /// Cập nhật khối kiến thức
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateKnowledges(Knowledges data)
        {
            return KnowledgesDB.Update(data);
        }
        /// <summary>
        /// Xóa nhà khối kiến thức có mã là id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteKnowledges(int id)
        {
            if (KnowledgesDB.IsUsed(id))
                return false;
            return KnowledgesDB.Delete(id);
        }
        /// <summary>
        /// Kiểm tra xem khối kiến thức có mã id hiện có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsUsedKnowledges(int id)
        {
            return KnowledgesDB.IsUsed(id);
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách thư viện trắc nghiệm
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<QuizQuestion> ListOfQuizQuestion(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = QuizQuestionDB.Count(searchValue);
            return QuizQuestionDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của một khối kiến thức theo mã học phần
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static QuizQuestion? GetQuizQuestion(int id)
        {
            return QuizQuestionDB.Get(id);
        }
        /// <summary>
        /// Bổ sung khối kiến thức
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddQuizQuestion(QuizQuestion data)
        {
            return QuizQuestionDB.Add(data);
        }
        /// <summary>
        /// Cập nhật khối kiến thức
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateQuizQuestion(QuizQuestion data)
        {
            return QuizQuestionDB.Update(data);
        }
        /// <summary>
        /// Xóa nhà khối kiến thức có mã là id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteQuizQuestion(int id)
        {
            if (QuizQuestionDB.IsUsed(id))
                return false;
            return QuizQuestionDB.Delete(id);
        }
        /// <summary>
        /// Kiểm tra xem khối kiến thức có mã id hiện có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsUsedQuizQuestion(int id)
        {
            return QuizQuestionDB.IsUsed(id);
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách câu hỏi trắc nghiệm của một thư viện 
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<QuestionDetail> ListOfQuestionDetail(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = QuestionDetailDB.Count(searchValue);
            return QuestionDetailDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của một khối kiến thức theo mã học phần
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static QuestionDetail? GetQuestionDetail(int id)
        {
            return QuestionDetailDB.Get(id);
        }
        /// <summary>
        /// Bổ sung khối kiến thức
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddQuestionDetail(QuestionDetail data)
        {
            return QuestionDetailDB.Add(data);
        }
        /// <summary>
        /// Cập nhật khối kiến thức
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateQuestionDetail(QuestionDetail data)
        {
            return QuestionDetailDB.Update(data);
        }
        /// <summary>
        /// Xóa nhà khối kiến thức có mã là id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteQuestionDetail(int id)
        {
            if (QuestionDetailDB.IsUsed(id))
                return false;
            return QuestionDetailDB.Delete(id);
        }
        /// <summary>
        /// Kiểm tra xem khối kiến thức có mã id hiện có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsUsedQuestionDetail(int id)
        {
            return QuestionDetailDB.IsUsed(id);
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách đáp án câu hỏi trắc nghiệm
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<QuizQuestionAnswer> ListOfQuizQuestionAnswer(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = QuizQuestionAnswerDB.Count(searchValue);
            return QuizQuestionAnswerDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của một khối kiến thức theo mã học phần
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static QuizQuestionAnswer? GetQuizQuestionAnswer(int id)
        {
            return QuizQuestionAnswerDB.Get(id);
        }
        /// <summary>
        /// Bổ sung khối kiến thức
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddQuizQuestionAnswer(QuizQuestionAnswer data)
        {
            return QuizQuestionAnswerDB.Add(data);
        }
        /// <summary>
        /// Cập nhật khối kiến thức
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateQuizQuestionAnswer(QuizQuestionAnswer data)
        {
            return QuizQuestionAnswerDB.Update(data);
        }
        /// <summary>
        /// Xóa nhà khối kiến thức có mã là id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteQuizQuestionAnswer(int id)
        {
            if (QuizQuestionAnswerDB.IsUsed(id))
                return false;
            return QuizQuestionAnswerDB.Delete(id);
        }
        /// <summary>
        /// Kiểm tra xem khối kiến thức có mã id hiện có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsUsedQuizQuestionAnswer(int id)
        {
            return QuizQuestionAnswerDB.IsUsed(id);
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách đáp án câu hỏi trắc nghiệm
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<TopicTemplate> ListOfTopicTemplate(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = TopicTemplateDB.Count(searchValue);
            return TopicTemplateDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin của một khối kiến thức theo mã học phần
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TopicTemplate? GetTopicTemplate(int id)
        {
            return TopicTemplateDB.Get(id);
        }
        /// <summary>
        /// Bổ sung khối kiến thức
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddTopicTemplate(TopicTemplate data)
        {
            return TopicTemplateDB.Add(data);
        }
        /// <summary>
        /// Cập nhật khối kiến thức
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateTopicTemplate(TopicTemplate data)
        {
            return TopicTemplateDB.Update(data);
        }
        /// <summary>
        /// Xóa nhà khối kiến thức có mã là id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteTopicTemplate(int id)
        {
            if (TopicTemplateDB.IsUsed(id))
                return false;
            return TopicTemplateDB.Delete(id);
        }
        /// <summary>
        /// Kiểm tra xem khối kiến thức có mã id hiện có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsUsedTopicTemplate(int id)
        {
            return TopicTemplateDB.IsUsed(id);
        }
    }

}