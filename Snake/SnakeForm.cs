using Snake.DijkstraControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static Snake.Settings;

namespace Snake {
    public partial class SnakeForm : Form {
        public int Counter { get; set; }
        private Queue<Cell> snake { get; set; } = new Queue<Cell>();
        private Cell head { get; set; }
        private IList<Cell> way { get; set; } = new List<Cell>();
        private Cell food { get; set; }
        private Direction direction { get; set; } = Direction.Up;
        private Random rnd = new Random();
        private Control Control { get; set; } = Control.ManualControl;

        private Ai Ai = new Ai();

        public SnakeForm() {
            InitializeComponent();
            WidthSizeInput.Text = WidthSize.ToString();
            HeightSizeInput.Text = HeightSize.ToString();
            CellSizeInput.Text = CellSize.ToString();
            GameSpeedInput.Text = GameSpeed.ToString();
            ExceptionLabel.Text = "";

            RecreateField();
            timer1.Stop();
            timer1.Interval = 1000 / GameSpeed;
        }

        #region EventHandlers

        private void StartButton_Click(object sender, EventArgs e) {
            Counter = 0;
            CounterLabel.Text = Counter.ToString();
            ExceptionLabel.Text = "";

            SpawnFood();
            SpawnSnake();
            direction = Direction.Rigth;
            timer1.Start();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e) {
            ChangeDirection(e.KeyCode);
        }

        private void StopButton_Click(object sender, EventArgs e) {
            if (timer1.Enabled) {
                timer1.Stop();
                StopButton.Text = "Продолжить";
            } else {
                timer1.Start();
                StopButton.Text = "Стоп";
            }
        }

        private void RecreateButton_Click(object sender, EventArgs e) {
            RecreateField();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            MoveSnake();
        }

        private void ManualControlRb_CheckedChanged(object sender, EventArgs e) {
            this.Control = Control.ManualControl;
        }

        private void DijkstraControlRb_CheckedChanged(object sender, EventArgs e) {
            this.Control = Control.DijkstraControl;
        }

        private void CellSizeInput_TextChanged(object sender, EventArgs e) {
            try {
                int.TryParse(CellSizeInput.Text, out int value);

                if (value > 0 && value <= 40) {
                    CellSize = value;
                }
            } catch {
            } finally {
                CellSizeInput.Text = CellSize.ToString();
            }
        }

        private void WidthSizeInput_TextChanged(object sender, EventArgs e) {
            try {
                int.TryParse(WidthSizeInput.Text, out int value);

                if (value > 0 && value <= 100) {
                    WidthSize = value;
                }
            } catch {
            } finally {
                WidthSizeInput.Text = WidthSize.ToString();
            }
        }

        private void HeightSizeInput_TextChanged(object sender, EventArgs e) {
            try {
                int.TryParse(HeightSizeInput.Text, out int value);

                if (value > 0 && value <= 100) {
                    HeightSize = value;
                }
            } catch {
            } finally {
                HeightSizeInput.Text = HeightSize.ToString();
            }
        }

        private void GameSpeedInput_TextChanged(object sender, EventArgs e) {
            try {
                int.TryParse(GameSpeedInput.Text, out int value);

                if (value > 0 && value <= 1000) {
                    GameSpeed = value;
                    timer1.Interval = 1000 / GameSpeed;
                }
            } catch {
            } finally {
                GameSpeedInput.Text = GameSpeed.ToString();
            }
        }

        #endregion

        #region Methods


        private bool CheckFoodCollision() {
            return head.Collision(food.X, food.Y);
        }

        private bool CheckWallCollision() {
            return snake.Any(x => x.Die);
        }

        private bool CheckSnakeCollision() {
            head.label.BringToFront();
            if (snake.Any(x => x != head && x.Collision(head.X, head.Y))) {
                return true;
            }
            return false;
        }

        private void ChangeDirection(Keys key) {
            if (Control != Control.ManualControl) {
                return;
            }

            switch (key) {
                case Keys.W:
                    ChangeDirection(0);
                    return;
                case Keys.D:
                    ChangeDirection(1);
                    return;
                case Keys.S:
                    ChangeDirection(2);
                    return;
                case Keys.A:
                    ChangeDirection(3);
                    return;
                default:
                    return;
            }
        }

        private void ChangeDirection(int index) {
            var allowBack = snake.Count == 1;
            switch (index) {
                case 0:
                    if (direction == Direction.Down && !allowBack) {
                        return;
                    }
                    direction = Direction.Up;
                    return;
                case 1:
                    if (direction == Direction.Left && !allowBack) {
                        return;
                    }
                    direction = Direction.Rigth;
                    return;
                case 2:
                    if (direction == Direction.Up && !allowBack) {
                        return;
                    }
                    direction = Direction.Down;
                    return;
                case 3:
                    if (direction == Direction.Rigth && !allowBack) {
                        return;
                    }
                    direction = Direction.Left;
                    return;
                default:
                    return;
            }
        }

        private Label SpawnLabel(Color color) {
            return new Label() {
                AutoSize = false,
                Width = CellSize,
                Height = CellSize,
                BackColor = color,
                Text = "",
                Parent = pictureBox1,
            };
        }

        private Cell SpawnCell(Color color, int x, int y) {
            return new Cell(SpawnLabel(color), x, y);
        }

        private void MoveSnake() {
            if (CheckWallCollision() || CheckSnakeCollision()) {
                timer1.Stop();
            }
            if (way.Count > 0) {
                DropOldWay();
            }
            if (Control != Control.ManualControl) {
                try {
                    AiControl();
                } catch (Exception e) {
                    ExceptionLabel.Text = e.Message;
                }
            }
            var increase = CheckFoodCollision();
            var headCoord = head.Coord;
            head.Go(direction);
            if (increase) {
                snake.Enqueue(SpawnCell(BodyColor, headCoord.X, headCoord.Y));
                Counter++;
                CounterLabel.Text = Counter.ToString();
                SpawnFood();
            }
            if (!increase && snake.Count > 0) {
                var cell = snake.Dequeue();
                cell.Go(headCoord);
                snake.Enqueue(cell);
            }
        }

        private void AiControl() {
            var nodes = GenerateMatrix();
            var nodesWay = Ai.FindWay(nodes);
            var head = nodesWay.SingleOrDefault(x => x.NodeType == NodeType.Head);
            AiControlChangeDirection(head);

            ShowWay(nodes);
        }

        private void ShowWay(IList<Node> nodes) {
            if (!ShowWidthsChecker.Checked) {
                if (way.Count > 0)
                    DropOldWay();
                return;
            }
            if (way.Count != nodes.Count) {
                DropOldWay();
                FillWay(nodes);
            }
            for (var i = 0; i < way.Count; i++) {
                var cell = way[i];
                var node = nodes[i];
                cell.label.Visible = node.NodeType == NodeType.Empty;
                cell.label.Text = node.Value == int.MaxValue ? "∞" : node.Value.ToString();
            }
        }

        private void FillWay(IEnumerable<Node> nodes) {
            foreach (var node in nodes) {
                var waypoint = SpawnCell(WaypointColor, node.X, node.Y);
                this.way.Add(waypoint);
            }
        }

        private void DropOldWay() {
            foreach (var cell in way) {
                cell.label.Dispose();
            }
            way.Clear();
        }

        private void AiControlChangeDirection(Node node) {
            if (node == null) return;
            var min = node.Neighbors
                .Where(x => x != null)
                .Where(x => x.NodeType == NodeType.Empty || x.NodeType == NodeType.Food)
                .Select(x => x.Value)
                .Min();
            var nextCell = node.Neighbors
                .Where(x => x != null)
                .Where(x => x.Value <= min)
                .Where(x => x.NodeType != NodeType.Body)
                .First();
            var index = node.Neighbors.IndexOf(nextCell);
            ChangeDirection(index);
            SafeFromWalls();
            //direction = (Direction)index;
        }

        private void SafeFromWalls() {
            var detector = SpawnCell(Color.Aqua, 0, 0);
            detector.Go(head.Coord);
            detector.Go(direction);
            if (detector.Die || snake.Any(x => x.Collision(detector.Coord))) {
                SafeTurn(detector);
            }

            detector.label.Dispose();
        }

        private void SafeTurn(Cell detector) {
            detector.Go(head.Coord);
            if (direction == Direction.Left || direction == Direction.Rigth) {
                detector.Go(Direction.Up);
                if (detector.Die || snake.Any(x => x.Collision(detector.Coord))) {
                    direction = Direction.Down;
                } else {
                    direction = Direction.Up;
                }
            } else {
                detector.Go(Direction.Rigth);
                if (detector.Die || snake.Any(x => x.Collision(detector.Coord))) {
                    direction = Direction.Left;
                } else {
                    direction = Direction.Rigth;
                }
            }
        }

        private IList<Node> GenerateMatrix() {
            var lenght = WidthSize * HeightSize;
            IList<Node> nodes = new List<Node>(lenght);
            for (int i = 0; i < lenght; i++) {
                var x = i % WidthSize;
                var y = i / WidthSize;
                nodes.Add(new Node() {
                    X = x,
                    Y = y,
                    NodeType = GetNodeType(x, y),
                });
            }
            FillNeighbors(nodes);
            return nodes;
        }

        private NodeType GetNodeType(int x, int y) {
            if (snake.Any(cell => cell.Collision(x, y))) {
                return NodeType.Body;
            }
            if (food.Collision(x, y)) {
                return NodeType.Food;
            }
            if (head.Collision(x, y)) {
                return NodeType.Head;
            }

            return NodeType.Empty;
        }

        private static void FillNeighbors(IList<Node> nodes) {
            var lenght = WidthSize * HeightSize;

            for (int i = 0; i < lenght; i++) {
                var x = i % WidthSize;
                var y = i / WidthSize;
                var node = nodes[i];
                var topNeighborIndex = y > 0 ? (y - 1) * WidthSize + x : -1;
                var rightNeighborIndex = x < WidthSize - 1 ? y * WidthSize + x + 1 : -1;
                var bottomNeighborIndex = y < HeightSize - 1 ? (y + 1) * WidthSize + x : -1;
                var leftNeighborIndex = x > 0 ? y * WidthSize + x - 1 : -1;

                node.Neighbors = new List<Node> {
                    topNeighborIndex >= 0 ? nodes[topNeighborIndex] : null,
                    rightNeighborIndex >= 0 ? nodes[rightNeighborIndex] : null,
                    bottomNeighborIndex >= 0 ? nodes[bottomNeighborIndex] : null,
                    leftNeighborIndex >= 0 ? nodes[leftNeighborIndex] : null,
                };
            }
        }

        private void SpawnFood() {
            while (true) {
                var newX = rnd.Next(1, WidthSize - 1);
                var newY = rnd.Next(1, HeightSize - 1);
                if (snake.All(x => !x.Collision(newX, newY)
                                && !x.Collision(newX + 1, newY)
                                && !x.Collision(newX - 1, newY)
                                && !x.Collision(newX, newY + 1)
                                && !x.Collision(newX, newY - 1))) {
                    if (food == null) {
                        food = SpawnCell(FoodColor, newX, newY);
                    } else {
                        food.Go(newX, newY);
                    }
                    break;
                }
            }
        }

        private void SpawnSnake() {
            if (snake.Count > 0) {
                foreach (var cell in snake) {
                    cell.label.Dispose();
                }
                snake.Clear();
            }
            if (head == null) {
                head = SpawnCell(HeadColor, 0, 0);
            } else {
                head.Go(0, 0);
            }
            snake = new();
        }

        private void RecreateField() {
            pictureBox1.Width = WidthSize * CellSize;
            pictureBox1.Height = HeightSize * CellSize;
            if (head != null) {
                head.Rewrite();
            }
            if (food == null) {
                SpawnFood();
            }
            food.Rewrite();
            foreach (var cell in snake) {
                cell.Rewrite();
            }
        }
        #endregion
    }
}
