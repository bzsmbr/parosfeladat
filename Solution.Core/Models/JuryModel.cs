namespace Solution.Core.Models;

public partial class JuryModel
{
    public string Id { get; set; }

    public ValidatableObject<string> Name { get; set; }

    public ValidatableObject<string> PhoneNumber { get; set; }

    public ValidatableObject<string> Email { get; set; }
    
    public List<CompetitionModel> AssignedCompetitions { get; set; }

    public JuryModel()
    {
        Name = new ValidatableObject<string>();
        PhoneNumber = new ValidatableObject<string>();
        Email = new ValidatableObject<string>();
        AssignedCompetitions = new List<CompetitionModel>();

        AddValidators();
    }

    public JuryModel(JuryEntity entity) : this()
    {
        this.Id = entity.PublicId;
        this.Name.Value = entity.Name;
        this.PhoneNumber.Value = entity.PhoneNumber;
        this.Email.Value = entity.Email;
        
        if (entity.Competitions != null)
        {
            this.AssignedCompetitions = entity.Competitions.Select(c => new CompetitionModel(c)).ToList();
        }
    }

    public JuryEntity ToEntity()
    {
        return new JuryEntity
        {
            PublicId = Id,
            Name = Name.Value,
            PhoneNumber = PhoneNumber.Value,
            Email = Email.Value
        };
    }

    public void ToEntity(JuryEntity entity)
    {
        entity.PublicId = Id;
        entity.Name = Name.Value;
        entity.PhoneNumber = PhoneNumber.Value;
        entity.Email = Email.Value;
    }

    private void AddValidators()
    {
        this.Name.Validations.Add(new IsNotNullOrEmptyRule<string>
        {
            ValidationMessage = "Name field is required!"
        });

        this.PhoneNumber.Validations.AddRange(new List<IValidationRule<string>>
        {
            new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "PhoneNumber field is required!"
            },
            new PhoneNumberRule<string>
            {
                ValidationMessage = "Incorrect format!"
            }
        });

        this.Email.Validations.AddRange(new List<IValidationRule<string>>
        {
            new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = "Email field is required!"
            },
            new EmailRule<string>
            {
                ValidationMessage = "Incorrect format!"
            }
        });
    }
}
