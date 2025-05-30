using System;
using System.Collections.Generic;

namespace Tricky_Animals
{
    class TrickyAnimals
    {
        static void Main()
        {
            long n = long.Parse(Console.ReadLine());
            string [] inputstrings = new string[n];                 
                                                                   
            for (int i = 0; i < n; i++)                             
                inputstrings[i] = Console.ReadLine();

            foreach (string s in inputstrings)
            {
                string graaf = Graaf_Creatie(s);
                Console.WriteLine(graaf.Length + " " + graaf);
            }
            Console.ReadKey();
        }

        static Knoop Switch(Knoop K, int i, int j)
        {
            char[] C = K.permutatie.ToCharArray();              
            char temp = C[i];
            C[i] = C[j];
            C[j] = temp;
            return new Knoop(new string(C));
        }

        static Knoop Rotate(Knoop K)
        {
            int n = K.permutatie.Length;                       
            char[] temp = new char[n];
            temp[0] = K.permutatie[0]; temp[n - 1] = K.permutatie[n - 1];

            for (int i = 2;i < n - 1;i++)
            {
                if (i == n - 2)
                {
                    temp[i] = K.permutatie[i - 1];
                    temp[1] = K.permutatie[n - 2];
                }
                else
                    temp[i] = K.permutatie[i - 1];
            }
            return new Knoop(new string(temp));
        }
        
        static string Reverse(string input)                 
        {
            char [] chars = input.ToCharArray();
            string reverseString = "";

            for (int i = chars.Length - 1; i >= 0; i--)
                reverseString += chars[i];
            return reverseString;
        }
        
        static string Graaf_Creatie(string source)
        {
            int n = source.Length;
            char[] chars = source.ToCharArray();
            Array.Sort(chars);
            string target = new string(chars);
                                                                                  
            HashSet<string> Visited = new HashSet<string>(); 
            Graph graph = new Graph();
            Queue<Knoop> queue = new Queue<Knoop>();   
            
            Knoop source_knoop = new Knoop(source);
            Knoop current = source_knoop;             
            queue.Enqueue(current);
            Visited.Add(current.permutatie);

            while (current.permutatie != target && queue.Count > 0) 
            {
                current = queue.Dequeue();                         
                graph.AddKnoop(current);
                
                string bxa = "axb";
                int t = 0;
                foreach(Knoop k in new List<Knoop> { Switch(current, 0, 1), Rotate(current), Switch(current,n - 2,n - 1)})
                {
                    if (!Visited.Contains(k.permutatie))
                    {
                        graph.AddKnoop(k);
                        graph.AddKant(current, k, bxa[t]);
                        queue.Enqueue(k);
                        Visited.Add(k.permutatie);
                    }
                    t++;
                }
            }
            return ReconstructPath(source_knoop, current);
        }

        static string ReconstructPath(Knoop start, Knoop einde)
        {
            string path = "";

            if (einde.permutatie == start.permutatie)
                return path;

            Knoop temp = einde;                   
            Knoop current = einde.parent;

            while (current.permutatie != start.permutatie) 
            {                                              
                foreach (Knoop knoop in current.child)
                    if (knoop.permutatie == temp.permutatie)
                        path += knoop.operatie;
                temp = current;
                current = current.parent;
            }

            foreach (Knoop knoop in current.child)
                if (knoop.permutatie == temp.permutatie)
                    path += knoop.operatie;

            return Reverse(path);
        }
    }
    
    public class Knoop
    {
        public string permutatie;                           
        public Knoop parent;
        public LinkedList<Knoop> child = new LinkedList<Knoop>();
        public char operatie;
        public Knoop(string perm)
        {
            permutatie = perm;
        }
    }
    
    public class Graph      
    {
        public Graph() { }

        public Dictionary<Knoop, LinkedList<Knoop>> AdjacencyList { get; } = new Dictionary<Knoop, LinkedList<Knoop>>();

        public void AddKnoop(Knoop knoop)
        {
            AdjacencyList[knoop] = new LinkedList<Knoop>();
        }

        public void AddKant(Knoop parent, Knoop child, char operatie)
        {
            child.parent = parent;
            parent.child.AddLast(child);
            child.operatie = operatie;
            AdjacencyList[parent] = parent.child;
        }
    }
}
