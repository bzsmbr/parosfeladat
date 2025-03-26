namespace Solution.ValidationLibrary.ValidationRules;

public class MaxDateRule<T>() : IValidationRule<T>
{
    public string ValidationMessage { get; set; } = $"Date can't be larger then {DateTime.Now}.";

    public bool Check(object value)
    {
        if (!DateTime.TryParse(value?.ToString(), out DateTime data))
        {
            return false;
        }

        return data <= DateTime.Now;
    }
}
