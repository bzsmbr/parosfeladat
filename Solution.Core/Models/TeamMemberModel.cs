namespace Solution.Core.Models;

public partial class TeamMemberModel
{
    public string Id { get; set; }

    public ValidatableObject<string> Name { get; set; }

    public string ImageId { get; set; }

    public string WebContentLink { get; set; }


    public TeamMemberModel()
    {
        Name = new ValidatableObject<string>();

        AddValidators();
    }

    public TeamMemberModel(TeamMemberEntity entity) : this()
    {
        this.Id = entity.PublicId;
        this.Name.Value = entity.Name;

        this.ImageId = entity.ImageId;
        this.WebContentLink = entity.WebContentLink;


    }

    public TeamMemberEntity ToEntity()
    {
        return new TeamMemberEntity
        {
            PublicId = Id,
            Name = Name.Value,

            ImageId = ImageId,
            WebContentLink = WebContentLink,
        };
    }

    public void ToEntity(TeamMemberEntity entity)
    {
        entity.PublicId = Id;
        entity.Name = Name.Value;

        entity.ImageId = ImageId;
        entity.WebContentLink = WebContentLink;
    }

    private void AddValidators()
    {
        this.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Name field is required!"
        });
    }
}
