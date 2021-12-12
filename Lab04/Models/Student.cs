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
    public class Student
    {

        public Student()
        {
            memberships = new HashSet<CommunityMembership>();
        }

        public string Id { get; set; }

        [StringLength(50)]
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [StringLength(50)]
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        public String FullName {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        public virtual ICollection<CommunityMembership> memberships { get; set; }

    }
}
