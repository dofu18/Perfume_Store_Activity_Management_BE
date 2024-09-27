namespace Perfume_Store_Activity_Management_Backend.src.Domain.Perfume;

public class PerfumeEdition
{
    public Guid PerfumeEditionId { get; set; }
    public Guid PerfumeId { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public string ScentIntensity { get; set; }
    public string Availability { get; set; }
    public string Gender { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }

    public Perfume Perfume { get; set; }
    public List<PerfumeCategory> PerfumeCategories { get; set; }
}
