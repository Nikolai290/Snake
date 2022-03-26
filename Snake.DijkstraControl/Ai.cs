using System.Collections.Generic;
using System.Linq;

namespace Snake.DijkstraControl {
    public class Ai {

        public IList<Node> FindWay(IList<Node> list) {
            var nodes = list.Select(x => x).ToList();
            var head = nodes.SingleOrDefault(x => x.NodeType == NodeType.Head);
            var food = nodes.SingleOrDefault(x => x.NodeType == NodeType.Food);
            FindFoodFromHead(head, 0);
            FindBestWay(food);

            return nodes;
        }

        private void FindBestWay(Node node) {
            if (node == null) return;
            if (node.NodeType == NodeType.Head) {
                return;
            }
            if(node.NodeType == NodeType.Empty) {
                node.NodeType = NodeType.Way;
            }
            var minValue = node.Neighbors.Select(x => x?.Value ?? int.MaxValue).Min();
            var nextNode = node.Neighbors.FirstOrDefault(x => x?.Value == minValue);
            FindBestWay(nextNode);
        }

        private bool FindFoodFromHead(Node node, int value) {
            if (node == null) return false;
            node.Value = value;
            if (node.NodeType == NodeType.Food) {
                return true;
            }
            if(node.Value.HasValue && value >= node.Value) {
                return false;
            }
            return node.Neighbors.Any(x => FindFoodFromHead(x, value + 1));
        }
    }
}
