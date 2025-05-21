using System.Text.RegularExpressions;

namespace Solution.ValidationLibrary.ValidationRules
{
    public class EmailRule<T> : IValidationRule<T>
    {
        private readonly Regex _regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

        public string ValidationMessage { get; set; }

        public bool Check(object value) => value is string str && _regex.IsMatch(str);
    }
}
