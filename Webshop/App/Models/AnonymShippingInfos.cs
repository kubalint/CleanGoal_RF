using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Persistence.Model;

namespace App.Models
{
    public class AnonymShippingInfos
    {
        
        public string OrderID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
        
        public DateTime DateOfOrder { get; set; }

        public bool SaveShipping { get; set; }
        
    }
}