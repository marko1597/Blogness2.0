namespace Blog.Backend.Services.SessionCleaner
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BlogSessionCleanerProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.BlogSessionCleanerInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // BlogSessionCleanerProcessInstaller
            // 
            this.BlogSessionCleanerProcessInstaller.Password = null;
            this.BlogSessionCleanerProcessInstaller.Username = null;
            // 
            // BlogSessionCleanerInstaller
            // 
            this.BlogSessionCleanerInstaller.Description = "Cleans up expired sessions for Bloggity Blog.";
            this.BlogSessionCleanerInstaller.ServiceName = "BlogSessionCleaner";
            this.BlogSessionCleanerInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.BlogSessionCleanerProcessInstaller,
            this.BlogSessionCleanerInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller BlogSessionCleanerProcessInstaller;
        private System.ServiceProcess.ServiceInstaller BlogSessionCleanerInstaller;
    }
}