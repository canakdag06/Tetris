
namespace Tetris
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Main_panel1 = new System.Windows.Forms.Panel();
            this.ScoreTable_panel1 = new System.Windows.Forms.Panel();
            this.ScoreTable_listView1 = new System.Windows.Forms.ListView();
            this.Ad_columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Score_columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Save_Score_label1 = new System.Windows.Forms.Label();
            this.Score_Table_label1 = new System.Windows.Forms.Label();
            this.Save_Score_panel1 = new System.Windows.Forms.Panel();
            this.Save_label1 = new System.Windows.Forms.Label();
            this.Save_Score_textBox1 = new System.Windows.Forms.TextBox();
            this.Player_Name_label1 = new System.Windows.Forms.Label();
            this.Game_Over_label1 = new System.Windows.Forms.Label();
            this.softDrop_timer1 = new System.Windows.Forms.Timer(this.components);
            this.hardDrop_timer1 = new System.Windows.Forms.Timer(this.components);
            this.Score_panel1 = new System.Windows.Forms.Panel();
            this.Score_label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Level_label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Lines_label1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Show_Next_panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.Main_panel1.SuspendLayout();
            this.ScoreTable_panel1.SuspendLayout();
            this.Save_Score_panel1.SuspendLayout();
            this.Score_panel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Main_panel1
            // 
            this.Main_panel1.BackColor = System.Drawing.Color.Black;
            this.Main_panel1.Controls.Add(this.ScoreTable_panel1);
            this.Main_panel1.Controls.Add(this.Save_Score_label1);
            this.Main_panel1.Controls.Add(this.Score_Table_label1);
            this.Main_panel1.Controls.Add(this.Save_Score_panel1);
            this.Main_panel1.Controls.Add(this.Game_Over_label1);
            this.Main_panel1.Location = new System.Drawing.Point(0, 0);
            this.Main_panel1.Name = "Main_panel1";
            this.Main_panel1.Size = new System.Drawing.Size(250, 500);
            this.Main_panel1.TabIndex = 0;
            // 
            // ScoreTable_panel1
            // 
            this.ScoreTable_panel1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ScoreTable_panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ScoreTable_panel1.Controls.Add(this.ScoreTable_listView1);
            this.ScoreTable_panel1.Location = new System.Drawing.Point(13, 66);
            this.ScoreTable_panel1.Name = "ScoreTable_panel1";
            this.ScoreTable_panel1.Size = new System.Drawing.Size(223, 330);
            this.ScoreTable_panel1.TabIndex = 4;
            this.ScoreTable_panel1.Visible = false;
            // 
            // ScoreTable_listView1
            // 
            this.ScoreTable_listView1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ScoreTable_listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Ad_columnHeader1,
            this.Score_columnHeader1});
            this.ScoreTable_listView1.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreTable_listView1.ForeColor = System.Drawing.Color.White;
            this.ScoreTable_listView1.HideSelection = false;
            this.ScoreTable_listView1.Location = new System.Drawing.Point(12, 8);
            this.ScoreTable_listView1.Name = "ScoreTable_listView1";
            this.ScoreTable_listView1.Size = new System.Drawing.Size(191, 308);
            this.ScoreTable_listView1.TabIndex = 0;
            this.ScoreTable_listView1.UseCompatibleStateImageBehavior = false;
            this.ScoreTable_listView1.View = System.Windows.Forms.View.Details;
            // 
            // Ad_columnHeader1
            // 
            this.Ad_columnHeader1.Text = "Ad";
            this.Ad_columnHeader1.Width = 81;
            // 
            // Score_columnHeader1
            // 
            this.Score_columnHeader1.Text = "Puan";
            this.Score_columnHeader1.Width = 106;
            // 
            // Save_Score_label1
            // 
            this.Save_Score_label1.BackColor = System.Drawing.Color.Green;
            this.Save_Score_label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Save_Score_label1.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Save_Score_label1.ForeColor = System.Drawing.Color.White;
            this.Save_Score_label1.Location = new System.Drawing.Point(17, 412);
            this.Save_Score_label1.Name = "Save_Score_label1";
            this.Save_Score_label1.Size = new System.Drawing.Size(99, 58);
            this.Save_Score_label1.TabIndex = 8;
            this.Save_Score_label1.Text = "PUANI KAYDET";
            this.Save_Score_label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Save_Score_label1.Visible = false;
            this.Save_Score_label1.Click += new System.EventHandler(this.Save_Score_label1_Click);
            this.Save_Score_label1.MouseLeave += new System.EventHandler(this.Save_Score_label1_MouseLeave);
            this.Save_Score_label1.MouseHover += new System.EventHandler(this.Save_Score_label1_MouseHover);
            // 
            // Score_Table_label1
            // 
            this.Score_Table_label1.BackColor = System.Drawing.Color.Brown;
            this.Score_Table_label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Score_Table_label1.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Score_Table_label1.ForeColor = System.Drawing.Color.White;
            this.Score_Table_label1.Location = new System.Drawing.Point(132, 411);
            this.Score_Table_label1.Name = "Score_Table_label1";
            this.Score_Table_label1.Size = new System.Drawing.Size(99, 59);
            this.Score_Table_label1.TabIndex = 7;
            this.Score_Table_label1.Text = "PUAN TABLOSU";
            this.Score_Table_label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Score_Table_label1.Visible = false;
            this.Score_Table_label1.Click += new System.EventHandler(this.Score_Table_label1_Click);
            this.Score_Table_label1.MouseLeave += new System.EventHandler(this.Score_Table_label1_MouseLeave);
            this.Score_Table_label1.MouseHover += new System.EventHandler(this.Score_Table_label1_MouseHover);
            // 
            // Save_Score_panel1
            // 
            this.Save_Score_panel1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Save_Score_panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Save_Score_panel1.Controls.Add(this.Save_label1);
            this.Save_Score_panel1.Controls.Add(this.Save_Score_textBox1);
            this.Save_Score_panel1.Controls.Add(this.Player_Name_label1);
            this.Save_Score_panel1.Location = new System.Drawing.Point(29, 91);
            this.Save_Score_panel1.Name = "Save_Score_panel1";
            this.Save_Score_panel1.Size = new System.Drawing.Size(191, 285);
            this.Save_Score_panel1.TabIndex = 3;
            this.Save_Score_panel1.Visible = false;
            // 
            // Save_label1
            // 
            this.Save_label1.BackColor = System.Drawing.Color.Blue;
            this.Save_label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Save_label1.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Save_label1.ForeColor = System.Drawing.Color.White;
            this.Save_label1.Location = new System.Drawing.Point(39, 207);
            this.Save_label1.Name = "Save_label1";
            this.Save_label1.Size = new System.Drawing.Size(104, 41);
            this.Save_label1.TabIndex = 8;
            this.Save_label1.Text = "KAYDET";
            this.Save_label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Save_label1.Visible = false;
            this.Save_label1.Click += new System.EventHandler(this.Save_label1_Click);
            this.Save_label1.MouseLeave += new System.EventHandler(this.Save_label1_MouseLeave);
            this.Save_label1.MouseHover += new System.EventHandler(this.Save_label1_MouseHover);
            // 
            // Save_Score_textBox1
            // 
            this.Save_Score_textBox1.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Save_Score_textBox1.Location = new System.Drawing.Point(18, 156);
            this.Save_Score_textBox1.Name = "Save_Score_textBox1";
            this.Save_Score_textBox1.Size = new System.Drawing.Size(152, 41);
            this.Save_Score_textBox1.TabIndex = 6;
            this.Save_Score_textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Save_Score_textBox1_KeyDown);
            // 
            // Player_Name_label1
            // 
            this.Player_Name_label1.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player_Name_label1.Location = new System.Drawing.Point(-4, 3);
            this.Player_Name_label1.Name = "Player_Name_label1";
            this.Player_Name_label1.Size = new System.Drawing.Size(194, 103);
            this.Player_Name_label1.TabIndex = 5;
            this.Player_Name_label1.Text = "ADINIZ NEDIR?";
            this.Player_Name_label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Game_Over_label1
            // 
            this.Game_Over_label1.BackColor = System.Drawing.Color.Black;
            this.Game_Over_label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Game_Over_label1.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Game_Over_label1.ForeColor = System.Drawing.Color.White;
            this.Game_Over_label1.Location = new System.Drawing.Point(29, 140);
            this.Game_Over_label1.Name = "Game_Over_label1";
            this.Game_Over_label1.Size = new System.Drawing.Size(191, 92);
            this.Game_Over_label1.TabIndex = 5;
            this.Game_Over_label1.Text = "OYUN BITTI";
            this.Game_Over_label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Game_Over_label1.Visible = false;
            // 
            // softDrop_timer1
            // 
            this.softDrop_timer1.Enabled = true;
            this.softDrop_timer1.Interval = 800;
            this.softDrop_timer1.Tick += new System.EventHandler(this.softDrop_timer1_Tick);
            // 
            // hardDrop_timer1
            // 
            this.hardDrop_timer1.Interval = 5;
            this.hardDrop_timer1.Tick += new System.EventHandler(this.hardDrop_timer1_Tick);
            // 
            // Score_panel1
            // 
            this.Score_panel1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Score_panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Score_panel1.Controls.Add(this.Score_label1);
            this.Score_panel1.Controls.Add(this.label4);
            this.Score_panel1.Controls.Add(this.Level_label1);
            this.Score_panel1.Controls.Add(this.label3);
            this.Score_panel1.Controls.Add(this.Lines_label1);
            this.Score_panel1.Controls.Add(this.label1);
            this.Score_panel1.Location = new System.Drawing.Point(256, 277);
            this.Score_panel1.Name = "Score_panel1";
            this.Score_panel1.Size = new System.Drawing.Size(181, 212);
            this.Score_panel1.TabIndex = 1;
            // 
            // Score_label1
            // 
            this.Score_label1.BackColor = System.Drawing.Color.Black;
            this.Score_label1.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Score_label1.ForeColor = System.Drawing.Color.White;
            this.Score_label1.Location = new System.Drawing.Point(27, 37);
            this.Score_label1.Name = "Score_label1";
            this.Score_label1.Size = new System.Drawing.Size(131, 26);
            this.Score_label1.TabIndex = 5;
            this.Score_label1.Text = "0";
            this.Score_label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(52, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 26);
            this.label4.TabIndex = 4;
            this.label4.Text = "PUAN";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Level_label1
            // 
            this.Level_label1.BackColor = System.Drawing.Color.Black;
            this.Level_label1.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Level_label1.ForeColor = System.Drawing.Color.White;
            this.Level_label1.Location = new System.Drawing.Point(27, 107);
            this.Level_label1.Name = "Level_label1";
            this.Level_label1.Size = new System.Drawing.Size(131, 26);
            this.Level_label1.TabIndex = 3;
            this.Level_label1.Text = "1";
            this.Level_label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(42, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "SEVIYE";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lines_label1
            // 
            this.Lines_label1.BackColor = System.Drawing.Color.Black;
            this.Lines_label1.Font = new System.Drawing.Font("Gill Sans Ultra Bold Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lines_label1.ForeColor = System.Drawing.Color.White;
            this.Lines_label1.Location = new System.Drawing.Point(27, 176);
            this.Lines_label1.Name = "Lines_label1";
            this.Lines_label1.Size = new System.Drawing.Size(131, 26);
            this.Lines_label1.TabIndex = 1;
            this.Lines_label1.Text = "0";
            this.Lines_label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "SATIRLAR";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Show_Next_panel1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(256, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(181, 247);
            this.panel1.TabIndex = 2;
            // 
            // Show_Next_panel1
            // 
            this.Show_Next_panel1.BackColor = System.Drawing.Color.Black;
            this.Show_Next_panel1.Location = new System.Drawing.Point(14, 37);
            this.Show_Next_panel1.Name = "Show_Next_panel1";
            this.Show_Next_panel1.Size = new System.Drawing.Size(150, 200);
            this.Show_Next_panel1.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 26);
            this.label5.TabIndex = 4;
            this.label5.Text = "SONRAKI";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(439, 501);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Score_panel1);
            this.Controls.Add(this.Main_panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tetris";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Main_panel1.ResumeLayout(false);
            this.ScoreTable_panel1.ResumeLayout(false);
            this.Save_Score_panel1.ResumeLayout(false);
            this.Save_Score_panel1.PerformLayout();
            this.Score_panel1.ResumeLayout(false);
            this.Score_panel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Main_panel1;
        private System.Windows.Forms.Timer softDrop_timer1;
        private System.Windows.Forms.Timer hardDrop_timer1;
        private System.Windows.Forms.Panel Score_panel1;
        private System.Windows.Forms.Label Lines_label1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Level_label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Score_label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel Show_Next_panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Game_Over_label1;
        private System.Windows.Forms.Label Score_Table_label1;
        private System.Windows.Forms.Label Save_Score_label1;
        private System.Windows.Forms.Panel Save_Score_panel1;
        private System.Windows.Forms.Label Save_label1;
        private System.Windows.Forms.TextBox Save_Score_textBox1;
        private System.Windows.Forms.Label Player_Name_label1;
        private System.Windows.Forms.Panel ScoreTable_panel1;
        private System.Windows.Forms.ListView ScoreTable_listView1;
        private System.Windows.Forms.ColumnHeader Ad_columnHeader1;
        private System.Windows.Forms.ColumnHeader Score_columnHeader1;
    }
}

