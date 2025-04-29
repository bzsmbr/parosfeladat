
namespace Solution.DesktopApp.Views;

public partial class JuryListView : ContentPage
{
    public JuryListViewModel ViewModel => this.BindingContext as JuryListViewModel;

    public static string Name => nameof(JuryListView);

    public JuryListView(JuryListViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();
    }
}
