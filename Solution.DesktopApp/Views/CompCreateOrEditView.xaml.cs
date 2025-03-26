namespace Solution.DesktopApp.Views;

public partial class CompCreateOrEditView : ContentPage
{
    public CompCreateOrEditViewModel ViewModel => this.BindingContext as CompCreateOrEditViewModel;

    public static string Name => nameof(CompCreateOrEditView);

    public CompCreateOrEditView(CompCreateOrEditViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();
    }
}