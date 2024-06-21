using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebBanHang.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }      
        public int Quantity { get; set; }
        //bo sung moi ket hop 1-n
        //[ForeignKey("OrderId")]
        public Order Order { get; set; }
        //[ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
