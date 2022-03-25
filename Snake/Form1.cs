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

        public int Counter { get; set; }
        private IList<Cell> snake { get; set; } = new List<Cell>();
        private Cell food { get; set; }

        private Direction direction { get; set; } = Direction.Up;

        private Random rnd = new Random();

        private void button1_Click(object sender, EventArgs e) {
            Counter = 0;
            label1.Text = Counter.ToString();
            if (food == null) {
                food = new Cell(SpawnLabel(Color.Red), rnd.Next(WIDTH_CELLS), rnd.Next(HEIGHT_CELLS));
            } else {
                food.Go(rnd.Next(WIDTH_CELLS), rnd.Next(HEIGHT_CELLS));
            }

            if (snake.Count > 0) {
                foreach (var cell in snake) {
                    cell.label.Dispose();
                }
                snake.Clear();
            }
            var head = SpawnLabel(Color.Blue);

            snake = new List<Cell> { new Cell(head, 0, 5) };
            direction = Direction.Rigth;
            timer1.Start();
        }



        private void timer1_Tick(object sender, EventArgs e) {
            MoveSnake();
        }



        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            label1.Text = e.KeyCode.ToString();
            ChangeDirection(e.KeyCode);
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

        private bool CheckFoodCollision() {
            return snake.First().Collision(food.X, food.Y);
        }

        private bool CheckWallCollision() {
            return snake.Any(x => x.Die);
        }

        private bool CheckSnakeCollision() {
            foreach (var cell in snake) {
                if (snake.Any(x => x != cell && cell.Collision(x.X, x.Y))) {
                    return true;
                }
            }
            return false;
        }

        private void ChangeDirection(Keys key) {
            switch (key) {
                case Keys.W:
                    direction = Direction.Up;
                    return;
                case Keys.D:
                    direction = Direction.Rigth;
                    return;
                case Keys.S:
                    direction = Direction.Down;
                    return;
                case Keys.A:
                    direction = Direction.Left;
                    return;
                default:
                    return;
            }
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


        private void MoveSnake() {
            int xMem = snake.First().X;
            int yMem = snake.First().Y;
            snake.First().Go(direction);
            if (CheckFoodCollision() && snake.Count > 1) {
                xMem = snake.Last().X;
                yMem = snake.Last().Y;
            }

            for (var i = snake.Count - 1; i > 0; i--) {
                snake[i].Go(snake[i - 1].X, snake[i - 1].Y);
            }
            if (CheckFoodCollision()) {
                snake.Add(new Cell(SpawnLabel(Color.LightBlue), xMem, yMem));
                Counter++;
                label1.Text = Counter.ToString();
                while (true) {
                    var newX = rnd.Next(WIDTH_CELLS);
                    var newY = rnd.Next(HEIGHT_CELLS);
                    if(snake.All(x => !x.Collision(newX, newY))) {
                        food.Go(newX, newY);
                        break;
                    }
                }

            }
            if (CheckWallCollision() || CheckSnakeCollision()) {
                timer1.Stop();
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e) {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) {

        }

        private void button1_KeyDown(object sender, KeyEventArgs e) {
            ChangeDirection(e.KeyCode);
        }
    }
}
