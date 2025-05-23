﻿using System.ComponentModel.DataAnnotations;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class JuryCreateOrEditViewModel(AppDbContext dbContext,
    IJuryService juryService,
    IGoogleDriveService googleDriveService) : JuryModel, IQueryAttributable
{
    #region life cycle commands
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingkAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    #endregion

    #region validation commands
    public IRelayCommand NameValidationCommand => new RelayCommand(() => this.Name.Validate());

    public IRelayCommand PhoneNumberValidationCommand => new RelayCommand(() => this.PhoneNumber.Validate());

    public IRelayCommand EmailValidationCommand => new RelayCommand(() => this.Email.Validate());
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

        bool hasValue = query.TryGetValue("Jury", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new Jury!";
            return;
        }

        JuryModel jury = result as JuryModel;

        this.Id = jury.Id;
        this.Name.Value = jury.Name.Value;
        this.PhoneNumber.Value = jury.PhoneNumber.Value;
        this.Email.Value = jury.Email.Value;


        asyncButtonAction = OnUpdateAsync;
        Title = "Update Jury";
    }

    private async Task OnSaveAsync()
    {
        if (!IsFormValid())
        {
            return;
        }

        var result = await juryService.CreateAsync(this);
        var message = result.IsError ? result.FirstError.Description : "Jury saved.";
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

        var result = await juryService.UpdateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Jury updated.";
        var title = result.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private void ClearForm()
    {
        this.Name.Value = null;
        this.PhoneNumber = null;
        this.Email = null;
    }

    private bool IsFormValid()
    {
        this.Name.Validate();
        this.PhoneNumber.Validate();
        this.Email.Validate();


        return this.Name.IsValid &&
               this.PhoneNumber.IsValid &&
               this.Email.IsValid;
    }
}
