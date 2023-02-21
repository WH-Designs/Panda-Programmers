using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemindersTest
{
    public class ModelValidator
    {
        public List<ValidationResult> Validations { get; private set; }
        public bool Valid { get; private set; } = false;

        public ModelValidator(object model)
        {
            Validations = new List<ValidationResult>();
            ValidationContext vctx = new ValidationContext(model);
            Valid = Validator.TryValidateObject(model, vctx, Validations, true);
        }

        public bool ContainsFailureFor(string member)
        {
            return Validations.Any(vr => vr.MemberNames.Contains(member));
        }

        public string GetAllErrors() =>
            Validations.Aggregate("", (accumulator, validation) => accumulator + $",{validation.ErrorMessage}");
    }
}
