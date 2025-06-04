namespace Solution.DesktopApp.Components;

public partial class CompetitionJuryListComponent : ContentView
{
    public static readonly BindableProperty JuryProperty = BindableProperty.Create(
         propertyName: nameof(Jury),
         returnType: typeof(JuryModel),
         declaringType: typeof(CompetitionJuryListComponent),
         defaultValue: null,
         defaultBindingMode: BindingMode.OneWay
    );

    public JuryModel Jury
    {
        get => (JuryModel)GetValue(JuryProperty);
        set => SetValue(JuryProperty, value);
    }

    public CompetitionJuryListComponent()
    {
        InitializeComponent();
    }
}