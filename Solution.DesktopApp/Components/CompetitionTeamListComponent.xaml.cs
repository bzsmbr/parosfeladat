using System.Windows.Input;

namespace Solution.DesktopApp.Components;

public partial class CompetitionTeamListComponent : ContentView
{
    public static readonly BindableProperty TeamProperty = BindableProperty.Create(
        nameof(Team), 
        typeof(TeamModel), 
        typeof(CompetitionTeamListComponent));

    public static readonly BindableProperty ViewMembersCommandProperty = BindableProperty.Create(
        nameof(ViewMembersCommand), 
        typeof(ICommand), 
        typeof(CompetitionTeamListComponent));

    public TeamModel Team
    {
        get => (TeamModel)GetValue(TeamProperty);
        set => SetValue(TeamProperty, value);
    }

    public ICommand ViewMembersCommand
    {
        get => (ICommand)GetValue(ViewMembersCommandProperty);
        set => SetValue(ViewMembersCommandProperty, value);
    }

    public CompetitionTeamListComponent()
    {
        InitializeComponent();
    }
}