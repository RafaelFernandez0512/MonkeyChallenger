using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MonkeyChallenger.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("MonkeyChallenger")]
[assembly: ExportEffect(typeof(MonkeyChallenger.Droid.Effects.EntryDroidEffect), nameof(EntryEffects))]
namespace MonkeyChallenger.Droid.Effects
{
    public class EntryDroidEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (this.Control !=null)
            {
                this.Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }

        protected override void OnDetached()
        {
           
        }
    }
}