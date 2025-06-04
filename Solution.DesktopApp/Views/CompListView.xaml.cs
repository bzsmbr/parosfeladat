namespace Solution.DesktopApp.Views;

public partial class CompListView : ContentPage
{
    public CompListViewModel ViewModel => this.BindingContext as CompListViewModel;

    public static string Name => nameof(CompListView);

    public CompListView(CompListViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();
    }
}
