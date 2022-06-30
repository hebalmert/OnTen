using Newtonsoft.Json;
using OnTen.Common.Helpers;
using OnTen.Common.Request;
using OnTen.Common.Responses;
using OnTen.Common.Services;
using OnTen.Prism.Helpers;
using OnTen.Prism.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace OnTen.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private bool _isRunning;
        private bool _isEnabled;
        private string _password;
        private DelegateCommand _loginCommand;
        private DelegateCommand _registerCommand;
        private DelegateCommand _forgotPasswordCommand;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        public LoginPageViewModel(INavigationService navigationService, IApiService apiService) 
            : base(navigationService)
        {
            Title = Languages.Login;
            IsEnabled = true;
            _navigationService = navigationService;
            _apiService = apiService;
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }


        public string Email { get; set; }

        //Propiedad que nos permite establecer el ReturPage
        private string _pageReturn;

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("pageReturn"))
            {
                _pageReturn = parameters.GetValue<string>("pageReturn");
            }

            if (string.IsNullOrEmpty(_pageReturn))
            {
                await _navigationService.NavigateAsync($"/{nameof(OnTenMasterDetailPage)}/NavigationPage/{nameof(ProductsPage)}");
            }
            else
            {
                await _navigationService.NavigateAsync($"/{nameof(OnTenMasterDetailPage)}/NavigationPage/{_pageReturn}");
            }

        }



        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(LoginAsync));

        private async void LoginAsync()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailError,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordError,
                    Languages.Accept);
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }

            string url = App.Current.Resources["UrlAPI"].ToString();
            TokenRequest request = new TokenRequest
            {
                Password = Password,
                Username = Email
            };

            Response response = await _apiService.GetTokenAsync(url, "api", "/Account/CreateToken", request);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LoginError, Languages.Accept);
                Password = string.Empty;
                return;
            }

            TokenResponse token = (TokenResponse)response.Result;
            Settings.Token = JsonConvert.SerializeObject(token);
            Settings.IsLogin = true;

            IsRunning = false;
            IsEnabled = true;

            await _navigationService.NavigateAsync($"/{nameof(OnTenMasterDetailPage)}/NavigationPage/{nameof(ProductsPage)}");
            Password = string.Empty;

        }

        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(RegisterAsync));

        private void RegisterAsync()
        {
            //TODO: RegisterAsync
            throw new NotImplementedException();
        }

        public DelegateCommand ForgotPasswordCommand => _forgotPasswordCommand ?? (_forgotPasswordCommand = new DelegateCommand(ForgotPasswordAsync));

        private void ForgotPasswordAsync()
        {
            //TODO: ForgotPasswordAsync
            throw new NotImplementedException();
        }
    }

}
