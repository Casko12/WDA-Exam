using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WDA_Exam.Entities;

public partial class AspDotNetExamContext : DbContext
{
    public AspDotNetExamContext()
    {
    }

    public AspDotNetExamContext(DbContextOptions<AspDotNetExamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=HUNGLAPTOP;Initial Catalog=ASP dotNet Exam;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__classroo__3213E83F41FFFDC6");

            entity.ToTable("classrooms");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__exams__3213E83F785DEC1E");

            entity.ToTable("exams");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassRoomId).HasColumnName("class_room_id");
            entity.Property(e => e.ExamDate)
                .HasColumnType("date")
                .HasColumnName("exam_date");
            entity.Property(e => e.ExamDuration).HasColumnName("exam_duration");
            entity.Property(e => e.FacultiesId).HasColumnName("faculties_id");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.SubjectsId).HasColumnName("subjects_id");

            entity.HasOne(d => d.ClassRoom).WithMany(p => p.Exams)
                .HasForeignKey(d => d.ClassRoomId)
                .HasConstraintName("FK__exams__class_roo__300424B4");

            entity.HasOne(d => d.Faculties).WithMany(p => p.Exams)
                .HasForeignKey(d => d.FacultiesId)
                .HasConstraintName("FK__exams__faculties__30F848ED");

            entity.HasOne(d => d.Subjects).WithMany(p => p.Exams)
                .HasForeignKey(d => d.SubjectsId)
                .HasConstraintName("FK__exams__subjects___2F10007B");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__facultie__3213E83FD6884E77");

            entity.ToTable("faculties");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__subjects__3213E83F04FA8DCC");

            entity.ToTable("subjects");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
