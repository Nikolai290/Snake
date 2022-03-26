using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake.DijkstraControl {
    public class Ai {
        private double counter = 0;
        public IList<Node> FindWay(IList<Node> list) {
            var nodes = list.Select(x => x).ToList();

            counter = Math.Pow(nodes.Count, nodes.Count);
            var head = nodes.SingleOrDefault(x => x.NodeType == NodeType.Head);
            var food = nodes.SingleOrDefault(x => x.NodeType == NodeType.Food);
            if(head == null || food == null) {
                return nodes;
            }
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

        private void FindFoodFromHead(Node node, int value, Node from = null) {
            //TODO: repair
            if(counter < 0) {
                return;
            }
            if (node == null) return;
            node.Value = value;
            if (node.NodeType == NodeType.Food) {
                return;
            }
            if(value > node.Value) {
                return;
            }
            if (node.Visited) {
                return;
            }
            foreach(var neighbor in node.Neighbors) {
                if(neighbor == from) {
                    continue;
                }
                FindFoodFromHead(neighbor, value + 1, node);
                node.Visited = true;
            }
        }
    }
}
