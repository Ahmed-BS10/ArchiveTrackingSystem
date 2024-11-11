using System.Text.Json.Serialization;

namespace ArchiveTrackingSystem.Core.Dto.AddressDtos
{
    public class AddressUpdateDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string City { get; set; }
        public string Dstrict { get; set; }
        public string? Note { get; set; }
    }
}
