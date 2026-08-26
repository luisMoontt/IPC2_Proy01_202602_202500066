using System;

namespace ChapinWarriors.Models
{
    public class City
    {
        private string name;
        private int c_rows;
        private int c_columns;
        private MapMatrix? map;
        private MilitaryList? m_list;

        public City(string name, int c_rows, int c_columns)
        {
            this.name = name;
            this.c_rows = c_rows;
            this.c_columns = c_columns;
            this.map = null;
            this.m_list = null;
        }

        public string getName() { return name; }
        public int getC_Rows() { return c_rows; }
        public int getC_Columns() { return c_columns; }
        public MapMatrix? getMap() { return map; }
        public MilitaryList? getMList() { return m_list; }

        public void setName(string name) { this.name = name; }
        public void setC_Rows(int c_rows) { this.c_rows = c_rows; }
        public void setC_Columns(int c_columns) { this.c_columns = c_columns; }
        public void setMap(MapMatrix? map) { this.map = map; }
        public void setMList(MilitaryList? m_list) { this.m_list = m_list; }
    }
}