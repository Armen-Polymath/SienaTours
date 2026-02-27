using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SienaTours.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int DurationMinutes { get; set; }
        public decimal PricePerPerson { get; set; }
        public required string Language { get; set; }
        public required string StartLocation { get; set; }
        public required string EndLocation { get; set; }
        public required string MeetingPointDetails { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public required string Tags { get; set; }


    }
}
