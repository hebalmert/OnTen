using OnTen.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnTen.Web.Data.Entities
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public float Quantity { get; set; }

        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public decimal Value => (decimal)Quantity * Price;
    }
}
