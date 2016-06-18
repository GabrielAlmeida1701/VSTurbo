using UnityEngine;
using System.Collections;
using Assets;

public class BellmanFord : MonoBehaviour { 
        public static void BellmanFordi(Graph graph, int source)
    {
        int listNodes = graph.listNodes.Count;
        int listEdges = graph.listEdges.Count;
        int[] distance = new int[listNodes];
       

        for (int i = 0; i < listNodes; i++)
            distance[i] = int.MaxValue;

        distance[source] = 0;

        for (int i = 1; i <= listNodes - 1; ++i)
        {
            for (int j = 0; j < listEdges; ++j)
            {
                int u = 1;//graph.[j].Source; 
                int v = 0;// graph.edge[j].Destination;
                int weight = graph.listEdges[j].weight;

                if (distance[u] != int.MaxValue && distance[u] + weight < distance[v])
                    distance[v] = distance[u] + weight;
            }
        }

        for (int i = 0; i < listEdges; ++i)
        {
            int u = 1;// graph.listEdges[i].Source;
            int v = 0;// graph.listEdges[i].Destination;
            int weight = graph.listEdges[i].weight;

            if (distance[u] != int.MaxValue && distance[u] + weight < distance[v]);
                //Console.WriteLine("Graph contains negative weight cycle.");
        }
    }

}
