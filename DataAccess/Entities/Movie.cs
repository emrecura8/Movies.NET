using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace DataAccess.Entities
{
    public class Movie
    {
        [Required]
        [StringLength(100)]
        public string MovieName { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public decimal? BoxOffice {  get; set; }
        public Director Director { get; set; }
        public int DirectorId { get; set; }
        public int Length {  get; set; }
        public string Country {get; set; }
        public string Language { get; set; }
        public decimal ImdbRate { get; set; }
        public decimal LetterboxdRate { get; set; }
        public decimal TomatoesRate { get; set; }
        public List<UserMovie> UserMovies { get; set; }


    }
}
