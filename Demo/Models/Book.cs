using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Display(Name = "書名"), Required, StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "標題"), Required, StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Display(Name = "類型"), RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), Required, StringLength(30)]
        public string Genre { get; set; }

        [Display(Name = "發售日"), DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "價格"), Range(1, 100), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
