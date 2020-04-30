using MonkeyChallenger.Helpers;
using MonkeyChallenger.Models;
using MonkeyChallenger.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Timers;

namespace MonkeyChallenger.ViewModels
{
    public class MainPageViewModel:BaseViewModel,INavigationAware
    {
        public ObservableCollection<Picture> Destinations { get; set; }
        private readonly Timer slideTime = new Timer(3000);
        public ObservableCollection<Picture> MyPictures { get; set; }
        private Picture currentPicture;

        public Picture CurrentPicture
        {
            get { return currentPicture; }
            set { currentPicture = value; }
        }
        private string inputText;

        public string InputText
        {
            get { return inputText; }
            set {  inputText = value;
            }
        }

        public DelegateCommand AddNewImageCommand { get; set; }
        public DelegateCommand LoadListCommand { get; set; }
        public DelegateCommand<string> GoToTranslateCommand { get; set; }
        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiBingSearch apiBingSearch) :base(navigationService, dialogService)
        {
            LoadListCommand = new DelegateCommand(async () =>
            {
                Destinations = new ObservableCollection<Picture>(await apiBingSearch.GetBingPicture());
                StartSlideShow();
            });
            LoadListCommand.Execute();
            AddNewImageCommand = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(new Uri($"{ConfigPage.AddImagePage}",UriKind.Relative));
            });
            MyPictures = Setting.GetPictureCache(nameof(MyPictures));
            GoToTranslateCommand = new DelegateCommand<string>(async (input) =>
            {
                var param = new NavigationParameters
                {
                    { ConfigPage.TranslatePage, input }
                };
                await navigationService.NavigateAsync(new Uri($"{ConfigPage.TranslatePage}",UriKind.Relative),param);
            });

        }
        public void StartSlideShow()
        {
            if (Destinations.Count > 0)
            {
                CurrentPicture = Destinations[0];
                slideTime.Elapsed += (o, a) =>
                {
                    var currentIndex = Destinations.IndexOf(CurrentPicture);
                    if (currentIndex == Destinations.Count - 1)
                    {
                        CurrentPicture = Destinations[0];
                    }
                    else
                    {
                        CurrentPicture = Destinations[currentIndex + 1];
                    }
                };
            }
            slideTime.Start();

        }
        public void StopSlideShow()
        {
            slideTime.Stop();
        }


        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(nameof(Picture)))
            {
                var param = parameters[nameof(Picture)] as Picture;
                MyPictures.Add(param);
            }
;
        }
    }
}
