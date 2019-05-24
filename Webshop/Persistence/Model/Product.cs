using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.Model
{
    public class Product
    {
        public Guid ID { get; set; }

        public int CategoryID { get; set; }

        public virtual ProductCategory Category { get; set; }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                this.UrlFriendlyName = ToUrlName(value);
            }
        }

        public string Description { get; set; }

        public double Price { get; set; }

        public string UrlFriendlyName { get; set; }

        public bool Available { get; set; }
        //[Required(AllowEmptyStrings = true)]
        public Guid? PhotoID { get; set; }

        [ForeignKey("PhotoID")]
        public virtual Photo Photo { get; set; }

        private string ToUrlName(string input)
        {
            StringBuilder sb = new StringBuilder();

            string inputLower = input.ToLower();

            for (int i = 0; i < input.Length; i++)
            {
                switch (inputLower[i])
                {
                    case 'á': sb.Append('a'); break;
                    case 'é': sb.Append('e'); break;
                    case 'í': sb.Append('i'); break;
                    case 'ö': sb.Append('o'); break;
                    case 'ő': sb.Append('o'); break;
                    case 'ü': sb.Append('u'); break;
                    case 'ű': sb.Append('u'); break;
                    case 'ú': sb.Append('u'); break;
                    case 'ó': sb.Append('o'); break;
                    case ' ': sb.Append('-'); break;

                    default: sb.Append(inputLower[i]); break;
                }
            }
            return sb.ToString();
        }
    }
}