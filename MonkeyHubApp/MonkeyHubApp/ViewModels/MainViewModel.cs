using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonkeyHubApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _searchTerm;

        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                if(SetProperty(ref _searchTerm, value))
                    SearchCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<string> Resultados { get; }

        public Command SearchCommand { get; }

        public MainViewModel()
        {
            SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);

            Resultados = new ObservableCollection<string>(new []{ "abc", "cde", "1", "2", "3", "4", "5", "6", "7" });
        }

        async void ExecuteSearchCommand()
        {
            //await Task.Delay(2000);

            bool resposta = await App.Current.MainPage.DisplayAlert("MonkeyHubApp",
                $"Você pesquisou por '{SearchTerm}'?", "Sim", "Não");

            if (resposta)
            {
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", "Obrigado.", "OK");
                Resultados.Clear();
                for (int i = 1; i <= 30; i++)
                {
                    Resultados.Add($"Sim {i}");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", "De nada.", "OK");
                Resultados.Clear();
            }
        }

        bool CanExecuteSearchCommand()
        {
            return !String.IsNullOrWhiteSpace(SearchTerm);
        }
    }
}
