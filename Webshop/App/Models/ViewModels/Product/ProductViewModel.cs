using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.ViewModels.Product
{
    public class ProductViewModel
    {
        public Guid ID { get; set; }

        public CategoryViewModel Category { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public bool Available { get; set; }

        public Guid? PhotoId { get; set; }

        public PhotoViewModel Photo { get; set; }

    }
}