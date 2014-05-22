using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Mocks
{
    public class PostMock : IPosts
    {
        public Post GetPost(int postId)
        {
            var post = DataStorage.Posts.FirstOrDefault(a => a.PostId == postId);
            return post;
        }

        public List<Post> GetPostsByTag(string tagName)
        {
            var posts = new List<Post>();
            var tag = DataStorage.Tags.FirstOrDefault(a => a.TagName == tagName);
            var postTags = DataStorage.PostTags.FindAll(a => tag != null && a.TagId == tag.TagId);
            postTags.ForEach(a => posts.Add(DataStorage.Posts.FirstOrDefault(b => b.PostId == a.PostId)));

            return posts;
        }

        public List<Post> GetPostsByUser(int userId)
        {
            var posts = DataStorage.Posts.FindAll(a => a.User.UserId == userId);
            return posts;
        }

        public Post AddPost(Post post)
        {
            var id = DataStorage.Posts.Select(a => a.PostId).Max();
            post.PostId = id + 1;
            DataStorage.Posts.Add(post);

            return post;
        }

        public Post UpdatePost(Post post)
        {
            var id = DataStorage.Posts.Select(a => a.PostId).Max();
            post.PostId = id + 1;
            DataStorage.Posts.Add(post);

            return post;
        }
        
        public bool DeletePost(int postId)
        {
            var tPost = DataStorage.Posts.FirstOrDefault(a => a.PostId == postId);
            DataStorage.Posts.Remove(tPost);

            return true;
        }
    }
}
