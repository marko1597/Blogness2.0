using System;
using System.Net;
using System.Web;
using Blog.Common.Utils.Helpers;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.DataAccess.Database.Repository;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core;
using Blog.Logic.Core.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Wcf;

namespace Blog.Services.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            // Allow Any Certificates
            // This should not be the same in Production
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            // Create the container as usual.
            var container = new Container();

            // Logic core registry. This includes all blog logic functionalities
            container.Register<IAddressLogic, AddressLogic>();
            container.Register<IAlbumLogic, AlbumLogic>();
            container.Register<ICommentLikesLogic, CommentLikesLogic>();
            container.Register<ICommentsLogic, CommentsLogic>();
            container.Register<IEducationLogic, EducationLogic>();
            container.Register<IHobbyLogic, HobbyLogic>();
            container.Register<IMediaLogic, MediaLogic>();
            container.Register<IPostContentsLogic, PostContentsLogic>();
            container.Register<IPostLikesLogic, PostLikesLogic>();
            container.Register<IPostsLogic, PostsLogic>();
            container.Register<ITagsLogic, TagsLogic>();
            container.Register<IUsersLogic, UsersLogic>();

            // Db repository registry. All entity framework repositories are registered here.
            container.Register<IAddressRepository, AddressRepository>();
            container.Register<IAlbumRepository, AlbumRepository>();
            container.Register<ICommentLikeRepository, CommentLikeRepository>();
            container.Register<ICommentRepository, CommentRepository>();
            container.Register<IEducationRepository, EducationRepository>();
            container.Register<IEducationTypeRepository, EducationTypeRepository>();
            container.Register<IHobbyRepository, HobbyRepository>();
            container.Register<IMediaRepository, MediaRepository>();
            container.Register<IPostContentRepository, PostContentRepository>();
            container.Register<IPostLikeRepository, PostLikeRepository>();
            container.Register<IPostRepository, PostRepository>();
            container.Register<ITagRepository, TagRepository>();
            container.Register<IUserRepository, UserRepository>();

            // Helpers registry. All utility classes are registered here.
            container.Register<IImageHelper, ImageHelper>();
            container.Register<IConfigurationHelper, ConfigurationHelper>();
            container.Register<IFileHelper, FileHelper>();

            // Register the container to the SimpleInjectorServiceHostFactory.
            SimpleInjectorServiceHostFactory.SetContainer(container);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}