using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Snake.Constants;

namespace Snake {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            timer1.Stop();
            timer1.Interval = INIT_TIMER_INERVAL;
        }

        private IList<Cell> snake { get; set; } = new List<Cell>();

        private Direction direction { get; set; } = Direction.Up;

        private Random rnd = new Random();

        private void button1_Click(object sender, EventArgs e) {
            if(snake.Count > 0) {
                foreach(var cell in snake) {
                    cell.label.Dispose();
                }
                snake.Clear();
            }
            var head = SpawnLabel(Color.Blue);

            snake = new List<Cell> { new Cell(head, 5, 5) };
            direction = Direction.Rigth;
            timer1.Start();
        }

        private Label SpawnLabel(Color color) {
            return new Label() {
                AutoSize = false,
                Width = CELL_SIZE,
                Height = CELL_SIZE,
                BackColor = color,
                Text = "",
                Parent = pictureBox1,
            };
        }



        private void timer1_Tick(object sender, EventArgs e) {
            MoveSnake();
        }

        private void MoveSnake() {
            for(var i = snake.Count-1; i >= 0; i--) {
                if(i == 0) {
                    snake[i].Go(direction);
                } else {
                    snake[i].Go(snake[i-1].X, snake[i-1].Y);
                }
            }
            if (CheckWallCollision() || CheckSnakeCollision()) {
                timer1.Stop();
            }
        }



        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Up:
                    direction = Direction.Up;
                    return;
                case Keys.Right:
                    direction = Direction.Rigth;
                    return;
                case Keys.Down:
                    direction = Direction.Down;
                    return;
                case Keys.Left:
                    direction = Direction.Left;
                    return;
                default:
                    return;
            }
        }

        private bool CheckWallCollision() {
            return snake.Any(x => x.Die);
        }

        private bool CheckSnakeCollision() {
            foreach (var cell in snake) {
                if(snake.Any(x => x != cell && x.Collision(cell.X, cell.Y))) {
                    return true;
                }
            }
            return false;
        }
    }
}
