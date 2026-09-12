using System;

namespace ChapinWarriors.Models
{
    public class MapNode
    {
        private int type;
        private string symbol;
        private bool state;

        public MapNode? Up { get; set; }
        public MapNode? Down { get; set; }
        public MapNode? Left { get; set; }
        public MapNode? Right { get; set; }

        public MapNode(int type=0, string symbol=" ")
        {
            this.type = type;
            this.symbol = symbol;
            this.state = true;
        }

        public int getNodeType(){ return type; }
        public string getSymbol(){ return symbol; }
        public bool getState(){ return state; }

        public void setNodeType(int type){ this.type = type; }
        public void setSymbol(string symbol){ this.symbol = symbol; }
        public void setState(bool state){ this.state = state; }
    }
    //////////////////////////////////////////////////////////////////////////////////
    public class MapMatrix
    {
        private MapNode? Head;
        private int rows;
        private int columns;

        public MapMatrix(int rows, int columns)
        {
            MakeMatrix(rows, columns);
        }

        public void MakeMatrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            if (rows <= 0 || columns <= 0) return;

            MapNode[,] grid = new MapNode[rows, columns];

            for (int r=0; r<rows; r++)
            {
                for (int c=0; c<columns; c++)
                {
                    grid[r, c] = new MapNode();
                }
            }

            for (int r=0; r<rows; r++)
            {
                for (int c=0; c<columns; c++)
                {
                    if (r > 0) grid[r, c].Up = grid[r-1, c];
                    if (r < rows-1) grid[r, c].Down = grid[r+1, c];
                    if (c > 0) grid[r, c].Left = grid[r, c - 1];
                    if (c < columns-1) grid[r, c].Right = grid[r, c + 1];
                }
            }
            Head = grid[0, 0];
        }

        public MapNode? GetMapNode(int row, int column)
        {
            if (row<0 || row>=this.rows || column<0 || column>=this.columns) return null;

            MapNode? current = Head;

            for (int r=0; r<row && current!=null; r++)
            {
                current = current.Down;
            }

            for (int c=0; c<column && current!=null; c++)
            {
                current = current.Right;
            }
            return current;
        }

        public int countSymbol(string s)
        {
            int count = 0;
            MapNode? currentRow = Head;

            while (currentRow != null)
            {
                MapNode? currentColumn = currentRow;
                while (currentColumn != null)
                {
                    if (currentColumn.getSymbol() == s && currentColumn.getState())
                    {
                        count++;
                    }
                    currentColumn = currentColumn.Right;
                }
                currentRow = currentRow.Down;
            }

            return count;
        }

        public MapNode? findSymbol(string s)
        {
            MapNode? currentRow = Head;

            while (currentRow != null)
            {
                MapNode? currentColumn = currentRow;
                while (currentColumn != null)
                {
                    if (currentColumn.getSymbol() == s && currentColumn.getState())
                    {
                        return currentColumn;
                    }
                    currentColumn = currentColumn.Right;
                }
                currentRow = currentRow.Down;
            }

            return null;
        }

        public int[] GetNodeCoordinates(MapNode node)
        {
            MapNode? currentRow = Head;
            int r = 0;

            while (currentRow != null)
            {
                MapNode? currentColumn = currentRow;
                int c = 0;

                while (currentColumn != null)
                {
                    if (currentColumn == node)
                    {
                        return new int[] { r+1, c+1 };
                    }
                    currentColumn = currentColumn.Right;
                    c++;
                }

                currentRow = currentRow.Down;
                r++;
            }

            return new int[] { 0, 0 };
        }

        public bool AddValue(int row, int column, int type, string symbol)
        {
            MapNode? target = GetMapNode(row, column);
            
            if (target==null)
            {
                Console.WriteLine($"Error: La posicion ({row}, {column}) no existe.");
                return false;
            }

            target.setNodeType(type);
            target.setSymbol(symbol);
            return true;
        }
////////////////////////////////////////////////////////////////////////
        public MapNode[] GetEntryNodes()
        {
            int entriesCount = countSymbol("E");

            MapNode[] entryNodes = new MapNode[entriesCount];
            int index = 0;

            MapNode? currentRow = Head;

            while (currentRow != null)
            {
                MapNode? currentColumn = currentRow;
                while (currentColumn != null)
                {
                    if (currentColumn.getSymbol() == "E" && currentColumn.getState())
                    {
                        entryNodes[index] = currentColumn;
                        index++;
                    }
                    currentColumn = currentColumn.Right;
                }
                currentRow = currentRow.Down;
            }
            return entryNodes;
        }

        public void ShowConsoleMap()
        {
            MapNode? rowStart = Head;

            while (rowStart!=null)
            {
                MapNode? current = rowStart;
                while (current!=null)
                {
                    Console.Write($"{current.getSymbol()}".PadRight(2));
                    current = current.Right;
                }

                Console.WriteLine();
                rowStart = rowStart.Down;
            }
        }
    }
}