namespace Solution.ValidationLibrary;

public class ValidatableObject<T> : ObservableObject
{
    private IEnumerable<string> errors;
    private bool isValid;
    private T value;

    public List<IValidationRule<T>> Validations { get; } = new();

    public IEnumerable<string> Errors
    {
        get => errors;
        private set => SetProperty(ref errors, value);
    }
    public bool IsValid
    {
        get => isValid;
        private set => SetProperty(ref isValid, value);
    }
    public T Value
    {
        get => value;
        set => SetProperty(ref this.value, value);
    }

    public ValidatableObject()
    {
        isValid = true;
        errors = [];

        if(typeof(T) == typeof(string))
        {
            value = default;
        }
        else if(typeof(T).IsValueType)
        {
            value = default;
        }
        else
        {
            value = Activator.CreateInstance<T>();
        }                    
    }
    public bool Validate()
    {
        Errors = Validations?.Where(x => !x.Check(Value))
                            ?.Select(x => x.ValidationMessage)
                            ?.ToArray() ?? [];

        IsValid = !Errors.Any();

        return IsValid;
    }
}
