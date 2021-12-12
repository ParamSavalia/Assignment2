using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*  
 *  Student Name : Param Savalia (040963842), Kunjesh Aghera (040980391)
 *  Reference: : https://github.com/aarad-ac/EFCore
 *               https://github.com/ParamSavalia/Lab4 
 *  Version : 1.0
*/


namespace Lab4.Models.ViewModels
{
    public class StudentMembershipViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<CommunityMembershipViewModel> Memberships { get; set; }

    }
}
