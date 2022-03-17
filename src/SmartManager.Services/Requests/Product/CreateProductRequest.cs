using System.ComponentModel.DataAnnotations;

namespace SmartManager.Services.Requests
{
    public class CreateProductRequest
    {   
        [Required]
        [MaxLength(100)]
        public String Name { get; set; }

        [Required]
        [MaxLength(255)]

        public String Description { get; set; }

        [Required]
        public int UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Status { get; set; }
        
        [Required]
        public long CategoryId { get; set; }
    }
}