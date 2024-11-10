namespace ArchiveTrackingSystem.Core.Dto.FileOutsideArchive
{
    public class FileOutsideGetDto
    {

        public string Employe { get; set; }
        public string File { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }

    }
}
