using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Color = Blog.Mobile.Helpers.Color;

namespace Blog.Mobile.Components
{
	public class DrawerLayout : BaseStackLayout
	{
		public DrawerLayout(ListView listView, IEnumerable<BaseScrollView> views)
		{
			ListView = listView;
			Layouts = views.ToList();
			Padding = new Thickness (0);

		    CreateDrawerLayout();
			Children.Add(GetBaseLayout ());
		}

		public static readonly BindableProperty CurrentViewProperty = BindableProperty.Create<DrawerLayout, BaseScrollView>(prop => prop.CurrentView, new BaseScrollView());

		public BaseScrollView CurrentView 
		{
			get { return (BaseScrollView)GetValue (CurrentViewProperty); }
			set { SetValue(CurrentViewProperty, value); }
		}

		private ListView ListView { get; set; }

		public List<BaseScrollView> Layouts { get; set; }

		private BaseStackLayout Drawer { get; set; }

		public BaseStackLayout GetBaseLayout()
		{
			var grid = new Grid
			{
				BackgroundColor = Color.White.ToFormsColor(),
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions =
				{
					new RowDefinition { Height = GridLength.Auto }
				},
				ColumnDefinitions =
				{
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
				}
			};

			grid.Children.Add (Drawer, 0, 1, 0, 1);
			foreach (var layout in Layouts) 
			{
				grid.Children.Add (layout, 1, 4, 0, 1);
			}

			var baseLayout = new BaseStackLayout 
			{ 
				Children = { grid },
				Padding = new Thickness(0)
			};

			return baseLayout;
		}

		void CreateDrawerLayout()
		{
			Drawer = new BaseStackLayout 
			{
				Padding = new Thickness(0)
			};

			var cell = new DataTemplate(typeof(TextCell));
			cell.SetBinding(TextCell.TextProperty, "Title");

			Drawer.Children.Add (ListView);
		}
	}
}