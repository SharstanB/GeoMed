using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMed.Main.DTO.Patients
{
    public class ActionPatientDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Gender { get; set; }  // enum (Gender)

        public int UserType { get; set; }  // enum(UserTypes)

        [ValidateDateRange]
        public Nullable<DateTime> BirthDate { get; set; }
        public int BloodType { get; set; }

        public int AreaId { get; set; }
        public int CareerId { get; set; }

    }

    public class ValidateDateRange : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // your validation logic
            if ((DateTime)value >= Convert.ToDateTime(DateTime.MinValue) &&
                (DateTime)value <= Convert.ToDateTime(DateTime.Now))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("خطأ في تاريخ الميلاد");
            }
        }
    }
}
