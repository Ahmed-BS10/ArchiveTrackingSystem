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


      

    }
}
