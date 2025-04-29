using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class JuryListViewModel(IJuryService juryService)
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
    private ObservableCollection<JuryModel> juries;

    private int page = 1;
    private bool isLoading = false;
    private bool hasNextPage = false;
    private int numberOfJuriesInDB = 0;

    private async Task OnAppearingAsync()
    {
        PreviousPageCommand = new Command(async () => await OnPreviousPageAsync(), () => page > 1 && !isLoading);
        NextPageCommand = new Command(async () => await OnNextPageAsync(), () => !isLoading && hasNextPage);

        await LoadJuriesAsync();
    }

    private async Task OnDisappearingAsync()
    { }

    private async Task OnPreviousPageAsync()
    {
        if (isLoading) return;

        page = page <= 1 ? 1 : --page;
        await LoadJuriesAsync();
    }

    private async Task OnNextPageAsync()
    {
        if (isLoading) return;

        page++;
        await LoadJuriesAsync();
    }

    private async Task LoadJuriesAsync()
    {
        isLoading = true;

        var result = await juryService.GetPagedAsync(page);

        if (result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Juries not loaded!", "OK");
            return;
        }

        Juries = new ObservableCollection<JuryModel>(result.Value.Items);
        numberOfJuriesInDB = result.Value.Count;

        hasNextPage = numberOfJuriesInDB - (page * 10) > 0;
        isLoading = false;

        ((Command)PreviousPageCommand).ChangeCanExecute();
        ((Command)NextPageCommand).ChangeCanExecute();
    }

    private async Task OnDeleteAsync(string? id)
    {
        var result = await juryService.DeleteAsync(id);

        var message = result.IsError ? result.FirstError.Description : "Jury deleted.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            var jury = juries.SingleOrDefault(x => x.Id == id);
            juries.Remove(jury);

            if (juries.Count == 0)
            {
                await LoadJuriesAsync();
            }
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }
}
