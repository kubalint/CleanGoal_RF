using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Model
{
    public class BasketEntry
    {
        [Key]
        [Column(Order = 1)]
        public string UserID { get; set; }
        [Key]
        [Column(Order = 2)]
        public string ProductID { get; set; }

        public int Quantity { get; set; }
    }
}