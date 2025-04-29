using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class TeamListViewModel(ITeamService teamService)
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    #region paging commands
    public ICommand PreviousPageCommand { get; private set; }
    public ICommand NextPageCommand { get; private set; }
    #endregion

    #region component commands
    public IAsyncRelayCommand DeleteCommand => new AsyncRelayCommand<string>((id) => OnDeleteAsync(id));
    #endregion

    [ObservableProperty]
    private ObservableCollection<TeamModel> teams;

    private int page = 1;
    private bool isLoading = false;
    private bool hasNextPage = false;
    private int numberOfTeamsInDB = 0;

    private async Task OnAppearingAsync()
    {
        PreviousPageCommand = new Command(async () => await OnPreviousPageAsync(), () => page > 1 && !isLoading);
        NextPageCommand = new Command(async () => await OnNextPageAsync(), () => !isLoading && hasNextPage);

        await LoadTeamsAsync();
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnPreviousPageAsync()
    {
        if (isLoading) return;

        page = page <= 1 ? 1 : --page;
        await LoadTeamsAsync();
    }

    private async Task OnNextPageAsync()
    {
        if (isLoading) return;

        page++;
        await LoadTeamsAsync();
    }

    private async Task LoadTeamsAsync()
    {
        isLoading = true;

        var result = await teamService.GetPagedAsync(page);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Teams not loaded!", "OK");
            return;
        }

        Teams = new ObservableCollection<TeamModel>(result.Value.Items);
        numberOfTeamsInDB = result.Value.Count;

        hasNextPage = numberOfTeamsInDB - (page * 10) > 0;
        isLoading = false;

        ((Command)PreviousPageCommand).ChangeCanExecute();
        ((Command)NextPageCommand).ChangeCanExecute();
    }

    private async Task OnDeleteAsync(string? id)
    {
        var result = await teamService.DeleteAsync(id);

        var message = result.IsError ? result.FirstError.Description : "Team deleted.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            var team = teams.SingleOrDefault(x => x.Id == id);
            teams.Remove(team);

            if (teams.Count == 0)
            {
                await LoadTeamsAsync();
            }
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }
}
