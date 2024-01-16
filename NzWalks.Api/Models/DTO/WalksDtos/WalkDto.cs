﻿using NzWalks.Api.Models.DTO.DiffecultiesDtos;
using NzWalks.Api.Models.DTO.RegionsDtos;

namespace NzWalks.Api.Models.DTO.WalksDtos
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LenghtInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        //Navigation proprity
        public DifficultyDto Difficulty { get; set; }
        public RegionDto Region { get; set; }

    }
}
