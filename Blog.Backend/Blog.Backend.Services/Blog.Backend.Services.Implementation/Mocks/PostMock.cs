using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation.Mocks
{
    public class PostMock : IPosts
    {
        public PostMock()
        {
            if (DataStorage.Posts.Count == 0)
            {
                var postId = 1;

                foreach (var u in DataStorage.Users)
                {
                    for (var j = 1; j < 6; j++)
                    {
                        DataStorage.Posts.Add(new Post
                        {
                            CreatedBy = u.UserId,
                            CreatedDate = DateTime.UtcNow.AddHours(-j),
                            ModifiedBy = u.UserId,
                            ModifiedDate = DateTime.UtcNow.AddHours(-j),
                            PostMessage = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                            PostTitle = "Post Title",
                            User = u,
                            PostId = postId,
                            Tags = DataStorage.Tags
                        });
                        postId++;
                    }
                }
            }
        }

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

        public bool UpdatePost(Post post)
        {
            var tPost = DataStorage.Posts.FirstOrDefault(a => a.PostId == post.PostId);
            DataStorage.Posts.Remove(tPost);
            DataStorage.Posts.Add(post);

            return true;
        }

        public bool AddPost(Post post)
        {
            var id = DataStorage.Posts.Select(a => a.PostId).Max();
            post.PostId = id + 1;
            DataStorage.Posts.Add(post);

            return true;
        }

        public bool DeletePost(int postId)
        {
            var tPost = DataStorage.Posts.FirstOrDefault(a => a.PostId == postId);
            DataStorage.Posts.Remove(tPost);

            return true;
        }
    }
}
