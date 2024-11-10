using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ArchiveTrackingSystem.Core.Entities
{
    public class Addrees
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Dstrict { get; set; }
        public string ? Note {  get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        [JsonIgnore]
        public ICollection<File> files { get; set; }


    }

}
