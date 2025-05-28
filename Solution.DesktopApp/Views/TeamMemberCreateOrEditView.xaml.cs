namespace Solution.DesktopApp.Views;

public partial class TeamMemberCreateOrEditView : ContentPage
{
    public TeamMemberCreateOrEditViewModel ViewModel => this.BindingContext as TeamMemberCreateOrEditViewModel;

    public static string Name => nameof(TeamMemberCreateOrEditView);

    public TeamMemberCreateOrEditView(TeamMemberCreateOrEditViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();
    }
}