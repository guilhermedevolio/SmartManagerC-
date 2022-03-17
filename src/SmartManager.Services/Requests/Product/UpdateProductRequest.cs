using System.ComponentModel.DataAnnotations;

namespace SmartManager.Services.Requests
{
    public class UpdateProductRequest
    {   
        public long Id {get; set;}

        [MaxLength(100)]
        [Required]
        public String Name { get; set; }

        [MaxLength(255)]
        [Required]
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