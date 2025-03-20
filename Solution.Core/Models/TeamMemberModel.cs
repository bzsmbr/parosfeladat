namespace Solution.Core.Models;

public partial class TeamMemberModel
{
    public string Id { get; set; }

    public ValidatableObject<string> Name { get; set; }

    public TeamMemberModel()
    {
        Name = new ValidatableObject<string>();

        AddValidators();
    }

    public TeamMemberModel(TeamMemberEntity entity) : this()
    {
        this.Id = entity.PublicId;
        this.Name.Value = entity.Name;
    }

    public TeamMemberEntity ToEntity()
    {
        return new TeamMemberEntity
        {
            PublicId = Id,
            Name = Name.Value,
        };
    }

    public void ToEntity(TeamMemberEntity entity)
    {
        entity.PublicId = Id;
        entity.Name = Name.Value;
    }

    private void AddValidators()
    {
        this.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Name field is required!"
        });
    }
}
