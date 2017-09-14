using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models {
    public class Customer {
         
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Date Of Birh")]
        [Min18ToEnter]
        public DateTime? Birthday { get; set; }

        public bool IsSubscribedToNewLetter { get; set; }

        public MemberShipTypes MemberShipTypes { get; set; }

        public byte MemberShipTypesId { get; set; }
    }
}