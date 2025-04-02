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

    public IRelayCommand CityIndexChangedCommand => new RelayCommand(() => this.Street.City.Validate());
    public IRelayCommand StreetNameIndexChangedCommand => new RelayCommand(() => this.Street.Name.Validate());

    public IRelayCommand StreetHouseNumberIndexChangedCommand => new RelayCommand(() => this.Street.HouseNumber.Validate());

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

    [ObservableProperty]
    private StreetModel street;

    private async Task OnAppearingkAsync()
    {
        Street = new StreetModel();
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
        this.Street = comp.Street.Value;


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

        this.Street.City.Value = null;
        this.Street.Name.Value = null;
        this.Street.HouseNumber.Value = null;
        this.Date.Value = DateTime.Now;
    }

    private bool IsFormValid()
    {
        this.Name.Validate();
        this.Street.City.Validate();
        this.Street.Name.Validate();
        this.Street.HouseNumber.Validate();
        this.Date.Validate();


        return (this.Street.City?.IsValid ?? false) &&
               this.Name.IsValid &&
               this.Street.Name.IsValid &&
               this.Street.HouseNumber.IsValid &&
               this.Date.IsValid;
    }
}