using ArchiveTrackingSystem.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.EF.Data
{
    public class ArchiveTrackingDbContext : IdentityDbContext<User, Role, int>
    {
        public ArchiveTrackingDbContext(DbContextOptions<ArchiveTrackingDbContext> options) : base(options) { }

        public DbSet<Archive> Archives { get; set; }
        public DbSet<Core.Entities.File> Files { get; set; }
        public DbSet<Addrees> Addreess { get; set; }
        public DbSet<Active> Activte { get; set; }
        public DbSet<Payment> TypePayments { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<FileOutsideArchive> FileOutsideArchives { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



            builder.Entity<Employe>()
            .HasIndex(e => e.Slug)
            .IsUnique();


            builder.Entity<Active>()
            .HasIndex(e => e.Slug)
            .IsUnique();


            builder.Entity<Payment>()
           .HasIndex(e => e.Slug)
           .IsUnique();

            builder.Entity<Employe>()
                .HasMany(em => em.fileOutsideArchives)
                .WithOne(fa => fa.employe)
                .HasForeignKey(fa => fa.EmployeID);

            builder.Entity<Payment>()
             .HasMany(p => p.actives)
             .WithOne(ac => ac.typePayment)
             .HasForeignKey(ac => ac.PaymentID);



            builder.Entity<Core.Entities.File>()
                .HasOne(fi => fi.activte)
                .WithMany(ac => ac.files)
                .HasForeignKey(fi => fi.ActiveID);


            builder.Entity<Core.Entities.File>()
               .HasOne(fi => fi.addrees)
               .WithMany(ad => ad.files)
               .HasForeignKey(fi => fi.AddressID);


            builder.Entity<Core.Entities.File>()
               .HasOne(fi => fi.typePayment)
               .WithMany(py => py.files)
               .HasForeignKey(fi => fi.PaymentID);


            builder.Entity<Core.Entities.File>()
              .HasMany(fi => fi.fileOutsideArchives)
              .WithOne(fa => fa.file)
              .HasForeignKey(fa => fa.FileID);
        }

    }
}
