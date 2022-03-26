using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Snake.Settings;

namespace Snake {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            timer1.Stop();
            timer1.Interval = 100 / GameSpeed;
        }

        public int Counter { get; set; }
        private IList<Cell> snake { get; set; } = new List<Cell>();
        private Cell food { get; set; }

        private Direction direction { get; set; } = Direction.Up;

        private Random rnd = new Random();

        private void button1_Click(object sender, EventArgs e) {
            Counter = 0;
            label1.Text = Counter.ToString();

            SpawnFood();

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

        private bool CheckFoodCollision() {
            return snake.First().Collision(food.X, food.Y);
        }

        private bool CheckWallCollision() {
            return snake.Any(x => x.Die);
        }

        private bool CheckSnakeCollision() {
            var head = snake.First();
            if (snake.Any(x => x != head && x.Collision(head.X, head.Y))) {
                return true;
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
            var increase = CheckFoodCollision();
            int xMem = snake.Last().X;
            int yMem = snake.Last().Y;

            for (var i = snake.Count - 1; i >= 0; i--) {
                if (i == 0) {
                    snake[i].Go(direction);
                } else {
                    snake[i].Go(snake[i - 1].X, snake[i - 1].Y);
                }
            }
            if (increase) {
                snake.Add(new Cell(SpawnLabel(Color.LightBlue), xMem, yMem));
                Counter++;
                label1.Text = Counter.ToString();
                SpawnFood();
            }
            if (CheckWallCollision() || CheckSnakeCollision()) {
                timer1.Stop();
            }
        }

        private void SpawnFood() {
            while (true) {
                var newX = rnd.Next(WIDTH_CELLS);
                var newY = rnd.Next(HEIGHT_CELLS);
                if (snake.All(x => !x.Collision(newX, newY))) {
                    if (food == null) {
                        food = new Cell(SpawnLabel(Color.Red), newX, newY);
                    } else {
                        food.Go(newX, newY);
                    }
                    break;
                }
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e) {
            ChangeDirection(e.KeyCode);
        }

        private void button2_Click(object sender, EventArgs e) {
            timer1.Stop();
        }

        private void GameSpeedInput_TextChanged(object sender, EventArgs e) {
            try {
                int.TryParse(GameSpeedInput.Text, out int value);

                if (value > 0 && value <= 100) {
                    GameSpeed = value;
                    timer1.Interval = 1000 / GameSpeed;
                }
            } catch {
            } finally {
                GameSpeedInput.Text = GameSpeed.ToString();
            }

        }
    }
}
