using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos {
    public class CustomerDto {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        //[Min18ToEnter]
        public DateTime? Birthday { get; set; }

        public bool IsSubscribedToNewLetter { get; set; }

        public byte MemberShipTypesId { get; set; }
    }
}