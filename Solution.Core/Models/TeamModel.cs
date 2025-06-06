﻿namespace Solution.Core.Models;

public partial class TeamModel
{
    public string Id { get; set; }

    public ValidatableObject<string> Name { get; set; }

    public ValidatableObject<uint?> Points { get; set; }

    public string ImageId { get; set; }

    public string WebContentLink { get; set; }

    public List<CompetitionModel> AssignedCompetitions { get; set; } = new();

    public TeamModel()
    {
        Name = new ValidatableObject<string>();
        Points = new ValidatableObject<uint?>();
        AssignedCompetitions = new List<CompetitionModel>();

        AddValidators();
    }

    public TeamModel(TeamEntity entity) : this()
    {
        this.Id = entity.PublicId;
        this.Name.Value = entity.Name;
        this.Points.Value = entity.Points;

        this.ImageId = entity.ImageId;
        this.WebContentLink = entity.WebContentLink;

        if (entity.Competitions != null)
        {
            this.AssignedCompetitions = entity.Competitions.Select(c => new CompetitionModel(c)).ToList();
        }
    }

    public TeamEntity ToEntity()
    {
        return new TeamEntity
        {
            PublicId = Id,
            Name = Name.Value,
            Points = Points.Value ?? 0,
            ImageId = ImageId,
            WebContentLink = WebContentLink
        };
    }

    public void ToEntity(TeamEntity entity)
    {
        entity.PublicId = Id;
        entity.Name = Name.Value;
        entity.Points = Points.Value ?? 0;
        entity.ImageId = ImageId;
        entity.WebContentLink = WebContentLink;
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
