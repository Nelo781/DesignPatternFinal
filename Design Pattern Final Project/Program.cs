using System;

namespace Design_Pattern_Final_Project
{
    class Program
    {
        static void Exo1()
        {
            Node<int> first = new Node<int>(10);
            Node<int> second = new Node<int>(24);
            Node<int> third = new Node<int>(7);

            CustomQueue<int> myqueue = new CustomQueue<int>();

            myqueue.Enqueue(first);
            myqueue.Enqueue(second);
            myqueue.Enqueue(third);

            // Using enumerable
            foreach (Node<int> node in myqueue)
            {
                Console.WriteLine(node.data);
            }

            myqueue.Dequeue();
            Console.WriteLine(myqueue);
        }

        static void Exo2()
        {
            string text = System.IO.File.ReadAllText("DeclarationDroithomme.txt");
            MapReduce counting_words = new MapReduce(text);  // Example 1
            //MapReduce exo2 = new MapReduce("One Two Tree Two Tree Tree");  //Example 2
            //MapReduce exo2 = new MapReduce("La fille du père du cousin de son cousin a un cousin père d'une fille");  //Example 3
            Console.WriteLine(counting_words);
        }
        static void Main(string[] args)
        {
            Exo2();

        }
    }
}
