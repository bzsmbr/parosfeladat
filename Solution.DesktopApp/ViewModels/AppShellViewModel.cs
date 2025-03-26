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


    private async Task OnExitAsync() => Application.Current.Quit();

    private async Task OnAddNewCompAsync()
    {
        Shell.Current.ClearNavigationStack();
    }

    private async Task OnListAllCompsAsync()
    {
        Shell.Current.ClearNavigationStack();
    }

    private async Task OnAddNewJuryAsync()
    {
        Shell.Current.ClearNavigationStack();
    }

    private async Task OnListAllJuriesAsync()
    {
        Shell.Current.ClearNavigationStack();
    }

    private async Task OnAddNewTeamAsync()
    {
        Shell.Current.ClearNavigationStack();
    }

    private async Task OnListAllTeamsAsync()
    {
        Shell.Current.ClearNavigationStack();
    }
}
