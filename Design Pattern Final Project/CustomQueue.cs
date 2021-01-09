using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Design_Pattern_Final_Project
{
    public class  CustomQueue<T> : IEnumerable<T>
    {
        public Node<T> Head { get; set; }

        public Node<T> Dequeue()
        {
            if(Head == null)
            {
                return null;
            }
            else
            {
                Node<T> temp = new Node<T>(Head.data);
                Head = Head.next;
                return temp;
            }
            

        }
        public Node<T> Peek()
        {
            return Head;
        }
        public void Enqueue(Node<T> new_node)
        {
            if(Head == null)
            {
                Head = new_node;
                new_node.next = null;
            }
            else
            {
                Node<T> temp = Head;
                while(temp.next != null)
                {
                    temp = temp.next;
                }
                temp.next = new_node;
            }
        }


        public CustomQueue(Node<T> head = null)
        {
            Head = head;
        }

        public override string ToString()
        {
            string str_out = "";
            foreach (Node<T> node in this)
            {
                str_out += "\n" + node.ToString();
            }
            return str_out;
        }

        public IEnumerator<Node<T>> GetEnumerator()
        {
            Node<T> current = Head;
            yield return current;
            while (current.next != null)
            {
                yield return current.next;
                current = current.next;
            }
        }

        
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = Head;
            while (current.next != null)
            {
                yield return current.data;
                current = current.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Node<T> current = Head;
            while (current.next != null)
            {
                yield return current;
                current = current.next;
            }
        }
    }
}
