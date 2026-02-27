using Microsoft.AspNetCore.Mvc.Rendering;
using SienaTours.Models;

namespace SienaTours.ViewModels
{
    public class TourFormViewModel
    {
        // Fields the user should edit
        public int? Id { get; set; } // null for Create, set for Edit

        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

        public int DurationMinutes { get; set; }
        public decimal PricePerPerson { get; set; }

        public string Language { get; set; } = "";
        public string StartLocation { get; set; } = "";
        public string EndLocation { get; set; } = "";
        public string MeetingPointDetails { get; set; } = "";

        public DifficultyLevel Difficulty { get; set; }

        public bool IsActive { get; set; }
        public string Tags { get; set; } = "";

        // Dropdown options for the enum
        public IEnumerable<SelectListItem> DifficultyOptions { get; set; }
            = Enumerable.Empty<SelectListItem>();
    }
}