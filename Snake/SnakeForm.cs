using Snake.DijkstraControl;
using Snake.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static Snake.Settings;
using Control = Snake.Enums.Control;

namespace Snake {
    public partial class SnakeForm : Form {
        /// <summary>
        /// Игровой счёт
        /// </summary>
        public int Counter { get; set; }
        /// <summary>
        /// Список ячеек тела змейки. Использована очередь для оптимизации при перемещении
        /// </summary>
        private Queue<Cell> snake { get; set; } = new Queue<Cell>();
        /// <summary>
        /// Голова змейки
        /// </summary>
        private Cell head { get; set; }
        /// <summary>
        /// Список ячеек показывающий веса клеток
        /// </summary>
        private IList<Cell> weights { get; set; } = new List<Cell>();
        /// <summary>
        /// Ячейка - еда
        /// </summary>
        private Cell food { get; set; }
        /// <summary>
        /// Направление движения головы змейки
        /// </summary>
        private Direction direction { get; set; } = Direction.Up;
        /// <summary>
        /// Способ управления змейкой
        /// </summary>
        private Control Control { get; set; } = Control.DijkstraControl;

        private Random rnd = new Random();
        private Ai Ai = new Ai();



        /// <summary>
        /// В конструкторе отрисовывается игровое поле указанных размеров и останавливается таймер при запуске
        /// </summary>
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


        // Блок кода, содержащий обработчики событий элементов, находящихся на форме
        // По названию каждого метода можно понять какому элементу он принадлежит и на какое действие реагирует

        #region EventHandlers
        // Далее буду писать кратко что делается в каждом из этих методов

        // Обнуляется счётчик, спавнится еда, змейка, указывается направление поумолчанию направо и включается игровой таймер
        private void StartButton_Click(object sender, EventArgs e) {
            Counter = 0;
            CounterLabel.Text = Counter.ToString();
            ExceptionLabel.Text = "";

            SpawnFood();
            SpawnSnake();
            direction = Direction.Rigth;
            timer1.Start();
        }

        // Обработчик события нажатия на клавиши для ручного управления змейкой, он повешан на кнопки интерфейса
        private void button1_KeyDown(object sender, KeyEventArgs e) {
            ChangeDirection(e.KeyCode);
        }

        // Переключает игровой таймер
        private void StopButton_Click(object sender, EventArgs e) {
            if (timer1.Enabled) {
                timer1.Stop();
                StopButton.Text = "Продолжить";
            } else {
                timer1.Start();
                StopButton.Text = "Стоп";
            }
        }

        // Перерисовывает игровое поле
        private void RecreateButton_Click(object sender, EventArgs e) {
            RecreateField();
        }

        // Каждый тик игрового таймера вызывается метод движения змейки, а из него уже и всё остальное
        private void timer1_Tick(object sender, EventArgs e) {
            MoveSnake();
        }

        // Установка ручного способа управления
        private void ManualControlRb_CheckedChanged(object sender, EventArgs e) {
            this.Control = Control.ManualControl;
        }

        // Установка автоматического управления змейкой методом Дейкстры
        private void DijkstraControlRb_CheckedChanged(object sender, EventArgs e) {
            this.Control = Control.DijkstraControl;
        }

        // Дальше просто идут обрабочики событий ввода текстовых полей,
        // в которых указываются параметры игрового поля и скорости игры

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

        /// <summary>
        /// Прверка съедания еды головой змейки
        /// </summary>
        /// <returns>true - если голова змейки находится на тех же координатах, что и еда</returns>
        private bool CheckFoodCollision() {
            return head.Collision(food.X, food.Y);
        }

        /// <summary>
        /// Проверка врезания в стены
        /// </summary>
        /// <returns>true - если голова оказывается за пределами игрового поля</returns>
        private bool CheckWallCollision() {
            return head.Die;
        }

        /// <summary>
        /// Проверка съедания змейкой самой себя
        /// </summary>
        /// <returns>true - если голова змейки находится на одной из ячеек её тела</returns>
        private bool CheckSnakeCollision() {
            head.label.BringToFront();
            return snake.Any(x => x.Collision(head.X, head.Y));
        }

        /// <summary>
        /// Смена направления движения змейки в зависимости от нажатой клавиши
        /// Если не выбран ручной метод управления то метод не срабатывает
        /// </summary>
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

        /// <summary>
        /// Смена направления движения змейки в зависимости от передаваемого индекса enum Direction
        /// Если змейка состоит из одной головы, она может разворачиваться и двигаться назад
        /// </summary>
        /// <param name="index">направление по enum Direction</param>
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

        /// <summary>
        /// Спавнит ячейку по указанным координатам
        /// </summary>
        /// <param name="color">Цвет ячейки</param>
        /// <param name="x">Координата Х</param>
        /// <param name="y">Координата Y</param>
        private Cell SpawnCell(Color color, int x, int y) {
            return new Cell(color, x, y, pictureBox1);
        }


        /// <summary>
        /// Метод содержит всю основную игровую логику:
        /// Проверяет условия проигрыша и останавливает таймер
        /// Отрисовывает и стирает ячейки с весами
        /// Вызывает метод управления змейкой ИИ
        /// Передвигает голову в текущем направлении
        /// Передвижение змейки осущствлено таким способом: последняя ячейка с хвоста перемещается на место головы. Для этого используется очередь.
        /// Если змейка покушала, то последняя ячейка остаётся на её месте, а новая появляется на прошлом месте головы
        /// </summary>
        private void MoveSnake() {
            if (CheckWallCollision() || CheckSnakeCollision()) {
                timer1.Stop();
            }
            if (weights.Count > 0) {
                DropWeigthsList();
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

        /// <summary>
        /// Основной метод ИИ управления
        /// Вызывает метод, создающий список всех клеток игрового поля с информацией о них
        /// Вызывает метод, расставляющий веса в клетках
        /// Вызывает метод,  который определяет направление движения змейки на следующем ходу
        /// Вызывает метод, отвечающий за отрисовку весов клеток
        /// </summary>
        private void AiControl() {
            var nodes = GenerateMatrix();
            var nodesWay = Ai.FindWay(nodes);
            var head = nodesWay.SingleOrDefault(x => x.NodeType == NodeType.Head);
            AiControlChangeDirection(head);

            ShowWeights(nodes);
        }

        /// <summary>
        /// Отрисовка весов клеток
        /// </summary>
        /// <param name="nodes"></param>
        private void ShowWeights(IList<Node> nodes) {
            if (!ShowWidthsChecker.Checked) {
                if (weights.Count > 0)
                    DropWeigthsList();
                return;
            }
            if (weights.Count != nodes.Count) {
                DropWeigthsList();
                FillWeightsList(nodes);
            }
            for (var i = 0; i < weights.Count; i++) {
                var cell = weights[i];
                var node = nodes[i];
                cell.label.Visible = node.NodeType == NodeType.Empty;
                cell.label.Text = node.Value == int.MaxValue ? "∞" : node.Value.ToString();
            }
        }

        /// <summary>
        /// Создание ячеек для отрисовки весов
        /// </summary>
        /// <param name="nodes"></param>
        private void FillWeightsList(IEnumerable<Node> nodes) {
            foreach (var node in nodes) {
                var waypoint = SpawnCell(WaypointColor, node.X, node.Y);
                this.weights.Add(waypoint);
            }
        }

        /// <summary>
        /// Очистка отрисовки весов
        /// </summary>
        private void DropWeigthsList() {
            foreach (var cell in weights) {
                cell.label.Dispose();
            }
            weights.Clear();
        }

        /// <summary>
        /// Определяет направление движение змейки в сторону соседней клетке с наименьшим весом
        /// Включает защиту от врезания в стенку или съедания самой себя
        /// </summary>
        /// <param name="node"></param>
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
        }

        /// <summary>
        /// Защита от врезаний в стены и съедания самой себя
        /// </summary>
        private void SafeFromWalls() {
            var detector = SpawnCell(Color.Aqua, 0, 0);
            detector.Go(head.Coord);
            detector.Go(direction);
            if (detector.Die || snake.Any(x => x.Collision(detector.Coord))) {
                SafeTurn(detector);
            }

            detector.label.Dispose();
        }

        /// <summary>
        /// Спасительный поворот, определяет направление куда нужно повернуть, чтобы не врезаться в стенку и не сожрать самой себя
        /// </summary>
        /// <param name="detector">Скрытая ячейка создающаяся перед носом змейки для определения шанса смерти на следующем ходу</param>
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

        /// <summary>
        /// Метод создающий список всех клеток игрового поля
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Определяет тип клетки по указанным координатам
        /// </summary>
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

        /// <summary>
        /// Проходясь по списку всех клеток игрового поля заполняет для каждой ячейки список её ближайших соседей сверху, справа, снизу слева
        /// </summary>
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

        /// <summary>
        /// Создаёт или перемещает существующую ячейку с едой
        /// Заданы такие условия, чтобы ячейка с едой спавнилась в одной или более клеток от стен и от тела змейки
        /// Не вплотную и не поверх змейки
        /// </summary>
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

        /// <summary>
        /// Удалет существующую змейку и создаёт новую
        /// </summary>
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

        /// <summary>
        /// Перересовывает поле при изменении параметров игрового поля
        /// </summary>
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
