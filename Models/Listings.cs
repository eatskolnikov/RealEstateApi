using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateListingApi.Models
{
    public class Listing : BaseEntity
    {
       // public string Id { get; set; } = string.Empty;  // Default to empty string if nulls aren't allowed

        [StringLength(maximumLength:100, MinimumLength =5)]
        [DataType(DataType.Text, ErrorMessage ="Title must be text")]
        public string Title { get; set; } = string.Empty;

        [Range(0, 500000)]
        [DataType(DataType.Currency, ErrorMessage ="Price must by a number")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }  // Decimal is a value type and non-nullable by default
     
        [StringLength(maximumLength:8000)]
        [DataType(DataType.Text, ErrorMessage ="Description must be text")]
        public string? Description { get; set; }  // Mark as nullable if appropriate
    }
}