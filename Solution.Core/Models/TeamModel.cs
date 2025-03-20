namespace Solution.Core.Models;

public partial class TeamModel
{
    public string Id { get; set; }

    public ValidatableObject<string> Name { get; set; }

    public ValidatableObject<uint?> Points { get; set; }

    public TeamModel()
    {
        Name = new ValidatableObject<string>();
        Points = new ValidatableObject<uint?>();

        AddValidators();
    }

    public TeamModel(TeamEntity entity) : this()
    {
        this.Id = entity.PublicId;
        this.Name.Value = entity.Name;
        this.Points.Value = entity.Points;
    }

    public TeamEntity ToEntity()
    {
        return new TeamEntity
        {
            PublicId = Id,
            Name = Name.Value,
            Points = Points.Value ?? 0
        };
    }

    public void ToEntity(TeamEntity entity)
    {
        entity.PublicId = Id;
        entity.Name = Name.Value;
        entity.Points = Points.Value ?? 0;
    }

    private void AddValidators()
    {
        this.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Name field is required!"
        });

        this.Points.Validations.AddRange(
        [
            new IsNotNullOrEmptyRule<uint?>
            {
                ValidationMessage = "Points field is required!"
            },
            new MinValueRule<uint?>(1)
            {
                ValidationMessage = "Points field must be greater than 0!"
            }
        ]);
    }
}
