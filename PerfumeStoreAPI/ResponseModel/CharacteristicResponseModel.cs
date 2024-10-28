namespace PerfumeStore.API.ResponseModel
{
    public class CharacteristicResponseModel
    {
        public Guid CharacteristicId { get; set; }

        public Guid PerfumeId { get; set; }

        public string? AttributeName { get; set; }

        public string? AttributeValue { get; set; }

        public DateTime? LastUpdated { get; set; }
    }
}
