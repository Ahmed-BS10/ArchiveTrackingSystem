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
    public class ArchiveTrackingDbContext : IdentityDbContext<User , Role , int>
    {
        public ArchiveTrackingDbContext(DbContextOptions<ArchiveTrackingDbContext> options) : base(options) { }
        
        public DbSet<Archive> Archives { get; set; }
        public DbSet<Core.Entities.File> Files { get; set; }
        public DbSet<Addrees> Addreess { get; set; }
        public DbSet<Activte> Activte { get; set; }
        public DbSet<TypePayment> TypePayments { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<FileOutsideArchive> FileOutsideArchives { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

          

            builder.Entity<TypePayment>()
                .HasOne(tp => tp.activte)
                .WithOne(ac => ac.typePayment)
                .HasForeignKey<Activte>();



            builder.Entity<Employe>()
                .HasMany(em => em.fileOutsideArchives)
                .WithOne(fa => fa.employe)
                .HasForeignKey(fa => fa.EmployeID);

            builder.Entity<Core.Entities.File>()
                .HasOne(fi => fi.activte)
                .WithOne(ac => ac.file)
                .HasForeignKey<Core.Entities.File>();


            builder.Entity<Core.Entities.File>()
               .HasOne(fi => fi.addrees)
               .WithOne((ad => ad.file))
               .HasForeignKey<Core.Entities.File>();
            

            builder.Entity<Core.Entities.File>()
               .HasOne(fi => fi.typePayment)
               .WithOne((ty => ty.file))
               .HasForeignKey<Core.Entities.File>();


            builder.Entity<Core.Entities.File>()
              .HasMany(fi => fi.fileOutsideArchives)
              .WithOne(fa => fa.file)
              .HasForeignKey(fa => fa.FileID);
        }

    }
}
