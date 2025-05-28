using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class TeamMemberListViewModel(ITeamMemberService teamMemberService)
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
    private ObservableCollection<TeamMemberModel> teamMembers;

    private int page = 1;
    private bool isLoading = false;
    private bool hasNextPage = false;
    private int numberOfTeamMembersInDB = 0;

    private async Task OnAppearingAsync()
    {
        PreviousPageCommand = new Command(async () => await OnPreviousPageAsync(), () => page > 1 && !isLoading);
        NextPageCommand = new Command(async () => await OnNextPageAsync(), () => !isLoading && hasNextPage);

        await LoadTeamMembersAsync();
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnPreviousPageAsync()
    {
        if (isLoading) return;

        page = page <= 1 ? 1 : --page;
        await LoadTeamMembersAsync();
    }

    private async Task OnNextPageAsync()
    {
        if (isLoading) return;

        page++;
        await LoadTeamMembersAsync();
    }

    private async Task LoadTeamMembersAsync()
    {
        isLoading = true;

        var result = await teamMemberService.GetPagedAsync(page);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Team members not loaded!", "OK");
            return;
        }

        TeamMembers = new ObservableCollection<TeamMemberModel>(result.Value.Items);
        numberOfTeamMembersInDB = result.Value.Count;

        hasNextPage = numberOfTeamMembersInDB - (page * 10) > 0;
        isLoading = false;

        ((Command)PreviousPageCommand).ChangeCanExecute();
        ((Command)NextPageCommand).ChangeCanExecute();
    }

    private async Task OnDeleteAsync(string? id)
    {
        var result = await teamMemberService.DeleteAsync(id);

        var message = result.IsError ? result.FirstError.Description : "Team member deleted.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            var teamMember = teamMembers.SingleOrDefault(x => x.Id == id);
            teamMembers.Remove(teamMember);

            if (teamMembers.Count == 0)
            {
                await LoadTeamMembersAsync();
            }
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }
}
