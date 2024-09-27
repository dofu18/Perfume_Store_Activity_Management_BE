using Perfume_Store_Activity_Management_Backend.src.Domain.Perfume;

namespace Perfume_Store_Activity_Management_Backend.src.Domain.Category;

public class Category
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<PerfumeCategory> PerfumeCategories { get; set; }
}
