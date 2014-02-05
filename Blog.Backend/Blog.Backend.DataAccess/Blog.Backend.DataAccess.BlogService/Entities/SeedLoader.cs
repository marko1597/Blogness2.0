using System;
using System.Drawing;
using System.Linq;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.DataAccess.BlogService.Entities
{
    public static class SeedLoader
    {
        public static void InitializeSeedData()
        {
            LoadUsers();
            LoadAddress();
            LoadEducationType();
            LoadEducation();
            LoadHobbies();
            LoadTags();
            LoadPosts();
            LoadPostTags();
            LoadComments();
            LoadPostLikes();
            LoadCommentLikes();
            LoadMediaGroup();
            LoadMedia();
            LoadPostContents();
        }

        private static void LoadUsers()
        {
            SeedEntities.USERS.Add(new User
            {
                FirstName = "Jason",
                LastName = "Magpantay",
                UserId = 1,
                UserName = "jama",
                Password = "testtest1",
                EmailAddress = "jason.magpantay@gmail.com",
                BirthDate = DateTime.UtcNow.AddYears(-25)
            });
            SeedEntities.USERS.Add(new User
            {
                FirstName = "Jason",
                LastName = "Avel",
                UserId = 2,
                UserName = "jaav",
                Password = "testtest1",
                EmailAddress = "jason.avel@gmail.com",
                BirthDate = DateTime.UtcNow.AddYears(-25)
            });
            SeedEntities.USERS.Add(new User
            {
                FirstName = "Avel",
                LastName = "Magpantay",
                UserId = 3,
                UserName = "avma",
                Password = "testtest1",
                EmailAddress = "avel.magpantay@gmail.com",
                BirthDate = DateTime.UtcNow.AddYears(-25)
            });
        }

        private static void LoadAddress()
        {
            var addId = 0;

            foreach (var u in SeedEntities.USERS)
            {
                addId++;
                SeedEntities.ADDRESS.Add(new Address
                {
                    AddressId = addId,
                    UserId = u.UserId,
                    StreetAddress = "Street Address",
                    City = "City",
                    State = "State",
                    Country = "Country",
                    Zip = 1234
                });
            }
        }

        private static void LoadEducationType()
        {
            SeedEntities.EDUCATIONTYPE.Add(new EducationType
            {
                EducationTypeId = 1,
                EducationTypeName = "Grade School"
            });

            SeedEntities.EDUCATIONTYPE.Add(new EducationType
            {
                EducationTypeId = 2,
                EducationTypeName = "High School"
            });

            SeedEntities.EDUCATIONTYPE.Add(new EducationType
            {
                EducationTypeId = 3,
                EducationTypeName = "College Education"
            });

            SeedEntities.EDUCATIONTYPE.Add(new EducationType
            {
                EducationTypeId = 3,
                EducationTypeName = "Post Graduate"
            });
        }

        private static void LoadEducation()
        {
            var educId = 0;

            foreach (var u in SeedEntities.USERS)
            {
                educId++;
                var ed1 = new Education
                {
                    EducationId = educId,
                    EducationTypeId = 1,
                    EducationType = SeedEntities.EDUCATIONTYPE.FirstOrDefault(t => t.EducationTypeId == 1),
                    UserId = u.UserId,
                    SchoolName = "Grade School",
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
                };

                educId++;
                var ed2 = new Education
                {
                    EducationId = educId,
                    EducationTypeId = 2,
                    EducationType = SeedEntities.EDUCATIONTYPE.FirstOrDefault(t => t.EducationTypeId == 1),
                    UserId = u.UserId,
                    SchoolName = "High School",
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
                };

                educId++;
                var ed3 = new Education
                {
                    EducationId = educId,
                    EducationTypeId = 3,
                    EducationType = SeedEntities.EDUCATIONTYPE.FirstOrDefault(t => t.EducationTypeId == 1),
                    UserId = u.UserId,
                    SchoolName = "College Education",
                    City = "City",
                    State = "State",
                    Country = "Country",
                    YearAttended = DateTime.UtcNow.AddYears(-8),
                    YearGraduated = DateTime.UtcNow.AddYears(-4),
                    Course = string.Empty,
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow
                };

                SeedEntities.EDUCATION.Add(ed1);
                SeedEntities.EDUCATION.Add(ed2);
                SeedEntities.EDUCATION.Add(ed3);
            }
        }

        private static void LoadHobbies()
        {
            var hobId = 0;

            foreach (var u in SeedEntities.USERS)
            {
                hobId++;
                var hob1 = new Hobby
                {
                    HobbyId = hobId,
                    HobbyName = "Games",
                    UserId = u.UserId,
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow
                };

                hobId++;
                var hob2 = new Hobby
                {
                    HobbyId = hobId,
                    HobbyName = "Movies",
                    UserId = u.UserId,
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow
                };

                hobId++;
                var hob3 = new Hobby
                {
                    HobbyId = hobId,
                    HobbyName = "Reading",
                    UserId = u.UserId,
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow
                };

                SeedEntities.HOBBIES.Add(hob1);
                SeedEntities.HOBBIES.Add(hob2);
                SeedEntities.HOBBIES.Add(hob3);
            }
        }

        private static void LoadTags()
        {
            SeedEntities.TAGS.Add(new Tag
            {
                CreatedBy = 1,
                CreatedDate = DateTime.UtcNow,
                ModifiedBy = 1,
                ModifiedDate = DateTime.UtcNow,
                TagId = 1,
                TagName = "lorem"
            });
            SeedEntities.TAGS.Add(new Tag
            {
                CreatedBy = 2,
                CreatedDate = DateTime.UtcNow,
                ModifiedBy = 2,
                ModifiedDate = DateTime.UtcNow,
                TagId = 2,
                TagName = "ipsum"
            });
            SeedEntities.TAGS.Add(new Tag
            {
                CreatedBy = 3,
                CreatedDate = DateTime.UtcNow,
                ModifiedBy = 3,
                ModifiedDate = DateTime.UtcNow,
                TagId = 3,
                TagName = "dolor"
            });
        }

        private static void LoadPosts()
        {
            var ctrPost = 1;
            for (var i = 1; i < 4; i++)
            {
                for (var j = 1; j < 6; j++)
                {
                    var tuser = SeedEntities.USERS.FirstOrDefault(a => a.UserId == i);
                    if (tuser != null)
                    {
                        var user = new User
                                       {
                                           UserId = tuser.UserId,
                                           UserName = tuser.UserName,
                                           Password = tuser.Password,
                                           FirstName = tuser.FirstName,
                                           LastName = tuser.LastName,
                                           EmailAddress = tuser.EmailAddress
                                       };

                        SeedEntities.POSTS.Add(new Post
                                                   {
                                                       CreatedBy = i,
                                                       CreatedDate = DateTime.UtcNow.AddHours(-i),
                                                       ModifiedBy = i,
                                                       ModifiedDate = DateTime.UtcNow.AddHours(-i),
                                                       PostId = ctrPost,
                                                       PostMessage = ctrPost + ".) Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                                       PostTitle = "Post Title " + ctrPost,
                                                       User = user,
                                                       UserId = i
                                                   });
                    }
                    ctrPost++;
                }
            }
        }

        private static void LoadPostTags()
        {
            var ctrPostTag = 1;
            foreach (var p in SeedEntities.POSTS)
            {
                foreach (var t in SeedEntities.TAGS)
                {
                    SeedEntities.POSTTAGS.Add(new PostTag
                    {
                        PostTagId = ctrPostTag,
                        PostId = p.PostId,
                        TagId = t.TagId,
                        CreatedBy = p.User.UserId,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedBy = p.User.UserId,
                        ModifiedDate = DateTime.UtcNow,
                    });
                    ctrPostTag++;
                }
            }
        }

        private static void LoadPostLikes()
        {
            var ctrPostLike = 1;
            var posts = SeedEntities.POSTS;

            foreach (var p in posts)
            {
                for (var i = 1; i < 4; i++)
                {
                    SeedEntities.POSTLIKES.Add(new PostLike
                    {
                        CreatedBy = i,
                        CreatedDate = DateTime.UtcNow.AddHours(-i),
                        ModifiedBy = i,
                        ModifiedDate = DateTime.UtcNow.AddHours(-i),
                        PostId = p.PostId,
                        UserId = i,
                        PostLikeId = ctrPostLike
                    });
                    ctrPostLike++;
                }

                p.PostLikes = SeedEntities.POSTLIKES.Where(a => a.PostId == p.PostId).ToList();
            }
        }

        private static void LoadComments()
        {
            int ctr = 1;
            foreach (var post in SeedEntities.POSTS)
            {
                for (int i = 1; i < 4; i++)
                {
                    var comment = new Comment
                    {
                        CommentId = ctr,
                        CommentMessage = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit.",
                        PostId = post.PostId,
                        ParentCommentId = null,
                        CreatedBy = i,
                        CreatedDate = DateTime.UtcNow.AddHours(-i),
                        ModifiedBy = post.User.UserId,
                        ModifiedDate = DateTime.UtcNow.AddHours(-i),
                        UserId = i,
                        CommentLocation = "Makati City, Philippines"
                    };
                    SeedEntities.COMMENTS.Add(comment);
                    ctr++;
                }
            }
        }

        private static void LoadCommentLikes()
        {
            var ctrCommentLike = 1;
            var comments = SeedEntities.COMMENTS;

            foreach (var c in comments)
            {
                for (var i = 1; i < 4; i++)
                {
                    SeedEntities.COMMENTLIKES.Add(new CommentLike
                    {
                        CreatedBy = i,
                        CreatedDate = DateTime.UtcNow.AddHours(i),
                        ModifiedBy = i,
                        ModifiedDate = DateTime.UtcNow.AddHours(i),
                        CommentId = c.CommentId,
                        UserId = i,
                        CommentLikeId = ctrCommentLike
                    });
                    ctrCommentLike++;
                }
            }
        }

        private static void LoadMediaGroup()
        {
            var mediaGroupId = 1;

            foreach (var u in SeedEntities.USERS)
            {
                SeedEntities.MEDIAGROUP.Add(new MediaGroup
                {
                    MediaGroupId = mediaGroupId,
                    MediaGroupName = "Stuff",
                    UserId = u.UserId,
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow,
                    IsUserDefault = false
                });
                mediaGroupId++;

                SeedEntities.MEDIAGROUP.Add(new MediaGroup
                {
                    MediaGroupId = mediaGroupId,
                    MediaGroupName = "Miscellaneous",
                    UserId = u.UserId,
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow,
                    IsUserDefault = true
                });
                mediaGroupId++;
            }
        }

        private static void LoadMedia()
        {
            var mediaId = 1;

            foreach (var u in SeedEntities.USERS)
            {
                for (int i = 1; i < 6; i++)
                {
                    var mediaPath = "C:\\SampleImages\\" + u.UserId + "\\" + i + ".jpg";
                    var tnPath = "C:\\SampleImages\\" + u.UserId + "\\tn\\" + i + ".jpg";
                    var image = Image.FromFile(mediaPath);

                    if (i < 4)
                    {
                        SeedEntities.MEDIA.Add(new Media
                        {
                            CreatedBy = u.UserId,
                            CreatedDate = DateTime.UtcNow,
                            ModifiedBy = u.UserId,
                            ModifiedDate = DateTime.UtcNow,
                            MediaId = mediaId,
                            MediaGroupId = 1,
                            FileName = i + ".jpg",
                            ExternalUrl = "http://localhost/blogapi/api/media/getmediaitem?mediaId=" + mediaId,
                            MediaType = "image/jpeg",
                            UserId = u.UserId,
                            MediaPath = mediaPath,
                            MediaContent = Common.BlogService.Utils.ImageToByteArray(image),
                            ThumbnailPath = tnPath,
                            ThumbnailContent = Common.BlogService.Utils.CreateThumbnail(mediaPath)
                        });
                    }
                    else
                    {
                        SeedEntities.MEDIA.Add(new Media
                        {
                            CreatedBy = u.UserId,
                            CreatedDate = DateTime.UtcNow,
                            ModifiedBy = u.UserId,
                            ModifiedDate = DateTime.UtcNow,
                            MediaId = mediaId,
                            MediaGroupId = 2,
                            FileName = i + ".jpg",
                            ExternalUrl = "http://localhost/blogapi/api/media/getmediaitem?mediaId=" + mediaId,
                            MediaType = "image/jpeg",
                            UserId = u.UserId,
                            MediaPath = mediaPath,
                            MediaContent = Common.BlogService.Utils.ImageToByteArray(image),
                            ThumbnailPath = tnPath,
                            ThumbnailContent = Common.BlogService.Utils.CreateThumbnail(mediaPath)
                        });
                    }

                    mediaId++;
                }
            }
        }

        private static void LoadPostContents()
        {
            var ctrPostContent = 1;

            foreach (var p in SeedEntities.POSTS)
            {
                SeedEntities.POSTCONTENT.Add(new PostContent
                {
                    CreatedBy = p.User.UserId,
                    CreatedDate = p.CreatedDate,
                    ModifiedBy = p.User.UserId,
                    ModifiedDate = p.ModifiedDate,
                    PostContentId = ctrPostContent,
                    PostId = p.PostId,
                    Media = SeedEntities.MEDIA.FirstOrDefault(m => m.MediaId == ctrPostContent),
                    MediaId = ctrPostContent
                });

                ctrPostContent++;
            }
        }
    }
}
