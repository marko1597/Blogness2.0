using System;
using System.Linq;
using Blog.Mobile.Components;
using Blog.Mobile.Models.Home;
using Blog.Mobile.ViewModels.Home;
using Blog.Mobile.Views.Posts;
using Xamarin.Forms;
using Color = Blog.Mobile.Helpers.Color;

namespace Blog.Mobile.Views.Home
{
    public class HomeMasterView : BaseContentPage
    {
        public Action<HomeMenuType> PageSelectionChanged;
        private HomeMenuType _menuType = HomeMenuType.Posts;

        private Page _pageSelection;
        public Page PageSelection
        {
            get { return _pageSelection; }
            set
            {
                _pageSelection = value;
                if (PageSelectionChanged != null)
                    PageSelectionChanged(_menuType);
            }
        }

        public ListView ListView { get; set; }

        private PostsListView _postsListView;

        public HomeMasterView(HomeViewModel viewModel)
        {
            Icon = "slideout.png";
            BindingContext = viewModel;
            
			var layout = new BaseStackLayout 
			{ 
				BackgroundColor = Color.DarkGray.ToFormsColor(),
				Spacing = 0,
				Padding = new Thickness(0)
			};

            #region Add list view to layout

			var cell = new DataTemplate(typeof(ListImageCell));
			cell.SetBinding(TextCell.TextProperty, "Title");
			cell.SetBinding(ImageCell.ImageSourceProperty, "Icon");

            ListView = new ListView
			{
				BackgroundColor = Color.DarkGray.ToFormsColor(),
				ItemTemplate = cell,
				ItemsSource = viewModel.MenuItems
			};

            // Set default selected item to settings view
            if (_postsListView == null)
                _postsListView = new PostsListView();

            PageSelection = _postsListView;

            //Change to the correct page
            ListView.ItemSelected += ListItemSelected;
			ListView.SelectedItem = viewModel.MenuItems.FirstOrDefault(a => a.MenuType == HomeMenuType.Posts);
            layout.Children.Add(ListView);
            
            #endregion

            Content = layout;
        }
        
        void ListItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var menuItem = ListView.SelectedItem as HomeMenuItem;
            if (menuItem == null) return;

            _menuType = menuItem.MenuType;
            switch (menuItem.MenuType)
            {
                case HomeMenuType.Posts:
                    if (_postsListView == null)
                        _postsListView = new PostsListView();
                    PageSelection = _postsListView;
                    break;
            }
        }
    }
}
