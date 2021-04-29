using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FYPManagment.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<WorkProgress> WorkProgress { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<GroupForm> GroupForms { get; set; }
        public DbSet<SubmitForm> submitForms { get; set; }
        public DbSet<MeetingAndPresentationDateTime> MeetingAndPresentationDateTimes { get; set; }

        public DbSet<GroupChat> GroupChats { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupMember>()
                .HasRequired(gm => gm.Group)
                .WithMany(g => g.GroupMembers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserNotification>()
                .HasRequired(un => un.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                  .HasRequired(d => d.Notification)
                  .WithMany()
                  .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkProgress>()
                .HasRequired(wp => wp.Group)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GroupForm>()
                .HasRequired(gf => gf.Form)
                .WithMany()
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<MeetingAndPresentationDateTime>()
            //    .Property(m => m.MeetingDateTime)
            //    .HasColumnType("datetime2");

            //modelBuilder.Entity<MeetingAndPresentationDateTime>()
            //    .Property(p => p.PresentationDateTime)
            //    .HasColumnType("datetime2");

            base.OnModelCreating(modelBuilder);
        }
    }
}