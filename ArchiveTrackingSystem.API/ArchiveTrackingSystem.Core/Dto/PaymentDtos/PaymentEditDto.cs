namespace ArchiveTrackingSystem.Core.Dto.PaymentDtos
{
    public class PaymentEditDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime UpdateAt { get; set; }
        public string? Note { get; set; }


    }
}
