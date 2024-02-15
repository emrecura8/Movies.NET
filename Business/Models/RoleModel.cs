using System;
using System.Collections.Generic;
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
        public string Name { get; set; }
        #endregion
    }
}
