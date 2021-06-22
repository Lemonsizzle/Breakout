using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breakout
{
    public partial class Form1 : Form
    {
        int blocksx = 10, blocksy = 5;
        int blockw = 50, blockh = 20;
        int blocksSpace = 2;
        PictureBox[,] blocks = new PictureBox[5, 10];
        //PictureBox[,] blocks;

        int lives = 5;

        bool goleft;
        bool goright;
        bool running;

        int ballx = 5;
        int bally = 5;

        int walk = 5;
        int runM = 2;

        public Form1()
        {
            InitializeComponent();

            this.Shown += CreateBlocksDelegate;
        }

        private void CreateBlocksDelegate(object sender, EventArgs e)
        {
            //PictureBox[,] blocks = new PictureBox[blocksy, blocksx];
            for (int j = 0; j < blocksy; j++)
            {
                for (int i = 0; i < blocksx; i++)
                {
                    blocks[j, i] = new PictureBox();
                    blocks[j, i].Size = new Size(blockw, blockh);
                    blocks[j, i].Location = new Point((blockw + blocksSpace) * i, ((blockh + blocksSpace) * j) + 50);
                    blocks[j, i].BackColor = Color.Red;
                    blocks[j, i].SetBounds(blocks[j,i].Location.X, blocks[j, i].Location.Y, blockw, blockh);
                    //blocks[j, i].SetBounds((blockw + blocksSpace) * i, ((blockh + blocksSpace) * j) + 50, blockw, blockh);

                    this.Controls.Add(blocks[j, i]);
                }
            }

            this.Size = new Size((blockw + blocksSpace) * blocksx, 500);
        }

        private void downPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }

            if (e.Modifiers == Keys.Shift)
            {
                running = true;
            }
        }

        private void upPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }

            if (e.Modifiers == Keys.Shift)
            {
                running = false;
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            ball.Top -= bally;
            ball.Left -= ballx;

            if (ball.Left < 0 || ball.Left + ball.Width > ClientSize.Width)
            {
                ballx = -ballx;
            }

            if (ball.Top < 0)
            {
                bally = -bally;
            }

            if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                bally = -bally;
            }

            if(ball.Bottom > ClientSize.Height)
            {
                ball.Top = ClientSize.Height / 2+100;
                ball.Left = ClientSize.Width / 2;
                bally = -bally;
            }

            for (int j = 0; j < blocksy; j++)
            {
                for (int i = 0; i < blocksx; i++)
                {
                    if (ball.Bounds.IntersectsWith(blocks[j,i].Bounds))
                    {
                        blocks[j,i].SetBounds(0,0,0,0);
                        this.Controls.Remove(blocks[j, i]);

                        bally = -bally;
                    }
                }
            }
            


            if (running)
            {
                if (goleft == true && player.Left > 0)
                {
                    player.Left -= walk * runM;
                }

                if (goright == true && player.Left + player.Width < ClientSize.Width)
                {
                    player.Left += walk * runM;
                }
            }
            else
            {
                if (goleft == true && player.Left > 0)
                {
                    player.Left -= walk;
                }

                if (goright == true && player.Left + player.Width < ClientSize.Width)
                {
                    player.Left += walk;
                }
            }
        }
    }
}
