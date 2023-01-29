using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Threading;

namespace Tetris
{
    public partial class Form1 : Form
    {

        OleDbConnection baglanti = new OleDbConnection ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=TetrisData.mdb");
        
        private void showData()
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand();
            komut.Connection = baglanti;
            komut.CommandText = ("Select * From ScoreTable Order By Score DESC");
            OleDbDataReader oku = komut.ExecuteReader();
            while(oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["Ad"].ToString();
                ekle.SubItems.Add(oku["Score"].ToString());

                ScoreTable_listView1.Items.Add(ekle);
            }
            baglanti.Close();
        }

        Random rnd = new Random();
        int Lines = 0;
        int Level = 1;
        int Score = 0;
        int[] Soft_Drop_Timer_Intervals = { 800, 800, 500, 240, 215, 190, 165, 140, 115, 90, 65,
                                                           40, 30, 25, 25, 25, 20, 20, 20, 15, 15,
                                                                       10, 10, 10, 10, 10, 10, 5};

        bool gamebegin = false;
        int Current_Rnd_Tetromino = 0;
        int Next_Rnd_Tetromino = 0;
        bool gameover = false;
        bool pause = false;

        public void StartScreen()
        {
             Application.Run(new SplashScreen());
        }

        public Form1()
        {
            Thread t = new Thread(new ThreadStart(StartScreen));
            t.Start();
            Thread.Sleep(5000);
            InitializeComponent();
            t.Abort();
            Next_Tetromino();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right && !rightLimit() && !pause)
            {
                moveRight();
            }
            if(e.KeyCode == Keys.Left && !leftLimit() && !pause)
            {
                moveLeft();
            }
            if(e.KeyCode == Keys.Down && !downLimit() && !pause)
            {
                moveDown();
                Score++;
                Score_label1.Text = Score.ToString();
            }
            if (e.KeyCode == Keys.Up && !pause)
            {
                Rotate_Tetromino();
            }
            if (e.KeyCode == Keys.Space && !pause && !gameover)
            {
                hardDrop_timer1.Enabled = true;
            }
            if(e.KeyCode == Keys.P)
            {
                if(pause)
                {
                    softDrop_timer1.Enabled = true;
                    pause = false;
                }
                else
                {
                    softDrop_timer1.Enabled = false;
                    hardDrop_timer1.Enabled = false;
                    pause = true;
                }
            }
        }

        public bool rightLimit()
        {
            bool Limit = false;
            foreach (Control c in Main_panel1.Controls)
            {
                if (c.GetType() == typeof(PictureBox))
                {
                    PictureBox pb = c as PictureBox;

                    if (pb.Enabled && pb.Visible)
                    {
                        if (pb.Left + 25 > 225)
                        {
                            Limit = true;
                        }
                    }
                }
            }
            if (!Limit)         // blokların üst üste binmesini önler
            {
                foreach (Control c in Main_panel1.Controls)
                {
                    if (c.GetType() == typeof(PictureBox))
                    {
                        PictureBox pb = c as PictureBox;

                        if (pb.Enabled && pb.Visible)
                        {
                            foreach (Control d in Main_panel1.Controls)
                            {
                                if (d.GetType() == typeof(PictureBox))
                                {
                                    PictureBox pb2 = d as PictureBox;

                                    if (!pb2.Enabled && pb.Location.X == pb2.Location.X - 25
                                        && pb.Location.Y == pb2.Location.Y)
                                    {
                                        Limit = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Limit;
        }

        public void moveRight()
        {
            foreach (Control c in Main_panel1.Controls)
            {
                if (c.GetType() == typeof(PictureBox))
                {
                    PictureBox pb = c as PictureBox;

                    if (pb.Enabled && pb.Visible)
                    {
                        pb.Left = pb.Left + 25;
                    }
                }
            }
        }

        public bool leftLimit()
        {
            bool Limit = false;

            foreach (Control c in Main_panel1.Controls)
            {
                if (c.GetType() == typeof(PictureBox))
                {
                    PictureBox pb = c as PictureBox;

                    if (pb.Enabled && pb.Visible)
                    {
                        if (pb.Left - 25 < 0)
                        {
                            Limit = true;
                        }
                    }
                }
            }

            if (!Limit)         // blokların üst üste binmesini önler
            {
                foreach (Control c in Main_panel1.Controls)
                {
                    if (c.GetType() == typeof(PictureBox))
                    {
                        PictureBox pb = c as PictureBox;

                        if (pb.Enabled && pb.Visible)
                        {
                            foreach (Control d in Main_panel1.Controls)
                            {
                                if (d.GetType() == typeof(PictureBox))
                                {
                                    PictureBox pb2 = d as PictureBox;

                                    if (!pb2.Enabled && pb.Location.X == pb2.Location.X + 25
                                        && pb.Location.Y == pb2.Location.Y)
                                    {
                                        Limit = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Limit;
        }

        public void moveLeft()
        {
            foreach (Control c in Main_panel1.Controls)
            {
                if (c.GetType() == typeof(PictureBox))
                {
                    PictureBox pb = c as PictureBox;

                    if (pb.Enabled && pb.Visible)
                    {
                        pb.Left = pb.Left - 25;
                    }
                }
            }
        }
        public bool downLimit()
        {
            bool Limit = false;

            foreach (Control c in Main_panel1.Controls)
            {
                if (c.GetType() == typeof(PictureBox))
                {
                    PictureBox pb = c as PictureBox;

                    if (pb.Enabled && pb.Visible)
                    {
                        if (pb.Top + 25 > 475)
                        {
                            Limit = true;
                        }
                    }
                }
            }

            if(!Limit)          // blokların üst üste binmesini önler
            {
                foreach (Control c in Main_panel1.Controls)
                {
                    if (c.GetType() == typeof(PictureBox))
                    {
                        PictureBox pb = c as PictureBox;

                        if (pb.Enabled && pb.Visible)
                        {
                            foreach (Control d in Main_panel1.Controls)
                            {
                                if (d.GetType() == typeof(PictureBox))
                                {
                                    PictureBox pb2 = d as PictureBox;

                                    if (!pb2.Enabled && pb2.Visible && pb.Location.X == pb2.Location.X 
                                        && pb.Location.Y == pb2.Location.Y - 25)
                                    {
                                        Limit = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Limit;
        }

        public void moveDown()
        {
            foreach (Control c in Main_panel1.Controls)
            {
                if (c.GetType() == typeof(PictureBox))
                {
                    PictureBox pb = c as PictureBox;

                    if (pb.Enabled && pb.Visible)
                    {
                        pb.Top = pb.Top + 25;
                    }
                }
            }
        }

        //DÜŞME HIZI
        private void softDrop_timer1_Tick(object sender, EventArgs e)
        {
            if (!downLimit() && !pause)
            {
                moveDown();
            }
            else
            {
                Tetromino_Disable();
                hardDrop_timer1.Enabled = false;
            }
        }
        private void hardDrop_timer1_Tick(object sender, EventArgs e)
        {
            if (!downLimit() && !pause)
            {
                moveDown();
                Score+=2;
                Score_label1.Text = Score.ToString();
            }
            else
            {
                hardDrop_timer1.Enabled = false;
            }
        }

        //TETROMINO OLUŞTURMA
        public void Tetromino_Disable()
        {
            foreach (Control c in Main_panel1.Controls)
            {
                if (c.GetType() == typeof(PictureBox))
                {
                    PictureBox pb = c as PictureBox;

                    if (pb.Enabled && pb.Visible)
                    {
                        pb.Enabled = false;
                    }

                    if(!pb.Enabled && pb.Visible && pb.Location.Y < 0)
                    {
                        Game_Over_label1.Visible = true;
                        Save_Score_label1.Visible = true;
                        Score_Table_label1.Visible = true;
                        softDrop_timer1.Enabled = false;
                        hardDrop_timer1.Enabled = false;
                        gameover = true;
                        break;
                    }
                }
            }
            if(!gameover)
            {
                hardDrop_timer1.Enabled = false;
                Check_Fulled_Rows();
                Next_Tetromino();
            }
        }

        public void Create_Tetrominos(int next_tetromino)       // farklı şekillerde tetrominoları oluşturur
        {
            PictureBox[] Tetrominos = new PictureBox[4];

            for(int i=0; i<4; i++)
            {
                var tetromino = new PictureBox();

                Tetrominos[i] = tetromino;

                tetromino.Size = new Size(25, 25);
                tetromino.BorderStyle = BorderStyle.FixedSingle;
                tetromino.Enabled = true;
                tetromino.Visible = true;

                if (next_tetromino == 0)
                {
                    tetromino.BackColor = Color.Red;
                    if(i == 0)
                    {
                        tetromino.Location = new Point(75, -50);
                    }
                    else if (i == 1)
                    {
                        tetromino.Location = new Point(100, -50);
                    }
                    else if (i == 2)
                    {
                        tetromino.Location = new Point(100, -25);
                    }
                    else if (i == 3)
                    {
                        tetromino.Location = new Point(125, -25);
                    }
                }
                else if (next_tetromino == 1)
                {
                    tetromino.BackColor = Color.Orange;
                    if (i == 0)
                    {
                        tetromino.Location = new Point(75, -25);
                    }
                    else if (i == 1)
                    {
                        tetromino.Location = new Point(100, -25);
                    }
                    else if (i == 2)
                    {
                        tetromino.Location = new Point(125, -25);
                    }
                    else if (i == 3)
                    {
                        tetromino.Location = new Point(125, -50);
                    }
                }
                else if (next_tetromino == 2)
                {
                    tetromino.BackColor = Color.Yellow;
                    if (i == 0)
                    {
                        tetromino.Location = new Point(100, -50);
                    }
                    else if (i == 1)
                    {
                        tetromino.Location = new Point(125, -50);
                    }
                    else if (i == 2)
                    {
                        tetromino.Location = new Point(100, -25);
                    }
                    else if (i == 3)
                    {
                        tetromino.Location = new Point(125, -25);
                    }
                }
                else if (next_tetromino == 3)
                {
                    tetromino.BackColor = Color.Green;
                    if (i == 0)
                    {
                        tetromino.Location = new Point(75, -25);
                    }
                    else if (i == 1)
                    {
                        tetromino.Location = new Point(100, -25);
                    }
                    else if (i == 2)
                    {
                        tetromino.Location = new Point(100, -50);
                    }
                    else if (i == 3)
                    {
                        tetromino.Location = new Point(125, -50);
                    }
                }
                else if (next_tetromino == 4)
                {
                    tetromino.BackColor = Color.Cyan;
                    if (i == 0)
                    {
                        tetromino.Location = new Point(75, -25);
                    }
                    else if (i == 1)
                    {
                        tetromino.Location = new Point(100, -25);
                    }
                    else if (i == 2)
                    {
                        tetromino.Location = new Point(125, -25);
                    }
                    else if (i == 3)
                    {
                        tetromino.Location = new Point(150, -25);
                    }
                }
                else if (next_tetromino == 5)
                {
                    tetromino.BackColor = Color.Blue;
                    if (i == 0)
                    {
                        tetromino.Location = new Point(75, -25);
                    }
                    else if (i == 1)
                    {
                        tetromino.Location = new Point(100, -25);
                    }
                    else if (i == 2)
                    {
                        tetromino.Location = new Point(125, -25);
                    }
                    else if (i == 3)
                    {
                        tetromino.Location = new Point(75, -50);
                    }
                }
                else if (next_tetromino == 6)
                {
                    tetromino.BackColor = Color.Purple;
                    if (i == 0)
                    {
                        tetromino.Location = new Point(75, -25);
                    }
                    else if (i == 1)
                    {
                        tetromino.Location = new Point(100, -25);
                    }
                    else if (i == 2)
                    {
                        tetromino.Location = new Point(125, -25);
                    }
                    else if (i == 3)
                    {
                        tetromino.Location = new Point(100, -50);
                    }
                }
                Main_panel1.Controls.Add(tetromino);
            }
        }


        public void Show_Next_Tetromino(int next_tetromino)     
        {
            PictureBox[] Tetrominos = new PictureBox[4];

            for (int i = 0; i < 4; i++)
            {
                var tetromino = new PictureBox();

                Tetrominos[i] = tetromino;

                tetromino.Size = new Size(25, 25);
                tetromino.BorderStyle = BorderStyle.FixedSingle;
                tetromino.Enabled = true;
                tetromino.Visible = true;

                if (next_tetromino == 0)
                {
                    tetromino.BackColor = Color.Red;
                    if (i == 0)
                    {
                        tetromino.Location = new Point(40, 75);
                    }
                    else if (i == 1)
                    {
                        tetromino.Location = new Point(65, 75);
                    }
                    else if (i == 2)
                    {
                        tetromino.Location = new Point(65, 100);
                    }
                    else if (i == 3)
                    {
                        tetromino.Location = new Point(90, 100);
                    }
                }
                else if (next_tetromino == 1)
                {
                    tetromino.BackColor = Color.Orange;
                    if (i == 0)
                    {
                        tetromino.Location = new Point(40, 100);
                    }
                    else if (i == 1)
                    {
                        tetromino.Location = new Point(65, 100);
                    }
                    else if (i == 2)
                    {
                        tetromino.Location = new Point(90, 100);
                    }
                    else if (i == 3)
                    {
                        tetromino.Location = new Point(90, 75);
                    }
                }
                else if (next_tetromino == 2)
                {
                    tetromino.BackColor = Color.Yellow;
                    if (i == 0)
                    {
                        tetromino.Location = new Point(50, 75);
                    }
                    else if (i == 1)
                    {
                        tetromino.Location = new Point(75, 75);
                    }
                    else if (i == 2)
                    {
                        tetromino.Location = new Point(50, 100);
                    }
                    else if (i == 3)
                    {
                        tetromino.Location = new Point(75, 100);
                    }
                }
                else if (next_tetromino == 3)
                {
                    tetromino.BackColor = Color.Green;
                    if (i == 0)
                    {
                        tetromino.Location = new Point(65, 75);
                    }
                    else if (i == 1)
                    {
                        tetromino.Location = new Point(90, 75);
                    }
                    else if (i == 2)
                    {
                        tetromino.Location = new Point(40, 100);
                    }
                    else if (i == 3)
                    {
                        tetromino.Location = new Point(65, 100);
                    }
                }
                else if (next_tetromino == 4)
                {
                    tetromino.BackColor = Color.Cyan;
                    if (i == 0)
                    {
                        tetromino.Location = new Point(25, 75);
                    }
                    else if (i == 1)
                    {
                        tetromino.Location = new Point(50, 75);
                    }
                    else if (i == 2)
                    {
                        tetromino.Location = new Point(75, 75);
                    }
                    else if (i == 3)
                    {
                        tetromino.Location = new Point(100, 75);
                    }
                }
                else if (next_tetromino == 5)
                {
                    tetromino.BackColor = Color.Blue;
                    if (i == 0)
                    {
                        tetromino.Location = new Point(40, 100);
                    }
                    else if (i == 1)
                    {
                        tetromino.Location = new Point(65, 100);
                    }
                    else if (i == 2)
                    {
                        tetromino.Location = new Point(90, 100);
                    }
                    else if (i == 3)
                    {
                        tetromino.Location = new Point(40, 75);
                    }
                }
                else if (next_tetromino == 6)
                {
                    tetromino.BackColor = Color.Purple;
                    if (i == 0)
                    {
                        tetromino.Location = new Point(40, 100);
                    }
                    else if (i == 1)
                    {
                        tetromino.Location = new Point(65, 100);
                    }
                    else if (i == 2)
                    {
                        tetromino.Location = new Point(90, 100);
                    }
                    else if (i == 3)
                    {
                        tetromino.Location = new Point(65, 75);
                    }
                }
                Show_Next_panel1.Controls.Add(tetromino);
            }
        }

        public void Next_Tetromino()
        {
            Show_Next_panel1.Controls.Clear();
            Next_Rnd_Tetromino = rnd.Next(0, 7);
            Show_Next_Tetromino(Next_Rnd_Tetromino);

            if(!gamebegin)
            {
                gamebegin = true;
                Current_Rnd_Tetromino = rnd.Next(0, 7);
            }

            Create_Tetrominos(Current_Rnd_Tetromino);
            Current_Rnd_Tetromino = Next_Rnd_Tetromino;
        }

        // DÖNDÜRME İŞLEMLERİ
        public void Rotate_Tetromino()
        {
            int[,] blocks_current_locations = new int[4,2];
            int[,] blocks_zero_locations = new int[4, 2];
            int count = 0;

            foreach (Control c in Main_panel1.Controls)
            {
                if (c.GetType() == typeof(PictureBox))
                {
                    PictureBox pb = c as PictureBox;

                    if (pb.Enabled && pb.Visible)
                    {
                        blocks_current_locations[count, 0] = pb.Location.X;     // blokların koordinatlarını atar.
                        blocks_current_locations[count, 1] = pb.Location.Y;
                        count++;
                    }
                }
            }

            int minuend_x = blocks_current_locations[0, 0];
            int minuend_y = blocks_current_locations[0, 1];

            for(int i=0; i<4; i++)
            {
                blocks_zero_locations[i, 0] = blocks_current_locations[i, 0] - minuend_x;
                blocks_zero_locations[i, 1] = blocks_current_locations[i, 1] - minuend_y;
            }

            if(!rotationLimit(blocks_current_locations, blocks_zero_locations))
            {
                foreach (Control c in Main_panel1.Controls)
                {
                    if (c.GetType() == typeof(PictureBox))
                    {
                        PictureBox pb = c as PictureBox;

                        if (pb.Enabled && pb.Visible)
                        {
                            // Z TETROMINO (KIRMIZI)
                            if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 25 && blocks_zero_locations[1, 1] == 0
                            && blocks_zero_locations[2, 0] == 25 && blocks_zero_locations[2, 1] == 25
                            && blocks_zero_locations[3, 0] == 50 && blocks_zero_locations[3, 1] == 25)
                            {   // 1den 2ye geçiş
                                // 0,0 | 25,0 | 25,25 | 50,25
                                // 25,-25 | 25,0 | 0,0 | 0,25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Left = pb.Left - 50;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == 25
                            && blocks_zero_locations[2, 0] == -25 && blocks_zero_locations[2, 1] == 25
                            && blocks_zero_locations[3, 0] == -25 && blocks_zero_locations[3, 1] == 50)
                            {   // 2den 3e geçiş
                                // 25,-25 | 25,0 | 0,0 | 0,25
                                // 50,0 | 25,0 | 25,-25 | 0,-25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Top = pb.Top - 50;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == -25 && blocks_zero_locations[1, 1] == 0
                            && blocks_zero_locations[2, 0] == -25 && blocks_zero_locations[2, 1] == -25
                            && blocks_zero_locations[3, 0] == -50 && blocks_zero_locations[3, 1] == -25)
                            {   // 3ten 4e geçiş
                                // 50,0 | 25,0 | 25,-25 | 0,-25
                                // 25,25 | 25,0 | 50,0 | 50,-25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Left = pb.Left + 50;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == -25
                            && blocks_zero_locations[2, 0] == 25 && blocks_zero_locations[2, 1] == -25
                            && blocks_zero_locations[3, 0] == 25 && blocks_zero_locations[3, 1] == -50)
                            {   // 4ten 1e geçiş
                                // 25,25 | 25,0 | 50,0 | 50,-25
                                // 0,0 | 25,0 | 25,25 | 50,25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Top = pb.Top + 50;
                                }
                            }
                            // Z TETROMINO (KIRMIZI) BİTİŞ
                            //////////////////////////
                            // L TETROMİNO (TURUNCU)
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 25 && blocks_zero_locations[1, 1] == 0
                            && blocks_zero_locations[2, 0] == 50 && blocks_zero_locations[2, 1] == 0
                            && blocks_zero_locations[3, 0] == 50 && blocks_zero_locations[3, 1] == -25)
                            {   // 1den 2ye geçiş
                                // 0,0 | 25,0 | 50,0 | 50,-25
                                // 25,-25 | 25,0 | 25,25 | 50,25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Top = pb.Top + 50;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == 25
                            && blocks_zero_locations[2, 0] == 0 && blocks_zero_locations[2, 1] == 50
                            && blocks_zero_locations[3, 0] == 25 && blocks_zero_locations[3, 1] == 50)
                            {   // 2den 3e geçiş
                                // 25,-25 | 25,0 | 25,25 | 50,25
                                // 50,0 | 25,0 | 0,0 | 0,25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Left = pb.Left - 50;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == -25 && blocks_zero_locations[1, 1] == 0
                            && blocks_zero_locations[2, 0] == -50 && blocks_zero_locations[2, 1] == 0
                            && blocks_zero_locations[3, 0] == -50 && blocks_zero_locations[3, 1] == 25)
                            {   // 3ten 4e geçiş
                                // 50,0 | 25,0 | 0,0 | 0,25
                                // 25,25 | 25,0 | 25,25 | 0,-25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Top = pb.Top - 50;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == -25
                            && blocks_zero_locations[2, 0] == 0 && blocks_zero_locations[2, 1] == -50
                            && blocks_zero_locations[3, 0] == -25 && blocks_zero_locations[3, 1] == -50)
                            {   // 4ten 1e geçiş
                                // 25,25 | 25,0 | 25,25 | 0,-25
                                // 0,0 | 25,0 | 50,0 | 50,-25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Left = pb.Left + 50;
                                }
                            }
                            // L TETROMINO (TURUNCU) BİTİŞ
                            /////////////////////////
                            // S TETROMINO (YEŞİL)
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 25 && blocks_zero_locations[1, 1] == 0
                            && blocks_zero_locations[2, 0] == 25 && blocks_zero_locations[2, 1] == -25
                            && blocks_zero_locations[3, 0] == 50 && blocks_zero_locations[3, 1] == -25)
                            {   // 1den 2ye geçiş
                                // 0,0 | 25,0 | 25,-25 | 50,-25
                                // 25,-25 | 25,0 | 50,0 | 50,25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Top = pb.Top + 50;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == 25
                            && blocks_zero_locations[2, 0] == 25 && blocks_zero_locations[2, 1] == 25
                            && blocks_zero_locations[3, 0] == 25 && blocks_zero_locations[3, 1] == 50)
                            {   // 2den 3e geçiş
                                // 25,-25 | 25,0 | 50,0 | 50,25
                                // 50,0 | 25,0 | 25,25 | 0,25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Left = pb.Left - 50;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == -25 && blocks_zero_locations[1, 1] == 0
                            && blocks_zero_locations[2, 0] == -25 && blocks_zero_locations[2, 1] == 25
                            && blocks_zero_locations[3, 0] == -50 && blocks_zero_locations[3, 1] == 25)
                            {   // 3ten 4e geçiş
                                // 50,0 | 25,0 | 25,25 | 0,25
                                // 25,25 | 25,0 | 0,0 | 0,-25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Top = pb.Top - 50;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == -25
                            && blocks_zero_locations[2, 0] == -25 && blocks_zero_locations[2, 1] == -25
                            && blocks_zero_locations[3, 0] == -25 && blocks_zero_locations[3, 1] == -50)
                            {   // 4ten 1e geçiş
                                // 25,25 | 25,0 | 0,0 | 0,-25
                                // 0,0 | 25,0 | 25,-25 | 50,-25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Left = pb.Left + 50;
                                }
                            }
                            // S TETROMINO (YEŞİL) BİTİŞ
                            ////////////////////////
                            // I TETROMINO (CYAN)
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 25 && blocks_zero_locations[1, 1] == 0
                            && blocks_zero_locations[2, 0] == 50 && blocks_zero_locations[2, 1] == 0
                            && blocks_zero_locations[3, 0] == 75 && blocks_zero_locations[3, 1] == 0)
                            {   // 1den 2ye geçiş
                                // 0,0 | 25,0 | 50,0 | 75,0
                                // 50,-25 | 50,0 | 50,25 | 50,50
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left + 50;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[1, 0]
                                && pb.Location.Y == blocks_current_locations[1, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top + 50;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == 25
                            && blocks_zero_locations[2, 0] == 0 && blocks_zero_locations[2, 1] == 50
                            && blocks_zero_locations[3, 0] == 0 && blocks_zero_locations[3, 1] == 75)
                            {   // 2den 3e geçiş
                                // 50,-25 | 50,0 | 50,25 | 50,50
                                // 75,25 | 50,25 | 25,25 | 0,25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top + 50;
                                }
                                else if (pb.Location.X == blocks_current_locations[1, 0]
                                && pb.Location.Y == blocks_current_locations[1, 1])
                                {
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Left = pb.Left - 50;
                                    pb.Top = pb.Top - 25;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == -25 && blocks_zero_locations[1, 1] == 0
                            && blocks_zero_locations[2, 0] == -50 && blocks_zero_locations[2, 1] == 0
                            && blocks_zero_locations[3, 0] == -75 && blocks_zero_locations[3, 1] == 0)
                            {   // 3ten 4e geçiş
                                // 75,25 | 50,25 | 25,25 | 0,25
                                // 25,50 | 25,25 | 25,0 | 25,-25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left - 50;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[1, 0]
                                && pb.Location.Y == blocks_current_locations[1, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top - 50;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == -25
                            && blocks_zero_locations[2, 0] == 0 && blocks_zero_locations[2, 1] == -50
                            && blocks_zero_locations[3, 0] == 0 && blocks_zero_locations[3, 1] == -75)
                            {   // 4ten 1e geçiş
                                // 25,50 | 25,25 | 25,0 | 25,-25
                                // 0,0 | 25,0 | 50,0 | 75,0
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top - 50;
                                }
                                else if (pb.Location.X == blocks_current_locations[1, 0]
                                && pb.Location.Y == blocks_current_locations[1, 1])
                                {
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Left = pb.Left + 50;
                                    pb.Top = pb.Top + 25;
                                }
                            }
                            // I TETROMINO (CYAN) BİTİŞ
                            ///////////////////////
                            // J TETROMINO (MAVİ)
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 25 && blocks_zero_locations[1, 1] == 0
                            && blocks_zero_locations[2, 0] == 50 && blocks_zero_locations[2, 1] == 0
                            && blocks_zero_locations[3, 0] == 0 && blocks_zero_locations[3, 1] == -25)
                            {   // 1den 2ye geçiş
                                // 0,0 | 25,0 | 50,0 | 0,-25
                                // 25,-25 | 25,0 | 25,25 | 50,-25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Left = pb.Left + 50;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == 25
                            && blocks_zero_locations[2, 0] == 0 && blocks_zero_locations[2, 1] == 50
                            && blocks_zero_locations[3, 0] == 25 && blocks_zero_locations[3, 1] == 0)
                            {   // 2den 3e geçiş
                                // 25,-25 | 25,0 | 25,25 | 50,-25
                                // 50,0 | 25,0 | 0,0 | 50,25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Top = pb.Top + 50;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == -25 && blocks_zero_locations[1, 1] == 0
                            && blocks_zero_locations[2, 0] == -50 && blocks_zero_locations[2, 1] == 0
                            && blocks_zero_locations[3, 0] == 0 && blocks_zero_locations[3, 1] == 25)
                            {   // 3ten 4e geçiş
                                // 50,0 | 25,0 | 0,0 | 50,25
                                // 25,25 | 25,0 | 25,25 | 0,25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Left = pb.Left - 50;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == -25
                            && blocks_zero_locations[2, 0] == 0 && blocks_zero_locations[2, 1] == -50
                            && blocks_zero_locations[3, 0] == -25 && blocks_zero_locations[3, 1] == 0)
                            {   // 4ten 1e geçiş
                                // 25,25 | 25,0 | 25,25 | 0,25
                                // 0,0 | 25,0 | 50,0 | 0,-25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Top = pb.Top - 50;
                                }
                            }
                            // J TETROMINO (MAVİ) BİTİŞ
                            ///////////////////////
                            // T TETROMINO (MOR)
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 25 && blocks_zero_locations[1, 1] == 0
                            && blocks_zero_locations[2, 0] == 50 && blocks_zero_locations[2, 1] == 0
                            && blocks_zero_locations[3, 0] == 25 && blocks_zero_locations[3, 1] == -25)
                            {   // 1den 2ye geçiş
                                // 0,0 | 25,0 | 50,0 | 25,-25
                                // 25,-25 | 25,0 | 25,25 | 50,0
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top + 25;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == 25
                            && blocks_zero_locations[2, 0] == 0 && blocks_zero_locations[2, 1] == 50
                            && blocks_zero_locations[3, 0] == 25 && blocks_zero_locations[3, 1] == 25)
                            {   // 2den 3e geçiş
                                // 25,-25 | 25,0 | 25,25 | 50,0
                                // 50,0 | 25,0 | 0,0 | 25,25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top + 25;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == -25 && blocks_zero_locations[1, 1] == 0
                            && blocks_zero_locations[2, 0] == -50 && blocks_zero_locations[2, 1] == 0
                            && blocks_zero_locations[3, 0] == -25 && blocks_zero_locations[3, 1] == 25)
                            {   // 3ten 4e geçiş
                                // 50,0 | 25,0 | 0,0 | 25,25
                                // 25,25 | 25,0 | 25,25 | 0,0
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top - 25;
                                }
                            }
                            else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                            && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == -25
                            && blocks_zero_locations[2, 0] == 0 && blocks_zero_locations[2, 1] == -50
                            && blocks_zero_locations[3, 0] == -25 && blocks_zero_locations[3, 1] == -25)
                            {   // 4ten 1e geçiş
                                // 25,25 | 25,0 | 25,25 | 0,0
                                // 0,0 | 25,0 | 50,0 | 25,-25
                                if (pb.Location.X == blocks_current_locations[0, 0]
                                && pb.Location.Y == blocks_current_locations[0, 1])
                                {
                                    pb.Left = pb.Left - 25;
                                    pb.Top = pb.Top - 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[2, 0]
                                && pb.Location.Y == blocks_current_locations[2, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top + 25;
                                }
                                else if (pb.Location.X == blocks_current_locations[3, 0]
                                && pb.Location.Y == blocks_current_locations[3, 1])
                                {
                                    pb.Left = pb.Left + 25;
                                    pb.Top = pb.Top - 25;
                                }
                            }
                            // T TETROMINO (MOR) BİTİŞ
                        }
                    }
                }
            }
        }

        public bool rotationLimit(int[,] blocks_current_locations, int[,] blocks_zero_locations)
        {
            bool Limit = false;

            foreach (Control c in Main_panel1.Controls)
            {
                if (c.GetType() == typeof(PictureBox))
                {
                    PictureBox pb = c as PictureBox;

                    if (pb.Enabled && pb.Visible)
                    {
                        // Z TETROMINO (KIRMIZI)
                        if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 25 && blocks_zero_locations[1, 1] == 0
                        && blocks_zero_locations[2, 0] == 25 && blocks_zero_locations[2, 1] == 25
                        && blocks_zero_locations[3, 0] == 50 && blocks_zero_locations[3, 1] == 25)
                        {   // 1den 2ye geçiş
                            // 0,0 | 25,0 | 25,25 | 50,25
                            // 25,-25 | 25,0 | 0,0 | 0,25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, -25); if(Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -50, 0); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == 25
                        && blocks_zero_locations[2, 0] == -25 && blocks_zero_locations[2, 1] == 25
                        && blocks_zero_locations[3, 0] == -25 && blocks_zero_locations[3, 1] == 50)
                        {   // 2den 3e geçiş
                            // 25,-25 | 25,0 | 0,0 | 0,25
                            // 50,0 | 25,0 | 25,-25 | 0,-25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 0, -50); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == -25 && blocks_zero_locations[1, 1] == 0
                        && blocks_zero_locations[2, 0] == -25 && blocks_zero_locations[2, 1] == -25
                        && blocks_zero_locations[3, 0] == -50 && blocks_zero_locations[3, 1] == -25)
                        {   // 3ten 4e geçiş
                            // 50,0 | 25,0 | 25,-25 | 0,-25
                            // 25,25 | 25,0 | 50,0 | 50,-25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 50, 0); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == -25
                        && blocks_zero_locations[2, 0] == 25 && blocks_zero_locations[2, 1] == -25
                        && blocks_zero_locations[3, 0] == 25 && blocks_zero_locations[3, 1] == -50)
                        {   // 4ten 1e geçiş
                            // 25,25 | 25,0 | 50,0 | 50,-25
                            // 0,0 | 25,0 | 25,25 | 50,25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 0, 50); if (Limit) { break; }
                            }
                        }
                        // Z TETROMINO (KIRMIZI) BİTİŞ
                        //////////////////////////
                        // L TETROMİNO (TURUNCU)
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 25 && blocks_zero_locations[1, 1] == 0
                        && blocks_zero_locations[2, 0] == 50 && blocks_zero_locations[2, 1] == 0
                        && blocks_zero_locations[3, 0] == 50 && blocks_zero_locations[3, 1] == -25)
                        {   // 1den 2ye geçiş
                            // 0,0 | 25,0 | 50,0 | 50,-25
                            // 25,-25 | 25,0 | 25,25 | 50,25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 0, 50); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == 25
                        && blocks_zero_locations[2, 0] == 0 && blocks_zero_locations[2, 1] == 50
                        && blocks_zero_locations[3, 0] == 25 && blocks_zero_locations[3, 1] == 50)
                        {   // 2den 3e geçiş
                            // 25,-25 | 25,0 | 25,25 | 50,25
                            // 50,0 | 25,0 | 0,0 | 0,25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -50, 0); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == -25 && blocks_zero_locations[1, 1] == 0
                        && blocks_zero_locations[2, 0] == -50 && blocks_zero_locations[2, 1] == 0
                        && blocks_zero_locations[3, 0] == -50 && blocks_zero_locations[3, 1] == 25)
                        {   // 3ten 4e geçiş
                            // 50,0 | 25,0 | 0,0 | 0,25
                            // 25,25 | 25,0 | 25,25 | 0,-25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 0, -50); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == -25
                        && blocks_zero_locations[2, 0] == 0 && blocks_zero_locations[2, 1] == -50
                        && blocks_zero_locations[3, 0] == -25 && blocks_zero_locations[3, 1] == -50)
                        {   // 4ten 1e geçiş
                            // 25,25 | 25,0 | 25,25 | 0,-25
                            // 0,0 | 25,0 | 50,0 | 50,-25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 50, 0); if (Limit) { break; }
                            }
                        }
                        // L TETROMINO (TURUNCU) BİTİŞ
                        /////////////////////////
                        // S TETROMINO (YEŞİL)
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 25 && blocks_zero_locations[1, 1] == 0
                        && blocks_zero_locations[2, 0] == 25 && blocks_zero_locations[2, 1] == -25
                        && blocks_zero_locations[3, 0] == 50 && blocks_zero_locations[3, 1] == -25)
                        {   // 1den 2ye geçiş
                            // 0,0 | 25,0 | 25,-25 | 50,-25
                            // 25,-25 | 25,0 | 50,0 | 50,25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 0, 50); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == 25
                        && blocks_zero_locations[2, 0] == 25 && blocks_zero_locations[2, 1] == 25
                        && blocks_zero_locations[3, 0] == 25 && blocks_zero_locations[3, 1] == 50)
                        {   // 2den 3e geçiş
                            // 25,-25 | 25,0 | 50,0 | 50,25
                            // 50,0 | 25,0 | 25,25 | 0,25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -50, 0); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == -25 && blocks_zero_locations[1, 1] == 0
                        && blocks_zero_locations[2, 0] == -25 && blocks_zero_locations[2, 1] == 25
                        && blocks_zero_locations[3, 0] == -50 && blocks_zero_locations[3, 1] == 25)
                        {   // 3ten 4e geçiş
                            // 50,0 | 25,0 | 25,25 | 0,25
                            // 25,25 | 25,0 | 0,0 | 0,-25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 0, -50); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == -25
                        && blocks_zero_locations[2, 0] == -25 && blocks_zero_locations[2, 1] == -25
                        && blocks_zero_locations[3, 0] == -25 && blocks_zero_locations[3, 1] == -50)
                        {   // 4ten 1e geçiş
                            // 25,25 | 25,0 | 0,0 | 0,-25
                            // 0,0 | 25,0 | 25,-25 | 50,-25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 50, 0); if (Limit) { break; }
                            }
                        }
                        // S TETROMINO (YEŞİL) BİTİŞ
                        ////////////////////////
                        // I TETROMINO (CYAN)
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 25 && blocks_zero_locations[1, 1] == 0
                        && blocks_zero_locations[2, 0] == 50 && blocks_zero_locations[2, 1] == 0
                        && blocks_zero_locations[3, 0] == 75 && blocks_zero_locations[3, 1] == 0)
                        {   // 1den 2ye geçiş
                            // 0,0 | 25,0 | 50,0 | 75,0
                            // 50,-25 | 50,0 | 50,25 | 50,50
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 50, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[1, 0]
                            && pb.Location.Y == blocks_current_locations[1, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, 0); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 0, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, 50); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == 25
                        && blocks_zero_locations[2, 0] == 0 && blocks_zero_locations[2, 1] == 50
                        && blocks_zero_locations[3, 0] == 0 && blocks_zero_locations[3, 1] == 75)
                        {   // 2den 3e geçiş
                            // 50,-25 | 50,0 | 50,25 | 50,50
                            // 75,25 | 50,25 | 25,25 | 0,25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, 50); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[1, 0]
                            && pb.Location.Y == blocks_current_locations[1, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 0, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, 0); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -50, -25); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == -25 && blocks_zero_locations[1, 1] == 0
                        && blocks_zero_locations[2, 0] == -50 && blocks_zero_locations[2, 1] == 0
                        && blocks_zero_locations[3, 0] == -75 && blocks_zero_locations[3, 1] == 0)
                        {   // 3ten 4e geçiş
                            // 75,25 | 50,25 | 25,25 | 0,25
                            // 25,50 | 25,25 | 25,0 | 25,-25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -50, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[1, 0]
                            && pb.Location.Y == blocks_current_locations[1, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, 0); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 0, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, -50); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == -25
                        && blocks_zero_locations[2, 0] == 0 && blocks_zero_locations[2, 1] == -50
                        && blocks_zero_locations[3, 0] == 0 && blocks_zero_locations[3, 1] == -75)
                        {   // 4ten 1e geçiş
                            // 25,50 | 25,25 | 25,0 | 25,-25
                            // 0,0 | 25,0 | 50,0 | 75,0
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, -50); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[1, 0]
                            && pb.Location.Y == blocks_current_locations[1, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 0, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, 0); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 50, 25); if (Limit) { break; }
                            }
                        }
                        // I TETROMINO (CYAN) BİTİŞ
                        ///////////////////////
                        // J TETROMINO (MAVİ)
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 25 && blocks_zero_locations[1, 1] == 0
                        && blocks_zero_locations[2, 0] == 50 && blocks_zero_locations[2, 1] == 0
                        && blocks_zero_locations[3, 0] == 0 && blocks_zero_locations[3, 1] == -25)
                        {   // 1den 2ye geçiş
                            // 0,0 | 25,0 | 50,0 | 0,-25
                            // 25,-25 | 25,0 | 25,25 | 50,-25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 50, 0); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == 25
                        && blocks_zero_locations[2, 0] == 0 && blocks_zero_locations[2, 1] == 50
                        && blocks_zero_locations[3, 0] == 25 && blocks_zero_locations[3, 1] == 0)
                        {   // 2den 3e geçiş
                            // 25,-25 | 25,0 | 25,25 | 50,-25
                            // 50,0 | 25,0 | 0,0 | 50,25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 0, 50); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == -25 && blocks_zero_locations[1, 1] == 0
                        && blocks_zero_locations[2, 0] == -50 && blocks_zero_locations[2, 1] == 0
                        && blocks_zero_locations[3, 0] == 0 && blocks_zero_locations[3, 1] == 25)
                        {   // 3ten 4e geçiş
                            // 50,0 | 25,0 | 0,0 | 50,25
                            // 25,25 | 25,0 | 25,25 | 0,25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -50, 0); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == -25
                        && blocks_zero_locations[2, 0] == 0 && blocks_zero_locations[2, 1] == -50
                        && blocks_zero_locations[3, 0] == -25 && blocks_zero_locations[3, 1] == 0)
                        {   // 4ten 1e geçiş
                            // 25,25 | 25,0 | 25,25 | 0,25
                            // 0,0 | 25,0 | 50,0 | 0,-25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 0, -50); if (Limit) { break; }
                            }
                        }
                        // J TETROMINO (MAVİ) BİTİŞ
                        ///////////////////////
                        // T TETROMINO (MOR)
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 25 && blocks_zero_locations[1, 1] == 0
                        && blocks_zero_locations[2, 0] == 50 && blocks_zero_locations[2, 1] == 0
                        && blocks_zero_locations[3, 0] == 25 && blocks_zero_locations[3, 1] == -25)
                        {   // 1den 2ye geçiş
                            // 0,0 | 25,0 | 50,0 | 25,-25
                            // 25,-25 | 25,0 | 25,25 | 50,0
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, 25); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == 25
                        && blocks_zero_locations[2, 0] == 0 && blocks_zero_locations[2, 1] == 50
                        && blocks_zero_locations[3, 0] == 25 && blocks_zero_locations[3, 1] == 25)
                        {   // 2den 3e geçiş
                            // 25,-25 | 25,0 | 25,25 | 50,0
                            // 50,0 | 25,0 | 0,0 | 25,25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, 25); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == -25 && blocks_zero_locations[1, 1] == 0
                        && blocks_zero_locations[2, 0] == -50 && blocks_zero_locations[2, 1] == 0
                        && blocks_zero_locations[3, 0] == -25 && blocks_zero_locations[3, 1] == 25)
                        {   // 3ten 4e geçiş
                            // 50,0 | 25,0 | 0,0 | 25,25
                            // 25,25 | 25,0 | 25,25 | 0,0
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, -25); if (Limit) { break; }
                            }
                        }
                        else if (blocks_zero_locations[0, 0] == 0 && blocks_zero_locations[0, 1] == 0
                        && blocks_zero_locations[1, 0] == 0 && blocks_zero_locations[1, 1] == -25
                        && blocks_zero_locations[2, 0] == 0 && blocks_zero_locations[2, 1] == -50
                        && blocks_zero_locations[3, 0] == -25 && blocks_zero_locations[3, 1] == -25)
                        {   // 4ten 1e geçiş
                            // 25,25 | 25,0 | 25,25 | 0,0
                            // 0,0 | 25,0 | 50,0 | 25,-25
                            if (pb.Location.X == blocks_current_locations[0, 0]
                            && pb.Location.Y == blocks_current_locations[0, 1])
                            {
                                Check_Location_Change(pb, ref Limit, -25, -25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[2, 0]
                            && pb.Location.Y == blocks_current_locations[2, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, 25); if (Limit) { break; }
                            }
                            else if (pb.Location.X == blocks_current_locations[3, 0]
                            && pb.Location.Y == blocks_current_locations[3, 1])
                            {
                                Check_Location_Change(pb, ref Limit, 25, -25); if (Limit) { break; }
                            }
                        }
                        // T TETROMINO (MOR) BİTİŞ
                    }
                }
            }
            return Limit;
        }

        public void Check_Location_Change(PictureBox pb, ref bool Limit, int position_x, int position_y)
        {
            if(position_x < 0)
            {
                if(pb.Left + position_x < 0)
                {
                    Limit = true;
                }
            }
            else if(position_x > 0)
            {
                if(pb.Left + position_x > 225)
                {
                    Limit = true;
                }
            }

            if (position_y > 0)
            {
                if (pb.Top + position_y > 475)
                {
                    Limit = true;
                }
            }

            if(!Limit)
            {
                foreach (Control d in Main_panel1.Controls)
                {
                    if (d.GetType() == typeof(PictureBox))
                    {
                        PictureBox pb2 = d as PictureBox;

                        if (!pb2.Enabled && pb.Location.X == pb2.Location.X - position_x
                            && pb.Location.Y == pb2.Location.Y - position_y)
                        {
                            Limit = true;
                        }
                    }
                }
            }
        }


        public void Check_Fulled_Rows()
        {
            int fulled_row_count = 0;
            int cleared_lines_count = 0;


            for(int i=0; i<20; i++)
            {
                foreach (Control c in Main_panel1.Controls)
                {
                    if (c.GetType() == typeof(PictureBox))
                    {
                        PictureBox pb = c as PictureBox;

                        if (!pb.Enabled && pb.Visible && pb.Location.Y == i*25)    // hala aktif olan bloklar satırı yok etmemeli
                        {
                            fulled_row_count++;
                        }
                    }
                }

                if (fulled_row_count > 9)
                {
                    foreach (Control c in Main_panel1.Controls)
                    {
                        if (c.GetType() == typeof(PictureBox))
                        {
                            PictureBox pb = c as PictureBox;

                            if (!pb.Enabled && pb.Visible && pb.Location.Y == i * 25)
                            {
                                pb.Visible = false;
                            }
                        }
                    }

                    foreach (Control c in Main_panel1.Controls)
                    {
                        if (c.GetType() == typeof(PictureBox))
                        {
                            PictureBox pb = c as PictureBox;

                            if (!pb.Enabled && pb.Visible && pb.Location.Y < i * 25) // Dolan satır yok edildikten
                                                                                     // sonra üsttekileri 1 birim
                                                                                     // aşağı alır.
                            {
                                pb.Top += 25;
                            }
                        }
                    }
                    cleared_lines_count++;
                    Lines++;
                    Lines_label1.Text = Lines.ToString();
                    if(Lines%10==0)
                    {
                        Level++;
                        Level_label1.Text = Level.ToString();
                        softDrop_timer1.Interval = Soft_Drop_Timer_Intervals[Level];
                    }
                }
                fulled_row_count = 0;
            }
            if(cleared_lines_count == 1)
            {
                Score = Score + 100;
            }
            else if (cleared_lines_count == 2)
            {
                Score = Score + 300;
            }
            else if (cleared_lines_count == 3)
            {
                Score = Score + 500;
            }
            else if (cleared_lines_count == 4)
            {
                Score = Score + 800;
            }
            Score_label1.Text = Score.ToString();
        }

        // GÖRSEL EKLEMELER
        private void Score_Table_label1_MouseHover(object sender, EventArgs e)
        {
            Score_Table_label1.BackColor = Color.Red;
        }

        private void Score_Table_label1_MouseLeave(object sender, EventArgs e)
        {
            Score_Table_label1.BackColor = Color.Brown;
        }

        private void Save_Score_label1_MouseHover(object sender, EventArgs e)
        {
            Save_Score_label1.BackColor = Color.ForestGreen;
        }

        private void Save_Score_label1_MouseLeave(object sender, EventArgs e)
        {
            Save_Score_label1.BackColor = Color.Green;
        }

        private void Save_Score_label1_Click(object sender, EventArgs e)
        {
            Game_Over_label1.Visible = false;
            Save_Score_label1.Visible = false;
            Score_Table_label1.Visible = false;
            Save_Score_panel1.Visible = true;
        }

        private void Score_Table_label1_Click(object sender, EventArgs e)
        {
            ScoreTable_listView1.Items.Clear();
            Game_Over_label1.Visible = false;
            Save_Score_label1.Visible = false;
            ScoreTable_panel1.Visible = true;
            Score_Table_label1.Visible = false;
            showData();
        }

        private void Save_label1_Click(object sender, EventArgs e)
        {
            Save_Score_panel1.Visible = false;
            ScoreTable_panel1.Visible = true;
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Insert Into ScoreTable (Ad,Score) Values ('" + Save_Score_textBox1.Text.ToString()
                                                   + "','" + Score.ToString() + "')", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            showData();
        }

        private void Save_Score_textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Save_Score_textBox1.Text != null)
            {
                Save_label1.Visible = true;
            }
        }

        private void Save_label1_MouseHover(object sender, EventArgs e)
        {
            Save_label1.BackColor = Color.MidnightBlue;
        }

        private void Save_label1_MouseLeave(object sender, EventArgs e)
        {
            Save_label1.BackColor = Color.Blue;
        }
    }
}
