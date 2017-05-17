using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MonkeyHubApp.Models;
using MonkeyHubApp.Services;
using Newtonsoft.Json;
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

        public ObservableCollection<Tag> Resultados { get; }

        public Command SearchCommand { get; }

        public Command AboutCommand { get; }

        public Command<Tag> ShowCategoriaCommand { get; }

        private readonly IMonkeyHubApiService _monkeyHubApiService;

        public MainViewModel(IMonkeyHubApiService monkeyHubApiService)
        {
            _monkeyHubApiService = monkeyHubApiService;

            SearchCommand = new Command(ExecuteSearchCommand, CanExecuteSearchCommand);

            AboutCommand = new Command(ExecutrAboutCommand);

            ShowCategoriaCommand = new Command<Tag>(ExecuteShowCategoriaCommand);

            Resultados = new ObservableCollection<Tag>();
        }

        async void ExecuteShowCategoriaCommand(Tag tag)
        {
            await PushAsync<CategoriaViewModel>(_monkeyHubApiService, tag);
        }

        async void ExecutrAboutCommand()
        {
            await PushAsync<AboutViewModel>();
        }

        async void ExecuteSearchCommand()
        {
            //await Task.Delay(2000);

            bool resposta = await App.Current.MainPage.DisplayAlert("MonkeyHubApp",
                $"Você pesquisou por '{SearchTerm}'?", "Sim", "Não");

            if (resposta)
            {
                await App.Current.MainPage.DisplayAlert("MonkeyHubApp", "Obrigado.", "OK");

                var tagsRetornadasDoServico = await _monkeyHubApiService.GeTagsAsync();

                Resultados.Clear();

                if (tagsRetornadasDoServico != null)
                {
                    foreach (var tag in tagsRetornadasDoServico)
                    {
                        Resultados.Add(tag);
                    }
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
