using System.Collections.Generic;
using System.Linq;
using Blog.Mobile.Components;
using Blog.Mobile.Models.Shared;
using Xamarin.Forms;

namespace Blog.Mobile.Helpers
{
	public static class Controls
	{
		public static Label CreateLabel(string text)
		{
			var label = new Label
			{
				Text = text
			};

			return label;
		}

		public static CustomTextEntry CreateTextEntry(string binding)
		{
			var entry = new CustomTextEntry();
			entry.SetBinding(Entry.TextProperty, binding);

			return entry;
		}

		public static CustomDatePicker CreateDatePicker(string binding)
		{
			var datePicker = new CustomDatePicker
			{
				Format = "MM/dd/yyyy",
				VerticalOptions = LayoutOptions.CenterAndExpand
			};
			datePicker.SetBinding(DatePicker.DateProperty, binding);

			return datePicker;
		}

		public static CustomPicker CreatePicker(IEnumerable<KeyValueItem> itemsSrc)
		{
			var keyValueItems = itemsSrc as KeyValueItem[] ?? itemsSrc.ToArray();

			var picker = new CustomPicker
			{
                // ReSharper disable once ImpureMethodCallOnReadonlyValueField
				BackgroundColor = Color.White.ToFormsColor(),
				ItemsSource = keyValueItems,
				SelectedItem = keyValueItems.FirstOrDefault(),
				HeightRequest = 35
			};

			return picker;
		}
	}
}

