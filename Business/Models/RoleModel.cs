using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
#nullable disable

using DataAccess.Records.Bases;

namespace Business.Models
{
    public class RoleModel : Record
    {
        #region Entity Properties
        [DisplayName("Role Name")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage ="{0} must be minimum {2} maximum {1} characters")]
        public string Name { get; set; }
        #endregion

        #region Extra Properties
        public int UserCount { get; set; }
        public string Users { get; set; }
        [DisplayName("Names")]
        public string UserNamesOutput { get; set; }
        #endregion
    }
}
