namespace SquareScape.Client
{
    partial class SquareScape
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
            this.gameArea = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gameArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gameArea
            // 
            this.gameArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gameArea.Controls.Add(this.pictureBox1);
            this.gameArea.Location = new System.Drawing.Point(13, 45);
            this.gameArea.MaximumSize = new System.Drawing.Size(716, 414);
            this.gameArea.MinimumSize = new System.Drawing.Size(716, 414);
            this.gameArea.Name = "gameArea";
            this.gameArea.Size = new System.Drawing.Size(716, 414);
            this.gameArea.TabIndex = 4;
            this.gameArea.TabStop = false;
            this.gameArea.Text = "SquareScape";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.DarkRed;
            this.pictureBox1.Location = new System.Drawing.Point(17, 359);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(42, 39);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // SquareScape
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(741, 471);
            this.Controls.Add(this.gameArea);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(757, 510);
            this.MinimumSize = new System.Drawing.Size(757, 510);
            this.Name = "SquareScape";
            this.Text = "SquareScape";
            this.Load += new System.EventHandler(this.SquareScape_Load);
            this.gameArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gameArea;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

