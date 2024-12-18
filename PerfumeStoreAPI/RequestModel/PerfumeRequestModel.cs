﻿namespace PerfumeStore.API.RequestModel
{
    public class PerfumeRequestModel
    {
        public Guid PerfumeId { get; set; }

        public string Name { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string Scent { get; set; } = null!;

        public string? Gender { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public int ViewCount { get; set; }

        public string Origin { get; set; } = null!;

        public int ReleaseYear { get; set; }

        public int Volume { get; set; }

        public int Discount { get; set; }

        public string TopNote { get; set; } = null!;

        public string MiddleNote { get; set; } = null!;

        public string BaseNote { get; set; } = null!;

        public DateTime? DateAdded { get; set; }
    }

    public class UpdatePerfumeRequestModel
    {
        public string? Name { get; set; } = null!;

        public string? Brand { get; set; } = null!;

        public string? Scent { get; set; } = null!;

        public string? Gender { get; set; }

        public decimal? Price { get; set; }

        public int? StockQuantity { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public int? ViewCount { get; set; }

        public string? Origin { get; set; } = null!;

        public int? ReleaseYear { get; set; }

        public int? Volume { get; set; }

        public int? Discount { get; set; }

        public string? TopNote { get; set; } = null!;

        public string? MiddleNote { get; set; } = null!;

        public string? BaseNote { get; set; } = null!;

        public DateTime? DateAdded { get; set; }
    }
}
