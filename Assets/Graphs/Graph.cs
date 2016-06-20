using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets
{
    public enum ROAD_TYPE
    {
       ASPHALT_GOOD = 1,
       ASPHALT_BAD = 2,
       DIRT = 3,
       BRIDGE = 4
    }
    
    //Aresta = Estrada
    public class Edge
    {
        public ROAD_TYPE road_type;
        public int weight;
        public Node adjacent;
    }

    //Vertice = Cidade
    public class Node
    {
        public int id;
        public float time;
        public List<Edge> adjacents;

        public Node()
        {
            adjacents = new List<Edge>();
            time = 5 + Random.value * 10;
        }
    }

    public class Graph
    {
        public List<Node> listNodes; //"cidades"
		
        public int nodesSize;

        public Graph(int size)
        {
            nodesSize = size;
            listNodes = new List<Node>();
        }

        public void Initialize()
        {
            GenGraph();
        }

        private Node createNode(int id)
        {
            Node node = new Node();
            node.id = id;
        
            return node;
        }

        private void GenGraph()
        {
            for (int i = 0; i < nodesSize; i++)
            {
                listNodes.Add(createNode(i));
            }

            AddEdge(0, 1, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(0, 7, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(0, 12, ROAD_TYPE.DIRT);
            AddEdge(0, 11, ROAD_TYPE.ASPHALT_BAD);

            AddEdge(1, 0, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(1, 2, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(1, 7, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(1, 11, ROAD_TYPE.ASPHALT_BAD);

            AddEdge(2, 1, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(2, 3, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(2, 7, ROAD_TYPE.DIRT);
            AddEdge(2, 12, ROAD_TYPE.ASPHALT_BAD);

            AddEdge(3, 2, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(3, 6, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(3, 5, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(3, 4, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(3, 9, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(3, 13, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(3, 8, ROAD_TYPE.DIRT);

            AddEdge(4, 3, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(4, 5, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(4, 6, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(4, 9, ROAD_TYPE.ASPHALT_GOOD);

            AddEdge(5, 3, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(5, 4, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(5, 6, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(5, 9, ROAD_TYPE.DIRT);
            AddEdge(5, 10, ROAD_TYPE.ASPHALT_BAD);

            AddEdge(6, 3, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(6, 4, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(6, 5, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(6, 10, ROAD_TYPE.ASPHALT_GOOD);

            AddEdge(7, 1, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(7, 0, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(7, 2, ROAD_TYPE.DIRT);
            AddEdge(7, 11, ROAD_TYPE.DIRT);
            AddEdge(7, 12, ROAD_TYPE.ASPHALT_GOOD);
            
            AddEdge(8, 3, ROAD_TYPE.DIRT);
            AddEdge(8, 9, ROAD_TYPE.DIRT);
            AddEdge(8, 12, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(8, 13, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(8, 15, ROAD_TYPE.DIRT);

            AddEdge(9, 3, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(9, 4, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(9, 5, ROAD_TYPE.DIRT);
            AddEdge(9, 8, ROAD_TYPE.DIRT);
            AddEdge(9, 10, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(9, 14, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(9, 13, ROAD_TYPE.DIRT);

            AddEdge(10, 5, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(10, 6, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(10, 9, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(10, 14, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(10, 17, ROAD_TYPE.ASPHALT_BAD);

            AddEdge(11, 0, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(11, 1, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(11, 7, ROAD_TYPE.DIRT);
            AddEdge(11, 12, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(11, 15, ROAD_TYPE.ASPHALT_GOOD);

            AddEdge(12, 0, ROAD_TYPE.DIRT);
            AddEdge(12, 2, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(12, 7, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(12, 8, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(12, 11, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(12, 13, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(12, 15, ROAD_TYPE.ASPHALT_BAD);

            AddEdge(13, 3, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(13, 8, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(13, 9, ROAD_TYPE.DIRT);
            AddEdge(13, 12, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(13, 14, ROAD_TYPE.DIRT);
            AddEdge(13, 15, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(13, 16, ROAD_TYPE.ASPHALT_BAD);

            AddEdge(14, 9, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(14, 10, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(14, 17, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(14, 13, ROAD_TYPE.DIRT);
            AddEdge(14, 16, ROAD_TYPE.DIRT);

            AddEdge(15, 11, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(15, 12, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(15, 8, ROAD_TYPE.DIRT);
            AddEdge(15, 13, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(15, 16, ROAD_TYPE.DIRT);

            AddEdge(16, 13, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(16, 14, ROAD_TYPE.DIRT);
            AddEdge(16, 15, ROAD_TYPE.DIRT);
            AddEdge(16, 17, ROAD_TYPE.ASPHALT_GOOD);

            AddEdge(17, 10, ROAD_TYPE.ASPHALT_BAD);
            AddEdge(17, 14, ROAD_TYPE.ASPHALT_GOOD);
            AddEdge(17, 16, ROAD_TYPE.ASPHALT_GOOD);
        }

        public void AddEdge(int S, int D, ROAD_TYPE rType)
        {
            Edge edge = new Edge();
            edge.adjacent = listNodes[S];
            edge.weight = Random.Range(5, 10);
            edge.road_type = rType;

            listNodes[D].adjacents.Add(edge);
        }

        public int IsAdjacent(int id, int next)
        {
            for (int i = 0; i < listNodes[id].adjacents.Count; i++) {
                Edge ed = listNodes[id].adjacents[i];
                if (ed.adjacent.id == next)
                    return i;
            }

            return -1;
        }

        public List<Edge> GetAdjacents(int id)
        {
            Node item = listNodes.Find(x => x.id == id);
            return item.adjacents;
        }
    }
}
