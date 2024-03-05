using LetterApp.Common.IpFinder;
using LetterApp.Entity.BaseEntity;
using LetterApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterApp.Dal.ProjectContext
{
    public class LetterAppContext:DbContext
    {
        public DbSet<User> Users  { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Diary> Diaries { get; set; }
        public DbSet<DiaryNote> DiaryNotes { get; set; }

        public LetterAppContext(DbContextOptions<LetterAppContext> option) : base(option)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=AhmetBaris\\SQLEXPRESS;database=LetterAppDB;uid=sa;pwd=3420;TrustServerCertificate=True");
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(x => x.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(up => up.Id);

            modelBuilder.Entity<User>()
                .HasMany(x=>x.Diaries)
                .WithOne(d=>d.User)
                .HasForeignKey(d => d.UserId);

            modelBuilder.Entity<Diary>()
                .HasMany(x=>x.DiaryNotes)
                .WithOne(dn=>dn.Diary)
                .HasForeignKey(d=>d.DiaryId);

            base.OnModelCreating(modelBuilder);
        }



        public override int SaveChanges()
        {
            var modifierEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added);
            try
            {
                foreach (var item in modifierEntries)
                {
                    var entityRepository = item.Entity as BaseClass;
                    if (entityRepository != null)
                    {

                        if (item.State == EntityState.Added)
                        {


                            entityRepository.Created_Date = DateTime.Now;
                            entityRepository.Created_MachineName = Environment.MachineName;
                            entityRepository.Created_Ip = IpFinder.GetIpAddress();


                        }
                        if (item.State == EntityState.Modified)
                        {
                            Entry(entityRepository).Property(x => x.Created_Date).IsModified = false;
                            Entry(entityRepository).Property(x => x.Created_MachineName).IsModified = false;
                            Entry(entityRepository).Property(x => x.Created_Ip).IsModified = false;

                            entityRepository.Updated_Date = DateTime.Now;
                            entityRepository.Updated_MachineName = Environment.MachineName;
                            entityRepository.Updated_Ip = IpFinder.GetIpAddress();

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifierEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added);
            try
            {
                foreach (var item in modifierEntries)
                {
                    var entityRepository = item.Entity as BaseClass;
                    if (entityRepository != null)
                    {


                        if (item.State == EntityState.Modified)
                        {

                            Entry(entityRepository).Property(x => x.Created_Date).IsModified = false;
                            Entry(entityRepository).Property(x => x.Created_MachineName).IsModified = false;
                            Entry(entityRepository).Property(x => x.Created_Ip).IsModified = false;
                            entityRepository.Updated_Date = DateTime.Now;
                            entityRepository.Updated_MachineName = Environment.MachineName;
                            entityRepository.Updated_Ip = IpFinder.GetIpAddress();


                        }
                        if (item.State == EntityState.Added)
                        {
                            entityRepository.Created_Date = DateTime.Now;
                            entityRepository.Created_MachineName = Environment.MachineName;
                            entityRepository.Created_Ip = IpFinder.GetIpAddress();


                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return base.SaveChangesAsync();
        }
    }
}
