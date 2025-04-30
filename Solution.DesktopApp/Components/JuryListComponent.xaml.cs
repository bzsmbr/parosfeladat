namespace Solution.DesktopApp.Components;

public partial class JuryListComponent : ContentView
{
    public static readonly BindableProperty JuryProperty = BindableProperty.Create(
         propertyName: nameof(Jury),
         returnType: typeof(JuryModel),
         declaringType: typeof(JuryListComponent),
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
         declaringType: typeof(JuryListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        propertyName: nameof(CommandParameter),
        returnType: typeof(string),
        declaringType: typeof(JuryListComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay
        );

    public string CommandParameter
    {
        get => (string)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public JuryModel Jury
    {
        get => (JuryModel)GetValue(JuryProperty);
        set => SetValue(JuryProperty, value);
    }

    public IAsyncRelayCommand EditCommand => new AsyncRelayCommand(OnEditAsync);

    public JuryListComponent()
    {
        InitializeComponent();
    }

    private async Task OnEditAsync()
    {
        ShellNavigationQueryParameters navigationQueryParameter = new ShellNavigationQueryParameters
        {
            { "Jury", this.Jury}
        };

        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(JuryCreateOrEditView.Name, navigationQueryParameter);
    }
}