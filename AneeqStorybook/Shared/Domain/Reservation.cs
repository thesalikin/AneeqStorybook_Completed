using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AneeqStorybook.Shared.Domain
{
    public class Reservation : BaseDomainModel, IValidatableObject
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOut { get; set; }
        public DateTime? DateIn { get; set; }
        [Required]
        public int? BookId { get; set; }
        public virtual Book Book { get; set; }
        [Required]
        public int? PatronId { get; set; }
        public virtual Patron Patron { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //throw new NotImplementedException();

            if (DateIn != null)
            {
                if (DateIn <= DateOut)
                {
                    yield return new ValidationResult("DateIn must be greater than DateOut", new[] { "DateIn" });
                }
            }

        }
    }
}