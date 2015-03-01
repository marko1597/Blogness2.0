using Blog.Mobile.Components;
using Blog.Mobile.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(InfiniteListView), typeof(InfiniteListViewRenderer))]
namespace Blog.Mobile.Droid.Renderer
{
	public class InfiniteListViewRenderer : ListViewRenderer 
	{
		protected override void OnElementChanged (ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged (e);

			if (Control == null)
				return;

			Control.Divider.SetVisible (false, false);
			Control.Divider = null;
			Control.DividerHeight = 0;
		}
	}
}

