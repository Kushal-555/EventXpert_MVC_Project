using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventXpert.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Match")]
        public string Match { get; set; } // e.g. "USF Bulls vs UCF Knights"

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; } // e.g. 2025-10-25

        [Required]
        [Display(Name = "Venue")]
        public string Venue { get; set; } // e.g. "Raymond James Stadium"

        [Required]
        [Range(0, 1000)]
        [Display(Name = "Price ($)")]
        public decimal Price { get; set; } // e.g. 80

        // Convenience property for charts/grouping
        [NotMapped]
        public string Team
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Match))
                    return string.Empty;

                // take everything before " vs "
                return Match.Contains(" vs ")
                    ? Match.Split(" vs ")[0]
                    : Match;
            }
        }
    }
}
