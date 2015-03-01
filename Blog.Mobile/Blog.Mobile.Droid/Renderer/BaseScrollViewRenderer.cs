using System;
using Android.Views;
using Blog.Mobile.Components;
using Blog.Mobile.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BaseScrollView), typeof(BaseScrollViewRenderer))]
namespace Blog.Mobile.Droid.Renderer
{
	public class BaseScrollViewRenderer : ScrollViewRenderer
	{
		float _startX, _startY;
		int _isHorizontal = -1;

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);
			if (((ScrollView)e.NewElement).Orientation == ScrollOrientation.Horizontal) _isHorizontal = 1;

		}

		public override bool DispatchTouchEvent(MotionEvent e)
		{
			switch (e.Action)
			{
			case MotionEventActions.Down:
				_startX = e.RawX;
				_startY = e.RawY;
				Parent.RequestDisallowInterceptTouchEvent(true);
				break;
			case MotionEventActions.Move:
				if (_isHorizontal * Math.Abs(_startX - e.RawX) < _isHorizontal * Math.Abs(_startY - e.RawY))
					Parent.RequestDisallowInterceptTouchEvent(false);
				break;
			case MotionEventActions.Up:
				Parent.RequestDisallowInterceptTouchEvent(false);
				break;
			}

			return base.DispatchTouchEvent(e);
		}
	}
}

