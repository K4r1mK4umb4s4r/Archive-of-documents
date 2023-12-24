using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ArchiveOfDocuments.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static ArchiveOfDocuments.DataAccess.Entities.UserEntity;


namespace ArchiveOfDocuments.DataAccess
{
    public class ArchiveOfDocumentsDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<DocumentEntity> Documents { get; set; }
        public DbSet<AccessLogEntity> AccessLogs { get; set; }
       

        public ArchiveOfDocumentsDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //default identity server tables
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("user_claims");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("user_logins").HasNoKey();
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("user_tokens").HasNoKey(); ;
            modelBuilder.Entity<UserRoleEntity>().ToTable("user_roles");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("user_role_claims");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("user_role_owners").HasNoKey();

            modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<UserEntity>().HasIndex(x => x.ExternalId).IsUnique();


            modelBuilder.Entity<DocumentEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<DocumentEntity>().HasIndex(x => x.ExternalId).IsUnique();

            modelBuilder.Entity<DocumentEntity>().HasOne(x => x.User)
                                                    .WithMany(x => x.Documents)
                                                    .HasForeignKey(x => x.IDUser);


            modelBuilder.Entity<AccessLogEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<AccessLogEntity>().HasIndex(x => x.ExternalId).IsUnique();
            modelBuilder.Entity<AccessLogEntity>().HasOne(x => x.User)
                                                     .WithMany(x => x.AccessLogs)
                                                     .HasForeignKey(x => x.IDUser);
            modelBuilder.Entity<AccessLogEntity>().HasOne(x => x.Document)
                                                     .WithMany(x => x.AccessLogs)
                                                     .HasForeignKey(x => x.IDDocument);
        }
    }
}