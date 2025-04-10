using Microsoft.EntityFrameworkCore;
using Solution.Core.Models;
using System;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class CompCreateOrEditViewModel(
    AppDbContext dbContext,
    ICompetitionService competitionService,
    IGoogleDriveService googleDriveService) : CompetitionModel(), IQueryAttributable
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingkAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    #region validation commands
    public IRelayCommand NameValidationCommand => new RelayCommand(() => this.Name.Validate());

    public IRelayCommand MaxDateTime => new RelayCommand(() => this.Date.Value = DateTime.Now);

    public IRelayCommand CityIndexChangedCommand => new RelayCommand(() => this.Street.Value.City.Validate());
    public IRelayCommand StreetNameValidationCommand => new RelayCommand(() => this.Street.Value.Name.Validate());

    public IRelayCommand StreetHouseNumberValidationCommand => new RelayCommand(() => this.Street.Value.HouseNumber.Validate());

    public IRelayCommand DateIndexChangedCommand => new RelayCommand(() => this.Date.Validate());
    #endregion

    #region event commands
    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
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

    private async Task OnAppearingkAsync()
    {
    }

    private async Task OnDisappearingAsync()
    { 
    }

    private async Task OnSubmitAsync() => await asyncButtonAction();

    private async Task LoadCitiesAsync()
    {
        Cities = await dbContext.Cities.AsNoTracking()
                                       .OrderBy(x => x.Name)
                                       .Select(x => new CityModel(x))
                                       .ToListAsync();
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        await Task.Run(LoadCitiesAsync);

        bool hasValue = query.TryGetValue("Competition", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new competition!";
            return;
        }

        CompetitionModel comp = result as CompetitionModel;

        this.Id = comp.Id;
        this.Name.Value = comp.Name.Value;
        this.Date.Value = comp.Date.Value;
        this.Street = comp.Street;


        asyncButtonAction = OnUpdateAsync;
        Title = "Update Competition";
    }

    private async Task OnSaveAsync()
    {
        if (!IsFormValid())
        {
            return;
        }

        var result = await competitionService.CreateAsync(this);
        var message = result.IsError ? result.FirstError.Description : "Competition saved.";
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

        var result = await competitionService.UpdateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Competition updated.";
        var title = result.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private void ClearForm()
    {
        this.Name.Value = null;

        this.Street.Value.City = null;
        this.Street.Value.Name = null;
        this.Street.Value.HouseNumber = null;
        this.Date.Value = DateTime.Now;
    }

    private bool IsFormValid()
    {
        this.Name.Validate();
        this.Street.Value.City.Validate();
        this.Street.Value.Name.Validate();
        this.Street.Value.HouseNumber.Validate();
        this.Date.Validate();


        return this.Street?.Value.City.IsValid ?? false &&
               this.Name.IsValid &&
               this.Street.Value.Name.IsValid &&
               this.Street.Value.HouseNumber.IsValid &&
               this.Date.IsValid;
    }
}