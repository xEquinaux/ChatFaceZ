namespace ChatFaceZ
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			Credentials = new TabControl();
			tabPage1 = new TabPage();
			button4 = new Button();
			button3 = new Button();
			tabPage2 = new TabPage();
			tabPage3 = new TabPage();
			button2 = new Button();
			button1 = new Button();
			richTextBox1 = new RichTextBox();
			backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			Credentials.SuspendLayout();
			tabPage1.SuspendLayout();
			tabPage3.SuspendLayout();
			SuspendLayout();
			// 
			// Credentials
			// 
			Credentials.Controls.Add(tabPage1);
			Credentials.Controls.Add(tabPage2);
			Credentials.Controls.Add(tabPage3);
			Credentials.Location = new Point(12, 12);
			Credentials.Margin = new Padding(4, 3, 4, 3);
			Credentials.Name = "Credentials";
			Credentials.SelectedIndex = 0;
			Credentials.Size = new Size(357, 426);
			Credentials.TabIndex = 0;
			// 
			// tabPage1
			// 
			tabPage1.Controls.Add(button4);
			tabPage1.Controls.Add(button3);
			tabPage1.Location = new Point(4, 24);
			tabPage1.Margin = new Padding(4, 3, 4, 3);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new Padding(4, 3, 4, 3);
			tabPage1.Size = new Size(349, 398);
			tabPage1.TabIndex = 0;
			tabPage1.Text = "Chat";
			tabPage1.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			button4.Location = new Point(213, 35);
			button4.Name = "button4";
			button4.Size = new Size(129, 23);
			button4.TabIndex = 1;
			button4.Text = "Close Chat Window";
			button4.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			button3.Location = new Point(213, 6);
			button3.Name = "button3";
			button3.Size = new Size(129, 23);
			button3.TabIndex = 0;
			button3.Text = "Load Chat Window";
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_Click;
			// 
			// tabPage2
			// 
			tabPage2.Location = new Point(4, 24);
			tabPage2.Margin = new Padding(4, 3, 4, 3);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new Padding(4, 3, 4, 3);
			tabPage2.Size = new Size(349, 398);
			tabPage2.TabIndex = 1;
			tabPage2.Text = "Avatars";
			tabPage2.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			tabPage3.Controls.Add(button2);
			tabPage3.Controls.Add(button1);
			tabPage3.Controls.Add(richTextBox1);
			tabPage3.Location = new Point(4, 24);
			tabPage3.Name = "tabPage3";
			tabPage3.Padding = new Padding(3);
			tabPage3.Size = new Size(349, 398);
			tabPage3.TabIndex = 2;
			tabPage3.Text = "Credentials";
			tabPage3.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			button2.Location = new Point(187, 369);
			button2.Name = "button2";
			button2.Size = new Size(75, 23);
			button2.TabIndex = 0;
			button2.Text = "Load";
			button2.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			button1.Location = new Point(268, 369);
			button1.Name = "button1";
			button1.Size = new Size(75, 23);
			button1.TabIndex = 0;
			button1.Text = "Save";
			button1.UseVisualStyleBackColor = true;
			// 
			// richTextBox1
			// 
			richTextBox1.Location = new Point(6, 6);
			richTextBox1.Name = "richTextBox1";
			richTextBox1.Size = new Size(337, 357);
			richTextBox1.TabIndex = 0;
			richTextBox1.Text = resources.GetString("richTextBox1.Text");
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(382, 450);
			Controls.Add(Credentials);
			Margin = new Padding(4, 3, 4, 3);
			Name = "Form1";
			Text = "ChatFaceZ";
			Credentials.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			tabPage3.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private TabControl Credentials;
		private TabPage tabPage1;
		private TabPage tabPage2;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private TabPage tabPage3;
		private Button button2;
		private Button button1;
		private RichTextBox richTextBox1;
		private Button button3;
		private Button button4;
	}
}
