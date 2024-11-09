using System.ComponentModel.DataAnnotations.Schema;

namespace ArchiveTrackingSystem.Core.Entities
{
    public class Active
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string NumberActive { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Note { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        //ForeignKey

        public int? PaymentID { get; set; }
        // Navigation Property
        [ForeignKey("PaymentID")]
        public Payment? typePayment { get; set; }
     
        public ICollection<File> files {  get; set; }       
    }


}
