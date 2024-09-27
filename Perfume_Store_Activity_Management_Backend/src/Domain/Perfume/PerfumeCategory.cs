namespace Perfume_Store_Activity_Management_Backend.src.Domain.Perfume;

using Perfume_Store_Activity_Management_Backend.src.Domain.Category;

public class PerfumeCategory
{
    public Guid PerfumeId { get; set; }
    public Guid CategoryId { get; set; }

    public PerfumeEdition PerfumeEdition { get; set; }
    public Category Category { get; set; }
}
