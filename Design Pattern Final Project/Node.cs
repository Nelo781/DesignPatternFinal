using System;
using System.Collections.Generic;
using System.Text;

namespace Design_Pattern_Final_Project
{
    
	public class Node<T> 
	{
		public Node(T val, Node <T> next_one = null)
		{
			data = val;
			next = next_one;
		}

		public T data { get; set; }
		public Node<T> next { get; set; }

        public override string ToString()
        {
			return "Type: " + data.GetType() + " Value: " + data.ToString();
        }
    }
	
}
