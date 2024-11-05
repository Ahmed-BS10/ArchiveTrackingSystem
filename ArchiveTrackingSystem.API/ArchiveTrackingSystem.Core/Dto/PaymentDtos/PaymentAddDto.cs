namespace ArchiveTrackingSystem.Core.Dto.PaymentDtos
{
    public class PaymentAddDto
    {

        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string Note { get; set; }


    }
}
