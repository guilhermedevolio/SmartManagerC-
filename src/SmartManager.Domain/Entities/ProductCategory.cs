using SmartManager.Domain.Entities;

namespace SmartManager.Entities
{
    public class ProductCategory : Base
    {
        public String Name { get; set; }

        public int Status { get; set; }

        public ProductCategory() {}

        public ProductCategory(string name, int status)
        {
            Name = name;
            Status = status;
        }

        public override bool Validate()
        {
            return true;
        }
    }
}