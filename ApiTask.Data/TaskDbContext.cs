using System;
using System.Collections.Generic;
using Task = ApiTask.Data.ScaffoldModels.Task;
using ApiTask.Data.ScaffoldModels;
using Microsoft.EntityFrameworkCore;


namespace ApiTask.Api;

public partial class TaskDbContext : DbContext
{
    public TaskDbContext()
    {
    }

    public TaskDbContext(DbContextOptions<TaskDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GroupTask> GroupTasks { get; set; }

    public virtual DbSet<StatusHistory> StatusHistories { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=../ApiTask.Data/Migration/daynor.tito.sqlite.db");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupTask>(entity =>
        {
            entity.ToTable("group_task");

            entity.Property(e => e.Id)
                .HasColumnType("VARCHAR(75)")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR(35)")
                .HasColumnName("name");
            entity.Property(e => e.UserId)
                .HasColumnType("VARCHAR(75)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.GroupTasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StatusHistory>(entity =>
        {
            entity.ToTable("status_history");

            entity.Property(e => e.Id)
                .HasColumnType("VARCHAR(75)")
                .HasColumnName("id");
            entity.Property(e => e.ChangedDate)
                .HasColumnType("DATE")
                .HasColumnName("changed_date");
            entity.Property(e => e.NewStatus)
                .HasColumnType("VARCHAR(35)")
                .HasColumnName("new_status");
            entity.Property(e => e.OldStatus)
                .HasColumnType("VARCHAR(35)")
                .HasColumnName("old_status");
            entity.Property(e => e.TaskId)
                .HasColumnType("VARCHAR(75)")
                .HasColumnName("task_id");

            entity.HasOne(d => d.Task).WithMany(p => p.StatusHistories)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.ToTable("task");

            entity.Property(e => e.Id)
                .HasColumnType("VARCHAR(75)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("DATE")
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("description");
            entity.Property(e => e.DueDate)
                .HasColumnType("DATE")
                .HasColumnName("due_date");
            entity.Property(e => e.GroupId)
                .HasColumnType("VARCHAR(75)")
                .HasColumnName("group_id");
            entity.Property(e => e.Priority)
                .HasColumnType("VARCHAR(40)")
                .HasColumnName("priority");
            entity.Property(e => e.Status)
                .HasColumnType("VARCHAR(40)")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("title");
            entity.Property(e => e.UserId)
                .HasColumnType("VARCHAR(75)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.Group).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.User).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");

            entity.Property(e => e.Id)
                .HasColumnType("VARCHAR(75)")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("email");
            entity.Property(e => e.Passwd)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("passwd");
            entity.Property(e => e.Username)
                .HasColumnType("VARCHAR(35)")
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
