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

    public IAsyncRelayCommand ImageSelectCommand => new AsyncRelayCommand(OnImageSelectAsync);

    #endregion

    private delegate Task ButtonActionDelagate();
    private ButtonActionDelagate asyncButtonAction;

    [ObservableProperty]
    private string title;

    [ObservableProperty]
    private ImageSource image;

    private FileResult selectedFile = null;

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
            Title = "Add new contender!";
            return;
        }

        TeamMemberModel teamMember = result as TeamMemberModel;

        this.Id = teamMember.Id;
        this.Name.Value = teamMember.Name.Value;


        asyncButtonAction = OnUpdateAsync;
        Title = "Updated contender";
    }

    private async Task OnSaveAsync()
    {
        if (!IsFormValid())
        {
            return;
        }

        await UploaImageAsync();

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

        await UploaImageAsync();

        var result = await teamMemberService.UpdateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Team member updated.";
        var title = result.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private void ClearForm()
    {
        this.Name.Value = null;

        this.Image = null;
        this.selectedFile = null;
        this.WebContentLink = null;
        this.ImageId = null;
    }

    private bool IsFormValid()
    {
        this.Name.Validate();


        return this.Name.IsValid;
    }

    private async Task OnImageSelectAsync()
    {
        selectedFile = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Please select the contester profile picture"
        });

        if (selectedFile is null)
        {
            return;
        }

        var stream = await selectedFile.OpenReadAsync();
        Image = ImageSource.FromStream(() => stream);
    }

    private async Task UploaImageAsync()
    {
        if (selectedFile is null)
        {
            return;
        }

        var imageUploadResult = await googleDriveService.UploadFileAsync(selectedFile);

        var message = imageUploadResult.IsError ? imageUploadResult.FirstError.Description : "Profile picture uploaded.";
        var title = imageUploadResult.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");

        this.ImageId = imageUploadResult.IsError ? null : imageUploadResult.Value.Id;
        this.WebContentLink = imageUploadResult.IsError ? null : imageUploadResult.Value.WebContentLink;
    }
}
