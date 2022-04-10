using System.Collections.Generic;
using System.Linq;

namespace Snake.DijkstraControl {
    public class Ai {
        /// <summary>
        /// Нахождение пути методом Дейкстры
        /// </summary>
        /// <param name="list">Список всех ячеек игрового поля слева направо сверху вниз построчно</param>
        /// <returns>Список ячеек игрового поля с проставленными весами</returns>
        public IList<Node> FindWay(IList<Node> list) {
            var nodes = list.Select(x => x).ToList();
            var head = nodes.Where(x => x.NodeType == NodeType.Head).Take(1).ToList();
            var food = nodes.Where(x => x.NodeType == NodeType.Food).Take(1).ToList();
            if (food == null || food.Count() == 0) {
                return nodes;
            }
            food.First().Value = 0;
            food.First().Visited = true;
            PreOrder(food, food.First().Value + 1);

            return nodes;
        }

        /// <summary>
        /// Метод, производящий рекурсивный обход списка соседей каждой клетки игрового поля и проставляющий веса
        /// </summary>
        /// <param name="nodes">Список соседей игрового поля</param>
        /// <param name="value">Значение веа</param>
        private void PreOrder(IEnumerable<Node> nodes, int value) {
            if (nodes.Count() == 0) {
                return;
            }

            var neighborsNotVisited = new HashSet<Node>(nodes.SelectMany(x => x.Neighbors))
                .Where(x => x != null && !x.Visited! && x.Value > value && x.NodeType == NodeType.Empty).ToList();

            foreach (var neighbor in neighborsNotVisited) {
                neighbor.Value = value;
                neighbor.Visited = true;
            }

            PreOrder(neighborsNotVisited, value + 1);
        }
    }
}
