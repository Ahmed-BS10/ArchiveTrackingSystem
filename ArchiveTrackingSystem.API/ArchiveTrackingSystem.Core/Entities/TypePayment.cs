namespace ArchiveTrackingSystem.Core.Entities
{
    public class TypePayment
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string Note { get; set; }

        // Navigation Property 
        public Activte activties { get; set; }
        
    }


}
