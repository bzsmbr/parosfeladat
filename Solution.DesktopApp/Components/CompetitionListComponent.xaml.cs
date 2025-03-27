namespace Solution.DesktopApp.Components;

public partial class CompetitionListComponent : ContentView
{
    public static readonly BindableProperty CompetitionProperty = BindableProperty.Create(
         propertyName: nameof(Competition),
         returnType: typeof(CompetitionModel),
         declaringType: typeof(CompetitionListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public IAsyncRelayCommand DeleteCommand
    {
        get => (IAsyncRelayCommand)GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        propertyName: nameof(CommandParameter),
        returnType: typeof(string),
        declaringType: typeof(CompetitionListComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay
        );

    public string CommandParameter
    {
        get => (string)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public CompetitionModel Competition
    {
        get => (CompetitionModel)GetValue(CompetitionProperty);
        set => SetValue(CompetitionProperty, value);
    }

    public IAsyncRelayCommand EditCommand => new AsyncRelayCommand(OnEditAsync);

    public CompetitionListComponent()
    {
        InitializeComponent();
    }

    private async Task OnEditAsync()
    {
        ShellNavigationQueryParameters navigationQueryParameter = new ShellNavigationQueryParameters
        {
            { "Competition", this.Competition}
        };

        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(CompCreateOrEditView.Name, navigationQueryParameter);
    }
}