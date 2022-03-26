
using System.Drawing;
using System.Windows.Forms;
using static Snake.Settings;

namespace Snake {
    public class Cell {

        public Label label { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Point Coord { get => new Point(X, Y); }

        public bool Die { get; set; } = false;

        public Cell(Label label, int x, int y) {
            this.label = label;
            Go(x, y);
        }

        public void Go(int x, int y) {
            X = x;
            Y = y;
            Go();
        }
        
        public void Go() {
            if(X < 0 || X >= WidthSize) {
                Die = true;
                return;
            }
            if(Y < 0 || Y >= HeightSize) {
                Die = true;
                return;
            }
            label.Location = new Point(X * CellSize, Y * CellSize);
        }

        public void Go(Point to) {
            SetCoord(to);
            Go();
        }

        public void SetCoord(Point coord) {
            X = coord.X;
            Y = coord.Y;
        }

        public void Go(Direction direction) {
            if(direction == Direction.Up) {
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

        public bool Collision (int x, int y) {
            return X == x && Y == y;
        }

        public void Rewrite() {
            label.Width = CellSize;
            label.Height = CellSize;
            Go();
        }
    }
}
