using System.ComponentModel.DataAnnotations.Schema;

namespace ArchiveTrackingSystem.Core.Entities
{
    public class FileOutsideArchive
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string Status { get; set; }

        // ForeignKey
        public int EmployeID { get; set; }
        public int FileID { get; set; }

        // Navigation Property 
        [ForeignKey("EmployeID")]
        public Employe employe { get; set; }
        [ForeignKey("FileID")]
        public File file { get; set; }
    }


}
