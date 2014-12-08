using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Blog.Common.Identity.Models;
using Blog.Common.Identity.Repository;
using Blog.Common.Identity.Role;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository;
using Blog.DataAccess.Database.Repository.Interfaces;
using Nito.AsyncEx;

namespace Blog.Tools.ApplicationSetup
{
	[ExcludeFromCodeCoverage]
	class Program
	{
		#region External properties

		private static readonly IAddressRepository AddressRepository = new AddressRepository();
		private static readonly ICommentLikeRepository CommentLikeRepository = new CommentLikeRepository();
		private static readonly ICommentRepository CommentRepository = new CommentRepository();
		private static readonly IEducationRepository EducationRepository = new EducationRepository();
		private static readonly IEducationTypeRepository EducationTypeRepository = new EducationTypeRepository();
		private static readonly IHobbyRepository HobbyRepository = new HobbyRepository();
		private static readonly IAlbumRepository AlbumRepository = new AlbumRepository();
		private static readonly ICommunityRepository CommunityRepository = new CommunityRepository();
		private static readonly IMediaRepository MediaRepository = new MediaRepository();
		private static readonly IPostContentRepository PostContentRepository = new PostContentRepository();
		private static readonly IPostLikeRepository PostLikeRepository = new PostLikeRepository();
		private static readonly IPostRepository PostRepository = new PostRepository();
		private static readonly ITagRepository TagRepository = new TagRepository();
		private static readonly IUserRepository UserRepository = new UserRepository();
		private static readonly IChatMessageRepository ChatMessageRepository = new ChatMessageRepository();
		private static readonly IBlogDbRepository BlogDbRepository = new BlogDbRepository();

		#endregion

		#region Members

		private static List<User> _users = new List<User>();
		private static List<Post> _posts = new List<Post>();
		private static List<Comment> _comments = new List<Comment>();
		private static List<Tag> _tags = new List<Tag>();
		static string _localIpAddress = string.Empty;

		#endregion

		static void Main(string[] args)
		{
			_localIpAddress = ConfigurationManager.AppSettings.Get("BlogServer");

			if (args == null || args.Length == 0)
			{
				InitializeApp();
				AddConsoleMessage("Successfully initialized app with an admin user");
			}
			else
			{
				var tArgs = args.ToList();

				if (tArgs.Contains("--drop"))
				{
					Rollback();
				}
				else if (tArgs.Contains("--seed"))
				{
					AsyncContext.Run(() => SeedData());
				}
				else if (tArgs.Contains("--help"))
				{
					ShowHelp();
				}
				else if (tArgs.Count > 1)
				{
					AddConsoleMessage("Invalid number or usage of arguments");
					ShowHelp();
				}
				else
				{
					AddConsoleMessage("Invalid number or usage of arguments");
					ShowHelp();
				}
			}
		}

		#region Load Data Stuff

		private static async void SeedData()
		{
			Rollback();
			Console.Clear();
			AddConsoleMessage("<================ START ================>");
			CopyImages();
			await LoadRoles();
			LoadUsers();
			await LoadIdentities();
			LoadCommunities();
			LoadChatMessages();
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
			MapUsersToIdentity();
			AddConsoleMessage("Successfully initialized app with seeded test data");
		}

		private static async void InitializeApp()
		{
			Rollback();

			await LoadRoles();

			var bloguser = new BlogRegisterModel
			{
				Username = "admin",
				Email = "admin@bloggity.com",
				Password = "admin!"
			};

			var result = await BlogDbRepository.RegisterUser(bloguser);
			if (result.Succeeded)
			{
				var savedUser = await BlogDbRepository.FindUser(bloguser.Username, bloguser.Password);
				var firstOrDefault = BlogDbRepository.GetRoles().FirstOrDefault();
				if (firstOrDefault != null)
				{
					var role = firstOrDefault.Name;
					await BlogDbRepository.AddToRolesAsync(savedUser.Id, new[] { role });
				}
			}

			UserRepository.Add(new User
			{
				FirstName = "Jason",
				LastName = "Magpantay",
				UserName = "admin",
				IdentityId = Guid.NewGuid().ToString(),
				EmailAddress = "admin@bloggity",
				BirthDate = DateTime.Now.AddYears(-25)
			});
		}

		private static async Task LoadRoles()
		{
			await BlogDbRepository.CreateRoleAsync(new BlogRole { Name = "Admin", Description = "Admin" });
			await BlogDbRepository.CreateRoleAsync(new BlogRole { Name = "Bloggity Staff", Description = "Bloggity Staff" });
			await BlogDbRepository.CreateRoleAsync(new BlogRole { Name = "Blogger", Description = "Blogger" });
		}

		private static void LoadUsers()
		{
			UserRepository.Add(new User
			{
				FirstName = "Jason",
				LastName = "Magpantay",
				UserName = "jamaness",
				IdentityId = Guid.NewGuid().ToString(),
				EmailAddress = "jason.magpantay@gmail.com",
				BirthDate = DateTime.Now.AddYears(-25)
			});
			UserRepository.Add(new User
			{
				FirstName = "Jason",
				LastName = "Avel",
				UserName = "jaavness",
				IdentityId = Guid.NewGuid().ToString(),
				EmailAddress = "jason.avel@gmail.com",
				BirthDate = DateTime.Now.AddYears(-25)
			});
			UserRepository.Add(new User
			{
				FirstName = "Avel",
				LastName = "Magpantay",
				UserName = "avmaness",
				IdentityId = Guid.NewGuid().ToString(),
				EmailAddress = "avel.magpantay@gmail.com",
				BirthDate = DateTime.Now.AddYears(-25)
			});

			_users = UserRepository.Find(
					a => a.UserId > 0,
					b => b.OrderBy(c => c.UserId), null).ToList();

			AddConsoleMessage("Successfully added users...");
		}

		private static void LoadCommunities()
		{
			CommunityRepository.Add(new Community
			{
				Name = "jamaness",
				Description = "Lorem ipsum dolor sit amet",
				LeaderUserId = 1,
				Members = _users.ToList(),
				CreatedBy = 1,
				CreatedDate = DateTime.Now,
				ModifiedBy = 1,
				ModifiedDate = DateTime.Now
			});
			CommunityRepository.Add(new Community
			{
				Name = "jaavness",
				Description = "Lorem ipsum dolor sit amet",
				LeaderUserId = 2,
				Members = _users.ToList(),
				CreatedBy = 2,
				CreatedDate = DateTime.Now,
				ModifiedBy = 2,
				ModifiedDate = DateTime.Now
			});
			CommunityRepository.Add(new Community
			{
				Name = "avmaness",
				Description = "Lorem ipsum dolor sit amet",
				LeaderUserId = 3,
				Members = _users.ToList(),
				CreatedBy = 3,
				CreatedDate = DateTime.Now,
				ModifiedBy = 3,
				ModifiedDate = DateTime.Now
			});

			AddConsoleMessage("Successfully added communities...");
		}

		private static async Task LoadIdentities()
		{
			foreach (var user in _users)
			{
				var bloguser = new BlogRegisterModel
				{
					Username = user.UserName,
					Email = user.EmailAddress,
					Password = "Testtest1!"
				};
				var result = await BlogDbRepository.RegisterUser(bloguser);
				if (result.Succeeded)
				{
					var savedUser = await BlogDbRepository.FindUser(bloguser.Username, bloguser.Password);
					var firstOrDefault = BlogDbRepository.GetRoles().FirstOrDefault();
					if (firstOrDefault != null)
					{
						var role = firstOrDefault.Name;
						await BlogDbRepository.AddToRolesAsync(savedUser.Id, new[] { role });
					}
				}
			}

			AddConsoleMessage("Successfully added users...");
		}

		private static void LoadAddress()
		{
			_users.ForEach(a => AddressRepository.Add(new Address
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

		private static void LoadEducationType()
		{
			EducationTypeRepository.Add(new EducationType
			{
				EducationTypeName = "Grade School"
			});

			EducationTypeRepository.Add(new EducationType
			{
				EducationTypeName = "High School"
			});

			EducationTypeRepository.Add(new EducationType
			{
				EducationTypeName = "College Education"
			});

			EducationTypeRepository.Add(new EducationType
			{
				EducationTypeName = "Post Graduate"
			});

			AddConsoleMessage("Successfully added education types...");
		}

		private static void LoadEducation()
		{
			foreach (var u in _users)
			{
				EducationRepository.Add(new Education
				{
					EducationTypeId = 1,
					UserId = u.UserId,
					SchoolName = "Grade School",
					City = "City",
					State = "State",
					Country = "Country",
					YearAttended = DateTime.Now.AddYears(-20),
					YearGraduated = DateTime.Now.AddYears(-14),
					Course = string.Empty,
					CreatedBy = u.UserId,
					CreatedDate = DateTime.Now,
					ModifiedBy = u.UserId,
					ModifiedDate = DateTime.Now
				});

				EducationRepository.Add(new Education
				{
					EducationTypeId = 2,
					UserId = u.UserId,
					SchoolName = "High School",
					City = "City",
					State = "State",
					Country = "Country",
					YearAttended = DateTime.Now.AddYears(-14),
					YearGraduated = DateTime.Now.AddYears(-8),
					Course = string.Empty,
					CreatedBy = u.UserId,
					CreatedDate = DateTime.Now,
					ModifiedBy = u.UserId,
					ModifiedDate = DateTime.Now
				});

				EducationRepository.Add(new Education
				{
					EducationTypeId = 3,
					UserId = u.UserId,
					SchoolName = "College Education",
					City = "City",
					State = "State",
					Country = "Country",
					YearAttended = DateTime.Now.AddYears(-8),
					YearGraduated = DateTime.Now.AddYears(-4),
					Course = "BS Computer Science",
					CreatedBy = u.UserId,
					CreatedDate = DateTime.Now,
					ModifiedBy = u.UserId,
					ModifiedDate = DateTime.Now
				});
			}

			AddConsoleMessage("Successfully added educations...");
		}

		private static void LoadHobbies()
		{
			foreach (var u in _users)
			{
				HobbyRepository.Add(new Hobby
				{
					HobbyName = "Reading",
					UserId = u.UserId,
					CreatedBy = u.UserId,
					CreatedDate = DateTime.Now,
					ModifiedBy = u.UserId,
					ModifiedDate = DateTime.Now
				});
			}

			AddConsoleMessage("Successfully added hobbies...");
		}

		private static void LoadChatMessages()
		{
			var index = 1;

			foreach (var u in _users)
			{
				var recipients = _users.Where(a => a.UserId != u.UserId).ToList();

				foreach (var recipient in recipients)
				{
					for (var i = 0; i < 3; i++)
					{
						ChatMessageRepository.Add(new ChatMessage
						{
							FromUserId = u.UserId,
							ToUserId = recipient.UserId,
							Text = string.Format("Lorem ipsum dolor sit amet ({0})", index),
							CreatedBy = u.UserId,
							CreatedDate = DateTime.Now,
							ModifiedBy = u.UserId,
							ModifiedDate = DateTime.Now
						});

						index++;
					}

				}
			}

			AddConsoleMessage("Successfully added chat messages...");
		}

		private static void LoadAlbums()
		{
			foreach (var u in _users)
			{
				AlbumRepository.Add(new Album
				{
					AlbumName = "Miscellaneous",
					UserId = u.UserId,
					CreatedBy = u.UserId,
					CreatedDate = DateTime.Now,
					ModifiedBy = u.UserId,
					ModifiedDate = DateTime.Now,
					IsUserDefault = false
				});

				AlbumRepository.Add(new Album
				{
					AlbumName = "Stuff",
					UserId = u.UserId,
					CreatedBy = u.UserId,
					CreatedDate = DateTime.Now,
					ModifiedBy = u.UserId,
					ModifiedDate = DateTime.Now,
					IsUserDefault = true
				});

				AlbumRepository.Add(new Album
				{
					AlbumName = "Profile",
					UserId = u.UserId,
					CreatedBy = u.UserId,
					CreatedDate = DateTime.Now,
					ModifiedBy = u.UserId,
					ModifiedDate = DateTime.Now,
					IsUserDefault = false
				});

				AlbumRepository.Add(new Album
				{
					AlbumName = "Background",
					UserId = u.UserId,
					CreatedBy = u.UserId,
					CreatedDate = DateTime.Now,
					ModifiedBy = u.UserId,
					ModifiedDate = DateTime.Now,
					IsUserDefault = false
				});
			}

			AddConsoleMessage("Successfully added albums...");
		}

		private static void LoadMedia()
		{
			var mediaId = 1;

			foreach (var u in _users)
			{
				// ReSharper disable once AccessToForEachVariableInClosure
				var albums = AlbumRepository.Find(a => a.UserId == u.UserId).ToList();

				for (var i = 1; i < 18; i++)
				{
					var albumId = 0;
					if (i > 2 && i < 8)
					{
						albumId = albums[0].AlbumId;
					}
					else if (i >= 8)
					{
						albumId = albums[1].AlbumId;
					}
					else switch (i)
					{
						case 2:
							albumId = albums[2].AlbumId;
							u.PictureId = mediaId;
							UserRepository.Edit(new User
												{
													UserId = u.UserId,
													FirstName = u.FirstName,
													LastName = u.LastName,
													EmailAddress = u.EmailAddress,
													BirthDate = u.BirthDate,
													UserName = u.UserName,
													PictureId = mediaId,
													IdentityId = u.IdentityId
												});
							break;
						case 1:
							albumId = albums[3].AlbumId;
							u.BackgroundId = mediaId;
							UserRepository.Edit(new User
												{
													UserId = u.UserId,
													FirstName = u.FirstName,
													LastName = u.LastName,
													EmailAddress = u.EmailAddress,
													BirthDate = u.BirthDate,
													UserName = u.UserName,
													BackgroundId = mediaId,
													IdentityId = u.IdentityId
												});
							break;
					}

					var mediaPath = "C:\\Temp\\SampleImages\\" + u.UserId + "\\";
					var tnPath = "C:\\Temp\\SampleImages\\" + u.UserId + "\\tn\\";
					var customName = Guid.NewGuid().ToString();

					MediaRepository.Add(new Media
					{
						CustomName = customName,
						CreatedBy = u.UserId,
						CreatedDate = DateTime.Now,
						ModifiedBy = u.UserId,
						ModifiedDate = DateTime.Now,
						AlbumId = albumId,
						FileName = i + (i > 2 && i < 8 ? ".gif" : ".jpg"),
						MediaUrl = string.Format("https://{0}/api/media/{1}", _localIpAddress, customName),
						MediaType = (i > 2 && i < 8 ? "image/gif" : "image/jpeg"),
						MediaPath = mediaPath,
						ThumbnailUrl = string.Format("https://{0}/api/media/{1}/thumb", _localIpAddress, customName),
						ThumbnailPath = tnPath
					});

					mediaId++;
				}
			}

			AddConsoleMessage("Successfully added media...");
		}

		private static void LoadTags()
		{
			TagRepository.Add(new Tag
			{
				CreatedBy = 1,
				CreatedDate = DateTime.Now,
				ModifiedBy = 1,
				ModifiedDate = DateTime.Now,
				TagId = 1,
				TagName = "lorem"
			});
			TagRepository.Add(new Tag
			{
				CreatedBy = 2,
				CreatedDate = DateTime.Now,
				ModifiedBy = 2,
				ModifiedDate = DateTime.Now,
				TagId = 2,
				TagName = "ipsum"
			});
			TagRepository.Add(new Tag
			{
				CreatedBy = 3,
				CreatedDate = DateTime.Now,
				ModifiedBy = 3,
				ModifiedDate = DateTime.Now,
				TagId = 3,
				TagName = "dolor"
			});

			_tags = TagRepository.Find(a => a.TagId > 0, false).ToList();

			AddConsoleMessage("Successfully added tags...");
		}

		private static void LoadPosts()
		{
			foreach (var u in _users)
			{
                var communities = CommunityRepository.GetJoinedCommunitiesByUser(u.UserId);

				for (var j = 1; j < 16; j++)
				{
					PostRepository.Add(new Post
					{
						CreatedBy = u.UserId,
						CreatedDate = DateTime.Now.AddHours(-j),
						ModifiedBy = u.UserId,
						Communities = communities,
						ModifiedDate = DateTime.Now.AddHours(-j),
						PostMessage = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
						PostTitle = "Lorem ipsum dolor",
						UserId = u.UserId,
						Tags = _tags
					});
				}
			}

			_posts = PostRepository.Find(a => a.PostId > 0, q => q.OrderByDescending(p => p.CreatedDate),
				"Comments,User,PostLikes,Tags,PostContents").ToList();

			AddConsoleMessage("Successfully added posts...");
		}

		private static void LoadPostContents()
		{
			foreach (var u in _users)
			{
				var u1 = u;
				var userposts = _posts.Where(a => a.UserId == u1.UserId).ToList();
				var album = AlbumRepository
					.Find(a => a.UserId == u1.UserId && a.AlbumName != "Background" && a.AlbumName != "Profile")
					.ToList();

				var alb1 = album[0].AlbumId;
				var alb2 = album[1].AlbumId;
				var media = new List<Media>();
				media.AddRange(MediaRepository.Find(a => a.AlbumId == alb1, false));
				media.AddRange(MediaRepository.Find(a => a.AlbumId == alb2, false));

				for (var i = 0; i < 15; i++)
				{
					PostContentRepository.Add(new PostContent
					{
						PostContentText = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
						PostContentTitle = "Lorem ipsum dolor",
						CreatedBy = u1.UserId,
						CreatedDate = DateTime.Now,
						ModifiedBy = u1.UserId,
						ModifiedDate = DateTime.Now,
						PostId = userposts[i].PostId,
						MediaId = media[i].MediaId
					});
				}
			}

			AddConsoleMessage("Successfully added post contents...");
		}

		private static void LoadPostLikes()
		{
			foreach (var p in _posts)
			{
				for (var i = 1; i < 4; i++)
				{
					PostLikeRepository.Add(new PostLike
					{
						CreatedBy = i,
						CreatedDate = DateTime.Now.AddHours(-i),
						ModifiedBy = i,
						ModifiedDate = DateTime.Now.AddHours(-i),
						PostId = p.PostId,
						UserId = i,
					});
				}
			}

			AddConsoleMessage("Successfully added post likes...");
		}

		private static void LoadComments()
		{
			foreach (var p in _posts)
			{
				for (var i = 1; i < 4; i++)
				{
					CommentRepository.Add(new Comment
					{
						CommentMessage = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit.",
						PostId = p.PostId,
						ParentCommentId = null,
						CreatedBy = i,
						CreatedDate = DateTime.Now.AddHours(-i),
						ModifiedBy = p.User.UserId,
						ModifiedDate = DateTime.Now.AddHours(-i),
						UserId = i,
						CommentLocation = "Makati City, Philippines"
					});
				}
			}

			var tc = CommentRepository.Find(a => a.CommentId > 0, true).ToList();
			foreach (var c in tc)
			{
				foreach (var u in _users)
				{
					CommentRepository.Add(new Comment
					{
						CommentMessage = "Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit.",
						PostId = null,
						ParentCommentId = c.CommentId,
						CreatedBy = u.UserId,
						CreatedDate = DateTime.Now.AddHours(-1),
						ModifiedBy = u.UserId,
						ModifiedDate = DateTime.Now.AddHours(-1),
						UserId = u.UserId,
						CommentLocation = "Makati City, Philippines"
					});
				}
			}

			_comments = CommentRepository.Find(a => a.CommentId > 0 && a.ParentCommentId == null, q => q.OrderByDescending(p => p.CreatedDate),
				"Comments,User,CommentLikes,ParentComment").ToList();

			AddConsoleMessage("Successfully added comments and comment replies...");
		}

		private static void LoadCommentLikes()
		{
			foreach (var c in _comments)
			{
				for (var i = 1; i < 4; i++)
				{
					CommentLikeRepository.Add(new CommentLike
					{
						CreatedBy = i,
						CreatedDate = DateTime.Now.AddHours(-i),
						ModifiedBy = i,
						ModifiedDate = DateTime.Now.AddHours(-i),
						CommentId = c.CommentId,
						UserId = i,
					});
				}
			}

			AddConsoleMessage("Successfully added comment likes...");
		}

		#endregion

		#region Events

		private static void ShowHelp()
		{
			AddConsoleMessage("Usage:");
			AddConsoleMessage("\t --help \t Displays this info");
			AddConsoleMessage("\t --drop \t Drops all database associated with the app");
			AddConsoleMessage("\t --seed \t Initializes the app and creates test data");
		}

		private static void Rollback()
		{
			try
			{
				DropDatabaseOnFail();
				Directory.Delete(@"C:\Temp\SampleImages\", true);
				Console.Clear();
				AddConsoleMessage("Dropped it like its hot!");
			}
			catch (Exception exception)
			{
				AddConsoleMessage(exception.Message);
			}
		}

		private static void AddConsoleMessage(string message)
		{
			Console.WriteLine(message);
		}

		private static void DropDatabaseOnFail()
		{
			try
			{
				DropBlogDb();
				DropIdentityDb();
			}
			catch (Exception ex)
			{
				AddConsoleMessage(ex.Message);
			}
		}

		private static void DropBlogDb()
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

		private static void DropIdentityDb()
		{
			var connectionstring = ConfigurationManager.AppSettings.Get("MasterDb");
			var dbName = ConfigurationManager.AppSettings.Get("BlogIdentityDbName");

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

		private static void MapUsersToIdentity()
		{
			var connectionstring = ConfigurationManager.AppSettings.Get("MasterDb");

			using (var con = new SqlConnection(connectionstring))
			{
				con.Open();

				#region sql command string
				const string sqlCommandText = @"
					declare @username varchar(max)
					declare @aspusers table 
					(
						username varchar(max),
						identityId varchar(max)
					)

					insert into @aspusers
					select username, id 
					from [blog_identity].[dbo].[aspnetusers]

					declare aspusers_cursor cursor for
					select username from @aspusers

					open aspusers_cursor
					fetch next from aspusers_cursor into @username   

					while @@FETCH_STATUS = 0   
					begin
						if exists (select * from [blog].[dbo].[users] where UserName = @username)
						begin
							update [blog].[dbo].[users] 
							set IdentityId = (select identityId from @aspusers where username = @username)
							where UserName = @username
						end
		
						fetch next from aspusers_cursor into @username   
					end   

					close aspusers_cursor   
					deallocate aspusers_cursor";
				#endregion

				var sqlCommand = new SqlCommand(sqlCommandText, con);
				sqlCommand.ExecuteNonQuery();
			}

			AddConsoleMessage("Successfully mapped users to identity...");
		}

		private static void CopyImages()
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

		#endregion
	}
}
