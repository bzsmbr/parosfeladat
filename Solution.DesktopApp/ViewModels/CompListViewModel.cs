using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class CompListViewModel(ICompetitionService competitionService, IJuryService juryService, ITeamService teamService)
{
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);

    public ICommand PreviousPageCommand { get; private set; }
    public ICommand NextPageCommand { get; private set; }

    public IAsyncRelayCommand DeleteCommand => new AsyncRelayCommand<string>((id) => OnDeleteAsync(id));
    public IAsyncRelayCommand ViewJuriesCommand => new AsyncRelayCommand<string>(ShowJuriesPopupAsync);
    public IAsyncRelayCommand ViewTeamsCommand => new AsyncRelayCommand<string>(ShowTeamsPopupAsync);
    public IAsyncRelayCommand ViewTeamMembersCommand => new AsyncRelayCommand<TeamModel>(OnViewTeamMembersAsync);

    [ObservableProperty]
    private ObservableCollection<CompetitionModel> competitions;

    [ObservableProperty]
    private CompetitionModel selectedCompetition;

    [ObservableProperty]
    private ObservableCollection<JuryModel> selectedCompetitionJuries;

    [ObservableProperty]
    private bool isLoadingJuries;

    [ObservableProperty]
    private bool isJuriesPopupVisible;

    [ObservableProperty]
    private ObservableCollection<TeamModel> selectedCompetitionTeams;

    [ObservableProperty]
    private ObservableCollection<TeamMemberModel> selectedTeamMembers;

    [ObservableProperty]
    private bool isLoadingTeams;

    [ObservableProperty]
    private bool isTeamsPopupVisible;

    [ObservableProperty]
    private TeamModel selectedTeam;

    [ObservableProperty]
    private bool isLoadingTeamMembers;

    [ObservableProperty]
    private string selectedCompetitionName;

    private int page = 1;
    private bool isLoading = false;
    private bool hasNextPage = false;
    private int numberOfCompetitionsInDB = 0;

    public ICommand CloseJuriesPopupCommand => new Command(() => IsJuriesPopupVisible = false);
    public ICommand CloseTeamsPopupCommand => new Command(() => IsTeamsPopupVisible = false);
    public ICommand BackToTeamsCommand => new Command(() => {
        SelectedTeam = null;
        SelectedTeamMembers = new ObservableCollection<TeamMemberModel>();
    });

    private async Task OnAppearingAsync()
    {
        PreviousPageCommand = new Command(async () => await OnPreviousPageAsync(), () => page > 1 && !isLoading);
        NextPageCommand = new Command(async () => await OnNextPageAsync(), () => !isLoading && hasNextPage);
        await LoadCompetitionsAsync();
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnPreviousPageAsync()
    {
        if (isLoading) return;
        page = page <= 1 ? 1 : --page;
        await LoadCompetitionsAsync();
    }

    private async Task OnNextPageAsync()
    {
        if (isLoading) return;
        page++;
        await LoadCompetitionsAsync();
    }

    private async Task LoadCompetitionsAsync()
    {
        isLoading = true;
        var result = await competitionService.GetPagedAsync(page);
        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Competitions not loaded!", "OK");
            return;
        }
        Competitions = new ObservableCollection<CompetitionModel>(result.Value.Items);
        numberOfCompetitionsInDB = result.Value.Count;
        hasNextPage = numberOfCompetitionsInDB - (page * 10) > 0;
        isLoading = false;
        ((Command)PreviousPageCommand).ChangeCanExecute();
        ((Command)NextPageCommand).ChangeCanExecute();
    }

    private async Task OnDeleteAsync(string? id)
    {
        var result = await competitionService.DeleteAsync(id);
        var message = result.IsError ? result.FirstError.Description : "Competition deleted.";
        var title = result.IsError ? "Error" : "Information";
        if (!result.IsError)
        {
            var competition = competitions.SingleOrDefault(x => x.Id == id);
            competitions.Remove(competition);
            if (competitions.Count == 0)
            {
                await LoadCompetitionsAsync();
            }
        }
        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task ShowJuriesPopupAsync(string competitionId)
    {
        var competition = Competitions.FirstOrDefault(c => c.Id == competitionId);
        if (competition == null) return;
        SelectedCompetition = competition;
        SelectedCompetitionName = competition.Name?.Value ?? "Unknown Competition";
        IsLoadingJuries = true;
        IsJuriesPopupVisible = true;
        var juriesResult = await juryService.GetByCompetitionIdAsync(competitionId);
        IsLoadingJuries = false;
        if (!juriesResult.IsError)
            SelectedCompetitionJuries = new ObservableCollection<JuryModel>(juriesResult.Value);
        else
            SelectedCompetitionJuries = new ObservableCollection<JuryModel>();
    }

    private async Task ShowTeamsPopupAsync(string competitionId)
    {
        var competition = Competitions.FirstOrDefault(c => c.Id == competitionId);
        if (competition == null) return;
        SelectedCompetition = competition;
        SelectedCompetitionName = competition.Name?.Value ?? "Unknown Competition";
        SelectedTeam = null;
        SelectedTeamMembers = new ObservableCollection<TeamMemberModel>();
        IsLoadingTeams = true;
        IsTeamsPopupVisible = true;
        var teamsResult = await teamService.GetByCompetitionIdAsync(competitionId);
        IsLoadingTeams = false;
        if (!teamsResult.IsError)
            SelectedCompetitionTeams = new ObservableCollection<TeamModel>(teamsResult.Value);
        else
            SelectedCompetitionTeams = new ObservableCollection<TeamModel>();
    }

    private async Task OnViewTeamMembersAsync(TeamModel team)
    {
        if (team == null) return;
        SelectedTeam = team;
        IsLoadingTeamMembers = true;
        var membersResult = await teamService.GetTeamMembersAsync(team.Id);
        IsLoadingTeamMembers = false;
        if (!membersResult.IsError)
            SelectedTeamMembers = new ObservableCollection<TeamMemberModel>(membersResult.Value);
        else
            SelectedTeamMembers = new ObservableCollection<TeamMemberModel>();
    }
}
