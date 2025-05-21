namespace Solution.ValidationLibrary.ValidationRules;

public class EmailRule<T> : IValidationRule<T>
{
    private readonly Regex _regex = new Regex("^\\S+@\\S+\\.\\S+$");

    public string ValidationMessage { get; set; }

    public bool Check(object value) => value is string str && _regex.IsMatch(str);
}
