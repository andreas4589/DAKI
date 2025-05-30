using System;

class Collatz
{
    static void Main()
    {
        Collatz col = new Collatz();
        string uitvoer = "\n";
     
        string invoer = Console.ReadLine();

        int aantal = int.Parse(invoer.Split(' ')[0]);

        for (int i = 0; i < aantal; i++)
        {
            long getal = int.Parse(Console.ReadLine().Split(' ')[0]);
            uitvoer += col.Collatzgetal(getal) + "\n";
        }
        Console.WriteLine(uitvoer);
        Console.ReadLine();
    }

    public int Collatzgetal(long c)
    {
        int lengte = 0;
        while (c != 1)
        {
            if (c % 2 == 0)
            {
                c /= 2;
                lengte++;
            }

            else
            {
                c = 3 * c + 1;
                lengte++;
            }
        }

        return lengte;
    }
}

