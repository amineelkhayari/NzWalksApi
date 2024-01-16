namespace NzWalks.Api.Models.DTO.WalksDtos
{
    public class UpdateWalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LenghtInKm { get; set; }
        public string? WalkImageUrl { get; set; }
    }
}
