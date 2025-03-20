namespace Solution.Core.Models;

public partial class StreetModel
{
    public string Id { get; set; }

    public ValidatableObject<CityModel> City { get; set; }

    public ValidatableObject<string> Name { get; set; }

    public ValidatableObject<uint?> HouseNumber { get; set; }

    public StreetModel()
    {
        City = new ValidatableObject<CityModel>();
        Name = new ValidatableObject<string>();
        HouseNumber = new ValidatableObject<uint?>();

        AddValidators();
    }

    public StreetModel(StreetEntity entity) : this()
    {
        this.Id = entity.PublicId;
        this.City.Value = new CityModel(entity.City);
        this.Name.Value = entity.Name;
        this.HouseNumber.Value = entity.HouseNumber;
    }

    public StreetEntity ToEntity()
    {
        return new StreetEntity
        {
            PublicId = Id,
            CityId = City.Value.Id,
            Name = Name.Value,
            HouseNumber = HouseNumber.Value ?? 0
        };
    }

    public void ToEntity(StreetEntity entity)
    {
        entity.PublicId = Id;
        entity.CityId = City.Value.Id;
        entity.Name = Name.Value;
        entity.HouseNumber = HouseNumber.Value ?? 0;
    }

    private void AddValidators()
    {
        this.City.Validations.Add(new PickerValidationRule<CityModel>
        {
            ValidationMessage = "CityId must be selected!"
        });

        this.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Name field is required!"
        });

        this.HouseNumber.Validations.AddRange(
        [
            new IsNotNullOrEmptyRule<uint?>
            {
                ValidationMessage = "HouseNumber field is required!"
            },
            new MinValueRule<uint?>(1)
            {
                ValidationMessage = "HouseNumber field must be greater than 0!"
            }
        ]);
    }
}
