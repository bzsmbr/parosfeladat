namespace Solution.DesktopApp.Components;

public partial class TeamListComponent : ContentView
{
    public static readonly BindableProperty TeamProperty = BindableProperty.Create(
         propertyName: nameof(Team),
         returnType: typeof(TeamModel),
         declaringType: typeof(TeamListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public IAsyncRelayCommand DeleteCommand
    {
        get => (IAsyncRelayCommand)GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(
         propertyName: nameof(DeleteCommand),
         returnType: typeof(IAsyncRelayCommand),
         declaringType: typeof(TeamListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        propertyName: nameof(CommandParameter),
        returnType: typeof(string),
        declaringType: typeof(TeamListComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay
        );

    public string CommandParameter
    {
        get => (string)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public TeamModel Competition
    {
        get => (TeamModel)GetValue(TeamProperty);
        set => SetValue(TeamProperty, value);
    }

    public IAsyncRelayCommand EditCommand => new AsyncRelayCommand(OnEditAsync);

    public TeamListComponent()
    {
        InitializeComponent();
    }

    private async Task OnEditAsync()
    {
        ShellNavigationQueryParameters navigationQueryParameter = new ShellNavigationQueryParameters
        {
            { "Team", this.Team}
        };

        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(TeamCreateOrEditView.Name, navigationQueryParameter);
    }
}