namespace LoadLibraryInjector.View
{
	partial class UserForm
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
			this.ProcessListBox = new System.Windows.Forms.ListBox();
			this.InjectButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// ProcessListBox
			// 
			this.ProcessListBox.FormattingEnabled = true;
			this.ProcessListBox.Location = new System.Drawing.Point(0, 0);
			this.ProcessListBox.Name = "ProcessListBox";
			this.ProcessListBox.Size = new System.Drawing.Size(354, 277);
			this.ProcessListBox.TabIndex = 0;
			// 
			// InjectButton
			// 
			this.InjectButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.InjectButton.Location = new System.Drawing.Point(0, 281);
			this.InjectButton.Name = "InjectButton";
			this.InjectButton.Size = new System.Drawing.Size(354, 23);
			this.InjectButton.TabIndex = 1;
			this.InjectButton.Text = "Inject";
			this.InjectButton.UseVisualStyleBackColor = true;
			// 
			// UserForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(355, 306);
			this.Controls.Add(this.InjectButton);
			this.Controls.Add(this.ProcessListBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "UserForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Process";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox ProcessListBox;
		private System.Windows.Forms.Button InjectButton;
	}
}