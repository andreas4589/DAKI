using System;
using System.Collections.Generic;
using System.Collections;

namespace Edsger_op_de_fiets
{
    class Edsger
    {
        static void Main()
        {
            string input = Console.ReadLine();
            int aantal_knopen = int.Parse(input.Split()[0]);
            char testmodus = char.Parse(input.Split()[1]);
            List<Vertex> locatielijst = new List<Vertex>();

            for(int i = 1; i <= aantal_knopen; i++)
                locatielijst.Add(new Vertex(Console.ReadLine().Split()[0], i) { d = int.MaxValue });

            PriorityQueue queue = new PriorityQueue(locatielijst);
            Graph graph = new Graph(locatielijst);

            switch (testmodus)
            {
                case 'p':
                    {
                        int i; int w; int x = int.Parse(Console.ReadLine().Split()[0]);
                        List<Vertex> extracts = new List<Vertex>();
                        for (int z = 0; z < x; z++)
                        {
                            input = Console.ReadLine();
                            switch(char.Parse(input.Split()[0]))
                            {
                                case 'u':
                                    {
                                        i = int.Parse(input.Split()[1]);
                                        w = int.Parse(input.Split()[2]);
                                        queue.DecreaseKey(graph.Adj[i].index, w);
                                    }
                                    break;
                                case 'e':
                                    extracts.Add(queue.ExtractMin());
                                    break;
                            }
                        }
                        foreach (Vertex v in extracts)
                            Console.WriteLine(v.ToString());
                    }
                    break;

                case 'd':
                    {
                        int i; int j; int k; int m = int.Parse(Console.ReadLine().Split()[0]); 
                        
                        for (int z = 0; z < m; z++)
                        {
                            input = Console.ReadLine();     
                            i = int.Parse(input.Split()[0]);
                            j = int.Parse(input.Split()[1]);
                            k = int.Parse(input.Split()[2]);
                            graph.Adj[i].kanten.Add(new Edge(graph.Adj[i],graph.Adj[j],k));              
                        }
                        Hashtable D = Dijkstra(graph, graph.Adj[1], queue);

                        for (int b = 1; b <= aantal_knopen; b++)
                            if(D.ContainsKey(b))
                                Console.WriteLine(D[b].ToString());       
                    }
                    break;
            }
            Console.ReadKey();
        }

        static void Init_Graph(Graph G, Vertex s)
        {
            foreach(Vertex v in G.Adj.Values)
                v.d = int.MaxValue;
            s.d = 0;
        }

        public static Hashtable Dijkstra(Graph G, Vertex s, PriorityQueue queue)
        {
            Init_Graph(G, s);
            Hashtable H = new Hashtable();
            
            while (queue.Size > 0)
            {
                Vertex current = queue.ExtractMin();

                if (current.d != int.MaxValue)
                {
                    H.Add(current.volgnummer, current);
                    
                    foreach (Edge edge in current.kanten)
                        if(!H.ContainsKey(edge.eind.volgnummer))
                            queue.DecreaseKey(edge.eind.index, edge.begin.d + edge.weight);   
                }
            }
            return H;
        }
    }

    public class Vertex
    {
        public string naam;
        public int volgnummer;
        public int d;
        public int index;
        public List<Edge> kanten = new List<Edge>();
        public Vertex(string locatie, int nummer)
        {
            naam = locatie;
            volgnummer = nummer;
        }
        public override string ToString()
        {
            return this.naam + " " + this.d;
        }
    }

    public class Edge
    {
        public Vertex begin;
        public Vertex eind;
        public int weight;
        public Edge(Vertex p, Vertex c, int w)
        {
            begin = p;
            eind = c;
            weight = w;
        }
    }

    public class Graph
    {
        public Dictionary<int,Vertex> Adj = new Dictionary<int, Vertex>();
        public Graph(List<Vertex> vertices)
        {
            foreach (Vertex v in vertices)
                Adj[v.volgnummer] = v;
        }
    }
    
    public class PriorityQueue
    {
        public int Size { get; private set; } = 0;
        private readonly Vertex[] Heap;
        private int ParentIndex(int i) => (i - 1) / 2;
        private int LeftChildIndex(int i) => (2 * i) + 1;
        private int RightChildIndex(int i) => (2 * i) + 2;

        public PriorityQueue(List<Vertex> vertices)
        {
            this.Heap = vertices.ToArray();
            this.Size = vertices.Count;
            
            for (int j = 0; j < Heap.Length; j++)
                Heap[j].index = j;
        }

        public Vertex ExtractMin()
        {
            Vertex min = Heap[0];
            Heap[0] = Heap[Size - 1];
            Heap[0].index = 0;

            Size--;
            MinHeapify(0);

            return min;
        }

        private void MinHeapify(int i)
        {
            int minIndex = i;
            int l = LeftChildIndex(i);
            int r = RightChildIndex(i);

            if (l < Size && Heap[l].d < Heap[minIndex].d)
                minIndex = l;
            
            if (r < Size && Heap[r].d < Heap[minIndex].d)
                minIndex = r;

            if (minIndex != i)
            {
                Swap(i, minIndex);
                MinHeapify(minIndex);
            }
        }

        public void DecreaseKey(int i, int key)
        {      
            if (key < Heap[i].d)
            {
                Heap[i].d = key;
                while (i > 0 && Heap[ParentIndex(i)].d > Heap[i].d)
                {
                    Swap(i, ParentIndex(i));
                    i = ParentIndex(i);
                }    
            }
        }

        private void Swap(int i, int j)
        {
            Vertex temp = Heap[i];
            Heap[i] = Heap[j]; 
            Heap[j] = temp;
            Heap[i].index = i;
            Heap[j].index = j;
        }
    }
}