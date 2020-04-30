using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MonkeyChallenger.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("MonkeyChallenger")]
[assembly: ExportEffect(typeof(MonkeyChallenger.iOS.Effects.EntryIosEffect), nameof(EntryEffects))]
namespace MonkeyChallenger.iOS.Effects
{
    public class EntryIosEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (this.Control!=null)
            {
                this.Control.BackgroundColor = UIColor.White;
            }
        }

        protected override void OnDetached()
        {

        }
    }
}