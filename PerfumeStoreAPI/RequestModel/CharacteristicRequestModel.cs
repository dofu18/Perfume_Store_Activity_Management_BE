namespace PerfumeStore.API.RequestModel
{
    public class CharacteristicRequestModel
    {
        public Guid CharacteristicId { get; set; }

        public Guid PerfumeId { get; set; }

        public string? AttributeName { get; set; }

        public string? AttributeValue { get; set; }
    }
}
