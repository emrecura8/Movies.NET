using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace Business.Models
{
    public class FavoriteModel
    {
        public int MovieId { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [DisplayName("MovieName")]
        public string MovieName { get; set; }
        
        [DisplayName("Release Date")]
        public DateTime? ReleaseDate { get; set; }
        [DisplayName("Length")]
        public int Length { get; set; }
        [DisplayName("Country")]
        public string Country { get; set; }
        [DisplayName("Imdb Rate")]
        public decimal ImdbRate { get; set; }
        
        public decimal? TotalBoxOffice { get; set; }
        [DisplayName("Total Box Office")]
        public string TotalBoxOfficeOutput { get; set; }
    }
}
