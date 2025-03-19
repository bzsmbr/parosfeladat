namespace Solution.ValidationLibrary.ValidationRules;

public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; } = "Required field";

    public bool Check(object value)
    {
        var isTypeOfT = value is T;
        var isEmpty = string.IsNullOrWhiteSpace(value?.ToString());

        return isTypeOfT && !isEmpty;
    }
}
