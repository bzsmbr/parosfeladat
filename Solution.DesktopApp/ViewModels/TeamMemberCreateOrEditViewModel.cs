namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class TeamMemberCreateOrEditViewModel(AppDbContext dbContext,
    ITeamMemberService teamMemberService,
    IGoogleDriveService googleDriveService) : TeamMemberModel, IQueryAttributable
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingkAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    #region validation commands
    public IRelayCommand NameValidationCommand => new RelayCommand(() => this.Name.Validate());

    #endregion

    #region event commands
    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);

    #endregion

    private delegate Task ButtonActionDelagate();
    private ButtonActionDelagate asyncButtonAction;

    [ObservableProperty]
    private string title;

    private async Task OnAppearingkAsync()
    {
    }

    private async Task OnDisappearingAsync()
    {
    }

    private async Task OnSubmitAsync() => await asyncButtonAction();

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {

        bool hasValue = query.TryGetValue("TeamMember", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new team member!";
            return;
        }

        TeamMemberModel teamMember = result as TeamMemberModel;

        this.Id = teamMember.Id;
        this.Name.Value = teamMember.Name.Value;


        asyncButtonAction = OnUpdateAsync;
        Title = "Update team member";
    }

    private async Task OnSaveAsync()
    {
        if (!IsFormValid())
        {
            return;
        }

        var result = await teamMemberService.CreateAsync(this);
        var message = result.IsError ? result.FirstError.Description : "Team member saved.";
        var title = result.IsError ? "Error" : "Information";

        if (!result.IsError)
        {
            ClearForm();
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task OnUpdateAsync()
    {
        if (!IsFormValid())
        {
            return;
        }

        var result = await teamMemberService.UpdateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Team member updated.";
        var title = result.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private void ClearForm()
    {
        this.Name.Value = null;
    }

    private bool IsFormValid()
    {
        this.Name.Validate();


        return this.Name.IsValid;
    }
}
