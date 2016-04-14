using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace UnconsciousBias.ViewModels
{
    public class DetailPageViewModel : ViewModelBase
    {
        public DetailPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }

        private string _Value = "Default";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        private UnconsiousBiasResult _result;
        public UnconsiousBiasResult Result { get { return _result; } set { Set(ref _result, value); } }


        private ObservableCollection<Datapoint> _positivityGraph;

        public ObservableCollection<Datapoint> PositivityGraph
        {
            get
            {
                return _positivityGraph ?? (_positivityGraph = new ObservableCollection<Datapoint>()
                {
                    new Datapoint()
                    {
                        X =78, Y = 1
                    }
                });

            }
            set { Set(ref _positivityGraph, value); }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            _result =  parameter as UnconsiousBiasResult;
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
    }
}

