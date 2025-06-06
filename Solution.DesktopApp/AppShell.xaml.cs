﻿namespace Solution.DesktopApp;

public partial class AppShell : Shell
{
    public AppShellViewModel ViewModel => this.BindingContext as AppShellViewModel;

    public AppShell(AppShellViewModel viewModel)
    {
        this.BindingContext = viewModel;

        InitializeComponent();

        ConfigureShellNavigation();
    }

    private static void ConfigureShellNavigation()
    {
        Routing.RegisterRoute(MainView.Name, typeof(MainView));


        Routing.RegisterRoute(CompCreateOrEditView.Name, typeof(CompCreateOrEditView));

        Routing.RegisterRoute(CompListView.Name, typeof(CompListView));


        Routing.RegisterRoute(JuryCreateOrEditView.Name, typeof(JuryCreateOrEditView));

        Routing.RegisterRoute(JuryListView.Name, typeof(JuryListView));


        Routing.RegisterRoute(TeamCreateOrEditView.Name, typeof(TeamCreateOrEditView));

        Routing.RegisterRoute(TeamListView.Name, typeof(TeamListView));


        Routing.RegisterRoute(TeamMemberCreateOrEditView.Name, typeof(TeamMemberCreateOrEditView));

        Routing.RegisterRoute(TeamMemberListView.Name, typeof(TeamMemberListView));
    }
}
