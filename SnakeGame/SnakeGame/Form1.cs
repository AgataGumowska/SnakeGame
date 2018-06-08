using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class frmSnake : Form
    {
        Random rand;
        enum GameBoardFields
        {
            Free,
            Snake,
            Bonus
        };

        enum Directions
        {
            Up,
            Down,
            Left,
            Right
        };

        struct SnakeCoordinates
        {
            public int x;
            public int y;
        };


        GameBoardFields[,] gameBoardField;
        SnakeCoordinates[] snakeXY;
        int snakeLength;
        Directions direction;
        Graphics g;



        public frmSnake()
        {
            InitializeComponent();
            gameBoardField = new GameBoardFields[11, 11];
            snakeXY = new SnakeCoordinates[100];
            rand = new Random();
        }

        private void frmSnake_Load(object sender, EventArgs e)
        {
            picGameBoard.Image = new Bitmap(420, 420);
            g = Graphics.FromImage(picGameBoard.Image);
            g.Clear(Color.White);

            for (int i = 1; i <= 10; i++)
            {
                //górna dolna ściana
                g.DrawImage(imgList.Images[6], i * 35, 0);
                g.DrawImage(imgList.Images[6], i * 35, 385);
            }

            for (int i = 0; i <= 11; i++)
            {
                //lewa prawa ściana
                g.DrawImage(imgList.Images[6], 0, i * 35);
                g.DrawImage(imgList.Images[6], 385, i * 35);
            }

            snakeXY[0].x = 5; //głowa
            snakeXY[0].y = 5;
            snakeXY[1].x = 5; // pierwsza część tułowia
            snakeXY[1].y = 6;
            snakeXY[2].x = 5; // druga część tułowia
            snakeXY[2].y = 7;


            g.DrawImage(imgList.Images[5], 5 * 35, 5 * 35); // głowa
            g.DrawImage(imgList.Images[4], 5 * 35, 6 * 35); // pierwsza część tułowia
            g.DrawImage(imgList.Images[4], 5 * 35, 7 * 35); // druga część tułowia

            gameBoardField[5, 5] = GameBoardFields.Snake;
            gameBoardField[5, 6] = GameBoardFields.Snake;
            gameBoardField[5, 7] = GameBoardFields.Snake;

            direction = Directions.Up;
            snakeLength = 3;

            for (int i = 0; i < 4; i++)
            {
                Bonus();
            }
        }

     private void Bonus()
        {
        int x, y;
        var imgIndex = rand.Next(0, 4);

            do
            {
                x = rand.Next(1, 10);
                y = rand.Next(1, 10);
            }

            while (gameBoardField[x, y] != GameBoardFields.Free);

            gameBoardField[x, y] = GameBoardFields.Bonus;
            g.DrawImage(imgList.Images[imgIndex], x * 35, y * 35);
        }

        private void frmSnake_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Up:
                    direction = Directions.Up;
                    break;

                case Keys.Down:
                    direction = Directions.Down;
                    break;

                case Keys.Left:
                    direction = Directions.Left;
                    break;

                case Keys.Right:
                    direction = Directions.Right;
                    break;

            }
        }

        private void Gameover()
        {
            timer.Enabled = false;
            MessageBox.Show("GAME OVER");

        }


    }
}
