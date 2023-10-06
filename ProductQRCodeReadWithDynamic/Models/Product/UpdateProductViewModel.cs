﻿namespace ProductQRCodeReadWithDynamic.Models.Product
{
    public class UpdateProductViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public double ProductPrice { get; set; }
        public string ProductCode { get; set; } = Guid.NewGuid().ToString();
        public int ReadCount { get; set; } = 0;
        public string UpdatedCode { get; set; }

        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; }
        public bool IsSuccess { get; set; } = true;
    }
}
