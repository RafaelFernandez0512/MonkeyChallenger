using MonkeyChallenger.Helpers;
using MonkeyChallenger.Models;
using MonkeyChallenger.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MonkeyChallenger.ViewModels
{
    public class AddImagePageViewModel:BaseViewModel
    {
        public DelegateCommand AddPictureCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public bool IsVisible { get; set; } = true;
        public ObservableCollection<Picture> MyPictures { get; set; } = new ObservableCollection<Picture>();
        public Picture Picture { get; set; } 
       
        IAPIComputerVision computerVision;
        public bool IsPosting { get; set; }
        public string Photo { get; set; }
        public Color Color { get; set; } = Color.SteelBlue;
        public AddImagePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IAPIComputerVision computerVision) : base(navigationService, dialogService)
        {
            Picture = new Picture();
            this.computerVision = computerVision;
            AddPictureCommand = new DelegateCommand(async () => await GetPicture());
            CloseCommand = new DelegateCommand(async () => await navigationService.GoBackAsync());
            SaveCommand = new DelegateCommand(async () => await SavePicture(Picture));
        }
        async Task GetPicture() {
            const string takePhoto = "Take photo";
            const string ChossePhoto = "Chosse photo";
            MediaFile file =null;
            var dialog = await dialogService.DisplayActionSheetAsync("What do you want?",null, "Cancel", takePhoto, ChossePhoto);
            switch (dialog)
            {
                case takePhoto:
                    {
                         file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { PhotoSize = PhotoSize.Medium });
                        Picture.Image = file.Path;
                        Photo = file.Path;
                        IsVisible = false;
                    }
                   
                    break;
                case ChossePhoto:
                    {
                         file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Medium });
                        Picture.Image = file.Path;
                        Photo = file.Path;
                        IsVisible = false;
                    }
                    break;
                default:
                    break;  
            }
            if(Picture.Image!=null)
            await PostImage(file.GetStreamWithImageRotatedForExternalStorage());
        }
        async Task SavePicture(Picture picture)
        {
            if (picture.Image!=null)
            {
                var list = Setting.GetPictureCache(nameof(MyPictures));
                MyPictures = list != null ? list : new ObservableCollection<Picture>();
                MyPictures.Add(picture);
                Setting.SavePictureCache(nameof(MyPictures), MyPictures);
                var param = new NavigationParameters
                {
                    { nameof(Picture), Picture }
                };
                await navigationService.GoBackAsync(param);
            }
       
        }
        async Task PostImage(Stream image)
        {
            IsPosting = true;
            try
            {
                var result = await computerVision.AnalyzePicture(image);
                Color = result.TypeColor;
                Picture.Title = result.Description;
                if (!string.IsNullOrWhiteSpace(result.LandmarkDescription))
                    Picture.Title += $". {result.LandmarkDescription}";
            }
            catch (Exception)
            {

            }
            finally
            {
                IsPosting = false;
            }

        }
    }
}
