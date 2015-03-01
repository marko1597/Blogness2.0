namespace Blog.Mobile.ViewModels.Shared
{
	public class DrawerContentViewModel : BaseViewModel
	{
		bool _isVisible;
		public bool IsVisible
		{
			get { return _isVisible; }
			set
			{
				_isVisible = value;
				OnPropertyChanged("IsVisible");
			}
		}
	}
}
