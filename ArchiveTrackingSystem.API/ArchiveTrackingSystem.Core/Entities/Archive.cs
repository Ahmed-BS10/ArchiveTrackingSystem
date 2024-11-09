namespace ArchiveTrackingSystem.Core.Entities
{
    public class Archive
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        // Navigation Property
        public ICollection<File> Files { get; set; }
    }


}
