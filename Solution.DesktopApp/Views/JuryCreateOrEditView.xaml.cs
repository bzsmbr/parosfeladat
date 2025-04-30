namespace Solution.DesktopApp.Views;

public partial class JuryCreateOrEditView : ContentPage
{
    public JuryCreateOrEditView ViewModel => this.BindingContext as JuryCreateOrEditView;

    public static string Name => nameof(JuryCreateOrEditView);

    public JuryCreateOrEditView(JuryCreateOrEditView viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();
    }
}