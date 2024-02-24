namespace MapMakerForSnakeGame
{
    partial class ManWindow
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
            textBox1 = new TextBox();
            Ok_Button = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 12);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(373, 201);
            textBox1.TabIndex = 0;
            textBox1.Text = "enter how many cells do you want";
            // 
            // Ok_Button
            // 
            Ok_Button.Location = new Point(414, 12);
            Ok_Button.Name = "Ok_Button";
            Ok_Button.Size = new Size(75, 23);
            Ok_Button.TabIndex = 1;
            Ok_Button.Text = "OK";
            Ok_Button.UseVisualStyleBackColor = true;
            Ok_Button.Click += button1_Click;
            // 
            // ManWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Ok_Button);
            Controls.Add(textBox1);
            Name = "ManWindow";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button Ok_Button;
    }
}
