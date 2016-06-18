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
        }
    }

    public class Graph
    {
        public List<Node> listNodes; //"cidades"
        public List<Edge> listEdges;

        int[,] matrizAdj;
        int nodesSize;

        public Graph(int size)
        {
            nodesSize = size;
            matrizAdj = new int[size, size];
            listNodes = new List<Node>();
        }

        public void Initialize()
        {
            //Criar cada vertice.
            //Definir os adjacentes de cada vertice e o tipo da aresta.
            Debug.Log("initializing graph...");

            GenGraph();
            ShowMatriz();
        }

        private Node createNode(int id)
        {
            Node node = new Node();
            node.id = id;
        
            return node;
        }

        private void GenGraph()
        {
            for(int i = 0; i < nodesSize; i++)
            {
                for(int j = 0; j < nodesSize; j++)
                {
                    if (i == j) matrizAdj[i, j] = 0;
                    else
                    {
                        int index = Random.Range(0, 2);
                        matrizAdj[i, j] = index;
                        matrizAdj[j, i] = index;
                    }
                }
            }

            TransformToList();
        }

        private void TransformToList()
        {
            for(int i = 0; i < nodesSize; i++)
            {
                listNodes.Add(createNode(i));
            }

            for(int i = 0; i < nodesSize; i++)
            {
                for(int j = 0; j < nodesSize; j++)
                {
                    if(matrizAdj[i,j] != 0)
                    {
                        Edge edge = new Edge();
                        edge.adjacent = listNodes[j];
                        edge.weight = Random.Range(5, 10);
                        ROAD_TYPE rtype = (ROAD_TYPE)Random.Range((int)ROAD_TYPE.ASPHALT_GOOD, (int)ROAD_TYPE.BRIDGE);
                        edge.road_type = rtype;

                        listEdges.Add(edge);
                    }
                }
            }
        }

        private void ShowMatriz()
        {
            for (int i = 0; i < nodesSize; i++)
            {
                for (int j = 0; j < nodesSize; j++)
                {
                    Debug.Log(matrizAdj[i,j]);
                }
            }
        }

        public List<Edge> GetAdjacents(int id)
        {
            Node item = listNodes.Find(x => x.id == id);

            return item.adjacents;
        }

        /*
            NOTA:
            
            VERIFICAR SE REALMENTE A PARTIR DE QUALQUER VERTICE, POSSA CHEGAR EM QUALQUER OUTRO VERTICE.
            SE NAO PUDER, TEMOS UM ERRO PARA RESOLVER.
             
            FAZER UMA FUNÇÃO BELLMAN FORD PARA VERIFICAR O MENOR CAMINHO E RETORNAR OS VERTICES DO CAMINHO.
        */
    }
}
