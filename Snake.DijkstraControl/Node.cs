using System.Collections.Generic;
using System.Drawing;

namespace Snake.DijkstraControl {
    /// <summary>
    /// Сущность, хранящая информацию о конкретной ячейке игрового поля и содержащая ссылки на всех её соседей
    /// </summary>
    public class Node {

        /// <summary>
        /// Координата Х игрового поля
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Координата Y игрового поля
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Координаты местоположения ячейки
        /// </summary>
        public Point Location => new Point(X, Y);
        /// <summary>
        /// Список всех соседних ячеек
        /// 0 = top, 1 = right, 2 = bottom, 3 = left, like direction enum
        /// </summary>
        public IList<Node> Neighbors { get; set; } = new List<Node>(4);
        /// <summary>
        /// Вес ячейки
        /// </summary>
        public int Value { get; set; } = int.MaxValue;
        /// <summary>
        /// Тип ячейки
        /// </summary>
        public NodeType NodeType { get; set; }
        /// <summary>
        /// Была ли посещена ячейки при расстановке весов
        /// </summary>
        public bool Visited { get; set; }
    }
}
