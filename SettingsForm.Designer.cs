namespace B19_Ex05_Othelo_SimaWeiss_309823011
{
    partial class SettingsForm
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
            this.PlayAgainstComputerButton = new System.Windows.Forms.Button();
            this.PlayAgainstFriendButton = new System.Windows.Forms.Button();
            this.BoardSizeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PlayAgainstComputerButton
            // 
            this.PlayAgainstComputerButton.Location = new System.Drawing.Point(12, 66);
            this.PlayAgainstComputerButton.Margin = new System.Windows.Forms.Padding(2);
            this.PlayAgainstComputerButton.Name = "PlayAgainstComputerButton";
            this.PlayAgainstComputerButton.Size = new System.Drawing.Size(102, 53);
            this.PlayAgainstComputerButton.TabIndex = 0;
            this.PlayAgainstComputerButton.Text = "Play Against Computer";
            this.PlayAgainstComputerButton.UseVisualStyleBackColor = true;
            this.PlayAgainstComputerButton.Click += new System.EventHandler(this.PlayAgainstComputerButton_Click);
            // 
            // PlayAgainstFriendButton
            // 
            this.PlayAgainstFriendButton.Location = new System.Drawing.Point(118, 66);
            this.PlayAgainstFriendButton.Margin = new System.Windows.Forms.Padding(2);
            this.PlayAgainstFriendButton.Name = "PlayAgainstFriendButton";
            this.PlayAgainstFriendButton.Size = new System.Drawing.Size(102, 53);
            this.PlayAgainstFriendButton.TabIndex = 1;
            this.PlayAgainstFriendButton.Text = "Play Against Friend";
            this.PlayAgainstFriendButton.UseVisualStyleBackColor = true;
            this.PlayAgainstFriendButton.Click += new System.EventHandler(this.PlayAgainstFriendButton_Click);
            // 
            // BoardSizeButton
            // 
            this.BoardSizeButton.Location = new System.Drawing.Point(12, 12);
            this.BoardSizeButton.Name = "BoardSizeButton";
            this.BoardSizeButton.Size = new System.Drawing.Size(208, 39);
            this.BoardSizeButton.TabIndex = 2;
            this.BoardSizeButton.Text = "Board Size: 6x6 (Click to increase)";
            this.BoardSizeButton.UseVisualStyleBackColor = true;
            this.BoardSizeButton.Click += new System.EventHandler(this.BoardSizeButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 136);
            this.Controls.Add(this.BoardSizeButton);
            this.Controls.Add(this.PlayAgainstFriendButton);
            this.Controls.Add(this.PlayAgainstComputerButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Othello - Game Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button PlayAgainstComputerButton;
        private System.Windows.Forms.Button PlayAgainstFriendButton;
        private System.Windows.Forms.Button BoardSizeButton;
    }
}