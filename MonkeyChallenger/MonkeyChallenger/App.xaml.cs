using MonkeyChallenger.Helpers;
using MonkeyChallenger.Services;
using MonkeyChallenger.ViewModels;
using MonkeyChallenger.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonkeyChallenger
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }
        protected override void OnInitialized()
        {

            InitializeComponent();
            NavigationService.NavigateAsync(new Uri($"{ConfigPage.MainPage}"));

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<AddImagePage>();
            containerRegistry.RegisterForNavigation<TranslatePage, TranslatePageViewModel>();
            containerRegistry.RegisterInstance<IAPITranslateService>(new APITranslateService());
            containerRegistry.RegisterInstance<IApiBingSearch>(new APIBingSearch());
            containerRegistry.RegisterInstance<IAPIComputerVision>(new APIComputerVision());
        }
    }
}
