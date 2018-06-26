using InfixCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixCalculator.DataStructures
{
    public class Stack<T>
    {
        public Node<T> Head { get; set; }

        public void Push(T value)
        {
            var newNode = new Node<T>(value);

            newNode.Next = Head;
            Head = newNode;
        }

        public T Pop()
        {
            var toReturn = Head;

            Head = toReturn.Next;

            return toReturn.Data;
        }

        public bool IsEmpty()
        {
            return Head == null;
        }

        public T Pull()
        {
            return Head.Data;
        }

        //public T AtIndex(int index)
        //{
        //    var current = Head;
        //    var counter = 0;
        //    while (current != null && current.Next != null && counter < index)
        //    {
        //        current = current.Next;
        //        counter++;
        //    }

        //    if (counter >= index || current == null || current.Next == null)
        //    {
        //        throw new ArgumentOutOfRangeException();
        //    }
        //}
    }
}
