using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace Blog.Mobile.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		private string _title = "Bloggity";
		public const string TitlePropertyName = "Title";

		/// <summary>
		/// Gets or sets the "Title" property
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value, TitlePropertyName); }
		}

		private bool _isLoggedIn;
		/// <summary>
		/// Gets or sets the "IsLoggedIn" property
		/// </summary>
		public const string IsLoggedInPropertyName = "IsLoggedIn";
		public bool IsLoggedIn
		{
			get { return _isLoggedIn; }
			set { SetProperty(ref _isLoggedIn, value, SubtitlePropertyName); }
		}

		private string _subTitle = string.Empty;
		/// <summary>
		/// Gets or sets the "Subtitle" property
		/// </summary>
		public const string SubtitlePropertyName = "Subtitle";
		public string Subtitle
		{
			get { return _subTitle; }
			set { SetProperty(ref _subTitle, value, SubtitlePropertyName); }
		}

		private string _icon = "ico_gcorp.png";
		/// <summary>
		/// Gets or sets the "Icon" of the viewmodel
		/// </summary>
		public const string IconPropertyName = "Icon";
		public string Icon
		{
			get { return _icon; }
			set { SetProperty(ref _icon, value, IconPropertyName); }
		}

		private bool _isBusy;
		/// <summary>
		/// Gets or sets if the view is busy.
		/// </summary>
		public const string IsBusyPropertyName = "IsBusy";
		public bool IsBusy
		{
			get { return _isBusy; }
			set { SetProperty(ref _isBusy, value, IsBusyPropertyName); }
		}

		private bool _canLoadMore = true;
		/// <summary>
		/// Gets or sets if we can load more.
		/// </summary>
		public const string CanLoadMorePropertyName = "CanLoadMore";
		public bool CanLoadMore
		{
			get { return _canLoadMore; }
			set { SetProperty(ref _canLoadMore, value, CanLoadMorePropertyName); }
		}

		protected void SetProperty<T>(ref T backingStore, T value,
			string propertyName, Action onChanged = null, Action<T> onChanging = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return;

			if (onChanging != null)
				onChanging(value);

			OnPropertyChanging(propertyName);

			backingStore = value;

			if (onChanged != null)
				onChanged();

			OnPropertyChanged(propertyName);
		}

		#region INotifyPropertyChanging implementation
		public event PropertyChangingEventHandler PropertyChanging;
		#endregion

		public void OnPropertyChanging(string propertyName)
		{
			if (PropertyChanging == null)
				return;

			PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
		}


		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		public void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
