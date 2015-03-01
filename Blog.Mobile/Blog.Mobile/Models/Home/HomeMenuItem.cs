using Blog.Mobile.Models.Shared;

namespace Blog.Mobile.Models.Home
{
    public class HomeMenuItem : BaseMenuItemModel
    {
        public HomeMenuItem()
        {
            MenuType = HomeMenuType.Posts;
        }
        public string Icon { get; set; }
        public HomeMenuType MenuType { get; set; }
    }
}
