﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Dtos {
    public class NewRenatlDto {
        public int CustomerId { get; set; }
        public IList<int> MovieIds { get; set; }
    }
}