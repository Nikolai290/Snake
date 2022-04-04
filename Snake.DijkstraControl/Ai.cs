using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Snake.DijkstraControl {
    public class Ai {

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
            //MarkWay(head);

            return nodes;
        }

        private void MarkWay(IEnumerable<Node> nodes) {
            if (nodes.Count() == 0) {
                return;
            }
            var min = nodes
                .Select(x => x.Value)
                .Min();
            var way = nodes.Where(x => x.NodeType == NodeType.Empty && x.Value == min);

            foreach (var node in way) {
                node.NodeType = NodeType.Way;
            }

            var neighbors = nodes
                .SelectMany(x => x.Neighbors)
                .Where(x => x != null && x.NodeType == NodeType.Empty && x.Value < min);
            MarkWay(neighbors);
        }

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
