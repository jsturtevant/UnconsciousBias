using Template10.Mvvm;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using UnconsciousBias.Helpers;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace UnconsciousBias.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";              
            }   
        }
       
        string _Value = "";
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

        public async Task GotoDetailsPage()
        {
            UnconsiousBiasResult result = null;
            var graphClient = await AuthenticationHelper.GetAuthenticatedClientAsync();

            if (graphClient != null)
            {
                var emails = await graphClient.Me
                                            .Messages
                                            .Request()
                                            .Search($"\"to:{Value}\"")
                                            .Select("UniqueBody")
                                            .GetAsync();


                StringBuilder sb = new StringBuilder();
                sb.Append("{\"documents\":[");
                int i = 1;
                foreach (var email in emails)
                {
                    Debug.WriteLine(email.UniqueBody);

                    // This is super-hacky processing out the HTML tags from the email.  
                    // TODO: replace this with a proper library that will do this better.  
                    string body = email.UniqueBody.Content;
                    string bodyWithoutHTMLtags = WebUtility.HtmlDecode(Regex.Replace(body, "<[^>]*(>|$)", string.Empty));
                    string step2 = Regex.Replace(bodyWithoutHTMLtags, @"[\s\r\n]+", " ");
                    sb.Append("{\"id\":\"" + i + "\",\"text\":\"" + step2 + "\"},");
                    i++;
                }
                // Remove the trailing comma to get well-formatted JSON
                string tempString = sb.ToString();
                if (tempString.LastIndexOf(",") == tempString.Length - 1) sb.Remove(tempString.Length - 1, 1);

                // Close JSON message
                sb.Append("]}");

                List<double> sentimentScores = await TextAnalyticsHelper.GetSentiment(sb.ToString());

                // Calculate average sentiment score
                double scoreSum = 0.0;
                int scoreCount = 0;
                foreach (double sentimentScore in sentimentScores)
                {
                    scoreSum += sentimentScore;
                    scoreCount++;
                }
                double averageSentimentScore = scoreSum / scoreCount;
                int sentimentPercentage = Convert.ToInt32(averageSentimentScore * 100);

                result = new UnconsiousBiasResult()
                {
                    Positivity = sentimentPercentage,
                    KeyWords = "TODO",
                    PositivityGraph = sentimentScores.ToArray()
                };
            }

            // can pass value to other screen and do fancy display
            NavigationService.Navigate(typeof(Views.DetailPage), result);

            await Task.CompletedTask;
        }

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }

    public class UnconsiousBiasResult
    {
        public int Positivity { get; set; }
        public string KeyWords { get; set; }
        public string Topics { get; set; }
        public double[] PositivityGraph { get; set; } 
    }

    public class Datapoint : ViewModelBase
    {

        private int _x;
        private double _y;

        public int X
        {
            get { return _x; }
            set { Set(ref _x, value); }
        }

        public double Y
        {
            get { return _y; }
            set { Set(ref _y, value); }
        }
    }
}

