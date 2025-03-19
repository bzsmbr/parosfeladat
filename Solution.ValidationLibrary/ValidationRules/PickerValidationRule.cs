namespace Solution.ValidationLibrary.ValidationRules;

public class PickerValidationRule<T> : IValidationRule<T> where T: IObjectValidator<uint>
{
    public string ValidationMessage { get; set; } = "Manufacturer must be selected";

    public bool Check(object value)
    {
        var isTypeOfT = value is T;
        var isNull = value is null;

        if(!isTypeOfT || isNull)
        {
            return false;
        }

        if(value is IObjectValidator<uint> objectValidator)
        {
            return objectValidator.Id != 0 &&
                   isTypeOfT &&
                   !isNull;
        }
      
        return false;
    }
}
