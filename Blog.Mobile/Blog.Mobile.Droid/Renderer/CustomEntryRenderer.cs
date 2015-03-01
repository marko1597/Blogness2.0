using Android.Util;
using Blog.Mobile.Components;
using Blog.Mobile.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer (typeof (CustomTextEntry), typeof (CustomEntryRenderer))]
namespace Blog.Mobile.Droid.Renderer
{
	public class CustomEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged (e);

			if (Control != null)
			{ 
				Control.SetTextColor(Color.DarkGray);
				Control.SetTextSize (ComplexUnitType.Dip, 15);
				Control.SetHeight (35);
			}
		}
	}
}

