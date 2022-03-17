using System.ComponentModel.DataAnnotations.Schema;
using SmartManager.Domain.Entities;

namespace SmartManager.Entities
{
    public class Product : Base
    {
        public String Name { get; set; }

        public String Description { get; set; }

        public int UnitPrice { get; set; }

        public int Quantity { get; set; }

        public int Status { get; set; }

        public virtual ProductCategory Category { get; set; }
        public long CategoryId { get; set; }

        public Product() {
                 _errors = new List<String>();
        }

        public Product(string name, string description, int unitPrice, int quantity, int status, ProductCategory category)
        {
            Name = name;
            Description = description;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Status = status;
            Category = category;
       
        }

        public override bool Validate()
        {
            return true;
        }
    }
}