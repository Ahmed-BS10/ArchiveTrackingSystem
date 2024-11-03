using System.ComponentModel.DataAnnotations.Schema;

namespace ArchiveTrackingSystem.Core.Entities
{
    public class Addrees
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Dstrict { get; set; }
        public string ? Note {  get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public File file { get; set; }


    }




}
