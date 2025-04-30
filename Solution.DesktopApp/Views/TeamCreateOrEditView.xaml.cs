namespace Solution.DesktopApp.Views;

public partial class TeamCreateOrEditView : ContentPage
{
    public TeamCreateOrEditView ViewModel => this.BindingContext as TeamCreateOrEditView;

    public static string Name => nameof(TeamCreateOrEditView);

    public TeamCreateOrEditView(TeamCreateOrEditView viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();
    }
}