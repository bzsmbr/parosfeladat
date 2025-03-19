namespace Solution.DesktopApp;

public partial class AppShell : Shell
{
  public AppShellViewModel ViewModel => this.ViewModel as AppShellViewModel;

  public AppShell(AppShellViewModel viewModel)
  {
    this.BindingContext = viewModel;

    InitializeComponent();

    ConfigureShellNavigation();
  }

  private void ConfigureShellNavigation()
  {
    Routing.RegisterRoute(HomePage.Name, typeof(HomePage));
  }
}
