namespace ArchiveTrackingSystem.Core.Dto.ActiveDtos
{
    public class ActiveUpdateDto
    {
        public string Slug { get; set; }
        public string NumberActive { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Note { get; set; }
        public int? PaymentID { get; set; }

    }
}
