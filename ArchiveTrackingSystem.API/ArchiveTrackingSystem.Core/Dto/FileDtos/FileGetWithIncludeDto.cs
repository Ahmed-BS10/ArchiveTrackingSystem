namespace ArchiveTrackingSystem.Core.Dto.FileDtos
{
    public class FileGetWithIncludeDto
    {

        public string Name { get; set; }
        public string CommercialNumber { get; set; }
        public string TaxNumber { get; set; }
        public DateTime StartDate { get; set; }
        public int DocumentCount { get; set; }
        public string EmployeeName { get; set; }
        public string FileStatus { get; set; }
        public string Notes { get; set; }

        // ForeignKey properties
        public string Archive { get; set; }
        public string Address { get; set; }
        public string Active { get; set; }
        public string Payment { get; set; }

        // Navigation properties

    }
}
