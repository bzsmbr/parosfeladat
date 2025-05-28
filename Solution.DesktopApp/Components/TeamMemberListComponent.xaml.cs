namespace Solution.DesktopApp.Components;

public partial class TeamMemberListComponent : ContentView
{
    public static readonly BindableProperty TeamMemberProperty = BindableProperty.Create(
         propertyName: nameof(TeamMember),
         returnType: typeof(TeamMemberModel),
         declaringType: typeof(TeamMemberListComponent),
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
         declaringType: typeof(TeamMemberListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
        propertyName: nameof(CommandParameter),
        returnType: typeof(string),
        declaringType: typeof(TeamMemberListComponent),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay
        );

    public string CommandParameter
    {
        get => (string)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public TeamMemberModel TeamMember
    {
        get => (TeamMemberModel)GetValue(TeamMemberProperty);
        set => SetValue(TeamMemberProperty, value);
    }

    public IAsyncRelayCommand EditCommand => new AsyncRelayCommand(OnEditAsync);

    public TeamMemberListComponent()
    {
        InitializeComponent();
    }

    private async Task OnEditAsync()
    {
        ShellNavigationQueryParameters navigationQueryParameter = new ShellNavigationQueryParameters
        {
            { "TeamMember", this.TeamMember}
        };

        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(TeamMemberCreateOrEditView.Name, navigationQueryParameter);
    }
}