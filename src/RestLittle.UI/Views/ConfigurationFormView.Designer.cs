namespace RestLittle.UI.Views
{
	partial class ConfigurationFormView
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationFormView));
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.txtMaxBusyTime = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtRestingTime = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.txtWarningInterval = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.txtTimeToIdle = new System.Windows.Forms.TextBox();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12, 12);
			this.label5.Margin = new System.Windows.Forms.Padding(3);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(113, 23);
			this.label5.TabIndex = 0;
			this.label5.Text = "Time-to-idle";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(12, 41);
			this.label6.Margin = new System.Windows.Forms.Padding(3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(113, 23);
			this.label6.TabIndex = 0;
			this.label6.Text = "Max Busy Time";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtMaxBusyTime
			// 
			this.txtMaxBusyTime.Location = new System.Drawing.Point(150, 42);
			this.txtMaxBusyTime.Name = "txtMaxBusyTime";
			this.txtMaxBusyTime.Size = new System.Drawing.Size(119, 23);
			this.txtMaxBusyTime.TabIndex = 1;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(12, 70);
			this.label7.Margin = new System.Windows.Forms.Padding(3);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(113, 23);
			this.label7.TabIndex = 0;
			this.label7.Text = "Resting Time";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtRestingTime
			// 
			this.txtRestingTime.Location = new System.Drawing.Point(150, 70);
			this.txtRestingTime.Name = "txtRestingTime";
			this.txtRestingTime.Size = new System.Drawing.Size(119, 23);
			this.txtRestingTime.TabIndex = 2;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(12, 99);
			this.label8.Margin = new System.Windows.Forms.Padding(3);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(113, 23);
			this.label8.TabIndex = 0;
			this.label8.Text = "Warning Interval";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtWarningInterval
			// 
			this.txtWarningInterval.Location = new System.Drawing.Point(150, 99);
			this.txtWarningInterval.Name = "txtWarningInterval";
			this.txtWarningInterval.Size = new System.Drawing.Size(119, 23);
			this.txtWarningInterval.TabIndex = 3;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(114, 139);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(195, 139);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// txtTimeToIdle
			// 
			this.txtTimeToIdle.Location = new System.Drawing.Point(150, 13);
			this.txtTimeToIdle.Name = "txtTimeToIdle";
			this.txtTimeToIdle.Size = new System.Drawing.Size(119, 23);
			this.txtTimeToIdle.TabIndex = 0;
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// ConfigurationFormView
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(295, 177);
			this.Controls.Add(this.txtMaxBusyTime);
			this.Controls.Add(this.txtRestingTime);
			this.Controls.Add(this.txtWarningInterval);
			this.Controls.Add(this.txtTimeToIdle);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ConfigurationFormView";
			this.ShowInTaskbar = false;
			this.Text = "Rest-a-Little Configuration";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtMaxBusyTime;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtRestingTime;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtWarningInterval;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox txtTimeToIdle;
		private System.Windows.Forms.ErrorProvider errorProvider1;
	}
}