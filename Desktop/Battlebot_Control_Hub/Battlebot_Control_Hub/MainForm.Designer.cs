namespace Battlebot_Control_Hub
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.HealthLB = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Enemy1LB = new System.Windows.Forms.Label();
            this.Enemy2LB = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Enemy4LB = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Enemy3LB = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.OurNexusLB = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TheirNexusLB = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(1087, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(331, 109);
            this.label1.TabIndex = 3;
            this.label1.Text = "Health:";
            // 
            // HealthLB
            // 
            this.HealthLB.AutoSize = true;
            this.HealthLB.Font = new System.Drawing.Font("Times New Roman", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HealthLB.ForeColor = System.Drawing.Color.Red;
            this.HealthLB.Location = new System.Drawing.Point(1457, 47);
            this.HealthLB.Name = "HealthLB";
            this.HealthLB.Size = new System.Drawing.Size(143, 109);
            this.HealthLB.TabIndex = 4;
            this.HealthLB.Text = "99";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1045, 195);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(289, 81);
            this.label3.TabIndex = 5;
            this.label3.Text = "Enemy 1";
            // 
            // Enemy1LB
            // 
            this.Enemy1LB.AutoSize = true;
            this.Enemy1LB.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Enemy1LB.Location = new System.Drawing.Point(1128, 297);
            this.Enemy1LB.Name = "Enemy1LB";
            this.Enemy1LB.Size = new System.Drawing.Size(107, 81);
            this.Enemy1LB.TabIndex = 6;
            this.Enemy1LB.Text = "24";
            // 
            // Enemy2LB
            // 
            this.Enemy2LB.AutoSize = true;
            this.Enemy2LB.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Enemy2LB.Location = new System.Drawing.Point(1446, 297);
            this.Enemy2LB.Name = "Enemy2LB";
            this.Enemy2LB.Size = new System.Drawing.Size(107, 81);
            this.Enemy2LB.TabIndex = 8;
            this.Enemy2LB.Text = "51";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1363, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(289, 81);
            this.label6.TabIndex = 7;
            this.label6.Text = "Enemy 2";
            // 
            // Enemy4LB
            // 
            this.Enemy4LB.AutoSize = true;
            this.Enemy4LB.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Enemy4LB.Location = new System.Drawing.Point(1446, 562);
            this.Enemy4LB.Name = "Enemy4LB";
            this.Enemy4LB.Size = new System.Drawing.Size(107, 81);
            this.Enemy4LB.TabIndex = 12;
            this.Enemy4LB.Text = "83";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1363, 460);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(289, 81);
            this.label8.TabIndex = 11;
            this.label8.Text = "Enemy 4";
            // 
            // Enemy3LB
            // 
            this.Enemy3LB.AutoSize = true;
            this.Enemy3LB.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Enemy3LB.Location = new System.Drawing.Point(1128, 562);
            this.Enemy3LB.Name = "Enemy3LB";
            this.Enemy3LB.Size = new System.Drawing.Size(107, 81);
            this.Enemy3LB.TabIndex = 10;
            this.Enemy3LB.Text = "99";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1045, 460);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(289, 81);
            this.label10.TabIndex = 9;
            this.label10.Text = "Enemy 3";
            // 
            // OurNexusLB
            // 
            this.OurNexusLB.AutoSize = true;
            this.OurNexusLB.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OurNexusLB.Location = new System.Drawing.Point(351, 865);
            this.OurNexusLB.Name = "OurNexusLB";
            this.OurNexusLB.Size = new System.Drawing.Size(143, 81);
            this.OurNexusLB.TabIndex = 14;
            this.OurNexusLB.Text = "853";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(256, 763);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(347, 81);
            this.label4.TabIndex = 13;
            this.label4.Text = "Our Nexus";
            // 
            // TheirNexusLB
            // 
            this.TheirNexusLB.AutoSize = true;
            this.TheirNexusLB.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TheirNexusLB.Location = new System.Drawing.Point(1181, 865);
            this.TheirNexusLB.Name = "TheirNexusLB";
            this.TheirNexusLB.Size = new System.Drawing.Size(143, 81);
            this.TheirNexusLB.TabIndex = 16;
            this.TheirNexusLB.Text = "134";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1067, 763);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(391, 81);
            this.label7.TabIndex = 15;
            this.label7.Text = "Their Nexus";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1010, 670);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1675, 997);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TheirNexusLB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.OurNexusLB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Enemy4LB);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Enemy3LB);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Enemy2LB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Enemy1LB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.HealthLB);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "Joystick -";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label HealthLB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Enemy1LB;
        private System.Windows.Forms.Label Enemy2LB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label Enemy4LB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label Enemy3LB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label OurNexusLB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label TheirNexusLB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

