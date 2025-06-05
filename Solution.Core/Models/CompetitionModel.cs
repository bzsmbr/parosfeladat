using System.ComponentModel;

namespace Solution.Core.Models;

public partial class CompetitionModel
{
    public string Id { get; set; }

    public ValidatableObject<StreetModel> Street { get; set; }

    public ValidatableObject<string> Name { get; set; }

    public ValidatableObject<DateTime> Date { get; set; }

    private bool _isExpanded;
    public bool IsExpanded
    {
        get => _isExpanded;
        set
        {
            if (_isExpanded != value)
            {
                _isExpanded = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsExpanded)));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public CompetitionModel()
    {
        Street = new ValidatableObject<StreetModel>();
        Name = new ValidatableObject<string>();
        Date = new ValidatableObject<DateTime>();

        AddValidators();
    }

    public CompetitionModel(CompetitionEntity entity) : this()
    {
        try
        {
            this.Id = entity.PublicId;
            
            if (entity.Street != null)
            {
                this.Street.Value = new StreetModel(entity.Street);
            }
            
            this.Name.Value = entity.Name ?? string.Empty;
            this.Date.Value = entity.Date;
        }
        catch (System.Exception ex)
        {
            if (this.Name.Value == null) this.Name.Value = string.Empty;
            if (this.Date.Value == default) this.Date.Value = DateTime.Now;
        }
    }

    public CompetitionEntity ToEntity()
    {
        return new CompetitionEntity
        {
            PublicId = Id,
            StreetId = Street?.Value?.Id ?? 0,
            Name = Name?.Value ?? string.Empty,
            Date = Date?.Value ?? DateTime.Now
        };
    }

    public void ToEntity(CompetitionEntity entity)
    {
        entity.PublicId = Id;
        entity.StreetId = Street?.Value?.Id ?? entity.StreetId;
        entity.Name = Name?.Value ?? entity.Name;
        entity.Date = Date?.Value ?? entity.Date;
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
