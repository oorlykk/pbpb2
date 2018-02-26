namespace pbpb
{
    partial class FormView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && ( components != null )) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(699, 410);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "FormView";
            this.Text = "PBPB View";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormView_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormView_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormView_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormView_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion
    }
}