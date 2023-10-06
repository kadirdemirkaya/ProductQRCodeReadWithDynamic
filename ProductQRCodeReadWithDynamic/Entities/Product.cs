using Dapper.Contrib.Extensions;
using ProductQRCodeReadWithDynamic.Entities.Base;

namespace ProductQRCodeReadWithDynamic.Entities
{
    [Table("Products")]
    public class Product : EntityBase
    {
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public double ProductPrice { get; set; }
        public string ProductCode { get; set; } = Guid.NewGuid().ToString();
        public int ReadCount { get; set; } = 0;

        public Guid? UpdatedCode { get; set; }

        public string? ImageCodePath { get; set; }
    }
}
