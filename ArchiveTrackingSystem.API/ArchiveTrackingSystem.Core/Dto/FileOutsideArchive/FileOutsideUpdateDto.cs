namespace ArchiveTrackingSystem.Core.Dto.FileOutsideArchive
{
    public class FileOutsideUpdateDto
    {
        public int Id { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }

        // ForeignKey
        public int EmployeID { get; set; }
        public int FileID { get; set; }
    }
}
