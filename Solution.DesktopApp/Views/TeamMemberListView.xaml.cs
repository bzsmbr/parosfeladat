
namespace Solution.DesktopApp.Views;

public partial class TeamMemberListView : ContentPage
{
    public TeamMemberListViewModel ViewModel => this.BindingContext as TeamMemberListViewModel;

    public static string Name => nameof(TeamMemberListView);

    public TeamMemberListView(TeamMemberListViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();
    }
}
