namespace Solution.Core.Models;

public partial class CompetitionModel
{
    public string Id { get; set; }

    public ValidatableObject<StreetModel> Street { get; set; }

    public ValidatableObject<string> Name { get; set; }

    public ValidatableObject<DateTime> Date { get; set; }


    public CompetitionModel()
    {
        Street = new ValidatableObject<StreetModel>();
        Name = new ValidatableObject<string>();
        Date = new ValidatableObject<DateTime>();

        AddValidators();
    }

    public CompetitionModel(CompetitionEntity entity): this()
    {
        this.Id = entity.PublicId;
        this.Street.Value = new StreetModel(entity.Street);
        this.Name.Value = entity.Name;
        this.Date.Value = entity.Date;
    }

    public CompetitionEntity ToEntity()
    {
        return new CompetitionEntity
        {
            PublicId = Id,
            StreetId = Street.Value.Id,
            Name = Name.Value,
            Date = Date.Value
        };
    }

    public void ToEntity(CompetitionEntity entity)
    {
        entity.PublicId = Id;
        entity.StreetId = Street.Value.Id;
        entity.Name = Name.Value;
        entity.Date = Date.Value;
    }

    private void AddValidators()
    {

        this.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Name field is required!"
        });

        this.Date.Validations.AddRange(
        [
            new IsNotNullOrEmptyRule<DateTime>
            {
                ValidationMessage = "Date field is required!"
            },
            new MaxDateRule<DateTime>
            {
                ValidationMessage = "Date must not be in the future!"
            }
        ]);
    }
}
    