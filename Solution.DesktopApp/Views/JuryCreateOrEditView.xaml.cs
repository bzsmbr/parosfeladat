namespace Solution.DesktopApp.Views;

public partial class JuryCreateOrEditView : ContentPage
{
    public JuryCreateOrEditViewModel ViewModel => this.BindingContext as JuryCreateOrEditViewModel;

    public static string Name => nameof(JuryCreateOrEditView);

    public JuryCreateOrEditView(JuryCreateOrEditViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();
    }
}