using Solution.Core.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class CompListViewModel(ICompetitionService competitionService)
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
    private ObservableCollection<CompetitionModel> comps;

    private int page = 1;
    private bool isLoading = false;
    private bool hasNextPage = false;
    private int numberOfCompsInDB = 0;

    private async Task OnAppearingAsync()
    {
        PreviousPageCommand = new Command(async () => await OnPreviousPageAsync(), () => page > 1 && !isLoading);
        NextPageCommand = new Command(async () => await OnNextPageAsync(), () => !isLoading && hasNextPage);

        await LoadCompsAsync();
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnPreviousPageAsync()
    {
        if (isLoading) return;

        page = page <= 1 ? 1 : --page;
        await LoadCompsAsync();
    }

    private async Task OnNextPageAsync()
    {
        if (isLoading) return;

        page++;
        await LoadCompsAsync();
    }

    private async Task LoadCompsAsync()
    {
        isLoading = true;

        var result = await competitionService.GetPagedAsync(page);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Competitions not loaded!", "OK");
            return;
        }

        Comps = new ObservableCollection<CompetitionModel>(result.Value.Items);
        numberOfCompsInDB = result.Value.Count;

        hasNextPage = numberOfCompsInDB - (page * 10) > 0;
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
            var motorcycle = comps.SingleOrDefault(x => x.Id == id);
            comps.Remove(motorcycle);

            if (comps.Count == 0)
            {
                await LoadCompsAsync();
            }
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }
}