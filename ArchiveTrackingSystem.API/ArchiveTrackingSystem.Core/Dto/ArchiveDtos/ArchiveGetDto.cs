using System.Text.Json.Serialization;

namespace ArchiveTrackingSystem.Core.Dto.ArchiveDtos
{
    public class ArchiveGetDto
    {
     
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }      
        public ICollection<string> Files { get; set; }
    }

}
