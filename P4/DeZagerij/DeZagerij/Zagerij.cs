using System;

namespace DeZagerij
{
    class Zagerij
    {
        static public int k;
        static void Main()
        {
            /*string input = Console.ReadLine().Trim();
            long h = long.Parse(input.Split()[0]);
            long b = long.Parse(input.Split()[1]);
            k = int.Parse(input.Split()[2]);

            Zaagbewegingen[] zb = new Zaagbewegingen[h+b-2];

            for (int i = 0; i < zb.Length; i++)
            {
                input = Console.ReadLine();
                int kosten = int.Parse(input.Trim().Split()[0]);
                if (i<h-1)
                    zb[i] = new Zaagbewegingen(kosten, "horizontaal");
                else
                    zb[i] = new Zaagbewegingen(kosten, "verticaal");
            }
            long antwoord = Zager(zb);
            
            Console.WriteLine(antwoord + " " + (h * b - 1).ToString());*/
            string[] test = {"hallo", "kaas", "bier"};
            QuickSort(test, 0, 2);
            foreach (string i in test)
                Console.WriteLine(i);
            Console.ReadKey();
        }

        static long Zager(Zaagbewegingen[] bewegingen)
        {
            long kosten = 0;
            long H_aantal = 0;
            long V_aantal = 0;

            Zagerij.QuickSort(bewegingen, 0, bewegingen.Length - 1);

            for (int i = bewegingen.Length-1; i >= 0; i--)
            {
                if (bewegingen[i].richting == "verticaal")
                {
                    V_aantal += 1;
                    kosten += (H_aantal + 1) * bewegingen[i].kosten;
                }
                if (bewegingen[i].richting == "horizontaal")
                {
                    H_aantal += 1;
                    kosten += (V_aantal + 1) * bewegingen[i].kosten;
                }
            }
            return kosten;
        }
        static void Swap(IComparable[] A, long i, long j)
        {
            IComparable temp = A[i];
            A[i] = A[j];
            A[j] = temp;
        }

        static void SelectionSort(IComparable[] A, long low, long high)
        {
            for (long i = low; i <= high; i++)
            {
                long kleinste = i;
                for (long j = i + 1; j < high + 1; j++)
                    if (A[j].CompareTo(A[kleinste]) < 0)
                        kleinste = j;

                Swap(A, kleinste, i);
            }
        }

        static void QuickSort(IComparable[] A, long low, long high)
        {
            if (high-low+1 <= k)
                SelectionSort(A,low,high);

            else if (low < high)
            {
                long partition = Partition(A, low, high);

                QuickSort(A, low, partition - 1);
                QuickSort(A, partition + 1, high);
            }
        }

        static long Partition(IComparable[] A, long low, long high)
        {
            IComparable pivot = A[high];

            long i = low - 1;

            for (long j = low; j < high; j++)
            {
                if (A[j].CompareTo(pivot) < 0)
                {
                    i++;
                    Swap(A, i, j);
                }
            }
            Swap(A, i + 1, high);

            return i + 1;
        }
    }

    public class Zaagbewegingen : IComparable
    {
        public long kosten;
        public string richting;

        public Zaagbewegingen(long kosten, string richting)
        {
            this.kosten = kosten;
            this.richting = richting;
        }

        public int CompareTo(object B)
        {
            Zaagbewegingen other = B as Zaagbewegingen;

            if (this.kosten < other.kosten)
            {
                return -1;
            }
            else if (this.kosten > other.kosten)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
