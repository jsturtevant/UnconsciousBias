using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using UnconsciousBias.Helpers;
using Windows.UI.Xaml.Navigation;
using static Microsoft.Graph.Authentication.Constants;

namespace UnconsciousBias.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
           
        }

        string _message = "Please enter your credentials";
        public string Message { get { return _message; } set { Set(ref _message, value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Message = suspensionState[nameof(Message)]?.ToString();
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Message)] = Message;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public async Task ConnectToService()
        {
            var graphClient = await AuthenticationHelper.GetAuthenticatedClientAsync();

            if (graphClient != null)
            {
                var user = await graphClient.Me.Request().GetAsync();

                this.Message = $"Welcome {user.DisplayName}";
                await Task.CompletedTask;
            }
            else
            {
                await Task.CompletedTask;
            }
        }


    }

    public static class UserMessagesCollectionRequestExtensions
    {
        public static IUserMessagesCollectionRequest Search(this IUserMessagesCollectionRequest request,  string value)
        {
            request.QueryOptions.Add(new QueryOption("$search", value));
            return request;
        }
    }
}
