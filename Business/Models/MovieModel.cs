using DataAccess.Records.Bases;
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
    public class MovieModel : Record
    {
        #region Entity Properties
        [Required(ErrorMessage = "{0} is required!" ) ]
        [MinLength(2, ErrorMessage = "{0} must be minimum {1} characters!")]
        [MaxLength(100, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("Movie Name")]
        public string Name { get; set; }

        [DisplayName("Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [DisplayName("Box Office")]
        public decimal? BoxOffice {  get; set; }

        [DisplayName("Director")]
        public int? DirectorId { get; set; }

        [DisplayName("Length")]
        public int Length {  get; set; }

        [DisplayName("Country")]
        public string Country {get; set; }

        [DisplayName("Imdb Rate")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} must be positive!")]
        public decimal ImdbRate { get; set; }
        #endregion

    }
}
