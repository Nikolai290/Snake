
using Snake.Enums;
using System.Drawing;
using System.Windows.Forms;
using static Snake.Settings;
using Control = System.Windows.Forms.Control;

namespace Snake {


    /// <summary>
    /// Сущность обозначающая ячейку на игровом поле
    /// Примеяется для отрисовки еды, головы и тела змейки
    /// </summary>
    public class Cell {
        /// <summary>
        /// Содержит элемент windows forms label, он отображается как цветной квадратик на игровом поле
        /// </summary>
        public Label label { get; set; }
        /// <summary>
        /// Поля Х и Y хранят информацию об условных координатах, где должна распологаться эта клетка
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Поля Х и Y хранят информацию об условных координатах, где должна распологаться эта клетка
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Поле Point Coord служит просто для удобства, чтобы получать координаты одним свойством, а не двумя (X, Y)
        /// </summary>
        public Point Coord { get => new Point(X, Y); }
        /// <summary>
        /// Вычисляемое поле Die возвращает true, если клетка оказывается за пределами игрового поля.
        /// Применяется для проверки если бошка змейки врежется в границы игрового поля.
        /// </summary>
        public bool Die => (X < 0 || X >= WidthSize) || (Y < 0 || Y >= HeightSize);

        /// <summary>
        /// Конструктор класса принимает ячейку нужного цвета и условные игровые координаты
        /// </summary>
        /// <param name="label"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Cell(Color color, int x, int y, Control parent) {
            this.label = new Label() {
                AutoSize = false,
                Width = CellSize,
                Height = CellSize,
                BackColor = color,
                Text = "",
                Parent = parent
            };
            Go(x, y);
        }

        /// <summary>
        /// Перемещает ячейку на указанные игровые координаты
        /// </summary>
        public void Go(int x, int y) {
            X = x;
            Y = y;
            Go();
        }
        /// <summary>
        /// Перемещает ячейку на игровые координаты хранящиейся в полях сущности
        /// </summary>
        public void Go() {
            label.Location = new Point(X * CellSize, Y * CellSize);
        }

        /// <summary>
        /// Перемещает ячейку на указанные игровые координаты
        /// </summary>
        public void Go(Point to) {
            SetCoord(to);
            Go();
        }

        /// <summary>
        /// Записывает местоположение ячейки но не перемещает её
        /// </summary>
        /// <param name="coord"></param>
        public void SetCoord(Point coord) {
            X = coord.X;
            Y = coord.Y;
        }
        /// <summary>
        /// Перемещает ячейку на одну клетку в указанном направлении. Применяется для передвижения головы змейки
        /// </summary>
        public void Go(Direction direction) {
            if (direction == Direction.Up) {
                Y--;
            } else if (direction == Direction.Rigth) {
                X++;
            } else if (direction == Direction.Down) {
                Y++;
            } else if (direction == Direction.Left) {
                X--;
            }

            Go();
        }

        /// <summary>
        /// Проверяет коллизию ячейки с потенциальным объектом на указанных координатах
        /// </summary>
        /// <returns>Возвращает true если координаты ячейки совпадают с переданными в параметрах</returns>
        public bool Collision(int x, int y) {
            return X == x && Y == y;
        }

        /// <summary>
        /// Проверяет коллизию ячейки с потенциальным объектом на указанных координатах
        /// </summary>
        /// <returns>Возвращает true если координаты ячейки совпадают с переданными в параметрах</returns>
        public bool Collision(Point coord) {
            return Collision(coord.X, coord.Y);
        }

        /// <summary>
        /// Перерисовывает ячейку при изменении в настройках размера ячеек
        /// </summary>
        public void Rewrite() {
            label.Width = CellSize;
            label.Height = CellSize;
            Go();
        }
    }
}
