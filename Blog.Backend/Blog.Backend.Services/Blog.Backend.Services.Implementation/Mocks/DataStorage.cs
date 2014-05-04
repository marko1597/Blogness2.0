using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Utils;

namespace Blog.Backend.Services.Implementation.Mocks
{
    public static class DataStorage
    {
        public static List<Address> Addresses = new List<Address>();
        public static List<Album> Albums = new List<Album>();
        public static List<Comment> Comments = new List<Comment>();
        public static List<CommentLike> CommentLikes = new List<CommentLike>();
        public static List<Education> Educations = new List<Education>();
        public static List<EducationType> EducationTypes = new List<EducationType>();
        public static List<Hobby> Hobbies = new List<Hobby>();
        public static List<Media> Media = new List<Media>();
        public static List<Post> Posts = new List<Post>();
        public static List<PostContent> PostContents = new List<PostContent>();
        public static List<PostLike> PostLikes = new List<PostLike>();
        public static List<Session> Sessions = new List<Session>();
        public static List<Tag> Tags = new List<Tag>();
        public static List<PostTag> PostTags = new List<PostTag>();
        public static List<User> Users = new List<User>();

        public static void LoadMockData()
        {
            LoadUsers();
            LoadAddress();
            LoadEducationType();
            LoadEducation();
            LoadHobbies();
            LoadAlbums();
            LoadMedia();
            LoadTags();
            LoadPosts();
            LoadPostContents();
            LoadPostLikes();
            LoadComments();
            LoadCommentLikes();
        }

        private static void LoadUsers()
        {
            Users.Add(new User
            {
                UserId = 1,
                FirstName = "Jason",
                LastName = "Magpantay",
                UserName = "jama",
                Password = "testtest1",
                EmailAddress = "jason.magpantay@gmail.com",
                BirthDate = DateTime.UtcNow.AddYears(-25)
            });
            Users.Add(new User
            {
                UserId = 2,
                FirstName = "Jason",
                LastName = "Avel",
                UserName = "jaav",
                Password = "testtest1",
                EmailAddress = "jason.avel@gmail.com",
                BirthDate = DateTime.UtcNow.AddYears(-25)
            });
            Users.Add(new User
            {
                UserId = 3,
                FirstName = "Avel",
                LastName = "Magpantay",
                UserName = "avma",
                Password = "testtest1",
                EmailAddress = "avel.magpantay@gmail.com",
                BirthDate = DateTime.UtcNow.AddYears(-25)
            });
        }

        private static void LoadAddress()
        {
            var addressId = 1;

            Users.ForEach(a =>
                                    {
                                        Addresses.Add(new Address
                                        {
                                            AddressId = addressId,
                                            UserId = a.UserId,
                                            StreetAddress = "Street Address",
                                            City = "City",
                                            State = "State",
                                            Country = "Country",
                                            Zip = 1234
                                        });
                                        addressId++;
                                    });
            
        }
        private static void LoadEducationType()
        {
            EducationTypes.Add(new EducationType
            {
                EducationTypeId = 1,
                EducationTypeName = "Grade School"
            });

            EducationTypes.Add(new EducationType
            {
                EducationTypeId = 2,
                EducationTypeName = "High School"
            });

            EducationTypes.Add(new EducationType
            {
                EducationTypeId = 3,
                EducationTypeName = "College Education"
            });

            EducationTypes.Add(new EducationType
            {
                EducationTypeId = 4,
                EducationTypeName = "Post Graduate"
            });
        }

        private static void LoadEducation()
        {
            var educationId = 1;

            foreach (var u in Users)
            {
                Educations.Add(new Education
                {
                    EducationId = educationId,
                    UserId = u.UserId,
                    SchoolName = "Grade School",
                    EducationType = EducationTypes.Find(a => a.EducationTypeId == 1),
                    City = "City",
                    State = "State",
                    Country = "Country",
                    YearAttended = DateTime.UtcNow.AddYears(-20),
                    YearGraduated = DateTime.UtcNow.AddYears(-14),
                    Course = string.Empty,
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow
                });
                educationId++;

                Educations.Add(new Education
                {
                    EducationId = educationId,
                    UserId = u.UserId,
                    SchoolName = "High School",
                    EducationType = EducationTypes.Find(a => a.EducationTypeId == 2),
                    City = "City",
                    State = "State",
                    Country = "Country",
                    YearAttended = DateTime.UtcNow.AddYears(-14),
                    YearGraduated = DateTime.UtcNow.AddYears(-8),
                    Course = string.Empty,
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow
                });
                educationId++;

                Educations.Add(new Education
                {
                    EducationId = educationId,
                    UserId = u.UserId,
                    SchoolName = "College Education",
                    EducationType = EducationTypes.Find(a => a.EducationTypeId == 3),
                    City = "City",
                    State = "State",
                    Country = "Country",
                    YearAttended = DateTime.UtcNow.AddYears(-8),
                    YearGraduated = DateTime.UtcNow.AddYears(-4),
                    Course = "BS Computer Science",
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow
                });
                educationId++;
            }
        }

        private static void LoadHobbies()
        {
            var hobbyId = 1;

            foreach (var u in Users)
            {
                Hobbies.Add(new Hobby
                {
                    HobbyId = hobbyId,
                    HobbyName = "Reading",
                    UserId = u.UserId,
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow
                });
                hobbyId++;
            }
        }

        private static void LoadAlbums()
        {
            var albumId = 1;

            foreach (var u in Users)
            {
                Albums.Add(new Album
                {
                    AlbumId = albumId,
                    AlbumName = "Stuff",
                    User = u,
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow,
                    IsUserDefault = false
                });
                albumId++;

                Albums.Add(new Album
                {
                    AlbumId = albumId,
                    AlbumName = "Miscellaneous",
                    User = u,
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow,
                    IsUserDefault = true
                });
                albumId++;
            }
        }

        private static void LoadMedia()
        {
            var localIpAddress = string.Empty;
            var mediaId = 1;

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork))
            {
                localIpAddress = ip.ToString();
                break;
            }

            foreach (var u in Users)
            {
                var u1 = u;
                var albums = Albums.FindAll(a => a.User.UserId == u1.UserId);

                for (var i = 1; i < 6; i++)
                {
                    var albumName = i < 4 ? "Stuff" : "Miscellaneous";
                    var mediaPath = "C:\\SampleImages\\" + u.UserId + "\\" + albumName + "\\" + i + ".jpg";
                    var tnPath = "C:\\SampleImages\\" + u.UserId + "\\" + albumName + "\\tn\\" + i + ".jpg";
                    var image = Image.FromFile(mediaPath);
                    var customName = Guid.NewGuid().ToString();

                    if (i < 4)
                    {
                        Media.Add(new Media
                        {
                            MediaId = mediaId,
                            CustomName = customName,
                            CreatedBy = u.UserId,
                            CreatedDate = DateTime.UtcNow,
                            ModifiedBy = u.UserId,
                            ModifiedDate = DateTime.UtcNow,
                            AlbumId = albums[0].AlbumId,
                            FileName = i + ".jpg",
                            MediaUrl = string.Format("https://{0}/blogapi/api/media/{1}", localIpAddress, customName),
                            MediaType = "image/jpeg",
                            MediaPath = mediaPath,
                            MediaContent = new ImageHelper().ImageToByteArray(image),
                            ThumbnailUrl = string.Format("https://{0}/blogapi/api/media/thumbnail/{1}", localIpAddress, customName),
                            ThumbnailPath = tnPath,
                            ThumbnailContent = new ImageHelper().CreateThumbnail(mediaPath)
                        });
                    }
                    else
                    {
                        Media.Add(new Media
                        {
                            MediaId = mediaId,
                            CustomName = customName,
                            CreatedBy = u.UserId,
                            CreatedDate = DateTime.UtcNow,
                            ModifiedBy = u.UserId,
                            ModifiedDate = DateTime.UtcNow,
                            AlbumId = albums[1].AlbumId,
                            FileName = i + ".jpg",
                            MediaUrl = string.Format("https://{0}/blogapi/api/media/{1}", localIpAddress, customName),
                            MediaType = "image/jpeg",
                            MediaPath = mediaPath,
                            MediaContent = new ImageHelper().ImageToByteArray(image),
                            ThumbnailUrl = string.Format("https://{0}/blogapi/api/media/thumbnail/{1}", localIpAddress, customName),
                            ThumbnailPath = tnPath,
                            ThumbnailContent = new ImageHelper().CreateThumbnail(mediaPath)
                        });
                    }

                    mediaId++;
                }
            }
        }

        private static void LoadTags()
        {
            Tags.Add(new Tag
            {
                CreatedBy = 1,
                CreatedDate = DateTime.UtcNow,
                ModifiedBy = 1,
                ModifiedDate = DateTime.UtcNow,
                TagId = 1,
                TagName = "lorem"
            });
            Tags.Add(new Tag
            {
                CreatedBy = 2,
                CreatedDate = DateTime.UtcNow,
                ModifiedBy = 2,
                ModifiedDate = DateTime.UtcNow,
                TagId = 2,
                TagName = "ipsum"
            });
            Tags.Add(new Tag
            {
                CreatedBy = 3,
                CreatedDate = DateTime.UtcNow,
                ModifiedBy = 3,
                ModifiedDate = DateTime.UtcNow,
                TagId = 3,
                TagName = "dolor"
            });

            foreach (var p in Posts)
            {
                Tags.ForEach(t => PostTags.Add(new PostTag
                {
                    PostId = p.PostId,
                    TagId = t.TagId
                }));
            }
        }

        private static void LoadPosts()
        {
            var postId = 1;

            foreach (var u in Users)
            {
                for (var j = 1; j < 6; j++)
                {
                    Posts.Add(new Post
                    {
                        CreatedBy = u.UserId,
                        CreatedDate = DateTime.UtcNow.AddHours(-j),
                        ModifiedBy = u.UserId,
                        ModifiedDate = DateTime.UtcNow.AddHours(-j),
                        PostMessage = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        PostTitle = "Post Title",
                        User = u,
                        PostId = postId,
                        Tags = Tags
                    });
                    postId++;
                }
            }
        }

        private static void LoadPostContents()
        {
            var postContentId = 1;

            foreach (var p in Posts)
            {
                var p1 = p;
                var m = Media.FirstOrDefault(a => a.MediaId == p1.PostId);

                PostContents.Add(new PostContent
                {
                    CreatedBy = p.User.UserId,
                    CreatedDate = p.CreatedDate,
                    ModifiedBy = p.User.UserId,
                    ModifiedDate = p.ModifiedDate,
                    PostId = p.PostId,
                    Media = m,
                    PostContentId = postContentId
                });
                postContentId++;
            }
        }

        private static void LoadPostLikes()
        {
            var postLikeId = 1;

            foreach (var p in PostLikes)
            {
                for (var i = 1; i < 4; i++)
                {
                    PostLikes.Add(new PostLike
                    {
                        CreatedBy = i,
                        CreatedDate = DateTime.UtcNow.AddHours(-i),
                        ModifiedBy = i,
                        ModifiedDate = DateTime.UtcNow.AddHours(-i),
                        PostId = p.PostId,
                        UserId = i,
                        PostLikeId = postLikeId
                    });
                    postLikeId++;
                }
            }
        }

        private static void LoadComments()
        {
            var commentId = 1;

            foreach (var p in Posts)
            {
                for (var i = 1; i < 4; i++)
                {
                    Comments.Add(new Comment
                    {
                        CommentId = commentId,
                        CommentMessage = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit.",
                        PostId = p.PostId,
                        ParentCommentId = null,
                        CreatedBy = i,
                        CreatedDate = DateTime.UtcNow.AddHours(-i),
                        ModifiedBy = p.User.UserId,
                        ModifiedDate = DateTime.UtcNow.AddHours(-i),
                        User = Users.FirstOrDefault(a => a.UserId == i),
                        CommentLocation = "Makati City, Philippines"
                    });
                    commentId++;
                }
            }
        }

        private static void LoadCommentLikes()
        {
            var commentLikeId = 1;

            foreach (var p in CommentLikes)
            {
                for (var i = 1; i < 4; i++)
                {
                    CommentLikes.Add(new CommentLike
                    {
                        CreatedBy = i,
                        CreatedDate = DateTime.UtcNow.AddHours(-i),
                        ModifiedBy = i,
                        ModifiedDate = DateTime.UtcNow.AddHours(-i),
                        CommentId = p.CommentId,
                        UserId = i,
                        CommentLikeId = commentLikeId
                    });
                    commentLikeId++;
                }
            }
        }
    }
}
