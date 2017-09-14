using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels {
    public class CustomerViewModel {
        public IEnumerable<MemberShipTypes> memberShipTypesList = new List<MemberShipTypes>();
        public Customer Customer { get; set; }
    }
}