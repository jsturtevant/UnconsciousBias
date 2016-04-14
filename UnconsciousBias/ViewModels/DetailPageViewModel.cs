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
                return _positivityGraph ?? (_positivityGraph = new ObservableCollection<Datapoint>());

            }
            set { Set(ref _positivityGraph, value); }
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            _result =  parameter as UnconsiousBiasResult;
            //_result.PositivityGraph = new double[] { 0.924, 0.806, 0.976, 0.945, 0.999, 0.681, 0.973, 0.994, 0.958, 0.553};

            this.Value = _result.Positivity.ToString();
            int i = 1;
            foreach(var result  in _result.PositivityGraph)
            {
                var point = new Datapoint()
                {
                    X = i,
                    Y = result
                };
                i++;

                this.PositivityGraph.Add(point);
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
    }
}

