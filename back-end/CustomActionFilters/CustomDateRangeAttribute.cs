using System.ComponentModel.DataAnnotations;

namespace back_end.Filters;

public class CustomDateRangeAttribute : ValidationAttribute
{
    public int MinYear { get; set; }
    public int MaxYear { get; set; }

    public override bool IsValid(object value)
    {
        if (value is DateTime date)
        {
            return date.Year >= MinYear && date.Year <= MaxYear;
        }

        return false;
    }
}