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
    public class CommunityMembershipViewModel
    {
        public string CommunityId { get; set; }
        public string Title { get; set; }

        public bool IsMember { get; set; }

    }
}
