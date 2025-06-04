using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Solution.ValidationLibrary;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class TeamCreateOrEditViewModel(AppDbContext dbContext,
    ITeamService teamService,
    IGoogleDriveService googleDriveService) : TeamModel, IQueryAttributable
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingkAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    #region validation commands
    public IRelayCommand NameValidationCommand => new RelayCommand(() => this.Name.Validate());

    public IRelayCommand PointsValidationCommand => new RelayCommand(() => this.Points.Validate());
    #endregion

    #region event commands
    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
    public IAsyncRelayCommand ImageSelectCommand => new AsyncRelayCommand(OnImageSelectAsync);
    public IAsyncRelayCommand RefreshCompetitionsCommand => new AsyncRelayCommand(LoadAvailableCompetitionsAsync);
    #endregion

    private delegate Task ButtonActionDelagate();
    private ButtonActionDelagate asyncButtonAction;

    [ObservableProperty]
    private string title = "Add new Team!";

    [ObservableProperty]
    private ImageSource image;

    [ObservableProperty]
    private ObservableCollection<CompetitionModel> availableCompetitions = new();

    [ObservableProperty]
    private ObservableCollection<CompetitionModel> selectedCompetitions = new();

    [ObservableProperty]
    private bool isLoadingCompetitions;


    private FileResult selectedFile = null;

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
            var result = await teamService.GetAvailableCompetitionsForTeamAsync();

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

            if (!string.IsNullOrEmpty(Id) && this.AssignedCompetitions != null)
            {
                SelectedCompetitions = new ObservableCollection<CompetitionModel>(
                    this.AssignedCompetitions.Where(ac => AvailableCompetitions.Any(c => c.Id == ac.Id)).ToList());
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

    private async Task OnSubmitAsync() => await asyncButtonAction();

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (AvailableCompetitions == null)
            AvailableCompetitions = new ObservableCollection<CompetitionModel>();

        if (SelectedCompetitions == null)
            SelectedCompetitions = new ObservableCollection<CompetitionModel>();

        bool hasValue = query.TryGetValue("Team", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new Team!";
            return;
        }

        TeamModel team = result as TeamModel;

        this.Id = team?.Id;
        this.Name.Value = team?.Name?.Value;
        this.Points.Value = team?.Points?.Value;
        this.AssignedCompetitions = team?.AssignedCompetitions ?? new List<CompetitionModel>();

        asyncButtonAction = OnUpdateAsync;
        Title = "Update Team";
    }

    private async Task OnSaveAsync()
    {
        if (!IsFormValid())
        {
            return;
        }

        await UploadImageAsync();

        var result = await teamService.CreateAsync(this);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", result.FirstError.Description, "OK");
            return;
        }

        var competitionIds = SelectedCompetitions.Select(c => c.Id).ToList();
        var assignmentResult = await teamService.AssignTeamToCompetitionsAsync(result.Value.Id, competitionIds);

        if (assignmentResult.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Warning",
                $"Team was created but competitions could not be assigned: {assignmentResult.FirstError.Description}", "OK");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Success", "Team saved and assigned to competitions.", "OK");
            ClearForm();
        }
    }

    private async Task OnUpdateAsync()
    {
        if (!IsFormValid())
        {
            return;
        }

        await UploadImageAsync();

        var result = await teamService.UpdateAsync(this);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", result.FirstError.Description, "OK");
            return;
        }

        var competitionIds = SelectedCompetitions.Select(c => c.Id).ToList();
        var assignmentResult = await teamService.AssignTeamToCompetitionsAsync(Id, competitionIds);

        if (assignmentResult.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Warning",
                $"Team was updated but competitions could not be assigned: {assignmentResult.FirstError.Description}", "OK");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Success", "Team updated and competitions reassigned.", "OK");
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
        this.Points.Value = null;
        this.Image = null;
        this.selectedFile = null;
        this.WebContentLink = null;
        this.ImageId = null;
        this.SelectedCompetitions.Clear();
    }

    private bool IsFormValid()
    {
        this.Name.Validate();
        this.Points.Validate();

        return this.Name.IsValid && this.Points.IsValid;
    }

    private async Task OnImageSelectAsync()
    {
        selectedFile = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Please select the team group image"
        });

        if (selectedFile is null)
        {
            return;
        }

        var stream = await selectedFile.OpenReadAsync();
        Image = ImageSource.FromStream(() => stream);
    }

    private async Task UploadImageAsync()
    {
        if (selectedFile is null)
        {
            return;
        }

        var imageUploadResult = await googleDriveService.UploadFileAsync(selectedFile);

        var message = imageUploadResult.IsError ? imageUploadResult.FirstError.Description : "Team image uploaded.";
        var title = imageUploadResult.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");

        this.ImageId = imageUploadResult.IsError ? null : imageUploadResult.Value.Id;
        this.WebContentLink = imageUploadResult.IsError ? null : imageUploadResult.Value.WebContentLink;
    }
}