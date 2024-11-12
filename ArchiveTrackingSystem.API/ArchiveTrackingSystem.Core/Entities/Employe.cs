using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ArchiveTrackingSystem.Core.Entities
{
    public class Employe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string job { get; set; }
        public string Deparatment { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        // Navigation Property 
        public ICollection<FileOutsideArchive> fileOutsideArchives { get; set; }

    }


}
