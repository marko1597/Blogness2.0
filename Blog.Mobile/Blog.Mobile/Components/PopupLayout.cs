using Xamarin.Forms;

namespace Blog.Mobile.Components
{
	public class PopupLayout : RelativeLayout
	{
		public enum PopupLocation
		{
			Top,
			Bottom
		}

		private View _content;

		private View _popup;

		public View Content
		{
			get { return _content; }
			set
			{
				if (_content != null)
				{
					Children.Remove(_content);
				}

				_content = value;
				Children.Add(_content, () => Bounds);
			}
		}

		public bool IsPopupActive
		{
			get { return _popup != null; }
		}

		public void ShowPopup(View popupView)
		{
			ShowPopup(
				popupView,
				Constraint.RelativeToParent(p => (Width - _popup.WidthRequest) / 2),
				Constraint.RelativeToParent(p => (Height- _popup.HeightRequest) / 2)
			);
		}

		public void ShowPopup(View popupView, Constraint xConstraint, Constraint yConstraint, Constraint widthConstraint = null, Constraint heightConstraint = null)
		{
			DismissPopup();
			_popup = popupView;

			_content.InputTransparent = true;
			Children.Add(_popup, xConstraint, yConstraint, widthConstraint, heightConstraint);

			UpdateChildrenLayout();
		}

		public void ShowPopup(View popupView, View presenter, PopupLocation location, float paddingX = 0, float paddingY = 0)
		{
			DismissPopup();
			_popup = popupView;

			Constraint constraintX = null, constraintY = null;

			switch (location)
			{
			case PopupLocation.Bottom:
				constraintX = Constraint.RelativeToParent(parent => presenter.X + (presenter.Width - _popup.WidthRequest)/2);
				constraintY = Constraint.RelativeToParent(parent => parent.Y + presenter.Y + presenter.Height + paddingY);
				break;
			case PopupLocation.Top:
				constraintX = Constraint.RelativeToParent(parent => presenter.X + (presenter.Width - _popup.WidthRequest)/2);
				constraintY = Constraint.RelativeToParent(parent =>
					parent.Y + presenter.Y - _popup.HeightRequest/2 - paddingY);
				break;
			}

			ShowPopup(popupView, constraintX, constraintY);
		}

		public void DismissPopup()
		{
			if (_popup != null)
			{
				Children.Remove(_popup);
				_popup = null;
			}

			_content.InputTransparent = false;
		}
	}
}

