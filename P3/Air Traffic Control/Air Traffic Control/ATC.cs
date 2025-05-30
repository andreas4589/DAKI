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

            Point [] vliegtuigen = new Point[n];
            string vliegtuigeninput;
            for (int i = 0; i < n; i++)
            {
                vliegtuigeninput = Console.ReadLine();
                long x = long.Parse(vliegtuigeninput.Split()[0]);
                long y = long.Parse(vliegtuigeninput.Split()[1]);
                Point punt = new Point();
                punt.X = x; punt.Y = y;                              //slaat vliegtuig coordinaten op in een Point punt
                vliegtuigen[i] = punt;                               //slaat alle vliegtuigen op in Point Array vliegtuigen
            }
            vliegtuigen = vliegtuigen.OrderBy(y => y.X).ToArray();   //sorteert vliegtuigen van kleinste naar grootste x coordinaat
            
            Console.WriteLine(atc.Recursive(vliegtuigen, n));
            Console.ReadLine();
        }

        long Recursive(Point[] P, long n)
        {
            if (n < 3)
                return KleinsteAfstand(P, n);              //Als de grootte van de array 2 of kleiner is wordt de afstand tussen de punten gereturned

            long mid = n / 2;                              // mid = x* (de grootste x-coordinaat van de linker helft van P

            Point[] L = P.Take(P.Length / 2).ToArray();    // L is de linker helft van P
            Point[] R = P.Skip(P.Length / 2).ToArray();    // R is de rechter helft van P
            long dl = Recursive(L, mid);                   // recursieve aanroepen die de min. afstand van L en R berekenen
            long dr = Recursive(R, n - mid);

            long delta = Math.Min(dl, dr);                 // delta is het minimum van dl en dr

            Point midPoint = P[mid];                       // grootste x* in L is gelijk aan P[mid] aangezien P gesorteerd is op oplopende x coordinaat
            Point[] S = new Point[n];                      // S is de nieuwe Point array waarbij de x-coordinaat van de punten kleiner is dan mid(x*)
            int j = 0;
            for (int i = 0; i < n; i++)
                if (Math.Abs(P[i].X - midPoint.X) < delta)
                {
                    S[j] = P[i];
                    j++;
                }

            return Math.Min(delta, S_Closest(S, j, delta)); // return het minimum van delta en de kleinste afstand in S (berekend met S_Closest)
        }

        long S_Closest(Point[] S, int size, long delta)
        {
            long min = delta;
            
            S = S.OrderBy(y => y.Y).ToArray();              // sorteer S array bij oplopende Y-coordinaat

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size && (S[j].Y - S[i].Y) < min; j++)
                    if (Distance(S[i], S[j]) < min && Distance(S[i], S[j]) > 0)
                        min = Distance(S[i], S[j]);         // als er in S een afstand is tussen twee punten dat kleiner is dan delta wordt dit ons minimum
            return min;
        }
        long Distance(Point p1, Point p2)
        { 
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y); // berekent de afstand tussen twee punten mbv Pythagoras
        }

        long KleinsteAfstand(Point[] P, long n)         // berekent de kleinste afstand in een Point Array iteratief
        {
            long min = long.MaxValue;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (Distance(P[i], P[j]) < min && i!= j)
                        min = Distance(P[i], P[j]);
            return min;
        }

        public struct Point             //struct om makkelijk alle points in op te slaan
        {
            public long X { get; set; }
            public long Y { get; set; }
        }
    }
}
