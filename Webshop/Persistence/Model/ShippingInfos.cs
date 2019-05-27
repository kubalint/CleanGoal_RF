using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Model
{
    public class ShippingInfos
    {
        [Key]
        public Guid ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public override string ToString()
        {
            return this.FirstName +" "+ this.LastName +" "+ this.Address;
        }
    }
}
