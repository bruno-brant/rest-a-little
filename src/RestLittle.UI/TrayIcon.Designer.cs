namespace RestLittle.UI
{
	partial class TrayIcon
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
			this.components = new System.ComponentModel.Container();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this._trayIconMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this._configurationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this._exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this._trayIconMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenuStrip = this._trayIconMenuStrip;
			this.notifyIcon1.Text = "Rest-a-Little";
			this.notifyIcon1.Visible = true;
			// 
			// _trayIconMenuStrip
			// 
			this._trayIconMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._configurationMenuItem,
            this.toolStripSeparator1,
            this._exitMenuItem});
			this._trayIconMenuStrip.Name = "contextMenuStrip1";
			this._trayIconMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this._trayIconMenuStrip.Size = new System.Drawing.Size(149, 54);
			// 
			// _configurationMenuItem
			// 
			this._configurationMenuItem.Name = "_configurationMenuItem";
			this._configurationMenuItem.Size = new System.Drawing.Size(148, 22);
			this._configurationMenuItem.Text = "&Configuration";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
			// 
			// _exitMenuItem
			// 
			this._exitMenuItem.Name = "_exitMenuItem";
			this._exitMenuItem.Size = new System.Drawing.Size(148, 22);
			this._exitMenuItem.Text = "&Exit";
			// 
			// TrayIcon
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "TrayIcon";
			this.Size = new System.Drawing.Size(350, 234);
			this._trayIconMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ContextMenuStrip _trayIconMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem _exitMenuItem;
		private System.Windows.Forms.ToolStripMenuItem _configurationMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}
