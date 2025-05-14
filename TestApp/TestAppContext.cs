using System;
using System.Collections.Generic;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;

namespace TestApp;

public partial class TestAppContext : DbContext
{
    public TestAppContext()
    {
    }

    public TestAppContext(DbContextOptions<TestAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StudentAnswer> StudentAnswers { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Загружаем переменные окружения из .env файла
            Env.Load();
            Env.TraversePath().Load();

            // Читаем данные из .env
            string host = Env.GetString("DB_HOST", "localhost");
            string user = Env.GetString("DB_USER", "postgres");
            string password = Env.GetString("DB_PASSWORD", "root");
            string database = Env.GetString("DB_NAME", "test_app");

            // Формируем строку подключения
            string connectionString = $"Host={host}; Username={user}; Password={password}; Database={database}";

            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("answer_pkey");

            entity.ToTable("answer");

            entity.Property(e => e.AnswerId).HasColumnName("answer_id");
            entity.Property(e => e.AnswerText).HasColumnName("answer_text");
            entity.Property(e => e.IsCorrect).HasColumnName("is_correct");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("answer_question_id_fkey");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("question_pkey");

            entity.ToTable("question");

            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.QestionText).HasColumnName("qestion_text");
            entity.Property(e => e.TestId).HasColumnName("test_id");

            entity.HasOne(d => d.Test).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("question_test_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(15)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<StudentAnswer>(entity =>
        {
            entity.HasKey(e => new { e.QuestionQuestionId, e.StudentUserId }).HasName("student_answer_pkey");

            entity.ToTable("student_answer");

            entity.Property(e => e.QuestionQuestionId).HasColumnName("question_question_id");
            entity.Property(e => e.StudentUserId).HasColumnName("student_user_id");
            entity.Property(e => e.AnswerId).HasColumnName("answer_id");

            entity.HasOne(d => d.Answer).WithMany(p => p.StudentAnswers)
                .HasForeignKey(d => d.AnswerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_answer_answer_id_fkey");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("subject_pkey");

            entity.ToTable("subject");

            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(50)
                .HasColumnName("subject_name");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("test_pkey");

            entity.ToTable("test");

            entity.Property(e => e.TestId).HasColumnName("test_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ImageSource).HasColumnName("image_source");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.TestName).HasColumnName("test_name");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Tests)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("test_created_by_fkey");

            entity.HasOne(d => d.Subject).WithMany(p => p.Tests)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("test_subject_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_pkey");

            entity.ToTable("user");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .HasColumnName("fullname");
            entity.Property(e => e.Login)
                .HasMaxLength(30)
                .HasColumnName("login");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("user_role_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
