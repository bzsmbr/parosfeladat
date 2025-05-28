namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AppShellViewModel
{
    public IAsyncRelayCommand ExitCommand => new AsyncRelayCommand(OnExitAsync);

    public IAsyncRelayCommand AddNewCompCommand => new AsyncRelayCommand(OnAddNewCompAsync);
    public IAsyncRelayCommand ListAllCompsCommand => new AsyncRelayCommand(OnListAllCompsAsync);

    public IAsyncRelayCommand AddNewJuryCommand => new AsyncRelayCommand(OnAddNewJuryAsync);
    public IAsyncRelayCommand ListAllJuriesCommand => new AsyncRelayCommand(OnListAllJuriesAsync);

    public IAsyncRelayCommand AddNewTeamCommand => new AsyncRelayCommand(OnAddNewTeamAsync);
    public IAsyncRelayCommand ListAllTeamsCommand => new AsyncRelayCommand(OnListAllTeamsAsync);

    public IAsyncRelayCommand AddNewTeamMemberCommand => new AsyncRelayCommand(OnAddNewTeamMemberAsync);
    public IAsyncRelayCommand ListAllTeamMembersCommand => new AsyncRelayCommand(OnListAllTeamMembersAsync);


    private async Task OnExitAsync() => Application.Current.Quit();

    private async Task OnAddNewCompAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(CompCreateOrEditView.Name);
    }

    private async Task OnListAllCompsAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(CompListView.Name);
    }

    private async Task OnAddNewJuryAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(JuryCreateOrEditView.Name);
    }

    private async Task OnListAllJuriesAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(JuryListView.Name);
    }

    private async Task OnAddNewTeamAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(TeamCreateOrEditView.Name);
    }

    private async Task OnListAllTeamsAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(TeamListView.Name);
    }

    private async Task OnAddNewTeamMemberAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(TeamMemberCreateOrEditView.Name);
    }

    private async Task OnListAllTeamMembersAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(TeamMemberListView.Name);
    }
}
