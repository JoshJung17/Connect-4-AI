namespace ConnectFour
{
    partial class Form1
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
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnAIMove = new System.Windows.Forms.Button();
            this.chkAuto = new System.Windows.Forms.CheckBox();
            this.chkShowMoves = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(88, 122);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(218, 106);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnAIMove
            // 
            this.btnAIMove.Location = new System.Drawing.Point(443, 62);
            this.btnAIMove.Name = "btnAIMove";
            this.btnAIMove.Size = new System.Drawing.Size(75, 136);
            this.btnAIMove.TabIndex = 1;
            this.btnAIMove.Text = "AI Move";
            this.btnAIMove.UseVisualStyleBackColor = true;
            this.btnAIMove.Click += new System.EventHandler(this.btnAIMove_Click);
            // 
            // chkAuto
            // 
            this.chkAuto.AutoSize = true;
            this.chkAuto.Checked = true;
            this.chkAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAuto.Location = new System.Drawing.Point(443, 210);
            this.chkAuto.Name = "chkAuto";
            this.chkAuto.Size = new System.Drawing.Size(91, 17);
            this.chkAuto.TabIndex = 2;
            this.chkAuto.Text = "Auto AI Move";
            this.chkAuto.UseVisualStyleBackColor = true;
            // 
            // chkShowMoves
            // 
            this.chkShowMoves.AutoSize = true;
            this.chkShowMoves.Location = new System.Drawing.Point(443, 233);
            this.chkShowMoves.Name = "chkShowMoves";
            this.chkShowMoves.Size = new System.Drawing.Size(119, 17);
            this.chkShowMoves.TabIndex = 3;
            this.chkShowMoves.Text = "Show Move Scores";
            this.chkShowMoves.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 380);
            this.Controls.Add(this.chkShowMoves);
            this.Controls.Add(this.chkAuto);
            this.Controls.Add(this.btnAIMove);
            this.Controls.Add(this.btnPlay);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnAIMove;
        private System.Windows.Forms.CheckBox chkAuto;
        private System.Windows.Forms.CheckBox chkShowMoves;
    }
}

