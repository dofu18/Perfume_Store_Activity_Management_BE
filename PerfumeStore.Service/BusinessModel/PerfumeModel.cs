﻿using PerfumeStore.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.BusinessModel
{
    public class PerfumeModel
    {
        public Guid PerfumeId { get; set; }

        public int ViewCount { get; set; }

        public string Origin { get; set; } = null!;

        public int ReleaseYear { get; set; }

        public string Concentration { get; set; } = null!;

        public string Bartender { get; set; } = null!;

        public string FlavorGroup { get; set; } = null!;

        public int Capacity { get; set; }

        public decimal Price { get; set; }

        public int Discount { get; set; }

        public string TopNote { get; set; } = null!;

        public string MiddleNote { get; set; } = null!;

        public string BaseNote { get; set; } = null!;

        public string PerfumeEditions { get; set; } = null!;
    }
}
