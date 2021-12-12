using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

/*  
 *  Student Name : Param Savalia (040963842), Kunjesh Aghera (040980391)
 *  Reference: : https://github.com/aarad-ac/EFCore
 *               https://github.com/ParamSavalia/Lab4 
 *  Version : 1.0
*/


namespace Lab4.Models
{
    public class Community
    {

        public Community()
        {
            Advertisements = new HashSet<Advertisement>();
            memberships = new HashSet<CommunityMembership>();
        }


        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Id")]
        [Required]
        public string Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }


        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        public virtual ICollection<Advertisement> Advertisements { get; set; }

        public virtual ICollection<CommunityMembership> memberships { get; set; }

    }

}
