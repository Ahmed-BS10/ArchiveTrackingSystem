using System.Text.Json.Serialization;

namespace ArchiveTrackingSystem.Core.Entities
{
    public class Archive
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        // Navigation Property
        [JsonIgnore]
        public ICollection<File> Files { get; set; }
    }


}
