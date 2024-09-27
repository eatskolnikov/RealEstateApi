using System.ComponentModel.DataAnnotations;

namespace RealEstateListingApi.Models
{
    public class Listing : BaseEntity
    {
       // public string Id { get; set; } = string.Empty;  // Default to empty string if nulls aren't allowed

        [StringLength(maximumLength:100, MinimumLength =5)]
        public string Title { get; set; } = string.Empty;

        [Range(0, 500000)]
        public decimal Price { get; set; }  // Decimal is a value type and non-nullable by default
     
        [StringLength(maximumLength:8000)]
        public string? Description { get; set; }  // Mark as nullable if appropriate
    }
}