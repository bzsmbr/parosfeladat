namespace Solution.ValidationLibrary;

public interface IObjectValidator<TKey>
{
  TKey Id { get; set; }
}
