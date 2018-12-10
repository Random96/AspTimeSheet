using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspTimeSheet.Models;

namespace AspTimeSheet.Data
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        /// <summary>
        /// Конструктор инициализации базы данных из параметров конфигурации
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Работники
        /// </summary>
        public DbSet<Person> Person { get; set; }
        /// <summary>
        /// Штатное расписание
        /// </summary>
        public DbSet<Staffing> Staffing { get; set; }
        /// <summary>
        /// Список должностей
        /// </summary>
        public DbSet<StaffPosition> StaffPosition { get; set; }
        /// <summary>
        /// Отсутствия на рабочем месте
        /// </summary>
        public DbSet<Hooky> Hooky { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Build(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Fluent-API описание контекста
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void Build(ModelBuilder modelBuilder)
        {
            #region Person
            modelBuilder.Entity<Person>().ToTable(nameof(Person));
            modelBuilder.Entity<Person>().HasMany(s => s.Stuffing).
                WithOne(p => p.Person).HasForeignKey(p => p.PersonId);
            modelBuilder.Entity<Person>().HasMany(s => s.Hookeys).
                WithOne(p => p.Person).HasForeignKey(p => p.PersonId);
            modelBuilder.Entity<Person>().Property(x => x.Name).HasMaxLength(250);
            modelBuilder.Entity<Person>().Property(x => x.MiddleName).HasMaxLength(250);
            modelBuilder.Entity<Person>().Property(x => x.LastName).HasMaxLength(250).IsRequired();
            modelBuilder.Entity<Person>().Property(x => x.PersonnelNumber).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Person>().HasIndex(x => x.PersonnelNumber).IsUnique();
            #endregion

            #region StuffPosition
            modelBuilder.Entity<StaffPosition>().ToTable(nameof(StaffPosition));
            modelBuilder.Entity<StaffPosition>().HasMany(s => s.Staffing).
                WithOne(p => p.Position).HasForeignKey(p => p.PositionId);
            modelBuilder.Entity<StaffPosition>().HasMany(s => s.Hooky).
                WithOne(p => p.Position).HasForeignKey(p => p.PositionId);
            #endregion

            modelBuilder.Entity<Hooky>().ToTable(nameof(Hooky));
            modelBuilder.Entity<Hooky>().Property(x => x.Comment).HasMaxLength(1024).IsRequired();

            modelBuilder.Entity<Staffing>().ToTable(nameof(Staffing));
        }
    }
}
