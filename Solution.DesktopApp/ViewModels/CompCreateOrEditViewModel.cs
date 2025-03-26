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
    private IList<TeamModel> teams = [];

    [ObservableProperty]
    private IList<JuryModel> juries = [];

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        //load team, jury

        bool hasValue = query.TryGetValue("Motorcycle", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new  motorcycle";
            return;
        }

        MotorcycleModel motorcycle = result as MotorcycleModel;

        this.Id = motorcycle.Id;
        this.Manufacturer.Value = motorcycle.Manufacturer.Value;
        this.Type.Value = motorcycle.Type.Value;
        this.Model.Value = motorcycle.Model.Value;
        this.ReleaseYear.Value = motorcycle.ReleaseYear.Value;
        this.Cubic.Value = motorcycle.Cubic.Value;
        this.NumberOfCylinders.Value = motorcycle.NumberOfCylinders.Value;
        this.ImageId = motorcycle.ImageId;
        this.WebContentLink = motorcycle.WebContentLink;

        if (!string.IsNullOrEmpty(motorcycle.WebContentLink))
        {
            Image = new UriImageSource
            {
                Uri = new Uri(motorcycle.WebContentLink),
                CacheValidity = new TimeSpan(10, 0, 0, 0)
            };
        }

        asyncButtonAction = OnUpdateAsync;
        Title = "Update motorcycle";
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