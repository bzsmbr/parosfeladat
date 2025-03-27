using Solution.Core.Models;
using System;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class CompCreateOrEditViewModel(
    AppDbContext dbContext,
    ICompetitionService motorcycleService,
    IGoogleDriveService googleDriveService) : CompetitionModel(), IQueryAttributable
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingkAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    #region validation commands
    //public IRelayCommand TypeIndexChangedCommand => new RelayCommand(() => this.Type.Validate());
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
    private IList<TeamModel> teamNames = [];

    [ObservableProperty]
    private IList<JuryModel> juries = [];

    [ObservableProperty]
    private IList<CityModel> cities = [];

    [ObservableProperty]
    private StreetModel street;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        //load team, jury, city

        bool hasValue = query.TryGetValue("Competition", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new competition!";
            return;
        }

        CompetitionModel comp = result as CompetitionModel;

        this.Id = comp.Id;
        //this.TeamNames.Value = comp.Teams.Name.Value;

        

        asyncButtonAction = OnUpdateAsync;
        Title = "Update Competition";
    }

    private async Task OnSaveAsync()
    {
        if (!IsFormValid())
        {
            return;
        }

        await UploaImageAsync();

        var result = await motorcycleService.CreateAsync(this);
        var message = result.IsError ? result.FirstError.Description : "Motorcycle saved.";
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

        var result = await motorcycleService.UpdateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Motorcycle updated.";
        var title = result.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }


    private async Task LoadManufacturersAsync()
    {
        Manufacturers = await dbContext.Manufacturers.AsNoTracking()
                                                     .OrderBy(x => x.Name)
                                                     .Select(x => new ManufacturerModel(x))
                                                     .ToListAsync();
    }

    //update
    //save
    //load
    //clear form
    //is form valid
}