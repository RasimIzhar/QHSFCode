using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Metadata
{
    public class Vt_UsersMetadata
    {
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
    }
}
