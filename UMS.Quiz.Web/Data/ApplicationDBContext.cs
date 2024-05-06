using Microsoft.EntityFrameworkCore;
using UMS.Quiz.DomainModels;

namespace UMS.Quiz.Web.Data
{
    public class ApplicationDBContext :DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            Account = Set<Account>();
            Terms = Set<Terms>();
            Knowledges = Set<Knowledges>();
            QuizQuestions = Set<QuizQuestion>();
            QuestionDetail = Set<QuestionDetail>();
            QuizQuestionAnswer = Set<QuizQuestionAnswer>();
            TopicTemplate = Set<TopicTemplate>();
            ExamQuestions = Set<ExamQuestions>();
            Exam = Set<Exam>();
            ExamDetailCandidates = Set<ExamDetailCandidates>();
            ExamDetailAnswer = Set<ExamDetailAnswer>(); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tự tăng ID của Knowledges
            modelBuilder.Entity<Knowledges>()
            .Property(a => a.KnowledgeId)
            .ValueGeneratedOnAdd();
            // Tự tăng ID của QuizQuestion
            modelBuilder.Entity<QuizQuestion>()
            .Property(a => a.QuizQuestionId)
            .ValueGeneratedOnAdd();
            // Tự tăng ID của QuizQuestion
            modelBuilder.Entity<QuestionDetail>()
            .Property(a => a.QuestionDetailID)
            .ValueGeneratedOnAdd();
            // Tự tăng ID của QuizQuestionAnswer
            modelBuilder.Entity<QuizQuestionAnswer>()
            .Property(a => a.QuizQuestionAnswerID)
            .ValueGeneratedOnAdd();
            // Tự tăng ID của TopicTemplate
            modelBuilder.Entity<TopicTemplate>()
            .Property(a => a.TopicTemplateID)
            .ValueGeneratedOnAdd();
            // Tự tăng ID của ExamQuestion
            modelBuilder.Entity<ExamQuestions>()
            .Property(a => a.ExamQuestionID)
            .ValueGeneratedOnAdd();
            // Tự tăng ID của Exam
            modelBuilder.Entity<Exam>()
            .Property(a => a.ExamID)
            .ValueGeneratedOnAdd();
            // Tự tăng ID của ExamDetailCandidates
            modelBuilder.Entity<ExamDetailCandidates>()
            .Property(a => a.ExamDetailCandidatesID)
            .ValueGeneratedOnAdd();
            // Tự tăng ID của Account
            modelBuilder.Entity<Account>()
            .Property(a => a.AccountId)
            .ValueGeneratedOnAdd();

            //modelBuilder.Entity<Knowledges>().HasKey(k => k.KnowledgeId);
            //modelBuilder.Entity<QuizQuestion>().HasKey(q => q.QuizQuestionId);
            //modelBuilder.Entity<QuizQuestionAnswer>().HasKey(q => q.AnswerID);
            modelBuilder.Entity<Terms>().HasKey(k => k.TermID);
            modelBuilder.Entity<Knowledges>().HasKey(k => k.KnowledgeId);
            modelBuilder.Entity<QuizQuestion>().HasKey(q => q.QuizQuestionId);
            modelBuilder.Entity<QuestionDetail>().HasKey(q => q.QuestionDetailID);
            modelBuilder.Entity<QuizQuestionAnswer>().HasKey(q => q.QuizQuestionAnswerID);
            modelBuilder.Entity<TopicTemplate>().HasKey(t => t.TopicTemplateID);
            modelBuilder.Entity<ExamQuestions>().HasKey(e => e.ExamQuestionID);
            modelBuilder.Entity<Exam>().HasKey(e => e.ExamID);
            modelBuilder.Entity<ExamDetailCandidates>().HasKey(e => e.ExamDetailCandidatesID);
            modelBuilder.Entity<ExamDetailAnswer>().HasKey(e => e.ExamDetailAnswerID);

            // Thiết lập mối quan hệ 1-n giữa Terms và Knowledges
            modelBuilder.Entity<Knowledges>()
                .HasOne(k => k.Terms)
                .WithMany(t => t.Knowledges)
                .HasForeignKey(k => k.TermID);
            // Mối quan hệ 1-1 giữa QuestionDetail và QuizQuestion
            modelBuilder.Entity<QuestionDetail>()
                .HasOne(qd => qd.QuizQuestion)
                .WithOne(q => q.QuestionDetail)
                .HasForeignKey<QuestionDetail>(qd => qd.QuizQuestionId);

            // Mối quan hệ 1-n giữa QuestionDetail và QuizQuestionAnswer
            modelBuilder.Entity<QuestionDetail>()
                .HasMany(qd => qd.QuizQuestionAnswers)
                .WithOne(qa => qa.QuestionDetail)
                .HasForeignKey(qa => qa.QuestionDetailId);

            // Thiết lập mối quan hệ một-nhiều giữa TopicTemplate và QuestionDetail
            modelBuilder.Entity<QuestionDetail>()
                .HasOne(t => t.TopicTemplate) 
                .WithMany(t => t.questionDetails) 
                .HasForeignKey(t => t.TopicTemplateID);

            // Thiết lập mối quan hệ một-nhiều giữa TopicTemplate và Knowledges
            modelBuilder.Entity<Knowledges>()
                .HasOne(t => t.TopicTemplate)
                .WithMany(t => t.Knowledges)
                .HasForeignKey(t => t.TopicTemplateID);

            // Thiết lập mối quan hệ một-nhiều giữa TopicTemplate và ExamQuestion 
            modelBuilder.Entity<ExamQuestions>()
            .HasOne(t => t.TopicTemplate)
            .WithMany(t => t.ExamQuestions) // ExamQuestions là tên của ICollection trong TopicTemplate
            .HasForeignKey(t => t.TopicTemplateID);


            // Thiết lập mối quan hệ một-nhiều giữa ExamQuestion và Exam 
            modelBuilder.Entity<Exam>()
                .HasOne(t => t.ExamQuestions)
                .WithMany(t => t.Exam)
                .HasForeignKey(t => t.ExamQuestionID);

            // Thiết lập mối quan hệ một-nhiều giữa Exam và ExamDetailCandidates
            modelBuilder.Entity<ExamDetailCandidates>()
                .HasOne(t => t.Exam)
                .WithMany(t => t.examDetailCandidates)
                .HasForeignKey(t => t.ExamID);

            // Thiết lập mối quan hệ một-nhiều giữa ExamDetailCandidates và ExamDetailAnswe
            modelBuilder.Entity<ExamDetailAnswer>()
                .HasOne(t => t.ExamDetailCandidates)
                .WithMany(t => t.examDetailAnswers)
                .HasForeignKey(t => t.ExamDetailCandidatesID);
        }
        public DbSet<Terms> Terms { get; set; }
        public DbSet<Knowledges> Knowledges { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuestionDetail> QuestionDetail { get; set; }
        public DbSet<QuizQuestionAnswer> QuizQuestionAnswer { get; set;}
        public DbSet<TopicTemplate> TopicTemplate { get; set; }
        public DbSet<ExamQuestions> ExamQuestions { get; set; }
        public DbSet<Exam> Exam { get; }
        public DbSet<ExamDetailCandidates> ExamDetailCandidates { get; set;}
        public DbSet<ExamDetailAnswer> ExamDetailAnswer { get; set;}
        public DbSet<Account> Account { get; set; }
    }
}
