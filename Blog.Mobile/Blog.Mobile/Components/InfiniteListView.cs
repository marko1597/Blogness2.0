using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace Blog.Mobile.Components
{
	public class InfiniteListView : ListView
	{
		public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create<InfiniteListView, ICommand>(bp => bp.LoadMoreCommand, default(ICommand));

		public ICommand LoadMoreCommand
		{
			get { return (ICommand) GetValue(LoadMoreCommandProperty); }
			set { SetValue(LoadMoreCommandProperty, value); }
		}

		public InfiniteListView()
		{
			ItemAppearing += InfiniteListViewItemAppearing;
		}

		void InfiniteListViewItemAppearing(object sender, ItemVisibilityEventArgs e)
		{
			var items = ItemsSource as IList;

			if (items != null && e.Item == items [items.Count - 1]) 
			{
				if (LoadMoreCommand != null && LoadMoreCommand.CanExecute (null))
					LoadMoreCommand.Execute (null);
			} 
		}
	}
}

