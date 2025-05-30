using System;
using System.Collections.Generic;
using System.Linq;


namespace Air_Traffic_Control
{
    class ATC

    {
        static void Main()
        {
            ATC atc = new ATC();

            string input = Console.ReadLine();
            int n = int.Parse(input.Split()[0]);

            Tuple<long , long>[] vliegtuigen = new Tuple<long, long>[n];
            string vliegtuigeninput;
            for (int i = 0; i < n; i++)
            {
                vliegtuigeninput = Console.ReadLine();
                long x = long.Parse(vliegtuigeninput.Split()[0]);
                long y = long.Parse(vliegtuigeninput.Split()[1]);

                vliegtuigen[i] = Tuple.Create(x, y);
            }
            
            atc.Delta(vliegtuigen);
            //Console.WriteLine(atc.Distance(vliegtuigen));
            Console.ReadLine();
        }
        //Array.Sort(vliegtuigen, Comparer<Tuple<long, long>>.Default);

        long Distance(Tuple<long, long> A, Tuple<long, long> B)
        { return (A.Item1 - B.Item1) * (A.Item1 - B.Item1) + (A.Item2 - B.Item2) * (A.Item2 - B.Item2); }
        
        void Delta(Tuple<long, long>[] vliegtuigen)
        {
            Tuple<long, long>[] L = vliegtuigen.Take(vliegtuigen.Length / 2).ToArray();
            Tuple<long, long>[] R = vliegtuigen.Skip(vliegtuigen.Length / 2).ToArray();
            List <Tuple<long, long>> S  = new List<Tuple<long, long>>();
            long dL = 10000;
            for (int i = 0; i < L.Length; i++)
                for (int j = 0; j < L.Length; j++)
                    if (Distance(L[i], L[j]) < dL && Distance(L[i], L[j]) > 0)
                        dL = Distance(L[i], L[j]);
            long dR = 10000;
            for (int i = 0; i < R.Length; i++)
                for (int j = 0; j < R.Length; j++)
                    if (Distance(R[i], R[j]) < dR && Distance(R[i], R[j]) > 0)
                        dR = Distance(R[i], R[j]);

            long delta = Math.Min(dL, dR);

            long x = 0;

            for (int i = 0; i < vliegtuigen.Length; i++)
                if (vliegtuigen[i].Item1 > x)
                    x = vliegtuigen[i].Item1;

            for (int i = 0; i < vliegtuigen.Length; i++)
                if (vliegtuigen[i].Item1 <= x)
                    S.Add(vliegtuigen[i]);

            S.Sort();
            Console.WriteLine(delta);
            //for (int i = 0; i < S.Count; i++)
              //  Console.WriteLine(S[i]);
        }
        
    }
}
