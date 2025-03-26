namespace Solution.DesktopApp;

public partial class MainView : ContentPage
{
    public MainViewModel ViewModel => this.BindingContext as MainViewModel;

    public static string Name => nameof(MainView);

    public MainView(MainViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();
    }
}
