using System;
using System.Linq;
using System.Collections.Generic;

namespace Bestelbus
{
    class Bestelbus
    {
        static void Main()
        {
            Bestelbus bb = new Bestelbus();
            string input = Console.ReadLine();

            int n = int.Parse(input.Split(' ')[0]); 
            int r = int.Parse(input.Split(' ')[1]);

            List<long> weightslist = new List<long>();
            while (weightslist.Count != n)
            {
                string inp = Console.ReadLine().Trim();
                int len = inp.Split(' ').Length;
                for (int i = 0; i < len; i++)
                    weightslist.Add(long.Parse(inp.Split(' ')[i])); 
            }
            Console.WriteLine(bb.BinarySearch(weightslist,r));
            Console.ReadLine();
        }
        long BinarySearch(List<long> weightslist, long ritten)
        {
            long low = weightslist.Max();
            long high = weightslist.Sum();
            
            while (low < high)
            {
                long mid = (low + high) / 2;
                
                if (Weight(weightslist, mid) == ritten) 
                    high = mid;
                if (Weight(weightslist, mid) > ritten) 
                    low = mid + 1;
                if (Weight(weightslist, mid) < ritten) 
                    high = mid - 1;
            }
            return low;
        }
        long Weight(List<long> weightlist, long mid)
        {
            long r = 0;
            long totaal = 0;

            for (int i = 0; i < weightlist.Count; i++)
            { 
                if (totaal + weightlist[i] == mid) 
                {
                    totaal += weightlist[i];
                }

                else if (totaal + weightlist[i] < mid) 
                    totaal += weightlist[i];
                  
                else
                {
                    r++;                            
                    totaal = 0;
                    i--;
                }
            }
            r++;
                
            return r;
        }
    }
}