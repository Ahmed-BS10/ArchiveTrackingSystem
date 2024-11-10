namespace ArchiveTrackingSystem.Core.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Slug { get; set; } 
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string Note { get; set; }

        // Navigation Property 
        public ICollection<Active>? actives { get; set; }
        
        public ICollection<File>? files { get; set; }
       // public File? file { get; set; }


    }


}
