namespace ArchiveTrackingSystem.Core.Dto.FileDtos
{
    public class FileGetWithIncludeDto
    {

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
        public string Archive { get; set; }
        public string City { get; set; }
        public string Dstrict { get; set; }
        public string Active { get; set; }
        public string Payment { get; set; }

        // Navigation properties

    }
}
