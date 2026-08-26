using System;

namespace ChapinWarriors.Models
{
    public class RobotNode<T>
    {
        public T Data;
        public RobotNode<T>? Next {get;set;}

        public RobotNode(T data)
        {
            Data = data;
            Next = null;
        }
    }
    
    public class RobotList<T>
    {
        public RobotNode<T>? Head {get; set;}

        public void Add(T data){
            RobotNode<T> newRN = new RobotNode<T>(data);

            if (Head==null)
            {
                Head = newRN;
                return;
            }

            RobotNode<T> current = Head;

            while (current.Next != null)
            {
                current = current.Next;
            }

            current.Next = newRN;
        }

        public void ShowRobotList()
        {
            RobotNode<T>? current = Head;

            while (current != null)
            {
                if ( current.Data is Rescue r){
                    Console.WriteLine($"Chapin Rescue | Nombre: {r.getName()}");
                } else if ( current.Data is Fighter f){
                    Console.WriteLine($"Chapin Fighter | Nombre: {f.getName()} - Capacidad Actual: {f.getCapacity()}");
                } else if ( current.Data is ElChimatron c){
                    Console.WriteLine($"El Chimatrón | Nombre: {c.getName()} - Capacidad Actual: {c.getCapacity()}");
                }

                current = current.Next;
            }
        }
    }
}
