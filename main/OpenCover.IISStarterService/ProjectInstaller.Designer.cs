namespace OpenCover.IISStarterService
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
            this.OpenCoverServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.OpenCoverServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // OpenCoverServiceProcessInstaller
            // 
            this.OpenCoverServiceProcessInstaller.Password = null;
            this.OpenCoverServiceProcessInstaller.Username = null;
            // 
            // OpenCoverServiceInstaller
            // 
            this.OpenCoverServiceInstaller.ServiceName = "OpenCoverService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.OpenCoverServiceProcessInstaller,
            this.OpenCoverServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller OpenCoverServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller OpenCoverServiceInstaller;
    }
}