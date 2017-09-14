using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models {
    public class Min18ToEnter : ValidationAttribute{
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MemberShipTypesId == 0 || customer.MemberShipTypesId == 1)
                return ValidationResult.Success;

            if (customer.Birthday == null)
                return new ValidationResult("Birhdate is required");

            var age = DateTime.Now.Year - customer.Birthday.Value.Year;

            return (age > 18)
                    ? ValidationResult.Success
                    : new ValidationResult("Customer should be at least eighteen years old to go on a membership.");

        }
    }
}