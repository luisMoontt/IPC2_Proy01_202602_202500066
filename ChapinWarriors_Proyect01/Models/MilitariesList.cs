using System;

namespace ChapinWarriors.Models
{
    public class MilitaryNode
    {
        private Military data;
        private MilitaryNode? next;

        public MilitaryNode(Military data)
        {
            this.data = data;
            this.next = null;
        }

        public Military getData() { return data; }
        public MilitaryNode? getNext() { return next; }
        public void setData(Military data) { this.data = data; }
        public void setNext(MilitaryNode? next) { this.next = next; }
    }

    public class MilitaryList
    {
        private MilitaryNode? head;

        public MilitaryList()
        {
            this.head = null;
        }

        public MilitaryNode? getHead() { return head; }
        public void setHead(MilitaryNode? head) { this.head = head; }

        public void AddMilitary(Military data)
        {
            MilitaryNode newMN = new MilitaryNode(data);

            if (head == null)
            {
                head = newMN;
                return;
            }

            MilitaryNode current = head;

            while (current.getNext() != null)
            {
                current = current.getNext()!;
            }

            current.setNext(newMN);
        }

        public void ShowMilitaryList()
        {
            MilitaryNode? current = head;

            while (current != null)
            {
                Military m = current.getData();
                Console.WriteLine($"Unidad Militar | Capacidad: {m.getM_Capacity()} - Posición: ({m.getM_Row()}, {m.getM_Column()})");

                current = current.getNext();
            }
        }
    }
}