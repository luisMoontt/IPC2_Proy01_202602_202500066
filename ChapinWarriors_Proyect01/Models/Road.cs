using System;

namespace ChapinWarriors.Models
{
    public class Road
    {
        private int pathSize;
        private int startR;
        private int startC;

        public Road(int pathSize, int startR, int startC)
        {
            this.pathSize = pathSize;
            this.startR = startR;
            this.startC = startC;
        }

        public int GetPathSize() { return pathSize; }
        public void SetPathSize(int pathSize) {this.pathSize = pathSize;}

        public int GetStartR() { return startR; }
        public void SetStartR(int value) {this.startR = startR;}

        public int GetStartC() { return startC; }
        public void SetStartC(int value) {this.startC = startC;}
    }
}