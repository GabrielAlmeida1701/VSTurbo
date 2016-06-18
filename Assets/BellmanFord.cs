using UnityEngine;
using System.Collections;
using Assets;

public class BellmanFord : MonoBehaviour
{ 
    public static void RunBellmanFord(Graph graph, int source)
    {
        int listNodesSize = graph.listNodes.Count;
        var listAdj = graph.listNodes;

        int[] distance = new int[listNodesSize];
       

        for (int i = 0; i < listNodesSize; i++)
            distance[i] = int.MaxValue;

        distance[source] = 0;

        for (int i = 1; i <= listNodesSize - 1; ++i)
        {
            for (int j = 0; j < listNodesSize; ++j)
            {
                for(int k = 0; k < listAdj[j].adjacents.Count; k++)
                {
                    int u = listAdj[j].id;
                    int v = listAdj[j].adjacents[k].adjacent.id;
                    int weight = listAdj[j].adjacents[k].weight;

                    if (distance[u] != int.MaxValue && distance[u] + weight < distance[v])
                        distance[v] = distance[u] + weight;
                }
            }
        }
    }

}
