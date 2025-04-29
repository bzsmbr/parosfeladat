
namespace Solution.DesktopApp.Views;

public partial class TeamListView : ContentPage
{
    public TeamListViewModel ViewModel => this.BindingContext as TeamListViewModel;

    public static string Name => nameof(TeamListView);

    public TeamListView(TeamListViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();
    }
}
