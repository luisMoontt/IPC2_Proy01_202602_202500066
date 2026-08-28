using System;

namespace ChapinWarriors
{
    public class PathNode
    {
        private int p_row;
        private int p_col;
        private bool state;
        private PathNode? parent;
        
        public PathNode(int p_row, int p_col, PathNode? parent = null)
        {
            this.p_row = p_row;
            this.p_col = p_col;
            this.state = true;
            Parent = parent;
        }

        public int getP_row(){ return this.p_row; }
        public int getP_col(){ return this.p_col; }
        public bool getState(){ return this.state; }
        public PathNode? getParent(){ return this.parent; }

        public void setP_row(int p_row){ this.p_row=p_row; }
        public void setP_col(int p_col){ this.p_col=p_col; }
        public void setState(bool state){ this.state=state; }
        public void setParent(PathNode parent){ this.parent=parent; }
    }

    public class DragPath
    {
        private PathNode? root;

        public PathNode? getRoot(){ return this.root; }
        public void setRoot(PathNode root){ this.root=root; }

        public DragPath(int rootRow, int rootCol)
        {
            this.root = new PathNode(int rootRow, int rootCol, null);
        }

        public PathNode addStep(PathNode p_node, int row, int col)
        {
            PathNode newNode = new PathNode(row, col, pNode);
            return newNode;
        }

        public void PrintToRoot(PathNode startNode)
        {
            PathNode? current = startNode;
            Console.Write("Recorrido (Nodo -> Raíz): ");

            while (current != null)
            {
                Console.Write($"[{current.Row}, {current.Column}]");

                if (current.Parent != null)
                {
                    Console.Write(" -> ");
                }

                current = current.Parent;
            }

            Console.WriteLine();
        }   
    }
}