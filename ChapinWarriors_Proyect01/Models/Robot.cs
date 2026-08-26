using System;

namespace ChapinWarriors.Models
{
    public abstract class Robot 
    {
        protected string name;

        public Robot(string name)
        {
            this.name = name;
        }

        public string getName(){ return name; }
    }

    ///////////////////////////////////////////////////////////////////////////////

    public class Rescue : Robot
    { 
        private string type;

        public Rescue(string name, string type) : base(name)
        {
            this.type = type;
        }

        public string getType(){ return type; }
        public void setType(string type){ this.type = type; }
    } 

    public class Fighter : Robot
    { 
        protected string type;
        protected int capacity;

        public Fighter(string name, string type, int capacity) : base(name)
        {
            this.type = type;
            this.capacity = capacity;
        }

        public string getType(){ return type; }
        public int getCapacity(){ return capacity; }
        public void setType(string type){ this.type = type; }
        public void setCapacity(int capacity){ this.capacity = capacity; }
    } 

    public class ElChimatron : Robot
    {
        protected string type;
        protected int capacity;
        
        public ElChimatron(string name, string type, int capacity) : base(name)
        {
            this.type = type;
            this.capacity = capacity;
        }

        public string getType(){ return type; }
        public int getCapacity(){ return capacity; }
        public void setType(string type){ this.type = type; }
        public void setCapacity(int capacity){ this.capacity = capacity; }
    }
}