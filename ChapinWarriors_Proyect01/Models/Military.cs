using System;

namespace ChapinWarriors.Models
{
    public class Military
    {
        private int capacity;
        private int row;
        private int column;

        public Military(int capacity, int row, int column)
        {
            this.capacity = capacity;
            this.row = row;
            this.column = column;
        }

        public int getM_Capacity(){ return capacity; }
        public int getM_Row(){ return row; }
        public int getM_Column(){ return column; }
        public void setM_Capacity( int capacity ){ this.capacity = capacity; }
        public void setM_Row( int row ){ this.row = row; }
        public void setM_Column ( int column ){ this.column = column; }
    }
}