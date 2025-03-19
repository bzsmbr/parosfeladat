namespace Solution.DesktopApp;

public partial class HomePage : ContentPage
{
  public static string Name => nameof(HomePage);

  public HomePageViewModel ViewModel => this.BindingContext as HomePageViewModel;

  public HomePage(HomePageViewModel viewModel)
  {
      this.BindingContext = viewModel;

      InitializeComponent();
  }
}
