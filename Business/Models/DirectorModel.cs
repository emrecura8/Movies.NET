using DataAccess.Records.Bases;
using MySql.Data.MySqlClient;
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
    public class DirectorModel : Record
    {
        #region Entity Properties
        [DisplayName("Director Name")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(200, ErrorMessage = "{0} must be maximum {1} characters")]
        public string Name { get; set; }
        #endregion

        #region Extra Properties
        public string Movies { get; set; }
        #endregion

    }
}
