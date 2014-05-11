using Blog.Backend.Common.Utils;
using Blog.Backend.DataAccess.Repository;

namespace Blog.Backend.DataAccess.Seed
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.BtnGenerate = new System.Windows.Forms.Button();
            this.TxtConsole = new System.Windows.Forms.TextBox();
            this.BtnDropDatabase = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnGenerate
            // 
            this.BtnGenerate.Location = new System.Drawing.Point(0, 0);
            this.BtnGenerate.Name = "BtnGenerate";
            this.BtnGenerate.Size = new System.Drawing.Size(268, 23);
            this.BtnGenerate.TabIndex = 5;
            this.BtnGenerate.Text = "Generate Data";
            this.BtnGenerate.UseVisualStyleBackColor = true;
            this.BtnGenerate.Click += new System.EventHandler(this.BtnGenerateClick);
            // 
            // TxtConsole
            // 
            this.TxtConsole.Location = new System.Drawing.Point(0, 29);
            this.TxtConsole.Multiline = true;
            this.TxtConsole.Name = "TxtConsole";
            this.TxtConsole.ReadOnly = true;
            this.TxtConsole.Size = new System.Drawing.Size(547, 302);
            this.TxtConsole.TabIndex = 6;
            // 
            // BtnDropDatabase
            // 
            this.BtnDropDatabase.Location = new System.Drawing.Point(279, 0);
            this.BtnDropDatabase.Name = "BtnDropDatabase";
            this.BtnDropDatabase.Size = new System.Drawing.Size(268, 23);
            this.BtnDropDatabase.TabIndex = 7;
            this.BtnDropDatabase.Text = "Drop Database";
            this.BtnDropDatabase.UseVisualStyleBackColor = true;
            this.BtnDropDatabase.Click += new System.EventHandler(this.BtnDropDatabaseClick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 330);
            this.Controls.Add(this.BtnDropDatabase);
            this.Controls.Add(this.TxtConsole);
            this.Controls.Add(this.BtnGenerate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "I Can Haz Seeds!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnGenerate;
        private readonly IAddressRepository _addressRepository = new AddressRepository();
        private readonly ICommentLikeRepository _commentLikeRepository = new CommentLikeRepository();
        private readonly ICommentRepository _commentRepository = new CommentRepository();
        private readonly IEducationRepository _educationRepository = new EducationRepository();
        private readonly IEducationTypeRepository _educationTypeRepository = new EducationTypeRepository();
        private readonly IHobbyRepository _hobbyRepository = new HobbyRepository();
        private readonly IAlbumRepository _albumRepository = new AlbumRepository();
        private readonly IMediaRepository _mediaRepository = new MediaRepository();
        private readonly IPostContentRepository _postContentRepository = new PostContentRepository();
        private readonly IPostLikeRepository _postLikeRepository = new PostLikeRepository();
        private readonly IPostRepository _postRepository = new PostRepository();
        private readonly ITagRepository _tagRepository = new TagRepository();
        private readonly IUserRepository _userRepository = new UserRepository();
        private readonly IImageHelper _imageHelper = new ImageHelper();
        private System.Windows.Forms.TextBox TxtConsole;
        private System.Windows.Forms.Button BtnDropDatabase;
    }
}

