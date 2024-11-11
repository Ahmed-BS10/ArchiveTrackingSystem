using ArchiveTrackingSystem.Core.Dto.AddressDtos;

namespace ArchiveTrackingSystem.Core.Dto.FileDtos
{
    public class FileUpdateDto
    {
      
        public string Name { get; set; }
        public string Slug { get; set; }
        public string CommercialNumber { get; set; }
        public string TaxNumber { get; set; }
        public int DocumentCount { get; set; }
        public string EmployeeName { get; set; }
        public string FileStatus { get; set; }
        public string Notes { get; set; }

        // ForeignKey properties
        public int ArchiveID { get; set; }
        public int ActiveID { get; set; }
        public int PaymentID { get; set; }

        // Navigation properties

        public AddressUpdateDto address { get; set; }

    }
}
