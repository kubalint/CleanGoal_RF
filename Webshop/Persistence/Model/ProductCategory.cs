using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Model
{
    public class ProductCategory
    {
        private int categoryId;

        [Key]
        public int CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }

        private string categoryName;

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        [ForeignKey("CategoryID")]
        public virtual List<Product> Products { get; set; }
    }
}