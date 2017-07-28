using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace LoginChat.Models
{
    public class UserContext:DbContext
    {
        public UserContext():base("ChatConcext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<UserContext>());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        //public DbSet<ViewModel> ViewModels { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Message>()
            .HasOptional<User>(s => s.From)
            .WithMany()
            .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<LoginChat.Models.ViewModel> ViewModels { get; set; }
    }
}