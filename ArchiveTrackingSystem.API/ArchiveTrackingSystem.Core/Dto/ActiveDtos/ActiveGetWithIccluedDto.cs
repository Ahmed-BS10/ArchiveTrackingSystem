namespace ArchiveTrackingSystem.Core.Dto.ActiveDtos
{
    public class ActiveGetWithIccluedDto
    {
        public string NumberActive { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Note { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? TypePaymnet { get; set; }

    }
}
