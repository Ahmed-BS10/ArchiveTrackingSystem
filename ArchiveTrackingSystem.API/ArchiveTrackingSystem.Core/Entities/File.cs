using System.ComponentModel.DataAnnotations.Schema;

namespace ArchiveTrackingSystem.Core.Entities
{
    public class File
    {
        public int Id { get; set; }
        public string FileNumber { get; set; }
        public string TaxpayerName { get; set; }
        public string Slug { get; set; }
        public string TradeName { get; set; }
        public string TaxNumber { get; set; }
        public string ExclusiveNymber { get; set; }
        public int DocumentCount { get; set; }
        public string EmployeeName { get; set; }
        public string FileStatus { get; set; }
        public string Notes { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

        // ForeignKey properties
        public int ArchiveID { get; set; }
        public int AddressID { get; set; }
        public int ActiveID { get; set; }
        public int PaymentID { get; set; }

        // Navigation properties
        public Archive archive { get; set; }
        public Addrees addrees { get; set; }
        public Active activte { get; set; }
        public Payment typePayment { get; set; }

        public ICollection<FileOutsideArchive> fileOutsideArchives { get; set; }
    }



}
