namespace Solution.ValidationLibrary.ValidationRules;

public class PhoneNumberRule<T> : IValidationRule<T>
{
    private readonly Regex _regex = new Regex("^\\+?[1-9][0-9]{7,14}$");

    public string ValidationMessage { get; set; }

    public bool Check(object value) => value is string str && _regex.IsMatch(str);
}
