using MonkeyChallenger.Helpers;
using MonkeyChallenger.Models;
using MonkeyChallenger.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace MonkeyChallenger.ViewModels
{
    public class TranslatePageViewModel:BaseViewModel,IInitialize
    {
        protected IAPITranslateService APITranslateService;
        public string InputText { get; set; }
        public DelegateCommand TranslateCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public ObservableCollection<Translation> Translations { get; set; }
        public TranslatePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IAPITranslateService APITranslateService) : base(navigationService, dialogService)
        {
            CloseCommand = new DelegateCommand(async () => await navigationService.GoBackAsync());
            this.APITranslateService = APITranslateService;
            TranslateCommand = new DelegateCommand(async () =>
            {
                await GetTranslate();
            });
        }
         async Task GetTranslate()
        {
            var trans = await APITranslateService.TranslateText(InputText, ConfigApi.TranslationsApiKey);
            var list = trans.OrderByDescending(e => e.DetectedLanguage).FirstOrDefault();
            Translations = new ObservableCollection<Translation>(list.Translations);
        }

        public void Initialize(INavigationParameters parameters)
        {
            InputText = parameters[ConfigPage.TranslatePage] as string;
            if (!string.IsNullOrWhiteSpace(InputText))
            {
                TranslateCommand.Execute();
            }
        }
    }
}
