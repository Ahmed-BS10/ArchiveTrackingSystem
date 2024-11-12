using ArchiveTrackingSystem.Core.Dto.AddressDtos;

namespace ArchiveTrackingSystem.Core.Dto.FileDtos
{
    public class FileUpdateDto
    {
        public string Slug {  get; set; }
        public string FileNumber { get; set; }
        public string TaxpayerName { get; set; }
        public string TradeName { get; set; }
        public string TaxNumber { get; set; }
        public string ExclusiveNymber { get; set; }
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
