namespace Solution.DesktopApp.Views;

public partial class TeamCreateOrEditView : ContentPage
{
    public TeamCreateOrEditViewModel ViewModel => this.BindingContext as TeamCreateOrEditViewModel;

    public static string Name => nameof(TeamCreateOrEditView);

    public TeamCreateOrEditView(TeamCreateOrEditViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();
    }
}