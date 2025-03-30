namespace VerticalSliceArchitecture.API.Models
{
    public record Staff
    {
        public int Rating { get; init; }

        public byte[]? Picture { get; init; }

        public DateTime LastLogin { get; init; }

        public required string Username { get; init; }
    }
}
