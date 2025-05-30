using System;
using System.Text;


namespace ToetsenStack
{
    class Toetsenspion
    {
        static void Main()
        {
            string input = Console.ReadLine();
            long n = long.Parse(input.Split()[0]);
            string [] toetsaanslagen_invoer = new string[n];

            for (int i = 0; i < n; i++)
                toetsaanslagen_invoer[i] = Console.ReadLine();
            
            foreach(string wachtwoord in toetsaanslagen_invoer)
                Console.WriteLine(Wachtwoordkraker(wachtwoord));

            Console.ReadKey();
        }

        static string Wachtwoordkraker(string toetsaanslagen)
        {
            StringBuilder Wachtwoord = new StringBuilder("", toetsaanslagen.Length);
            
            LinkedListStack<char> wwStack = new LinkedListStack<char>();
            LinkedListStack<char> temp = new LinkedListStack<char>();
            LinkedListStack<char> reverse = new LinkedListStack<char>();

            foreach (char c in toetsaanslagen.ToCharArray())
            {
                LinkedListStack<char>.StackElement Element = new LinkedListStack<char>.StackElement(c);
                
                switch (c)
                {
                    case '-':
                        if (wwStack.IsEmpty() == false)
                            wwStack.Pop();
                        break;

                    case '<':
                        if (wwStack.IsEmpty() == false)
                            temp.Push(wwStack.Pop());
                        break;

                    case '>':
                        if (temp.IsEmpty() == false)
                            wwStack.Push(temp.Pop());
                        break;

                    default:
                        wwStack.Push(Element.key);
                        break;
                }
            }

            while (temp.IsEmpty() == false)
                wwStack.Push(temp.Pop());
            
            while (wwStack.IsEmpty() == false)
                reverse.Push(wwStack.Pop());

            while (reverse.IsEmpty() == false)
                Wachtwoord.Append(reverse.Pop());
                
            return Wachtwoord.ToString();
        }
    }

    public class LinkedListStack<T>
    {
        public class StackElement
        {
            public StackElement(T element)
            { key = element; }

            public T key;

            public StackElement next;
            public StackElement prev;
        }

        StackElement head;

        public LinkedListStack() { this.head = null; }

        public StackElement Push(T x)
        {
            StackElement temp = new StackElement(x);

            temp.next = head; 
            head = temp;
            return head;
        }

        public T Pop()
        {
            T temp = head.key;
            head = head.next;
            return temp;
        }

        public bool IsEmpty() 
        { 
            return head == null; 
        }
    }
}
