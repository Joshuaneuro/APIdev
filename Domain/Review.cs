using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Review
    {
        public int ReviewID { get; set; }

        public int ProductID { get; set; }

        [Required] 
        [Range(1, 5)]
        public int ReviewScore { get; set; }

        [MaxLength(500)]
        public string? ReviewText { get; set; }

        public virtual Product Product { get; set; } // Many-to-one relationship
    }
}
