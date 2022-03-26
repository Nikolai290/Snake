using System.Collections.Generic;
using System.Drawing;

namespace Snake.DijkstraControl {
    public class Node {
        public int X { get; set; }
        public int Y { get; set; }
        public Point Location => new Point(X, Y);

        /// <summary>
        /// 0 = top, 1 = right, 2 = bottom, 3 = left, like direction enum
        /// </summary>
        public IList<Node> Neighbors { get; set; } = new List<Node>(4);

        public int? Value { get; set; }
        public NodeType NodeType { get; set; }
    }
}
