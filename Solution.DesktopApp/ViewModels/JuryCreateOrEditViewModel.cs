using System.Collections.ObjectModel;
using System.Diagnostics;
using Solution.ValidationLibrary;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class JuryCreateOrEditViewModel(AppDbContext dbContext,
    IJuryService juryService,
    ICompetitionService competitionService) : JuryModel, IQueryAttributable
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingkAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    #region validation commands
    public IRelayCommand NameValidationCommand => new RelayCommand(() => this.Name.Validate());

    public IRelayCommand PhoneNumberValidationCommand => new RelayCommand(() => this.PhoneNumber.Validate());

    public IRelayCommand EmailValidationCommand => new RelayCommand(() => this.Email.Validate());
    #endregion

    #region event commands
    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
    public IAsyncRelayCommand RefreshCompetitionsCommand => new AsyncRelayCommand(LoadAvailableCompetitionsAsync);
    #endregion

    private delegate Task ButtonActionDelagate();
    private ButtonActionDelagate asyncButtonAction = null;

    [ObservableProperty]
    private string title = "Add new Jury!";
    
    [ObservableProperty]
    private ObservableCollection<CompetitionModel> availableCompetitions = new();
    
    [ObservableProperty]
    private ObservableCollection<CompetitionModel> selectedCompetitions = new();
    
    [ObservableProperty]
    private bool isLoadingCompetitions;

    private async Task OnAppearingkAsync()
    { 
        if (AvailableCompetitions == null)
            AvailableCompetitions = new ObservableCollection<CompetitionModel>();
            
        if (SelectedCompetitions == null)
            SelectedCompetitions = new ObservableCollection<CompetitionModel>();
            
        await LoadAvailableCompetitionsAsync();
    }

    private async Task OnDisappearingAsync()
    {
    }
    
    private async Task LoadAvailableCompetitionsAsync()
    {
        IsLoadingCompetitions = true;
        
        try
        {
            var result = await juryService.GetAvailableCompetitionsAsync();
            
            if (result.IsError)
            {
                await Application.Current.MainPage.DisplayAlert("Error", result.FirstError.Description, "OK");
                AvailableCompetitions = new ObservableCollection<CompetitionModel>();
                return;
            }
            
            foreach (var comp in result.Value)
            {
                if (comp.Name?.Value == null)
                {
                    comp.Name = new ValidatableObject<string> { Value = string.Empty };
                }
                
                if (comp.Date?.Value == default)
                {
                    comp.Date = new ValidatableObject<DateTime> { Value = DateTime.Now };
                }
                
            }
            
            AvailableCompetitions = new ObservableCollection<CompetitionModel>(result.Value);
            
            if (!string.IsNullOrEmpty(Id) && AssignedCompetitions != null)
            {
                SelectedCompetitions = new ObservableCollection<CompetitionModel>(
                    AssignedCompetitions.Where(ac => AvailableCompetitions.Any(c => c.Id == ac.Id)).ToList());
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load competitions: {ex.Message}", "OK");
            AvailableCompetitions = new ObservableCollection<CompetitionModel>();
        }
        finally
        {
            IsLoadingCompetitions = false;
        }
    }

    private async Task OnSubmitAsync() 
    { 
        if (asyncButtonAction != null)
            await asyncButtonAction();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (AvailableCompetitions == null)
            AvailableCompetitions = new ObservableCollection<CompetitionModel>();
            
        if (SelectedCompetitions == null)
            SelectedCompetitions = new ObservableCollection<CompetitionModel>();
    
        bool hasValue = query.TryGetValue("Jury", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new Jury!";
            return;
        }

        JuryModel jury = result as JuryModel;

        this.Id = jury?.Id;
        this.Name.Value = jury?.Name?.Value;
        this.PhoneNumber.Value = jury?.PhoneNumber?.Value;
        this.Email.Value = jury?.Email?.Value;
        this.AssignedCompetitions = jury?.AssignedCompetitions ?? new List<CompetitionModel>();
        
        asyncButtonAction = OnUpdateAsync;
        Title = "Update Jury";
    }

    private async Task OnSaveAsync()
    {
        if (!IsFormValid())
        {
            return;
        }

        var result = await juryService.CreateAsync(this);
        
        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", result.FirstError.Description, "OK");
            return;
        }
        
        var competitionIds = SelectedCompetitions.Select(c => c.Id).ToList();
        var assignmentResult = await juryService.AssignJuryToCompetitionsAsync(result.Value.Id, competitionIds);
        
        if (assignmentResult.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Warning", 
                $"Jury was created but competitions could not be assigned: {assignmentResult.FirstError.Description}", "OK");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Success", "Jury saved and assigned to competitions.", "OK");
            ClearForm();
        }
    }

    private async Task OnUpdateAsync()
    {
        if (!IsFormValid())
        {
            return;
        }

        var result = await juryService.UpdateAsync(this);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", result.FirstError.Description, "OK");
            return;
        }
        
        var competitionIds = SelectedCompetitions.Select(c => c.Id).ToList();
        var assignmentResult = await juryService.AssignJuryToCompetitionsAsync(Id, competitionIds);
        
        if (assignmentResult.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Warning", 
                $"Jury was updated but competitions could not be assigned: {assignmentResult.FirstError.Description}", "OK");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Success", "Jury updated and competitions reassigned.", "OK");
        }
    }
    
    [RelayCommand]
    private void AddCompetition(CompetitionModel competition)
    {
        if (competition == null)
        {
            return;
        }
        
        if (!SelectedCompetitions.Any(c => c.Id == competition.Id))
        {
            SelectedCompetitions.Add(competition);
        }
        else
        {
        }
    }
    
    [RelayCommand]
    private void RemoveCompetition(CompetitionModel competition)
    {
        if (competition == null)
        {
            return;
        }
        
        var compToRemove = SelectedCompetitions.FirstOrDefault(c => c.Id == competition.Id);
        if (compToRemove != null)
        {
            SelectedCompetitions.Remove(compToRemove);
        }
        else
        {
        }
    }

    private void ClearForm()
    {
        this.Name.Value = null;
        this.PhoneNumber.Value = null;
        this.Email.Value = null;
        this.SelectedCompetitions.Clear();
    }

    private bool IsFormValid()
    {
        this.Name.Validate();
        this.PhoneNumber.Validate();
        this.Email.Validate();

        return this.Name.IsValid &&
               this.PhoneNumber.IsValid &&
               this.Email.IsValid;
    }
}
