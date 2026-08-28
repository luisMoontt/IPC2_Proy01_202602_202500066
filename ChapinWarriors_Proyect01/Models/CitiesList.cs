using System;

namespace ChapinWarriors.Models
{
    public class CityNode
    {
        private City data;
        private CityNode? next;

        public CityNode(City data)
        {
            this.data = data;
            this.next = null;
        }

        public City getData() { return data; }
        public CityNode? getNext() { return next; }
        public void setData(City data) { this.data = data; }
        public void setNext(CityNode? next) { this.next = next; }
    }

    public class CitiesList
    {
        private CityNode? head;

        public CitiesList()
        {
            this.head = null;
        }

        public CityNode? getHead() { return head; }
        public void setHead(CityNode? head) { this.head = head; }

        public void AddCity(City data)
        {
            CityNode newCN = new CityNode(data);

            if (head == null)
            {
                head = newCN;
                return;
            }

            CityNode current = head;

            while (current.getNext() != null)
            {
                current = current.getNext()!;
            }

            current.setNext(newCN);
        }

        public void cityNames(){
            CityNode? current = head;

            while (current != null)
            {
                City c = current.getData();
                Console.WriteLine($"Ciudad: {c.getName()} | Dimensiones: {c.getC_Rows()}x{c.getC_Columns()}");
                current = current.getNext();
            }
        }

        public bool cityExists(string c_name){
            CityNode? current = head;
            bool find = false;

            while (current != null)
            {
                City c = current.getData();
                if (c.getName() == c_name)
                {
                    find = true;
                    break;
                }

                current = current.getNext();
            }

            return find;
        }

        public City getCity(string c_name)
        {
            CityNode? current = head;
            City? f_City =  null;

            while (current != null)
            {
                City c = current.getData();

                if (c.getName() == c_name)
                {
                    f_City = c;
                    break;
                }
                current = current.getNext();
            }

            return f_City;
        }

        public void ShowCityList()
        {
            CityNode? current = head;

            while (current != null)
            {
                City c = current.getData();
                MapMatrix cityMap = c.getMap();
                MilitaryList m_list = c.getMList();

                Console.WriteLine($"\n--- MAPA DE CIUDAD: {c.getName()} ---");
                cityMap.ShowConsoleMap();
                Console.WriteLine($"Ciudad: {c.getName()} | Dimensiones: {c.getC_Rows()}x{c.getC_Columns()}");
                Console.WriteLine("\n-- UNIDADES MILITARES EN EL AREA --");
                m_list.ShowMilitaryList();
                

                current = current.getNext();
            }
        }
    }
}