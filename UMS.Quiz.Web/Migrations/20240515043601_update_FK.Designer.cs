﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UMS.Quiz.Web.Data;

#nullable disable

namespace UMS.Quiz.Web.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240515043601_update_FK")]
    partial class update_FK
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("UMS.Quiz.DomainModels.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"), 1L, 1);

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TermId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.Exam", b =>
                {
                    b.Property<int>("ExamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExamID"), 1L, 1);

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExamName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ExamQuestionID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ExamID");

                    b.HasIndex("ExamQuestionID");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.ExamDetailAnswer", b =>
                {
                    b.Property<int>("ExamDetailAnswerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExamDetailAnswerID"), 1L, 1);

                    b.Property<float>("AllPoint")
                        .HasColumnType("real");

                    b.Property<int>("ExamDetailCandidatesID")
                        .HasColumnType("int");

                    b.Property<float>("Point")
                        .HasColumnType("real");

                    b.HasKey("ExamDetailAnswerID");

                    b.HasIndex("ExamDetailCandidatesID");

                    b.ToTable("ExamDetailAnswer");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.ExamDetailCandidates", b =>
                {
                    b.Property<int>("ExamDetailCandidatesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExamDetailCandidatesID"), 1L, 1);

                    b.Property<string>("AccountStudent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ExamID")
                        .HasColumnType("int");

                    b.Property<string>("PassworkStudent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExamDetailCandidatesID");

                    b.HasIndex("ExamID");

                    b.ToTable("ExamDetailCandidates");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.ExamQuestions", b =>
                {
                    b.Property<int>("ExamQuestionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExamQuestionID"), 1L, 1);

                    b.Property<string>("ExamQuestionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExamTime")
                        .HasColumnType("int");

                    b.Property<int>("QuestionPoint")
                        .HasColumnType("int");

                    b.Property<string>("QuestionText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<int?>("TopicTemplateID")
                        .HasColumnType("int");

                    b.HasKey("ExamQuestionID");

                    b.HasIndex("TopicTemplateID");

                    b.ToTable("ExamQuestions");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.Knowledges", b =>
                {
                    b.Property<int>("KnowledgeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KnowledgeId"), 1L, 1);

                    b.Property<string>("KnowledgeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TermID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("TopicTemplateID")
                        .HasColumnType("int");

                    b.HasKey("KnowledgeId");

                    b.HasIndex("TermID");

                    b.HasIndex("TopicTemplateID");

                    b.ToTable("Knowledges");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.QuestionDetail", b =>
                {
                    b.Property<int>("QuestionDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionDetailID"), 1L, 1);

                    b.Property<int>("QuestionPoint")
                        .HasColumnType("int");

                    b.Property<string>("QuestionText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("QuizQuestionId")
                        .HasColumnType("int");

                    b.Property<int>("TopicTemplateID")
                        .HasColumnType("int");

                    b.HasKey("QuestionDetailID");

                    b.HasIndex("QuizQuestionId");

                    b.HasIndex("TopicTemplateID");

                    b.ToTable("QuestionDetail");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.QuizQuestion", b =>
                {
                    b.Property<int>("QuizQuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuizQuestionId"), 1L, 1);

                    b.Property<int>("KnowledgeId")
                        .HasColumnType("int");

                    b.Property<int>("QuizNumber")
                        .HasColumnType("int");

                    b.HasKey("QuizQuestionId");

                    b.HasIndex("KnowledgeId")
                        .IsUnique();

                    b.ToTable("QuizQuestions");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.QuizQuestionAnswer", b =>
                {
                    b.Property<int>("QuizQuestionAnswerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuizQuestionAnswerID"), 1L, 1);

                    b.Property<string>("AnswerText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<float>("PercenterValue")
                        .HasColumnType("real");

                    b.Property<int>("QuestionDetailId")
                        .HasColumnType("int");

                    b.HasKey("QuizQuestionAnswerID");

                    b.HasIndex("QuestionDetailId");

                    b.ToTable("QuizQuestionAnswer");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.Terms", b =>
                {
                    b.Property<string>("TermID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TermName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TermID");

                    b.ToTable("Terms");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.TopicTemplate", b =>
                {
                    b.Property<int>("TopicTemplateID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TopicTemplateID"), 1L, 1);

                    b.Property<int>("ExamTime")
                        .HasColumnType("int");

                    b.Property<int>("PointGet")
                        .HasColumnType("int");

                    b.Property<int>("QuantityGet")
                        .HasColumnType("int");

                    b.Property<string>("TopicTemplateName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TopicTemplateID");

                    b.ToTable("TopicTemplate");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.Exam", b =>
                {
                    b.HasOne("UMS.Quiz.DomainModels.ExamQuestions", "ExamQuestions")
                        .WithMany("Exam")
                        .HasForeignKey("ExamQuestionID");

                    b.Navigation("ExamQuestions");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.ExamDetailAnswer", b =>
                {
                    b.HasOne("UMS.Quiz.DomainModels.ExamDetailCandidates", "ExamDetailCandidates")
                        .WithMany("examDetailAnswers")
                        .HasForeignKey("ExamDetailCandidatesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExamDetailCandidates");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.ExamDetailCandidates", b =>
                {
                    b.HasOne("UMS.Quiz.DomainModels.Exam", "Exam")
                        .WithMany("examDetailCandidates")
                        .HasForeignKey("ExamID");

                    b.Navigation("Exam");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.ExamQuestions", b =>
                {
                    b.HasOne("UMS.Quiz.DomainModels.TopicTemplate", "TopicTemplate")
                        .WithMany("ExamQuestions")
                        .HasForeignKey("TopicTemplateID");

                    b.Navigation("TopicTemplate");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.Knowledges", b =>
                {
                    b.HasOne("UMS.Quiz.DomainModels.Terms", "Terms")
                        .WithMany("Knowledges")
                        .HasForeignKey("TermID");

                    b.HasOne("UMS.Quiz.DomainModels.TopicTemplate", "TopicTemplate")
                        .WithMany("Knowledges")
                        .HasForeignKey("TopicTemplateID");

                    b.Navigation("Terms");

                    b.Navigation("TopicTemplate");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.QuestionDetail", b =>
                {
                    b.HasOne("UMS.Quiz.DomainModels.QuizQuestion", "quizQuestion")
                        .WithMany("questionDetails")
                        .HasForeignKey("QuizQuestionId");

                    b.HasOne("UMS.Quiz.DomainModels.TopicTemplate", "TopicTemplate")
                        .WithMany("questionDetails")
                        .HasForeignKey("TopicTemplateID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TopicTemplate");

                    b.Navigation("quizQuestion");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.QuizQuestion", b =>
                {
                    b.HasOne("UMS.Quiz.DomainModels.Knowledges", "Knowledge")
                        .WithOne("QuizQuestion")
                        .HasForeignKey("UMS.Quiz.DomainModels.QuizQuestion", "KnowledgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Knowledge");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.QuizQuestionAnswer", b =>
                {
                    b.HasOne("UMS.Quiz.DomainModels.QuestionDetail", "QuestionDetail")
                        .WithMany("QuizQuestionAnswers")
                        .HasForeignKey("QuestionDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuestionDetail");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.Exam", b =>
                {
                    b.Navigation("examDetailCandidates");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.ExamDetailCandidates", b =>
                {
                    b.Navigation("examDetailAnswers");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.ExamQuestions", b =>
                {
                    b.Navigation("Exam");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.Knowledges", b =>
                {
                    b.Navigation("QuizQuestion");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.QuestionDetail", b =>
                {
                    b.Navigation("QuizQuestionAnswers");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.QuizQuestion", b =>
                {
                    b.Navigation("questionDetails");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.Terms", b =>
                {
                    b.Navigation("Knowledges");
                });

            modelBuilder.Entity("UMS.Quiz.DomainModels.TopicTemplate", b =>
                {
                    b.Navigation("ExamQuestions");

                    b.Navigation("Knowledges");

                    b.Navigation("questionDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
