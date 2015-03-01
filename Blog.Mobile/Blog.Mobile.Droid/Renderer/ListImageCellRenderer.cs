using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using Blog.Mobile.Components;
using Blog.Mobile.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;
using ListView = Android.Widget.ListView;
using View = Android.Views.View;

[assembly: ExportCell(typeof(ListImageCell), typeof(ListImageCellRenderer))]
namespace Blog.Mobile.Droid.Renderer
{
	public class ListImageCellRenderer : ImageCellRenderer
	{
		protected override View GetCellCore (Cell item, View convertView, ViewGroup parent, Context context)
		{
			var cell = (LinearLayout)base.GetCellCore(item, convertView, parent, context);
			cell.SetPadding(20, 30, 0, 30);
			cell.DividerPadding = 50;

			var div = new ShapeDrawable();
			div.SetIntrinsicHeight(1);
			div.Paint.Set(new Paint { Color = Color.FromHex("00FFFFFF").ToAndroid() });

			if (parent is ListView)
			{
				((ListView)parent).Divider = div;
				((ListView)parent).DividerHeight = 1;
			}

			var image = (ImageView)cell.GetChildAt(0);
			image.SetScaleType(ImageView.ScaleType.FitCenter);

			image.LayoutParameters.Width = 60;
			image.LayoutParameters.Height = 60;

			var linear = (LinearLayout)cell.GetChildAt(1);
			linear.SetGravity(GravityFlags.CenterVertical);

			var label = (TextView)linear.GetChildAt(0);
			label.SetTextColor(Color.White.ToAndroid());
			label.TextSize = Font.SystemFontOfSize(NamedSize.Medium).ToScaledPixel() * 1.25f;
			label.Gravity = (GravityFlags.CenterVertical);
			label.SetTextColor(Color.FromHex("FFFFFF").ToAndroid());
			var secondaryLabel = (TextView)linear.GetChildAt(1);
			secondaryLabel.Visibility = ViewStates.Gone;

			return cell;
		}
	}
}

