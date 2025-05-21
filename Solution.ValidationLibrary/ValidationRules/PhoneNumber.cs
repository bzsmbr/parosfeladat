namespace Solution.ValidationLibrary.ValidationRules
{
    public class PhoneNumberRule<T> : IValidationRule<T>
    {
        private readonly Regex _regex = new Regex(@"^(?:\+36|06)[ -]?\d{1,2}[ -]?\d{3}[ -]?\d{3,4}$");

        public string ValidationMessage { get; set; } = "Invalid phone number format.";

        public bool Check(object value) => value is string str && _regex.IsMatch(str);
    }
}
