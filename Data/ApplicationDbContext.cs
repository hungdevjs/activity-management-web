using Microsoft.EntityFrameworkCore;
using ActivityManagementWeb.Models;
using ActivityManagementWeb.Helpers;

namespace ActivityManagementWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentActivity> StudentActivities { get; set; }
        public DbSet<StudentPoint> StudentPoints { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherActivity> TeacherActivities { get; set; }
        public DbSet<Year> Years { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            RegisterCoreModels(builder);
        }

        public static void RegisterCoreModels(ModelBuilder builder)
        {
            builder.Entity<StudentActivity>()
                .HasKey(e => new { e.StudentId, e.ActivityId });

            builder.Entity<StudentPoint>()
                .HasKey(e => new { e.StudentId, e.SemesterId });

            builder.Entity<TeacherActivity>()
                .HasKey(e => new { e.TeacherId, e.ActivityId });

            builder.Entity<TeacherActivity>()
                .HasOne(e => e.Teacher)
                .WithMany(e => e.Activities)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<StudentActivity>()
                .HasOne(e => e.Student)
                .WithMany(e => e.Activities)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
