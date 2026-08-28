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

        public Fighter getFighter(string r_name)
        {
           RobotNode<T>? current = Head;
           Fighter? r_fighter = null;


            while (current != null)
            {
                if(current.Data is Fighter f)
                {
                    if(f.getName()==r_name)
                    {
                        r_fighter = f;
                        break;
                    }
                }
                current = current.Next;
            }
            return r_fighter;   
        }

        public Rescue getRescue(string r_name)
        {
           RobotNode<T>? current = Head;
           Rescue? r_rescue = null;
           
            while (current != null)
            {
                if(current.Data is Rescue r)
                {
                    if(r.getName()==r_name)
                    {
                        r_rescue = r;
                        break;
                    }
                }
                current = current.Next;
            }
            return r_rescue;   
        }

        public void listFighters(){
            RobotNode<T>? current = Head;

            while (current != null)
            {
                if ( current.Data is Fighter f){
                    Console.WriteLine($"Chapin Fighter | Nombre: {f.getName()} - Capacidad Actual: {f.getCapacity()}");
                } 
                current = current.Next;
            }
        }

        public void listRescues(){
            RobotNode<T>? current = Head;

             while (current != null)
            {
                if ( current.Data is Rescue r){
                    Console.WriteLine($"Chapin Rescue  | Nombre: {r.getName()}");
                } 
                current = current.Next;
            }
        }

        public void ShowRobotList()
        {
            RobotNode<T>? current = Head;

            while (current != null)
            {
                if ( current.Data is Rescue r){
                    Console.WriteLine($"Chapin Rescue  | Nombre: {r.getName()}");
                } else if ( current.Data is Fighter f){
                    Console.WriteLine($"Chapin Fighter | Nombre: {f.getName()} - Capacidad Actual: {f.getCapacity()}");
                } else if ( current.Data is ElChimatron c){
                    Console.WriteLine($"3lCh1m4tr0nASI | Nombre: {c.getName()} - Capacidad Actual: {c.getCapacity()}");
                }

                current = current.Next;
            }
        }
    }
}
