namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AppShellViewModel
{
    public IAsyncRelayCommand ExitCommand => new AsyncRelayCommand(OnExitAsync);

    public IAsyncRelayCommand AddNewCompCommand => new AsyncRelayCommand(OnAddNewCompAsync);
    public IAsyncRelayCommand ListAllCompsCommand => new AsyncRelayCommand(OnListAllCompsAsync);


    private async Task OnExitAsync() => Application.Current.Quit();

    private async Task OnAddNewCompAsync()
    {
        Shell.Current.ClearNavigationStack();
    }

    private async Task OnListAllCompsAsync()
    {
        Shell.Current.ClearNavigationStack();
    }
}
