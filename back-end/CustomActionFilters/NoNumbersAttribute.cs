using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace back_end.CustomActionFilters;

public class NoNumbersAttribute: ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string stringValue)
        {
            // Verifica se a string contém números
            bool hasNumber = stringValue.Any(char.IsDigit);

            if (hasNumber)
            {
                return new ValidationResult("A string não deve conter números.");
            }
        }

        return ValidationResult.Success;
    }
}