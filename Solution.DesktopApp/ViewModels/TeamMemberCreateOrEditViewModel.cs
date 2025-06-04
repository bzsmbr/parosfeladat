using Microsoft.EntityFrameworkCore;
using Solution.Core.Models;
using Solution.Services.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;
using ErrorOr;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class TeamMemberCreateOrEditViewModel(AppDbContext dbContext,
    ITeamMemberService teamMemberService,
    IGoogleDriveService googleDriveService) : TeamMemberModel, IQueryAttributable
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingkAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    #region validation commands
    public IRelayCommand NameValidationCommand => new RelayCommand(() => this.Name.Validate());
    #endregion

    #region event commands
    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
    public IAsyncRelayCommand ImageSelectCommand => new AsyncRelayCommand(OnImageSelectAsync);
    public IAsyncRelayCommand RefreshTeamsCommand => new AsyncRelayCommand(LoadAvailableTeamsAsync);
    #endregion

    private delegate Task ButtonActionDelagate();
    private ButtonActionDelagate asyncButtonAction;

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private ImageSource image;

    [ObservableProperty]
    private ObservableCollection<TeamModel> availableTeams;

    [ObservableProperty]
    private ObservableCollection<TeamModel> selectedTeams;

    [ObservableProperty]
    private bool isLoadingTeams;

    private FileResult selectedFile = null;

    private async Task OnAppearingkAsync()
    {
        if (AvailableTeams == null)
            AvailableTeams = new ObservableCollection<TeamModel>();
        if (SelectedTeams == null)
            SelectedTeams = new ObservableCollection<TeamModel>();
        await LoadAvailableTeamsAsync();
    }

    private async Task OnDisappearingAsync() { }

    private async Task LoadAvailableTeamsAsync()
    {
        IsLoadingTeams = true;
        try
        {
            var result = await dbContext.Teams
                .AsNoTracking()
                .Select(t => new TeamModel(t))
                .ToListAsync();
            AvailableTeams = new ObservableCollection<TeamModel>(result ?? new List<TeamModel>());
            if (!string.IsNullOrEmpty(Id) && this.AssignedTeams != null)
            {
                SelectedTeams = new ObservableCollection<TeamModel>(
                    this.AssignedTeams.Where(at => AvailableTeams.Any(t => t.Id == at.Id)).ToList());
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
            IsLoadingTeams = false;
        }
    }

    private async Task OnSubmitAsync() => await asyncButtonAction();

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (AvailableTeams == null)
            AvailableTeams = new ObservableCollection<TeamModel>();
        if (SelectedTeams == null)
            SelectedTeams = new ObservableCollection<TeamModel>();
        bool hasValue = query.TryGetValue("TeamMember", out object result);
        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new contender!";
            return;
        }
        TeamMemberModel teamMember = result as TeamMemberModel;
        this.Id = teamMember?.Id;
        this.Name.Value = teamMember?.Name?.Value;
        this.ImageId = teamMember?.ImageId;
        this.WebContentLink = teamMember?.WebContentLink;
        this.AssignedTeams = teamMember?.AssignedTeams ?? new List<TeamModel>();
        if (this.AssignedTeams?.Count > 0)
            SelectedTeams = new ObservableCollection<TeamModel>(this.AssignedTeams);
        asyncButtonAction = OnUpdateAsync;
        Title = "Update Contender";
    }

    private async Task OnSaveAsync()
    {
        if (!IsFormValid()) return;
        await UploadImageAsync();
        var result = await teamMemberService.CreateAsync(this);
        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", result.FirstError.Description, "OK");
            return;
        }
        var teamIds = SelectedTeams.Select(t => t.Id).ToList();
        var assignmentResult = await teamMemberService.AssignTeamMemberToTeamsAsync(result.Value.Id, teamIds);
        if (assignmentResult.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Warning", $"Team member was created but teams could not be assigned: {assignmentResult.FirstError.Description}", "OK");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Success", "Team member saved and assigned to teams.", "OK");
            ClearForm();
        }
    }

    private async Task OnUpdateAsync()
    {
        if (!IsFormValid()) return;
        await UploadImageAsync();
        var result = await teamMemberService.UpdateAsync(this);
        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", result.FirstError.Description, "OK");
            return;
        }
        var teamIds = SelectedTeams.Select(t => t.Id).ToList();
        var assignmentResult = await teamMemberService.AssignTeamMemberToTeamsAsync(Id, teamIds);
        if (assignmentResult.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Warning", $"Team member was updated but teams could not be assigned: {assignmentResult.FirstError.Description}", "OK");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Success", "Team member updated and teams reassigned.", "OK");
        }
    }

    [RelayCommand]
    private void AddTeam(TeamModel team)
    {
        if (team == null) return;
        if (!SelectedTeams.Any(t => t.Id == team.Id))
            SelectedTeams.Add(team);
    }

    [RelayCommand]
    private void RemoveTeam(TeamModel team)
    {
        if (team == null) return;
        var teamToRemove = SelectedTeams.FirstOrDefault(t => t.Id == team.Id);
        if (teamToRemove != null)
            SelectedTeams.Remove(teamToRemove);
    }

    private void ClearForm()
    {
        this.Name.Value = null;
        this.Image = null;
        this.selectedFile = null;
        this.WebContentLink = null;
        this.ImageId = null;
        if (SelectedTeams != null)
            this.SelectedTeams.Clear();
    }

    private bool IsFormValid()
    {
        this.Name.Validate();
        return this.Name.IsValid;
    }

    private async Task OnImageSelectAsync()
    {
        selectedFile = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Please select the contester profile picture"
        });
        if (selectedFile is null) return;
        var stream = await selectedFile.OpenReadAsync();
        Image = ImageSource.FromStream(() => stream);
    }

    private async Task UploadImageAsync()
    {
        if (selectedFile is null) return;
        var imageUploadResult = await googleDriveService.UploadFileAsync(selectedFile);
        var message = imageUploadResult.IsError ? imageUploadResult.FirstError.Description : "Profile picture uploaded.";
        var title = imageUploadResult.IsError ? "Error" : "Information";
        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
        this.ImageId = imageUploadResult.IsError ? null : imageUploadResult.Value.Id;
        this.WebContentLink = imageUploadResult.IsError ? null : imageUploadResult.Value.WebContentLink;
    }
}