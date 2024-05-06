﻿using UMS.Quiz.DomainModels;

namespace UMS.Quiz.Web.Models
{
    public class BasePaginationResult
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SearchValue { get; set; } = "";
        public int RowCount { get; set; }
        public int PageCount
        {
            get
            {
                if (PageSize == 0)
                    return 1;

                int c = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    c += 1;
                return c;
            }
        }
    }
    /// <summary>
    /// Kết quả tìm kiếm và lấy danh sách khách hàng
    /// </summary>
    public class KnowledgesSearchResult : BasePaginationResult
    {
        public List<Knowledges> Data { get; set; } = new List<Knowledges>();
    }
    /// <summary>
    /// 
    /// </summary>
    public class QuizQuestionSearchResult : BasePaginationResult
    {
        public List<QuizQuestion> Data { get; set; } = new List<QuizQuestion>();
    }
    /// <summary>
    /// 
    /// </summary>
    public class QuestionDetailSearchResult : BasePaginationResult
    {
        public List<QuestionDetail> Data { get; set; } = new List<QuestionDetail>();
    }
    /// <summary>
    /// 
    /// </summary>
    public class TopicTemplateSearchResult : BasePaginationResult
    {
        public List<TopicTemplate> Data { get; set; } = new List<TopicTemplate>();
    }

    public class ExamQuestionSearchResult : BasePaginationResult
    {
        public List<ExamQuestions> Data { get; set; } = new List<ExamQuestions>();
    }
    public class ExamDetailAnswerSearchResult : BasePaginationResult
    {
        public List<ExamDetailAnswer> Data { get; set; } = new List<ExamDetailAnswer>();
    }

    public class ExamSearchResult : BasePaginationResult
    {
        public List<Exam> Data { get; set; } = new List<Exam>();
    }
    public class ExamDetailCandidatesAnswerSearchResult : BasePaginationResult
    {
        public List<ExamDetailCandidates> Data { get; set; } = new List<ExamDetailCandidates>();
    }
    public class ExamDetailAnswerAnswerSearchResult : BasePaginationResult
    {
        public List<ExamDetailAnswer> Data { get; set; } = new List<ExamDetailAnswer>();
    }
}