using System;

namespace ChapinWarriors
{
    public class PathNode
    {
        private int p_row;
        private int p_col;
        private bool state;
        private PathNode? parent;

        public PathNode? Up { get; set; }
        public PathNode? Down { get; set; }
        public PathNode? Left { get; set; }
        public PathNode? Right { get; set; } 
        
        public PathNode(int p_row, int p_col, PathNode? parent = null)
        {
            this.p_row = p_row;
            this.p_col = p_col;
            this.state = true;
            this.parent = parent;
            this.Up = null;
            this.Down = null;
            this.Left = null;
            this.Right = null;
        }

        public int getP_row() { return this.p_row; }
        public int getP_col() { return this.p_col; }
        public bool getState() { return this.state; }
        public PathNode? getParent() { return this.parent; }

        public void setP_row(int p_row) { this.p_row = p_row; }
        public void setP_col(int p_col) { this.p_col = p_col; }
        public void setState(bool state) { this.state = state; }
        public void setParent(PathNode? parent) { this.parent = parent; }
    }

    public class DragPath
    {
        private PathNode? root;

        public PathNode? getRoot() { return this.root; }
        public void setRoot(PathNode? root) { this.root = root; }

        public DragPath(int rootRow, int rootCol)
        {
            this.root = new PathNode(rootRow, rootCol, null);
        }

        public PathNode? addStep(PathNode parent, int row, int col, string lado)
        {
            if (parent == null) return null;

            PathNode newNode = new PathNode(row, col, parent);
            switch (lado)
            {
                case "u": 
                    parent.Up = newNode;
                    newNode.Down = parent;
                    break;
                case "d": 
                    parent.Down = newNode;
                    newNode.Up = parent;
                    break;
                case "l": 
                    parent.Left = newNode;
                    newNode.Right = parent;
                    break;
                case "r": 
                    parent.Right = newNode;
                    newNode.Left = parent;
                    break;
                default:
                    return null;
            }

            newNode.setParent(parent);
            return newNode;
        }
        
    }
}