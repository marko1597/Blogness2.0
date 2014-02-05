using Blog.Backend.Services.BlogService.Contracts.ViewModels;
using System.Collections.Generic;

namespace Blog.Frontend.Web.Models
{
    public class MediaListView
    {
        public List<UserMediaGroup> Files { get; set; }
        public string CKEditorFuncNum { get; set; }
    }
}