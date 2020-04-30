using MonkeyCache.FileStore;
using MonkeyChallenger.Models;
using MonkeyChallenger.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MonkeyChallenger.Helpers
{
    public static class Setting
    {
        public static void SavePictureCache(string key,ObservableCollection<Picture>data)
        {
            Barrel.ApplicationId = ConfigApi.CacheChallenger;
            Barrel.Current.Add(key: key, data: data, expireIn: TimeSpan.FromDays(30));
        }
        public static ObservableCollection<Picture> GetPictureCache(string key)
        {
            Barrel.ApplicationId = ConfigApi.CacheChallenger;
            if (!Barrel.Current.Exists(key))
            {
                return new ObservableCollection<Picture>();
            }
            var list = Barrel.Current.Get<ObservableCollection<Picture>>(key: key);
            if (list!=null)
            {
                return list;
            }
            return new ObservableCollection<Picture>();
        }
    }
}
