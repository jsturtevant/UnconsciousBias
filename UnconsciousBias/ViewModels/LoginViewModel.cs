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
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }

        string _Value = "Gas";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(Value)]?.ToString();
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Value)] = Value;
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

                var rq = graphClient.Me.Messages.Request();
                rq.QueryOptions.Add(new QueryOption("$search", "\"to:jstur@microsoft.com\""));
                var emails = await rq.GetAsync();
             

                foreach (var email in emails)
                {
                    Debug.WriteLine(email.BodyPreview);
                }

                string userId = user.Id;
                await Task.CompletedTask;

            }
            else
            {
                await Task.CompletedTask;
            }
        }


    }
}
