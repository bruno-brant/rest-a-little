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
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.tpTimeToIdle = new RestLittle.UI.Views.TimePicker();
			this.tpMaxBusyTime = new RestLittle.UI.Views.TimePicker();
			this.tpRestingTime = new RestLittle.UI.Views.TimePicker();
			this.tpWarningInterval = new RestLittle.UI.Views.TimePicker();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 15);
			this.label5.Margin = new System.Windows.Forms.Padding(3);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(113, 23);
			this.label5.TabIndex = 0;
			this.label5.Text = "Time-to-idle";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolTip1.SetToolTip(this.label5, "How long to wait to consider the user idle.");
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 44);
			this.label6.Margin = new System.Windows.Forms.Padding(3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(113, 23);
			this.label6.TabIndex = 0;
			this.label6.Text = "Max Busy Time";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolTip1.SetToolTip(this.label6, "How much time can the user use the computer until the warning shows.");
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 73);
			this.label7.Margin = new System.Windows.Forms.Padding(3);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(113, 23);
			this.label7.TabIndex = 0;
			this.label7.Text = "Resting Time";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolTip1.SetToolTip(this.label7, "How long does a user needs to stay idle to be considered rested.");
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 102);
			this.label8.Margin = new System.Windows.Forms.Padding(3);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(113, 23);
			this.label8.TabIndex = 0;
			this.label8.Text = "Warning Interval";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolTip1.SetToolTip(this.label8, "Interval between consecutive warnings.");
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(76, 142);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(157, 142);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// tpTimeToIdle
			// 
			this.tpTimeToIdle.CustomFormat = "mmm:ss";
			this.tpTimeToIdle.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.tpTimeToIdle.Location = new System.Drawing.Point(173, 15);
			this.tpTimeToIdle.MaxDate = new System.DateTime(2021, 1, 28, 23, 59, 59, 0);
			this.tpTimeToIdle.MinDate = new System.DateTime(2021, 1, 28, 0, 0, 0, 0);
			this.tpTimeToIdle.Name = "tpTimeToIdle";
			this.tpTimeToIdle.ShowUpDown = true;
			this.tpTimeToIdle.Size = new System.Drawing.Size(60, 23);
			this.tpTimeToIdle.TabIndex = 8;
			this.tpTimeToIdle.TimeSpan = System.TimeSpan.Parse("12:19:08.6650000");
			this.tpTimeToIdle.Value = new System.DateTime(2021, 1, 28, 12, 19, 8, 665);
			// 
			// tpMaxBusyTime
			// 
			this.tpMaxBusyTime.CustomFormat = "mmm:ss";
			this.tpMaxBusyTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.tpMaxBusyTime.Location = new System.Drawing.Point(173, 44);
			this.tpMaxBusyTime.MaxDate = new System.DateTime(2021, 1, 28, 23, 59, 59, 0);
			this.tpMaxBusyTime.MinDate = new System.DateTime(2021, 1, 28, 0, 0, 0, 0);
			this.tpMaxBusyTime.Name = "tpMaxBusyTime";
			this.tpMaxBusyTime.ShowUpDown = true;
			this.tpMaxBusyTime.Size = new System.Drawing.Size(60, 23);
			this.tpMaxBusyTime.TabIndex = 8;
			this.tpMaxBusyTime.TimeSpan = System.TimeSpan.Parse("12:19:08.6650000");
			this.tpMaxBusyTime.Value = new System.DateTime(2021, 1, 28, 12, 19, 8, 665);
			// 
			// tpRestingTime
			// 
			this.tpRestingTime.CustomFormat = "mmm:ss";
			this.tpRestingTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.tpRestingTime.Location = new System.Drawing.Point(173, 71);
			this.tpRestingTime.MaxDate = new System.DateTime(2021, 1, 28, 23, 59, 59, 0);
			this.tpRestingTime.MinDate = new System.DateTime(2021, 1, 28, 0, 0, 0, 0);
			this.tpRestingTime.Name = "tpRestingTime";
			this.tpRestingTime.ShowUpDown = true;
			this.tpRestingTime.Size = new System.Drawing.Size(60, 23);
			this.tpRestingTime.TabIndex = 8;
			this.tpRestingTime.TimeSpan = System.TimeSpan.Parse("12:19:08.6650000");
			this.tpRestingTime.Value = new System.DateTime(2021, 1, 28, 12, 19, 8, 665);
			// 
			// tpWarningInterval
			// 
			this.tpWarningInterval.CustomFormat = "mmm:ss";
			this.tpWarningInterval.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.tpWarningInterval.Location = new System.Drawing.Point(173, 100);
			this.tpWarningInterval.MaxDate = new System.DateTime(2021, 1, 28, 23, 59, 59, 0);
			this.tpWarningInterval.MinDate = new System.DateTime(2021, 1, 28, 0, 0, 0, 0);
			this.tpWarningInterval.Name = "tpWarningInterval";
			this.tpWarningInterval.ShowUpDown = true;
			this.tpWarningInterval.Size = new System.Drawing.Size(60, 23);
			this.tpWarningInterval.TabIndex = 8;
			this.tpWarningInterval.TimeSpan = System.TimeSpan.Parse("12:19:08.6650000");
			this.tpWarningInterval.Value = new System.DateTime(2021, 1, 28, 12, 19, 8, 665);
			// 
			// ConfigurationFormView
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(250, 177);
			this.Controls.Add(this.tpWarningInterval);
			this.Controls.Add(this.tpRestingTime);
			this.Controls.Add(this.tpMaxBusyTime);
			this.Controls.Add(this.tpTimeToIdle);
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
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private TimePicker tpTimeToIdle;
		private TimePicker tpWarningInterval;
		private TimePicker tpRestingTime;
		private TimePicker tpMaxBusyTime;
	}
}