using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Windows.Forms;
using Blog.Backend.DataAccess.Entities.Objects;

namespace Blog.Backend.DataAccess.Seed
{
    public partial class Main : Form
    {
        private List<User> _users = new List<User>();
        private List<Post> _posts = new List<Post>();
        private List<Comment> _comments = new List<Comment>();
        private List<Tag> _tags = new List<Tag>();
        private readonly string _localIpAddress = string.Empty;

        public Main()
        {
            InitializeComponent();

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork))
            {
                _localIpAddress = ip.ToString();
                break;
            }
        }

        private void AddConsoleMessage(string message)
        {
            TxtConsole.AppendText(message + Environment.NewLine);
        }

        private void BtnDropDatabaseClick(object sender, EventArgs e)
        {
            Rollback();
        }

        private void Rollback()
        {
            try
            {
                DropDatabaseOnFail();
                Directory.Delete(@"C:\Temp\SampleImages\", true);
                AddConsoleMessage("Dropped it like its hot!");
            }
            catch (Exception exception)
            {
                AddConsoleMessage(exception.Message);
            }
        }

        private void BtnGenerateClick(object sender, EventArgs e)
        {
            try
            {
                TxtConsole.Text = string.Empty;
                AddConsoleMessage("<================ START ================>");
                CopyImages();
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

                AddConsoleMessage("<================ END ================>");
            }
            catch (Exception ex)
            {
                Rollback();
                AddConsoleMessage(ex.Message);
                AddConsoleMessage("Rolled back the entire thing.");
            }
        }

        #region Load Data Stuff

        private void LoadUsers()
        {
            _userRepository.Add(new User
            {
                FirstName = "Jason",
                LastName = "Magpantay",
                UserName = "jama",
                Password = "testtest1",
                EmailAddress = "jason.magpantay@gmail.com",
                BirthDate = DateTime.UtcNow.AddYears(-25)
            });
            _userRepository.Add(new User
            {
                FirstName = "Jason",
                LastName = "Avel",
                UserName = "jaav",
                Password = "testtest1",
                EmailAddress = "jason.avel@gmail.com",
                BirthDate = DateTime.UtcNow.AddYears(-25)
            });
            _userRepository.Add(new User
            {
                FirstName = "Avel",
                LastName = "Magpantay",
                UserName = "avma",
                Password = "testtest1",
                EmailAddress = "avel.magpantay@gmail.com",
                BirthDate = DateTime.UtcNow.AddYears(-25)
            });

            _users =
                _userRepository.Find(
                    a => a.UserId > 0,
                    b => b.OrderBy(c => c.UserId),
                    "Address,Education,Hobbies").ToList();

            AddConsoleMessage("Successfully added users...");
        }

        private void LoadAddress()
        {
            _users.ForEach(a => _addressRepository.Add(new Address
            {
                UserId = a.UserId,
                StreetAddress = "Street Address",
                City = "City",
                State = "State",
                Country = "Country",
                Zip = 1234
            }));

            AddConsoleMessage("Successfully added addresses...");
        }

        private void LoadEducationType()
        {
            _educationTypeRepository.Add(new EducationType
            {
                EducationTypeName = "Grade School"
            });

            _educationTypeRepository.Add(new EducationType
            {
                EducationTypeName = "High School"
            });

            _educationTypeRepository.Add(new EducationType
            {
                EducationTypeName = "College Education"
            });

            _educationTypeRepository.Add(new EducationType
            {
                EducationTypeName = "Post Graduate"
            });

            AddConsoleMessage("Successfully added education types...");
        }

        private void LoadEducation()
        {
            foreach (var u in _users)
            {
                _educationRepository.Add(new Education
                {
                    EducationTypeId = 1,
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
                });

                _educationRepository.Add(new Education
                {
                    EducationTypeId = 2,
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
                });

                _educationRepository.Add(new Education
                {
                    EducationTypeId = 3,
                    UserId = u.UserId,
                    SchoolName = "College Education",
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
            }

            AddConsoleMessage("Successfully added educations...");
        }

        private void LoadHobbies()
        {
            foreach (var u in _users)
            {
                _hobbyRepository.Add(new Hobby
                {
                    HobbyName = "Reading",
                    UserId = u.UserId,
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow
                });
            }

            AddConsoleMessage("Successfully added hobbies...");
        }

        private void LoadAlbums()
        {
            foreach (var u in _users)
            {
                _albumRepository.Add(new Album
                {
                    AlbumName = "Miscellaneous",
                    UserId = u.UserId,
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow,
                    IsUserDefault = false
                });

                _albumRepository.Add(new Album
                {
                    AlbumName = "Stuff",
                    UserId = u.UserId,
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow,
                    IsUserDefault = true
                });

                _albumRepository.Add(new Album
                {
                    AlbumName = "Profile",
                    UserId = u.UserId,
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow,
                    IsUserDefault = false
                });

                _albumRepository.Add(new Album
                {
                    AlbumName = "Background",
                    UserId = u.UserId,
                    CreatedBy = u.UserId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = u.UserId,
                    ModifiedDate = DateTime.UtcNow,
                    IsUserDefault = false
                });
            }

            AddConsoleMessage("Successfully added albums...");
        }

        private void LoadMedia()
        {
            var mediaId = 1;

            foreach (var u in _users)
            {
                var u1 = u;
                var albums = _albumRepository.Find(a => a.UserId == u1.UserId).ToList();

                for (var i = 1; i < 18; i++)
                {
                    var albumId = 0;
                    var albumName = string.Empty;
                    if (i > 2 && i < 8)
                    {
                        albumName = "Miscellaneous";
                        albumId = albums[0].AlbumId;
                    }
                    else if (i >= 8)
                    {
                        albumName = "Stuff";
                        albumId = albums[1].AlbumId;
                    }
                    else if (i == 2)
                    {
                        albumName = "Profile";
                        albumId = albums[2].AlbumId;
                        u1.PictureId = mediaId;
                        _userRepository.Edit(u1);
                    }
                    else if (i == 1)
                    {
                        albumName = "Background";
                        albumId = albums[3].AlbumId;
                        u1.BackgroundId = mediaId;
                        _userRepository.Edit(u1);
                    }

                    var mediaPath = "C:\\Temp\\SampleImages\\" + u.UserId + "\\" + albumName + "\\";
                    var tnPath = "C:\\Temp\\SampleImages\\" + u.UserId + "\\" + albumName + "\\tn\\";
                    var customName = Guid.NewGuid().ToString();

                    _mediaRepository.Add(new Media
                        {
                            CustomName = customName,
                            CreatedBy = u.UserId,
                            CreatedDate = DateTime.UtcNow,
                            ModifiedBy = u.UserId,
                            ModifiedDate = DateTime.UtcNow,
                            AlbumId = albumId,
                            FileName = i + (i > 2 && i < 8 ? ".gif" : ".jpg"),
                            MediaUrl = string.Format("https://{0}/blogapi/api/media/{1}", _localIpAddress, customName),
                            MediaType = (i > 2 && i < 8 ? "image/gif" : "image/jpeg"),
                            MediaPath = mediaPath,
                            ThumbnailUrl = string.Format("https://{0}/blogapi/api/media/{1}/thumb", _localIpAddress, customName),
                            ThumbnailPath = tnPath
                        });

                    mediaId++;
                }
            }

            AddConsoleMessage("Successfully added media...");
        }

        private void LoadTags()
        {
            _tagRepository.Add(new Tag
            {
                CreatedBy = 1,
                CreatedDate = DateTime.UtcNow,
                ModifiedBy = 1,
                ModifiedDate = DateTime.UtcNow,
                TagId = 1,
                TagName = "lorem"
            });
            _tagRepository.Add(new Tag
            {
                CreatedBy = 2,
                CreatedDate = DateTime.UtcNow,
                ModifiedBy = 2,
                ModifiedDate = DateTime.UtcNow,
                TagId = 2,
                TagName = "ipsum"
            });
            _tagRepository.Add(new Tag
            {
                CreatedBy = 3,
                CreatedDate = DateTime.UtcNow,
                ModifiedBy = 3,
                ModifiedDate = DateTime.UtcNow,
                TagId = 3,
                TagName = "dolor"
            });

            _tags = _tagRepository.Find(a => a.TagId > 0, false).ToList();

            AddConsoleMessage("Successfully added tags...");
        }

        private void LoadPosts()
        {
            foreach (var u in _users)
            {
                for (var j = 1; j < 16; j++)
                {
                    _postRepository.Add(new Post
                    {
                        CreatedBy = u.UserId,
                        CreatedDate = DateTime.UtcNow.AddHours(-j),
                        ModifiedBy = u.UserId,
                        ModifiedDate = DateTime.UtcNow.AddHours(-j),
                        PostMessage = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                        PostTitle = "Post Title",
                        UserId = u.UserId,
                        Tags = _tags
                    });
                }
            }
            
            _posts = _postRepository.Find(a => a.PostId > 0, q => q.OrderByDescending(p => p.CreatedDate),
                "Comments,User,PostLikes,Tags,PostContents").ToList();

            AddConsoleMessage("Successfully added posts...");
        }

        private void LoadPostContents()
        {
            foreach (var u in _users)
            {
                var u1 = u;
                var userposts = _posts.Where(a => a.UserId == u1.UserId).ToList();
                var album = _albumRepository
                    .Find(a => a.UserId == u1.UserId && a.AlbumName != "Background" && a.AlbumName != "Profile")
                    .ToList();

                var alb1 = album[0].AlbumId;
                var alb2 = album[1].AlbumId;
                var media = new List<Media>();
                media.AddRange(_mediaRepository.Find(a => a.AlbumId == alb1, false));
                media.AddRange(_mediaRepository.Find(a => a.AlbumId == alb2, false));

                for (var i = 0; i < 15; i++)
                {
                    _postContentRepository.Add(new PostContent
                    {
                        CreatedBy = u1.UserId,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedBy = u1.UserId,
                        ModifiedDate = DateTime.UtcNow,
                        PostId = userposts[i].PostId,
                        MediaId = media[i].MediaId
                    }); 
                }
            }

            AddConsoleMessage("Successfully added post contents...");
        }

        private void LoadPostLikes()
        {
            foreach (var p in _posts)
            {
                for (var i = 1; i < 4; i++)
                {
                    _postLikeRepository.Add(new PostLike
                    {
                        CreatedBy = i,
                        CreatedDate = DateTime.UtcNow.AddHours(-i),
                        ModifiedBy = i,
                        ModifiedDate = DateTime.UtcNow.AddHours(-i),
                        PostId = p.PostId,
                        UserId = i,
                    });
                }
            }

            AddConsoleMessage("Successfully added post likes...");
        }

        private void LoadComments()
        {
            foreach (var p in _posts)
            {
                for (var i = 1; i < 4; i++)
                {
                    _commentRepository.Add(new Comment
                    {
                        CommentMessage = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit.",
                        PostId = p.PostId,
                        ParentCommentId = null,
                        CreatedBy = i,
                        CreatedDate = DateTime.UtcNow.AddHours(-i),
                        ModifiedBy = p.User.UserId,
                        ModifiedDate = DateTime.UtcNow.AddHours(-i),
                        UserId = i,
                        CommentLocation = "Makati City, Philippines"
                    });
                }
            }

            var tc = _commentRepository.Find(a => a.CommentId > 0, true).ToList();
            foreach (var c in tc)
            {
                foreach (var u in _users)
                {
                    _commentRepository.Add(new Comment
                    {
                        CommentMessage = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit.",
                        PostId = null,
                        ParentCommentId = c.CommentId,
                        CreatedBy = u.UserId,
                        CreatedDate = DateTime.UtcNow.AddHours(-1),
                        ModifiedBy = u.UserId,
                        ModifiedDate = DateTime.UtcNow.AddHours(-1),
                        UserId = u.UserId,
                        CommentLocation = "Makati City, Philippines"
                    });
                }
            }

            _comments = _commentRepository.Find(a => a.CommentId > 0 && a.ParentCommentId == null, q => q.OrderByDescending(p => p.CreatedDate),
                "Comments,User,CommentLikes,ParentComment").ToList();

            AddConsoleMessage("Successfully added comments and comment replies...");
        }

        private void LoadCommentLikes()
        {
            foreach (var c in _comments)
            {
                for (var i = 1; i < 4; i++)
                {
                    _commentLikeRepository.Add(new CommentLike
                    {
                        CreatedBy = i,
                        CreatedDate = DateTime.UtcNow.AddHours(-i),
                        ModifiedBy = i,
                        ModifiedDate = DateTime.UtcNow.AddHours(-i),
                        CommentId = c.CommentId,
                        UserId = i,
                    });
                }
            }

            AddConsoleMessage("Successfully added comment likes...");
        }

        #endregion

        private void DropDatabaseOnFail()
        {
            try
            {
                var connectionstring = ConfigurationManager.AppSettings.Get("MasterDb");
                var dbName = ConfigurationManager.AppSettings.Get("BlogDbName");

                using (var con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    var sqlCommandText = @"
                    ALTER DATABASE " + dbName + @" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    DROP DATABASE [" + dbName + "]";
                    var sqlCommand = new SqlCommand(sqlCommandText, con);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                AddConsoleMessage(ex.Message);
            }
            
        }

        private void CopyImages()
        {
            var imagesPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\SampleImages";
            const string destinationPath = @"C:\Temp\SampleImages\";
            
            foreach (var dest in Directory.GetDirectories(imagesPath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dest.Replace(imagesPath, destinationPath));
            }
            
            foreach (var newPath in Directory.GetFiles(imagesPath, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(imagesPath, destinationPath), true);
            }
            
            AddConsoleMessage("Successfully moved sample images...");
        }
    }
}
