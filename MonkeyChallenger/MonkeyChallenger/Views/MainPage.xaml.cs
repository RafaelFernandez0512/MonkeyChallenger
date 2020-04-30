using MonkeyChallenger.Helpers;
using MonkeyChallenger.ViewModels;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MonkeyChallenger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private  MainPageViewModel _mainPageViewModel;
        public MainPage()
        {
            InitializeComponent();
        }
        protected async override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext is MainPageViewModel mainPageViewModel)
                _mainPageViewModel = mainPageViewModel;
        }
         private void Entry_Completed(object sender, EventArgs e)
         {

            if (!string.IsNullOrEmpty(TextEntry.Text))
            {
                _mainPageViewModel.GoToTranslateCommand.Execute(TextEntry.Text);
            }
        }
    }
}