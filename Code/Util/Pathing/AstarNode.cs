using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public class AstarNode : IComparable
    {
        
        public double x; //upper left corner
        public double y;
        public double x2; //lower right corner
        public double y2;
        public double cx; //center x
        public double cy; //center y
        public int xpos;
        public int ypos;
        public bool passable;
        public bool diagonal;

        public int fvalue;
        public int gvalue;
        public int hvalue;
        public int ivalue; //iterative cost...

       public  AstarNode parent = null;
        
        public System.Collections.ArrayList adjacentNodes = new System.Collections.ArrayList();
        public System.Collections.ArrayList children = new System.Collections.ArrayList();

        public AstarNode()
        {
            x = 0; y = 0; x2 = 0; y2 = 0; cx = 0; cy = 0;
            xpos = 0; ypos = 0;
            fvalue = 0; gvalue = 0;
            hvalue = 0;
            passable = true;
            ivalue = 0;
            diagonal = false;
        }

         public AstarNode getParent()
        {
            return parent;
        }

        public void calcMidPoint()
        {
            cx = (x + x2) / 2;
            cy = (y + y2) / 2;
        }

        public AstarNode Clone()
        {
            AstarNode tmpNode = new AstarNode();
            tmpNode.x = this.x; //upper left corner
            tmpNode.y = this.y;
            tmpNode.x2 = this.x2;
            tmpNode.y2 = this.y2;
            tmpNode.cx = this.cx;
            tmpNode.cy = this.cy;
            tmpNode.xpos = this.xpos;
            tmpNode.ypos = this.ypos;
            tmpNode.passable = this.passable;
            tmpNode.fvalue = this.fvalue;
            tmpNode.hvalue = this.hvalue;
            tmpNode.gvalue = this.gvalue;
            tmpNode.parent = this.parent;
            tmpNode.adjacentNodes = (System.Collections.ArrayList)this.adjacentNodes.Clone();
            tmpNode.children = (System.Collections.ArrayList)this.children.Clone();
            tmpNode.diagonal = this.diagonal;

            return tmpNode;


        }

        public int CompareTo(object x)
        {
            AstarNode n1;

            if (x is AstarNode)
                n1 = x as AstarNode;
            else
                throw new ArgumentException("Object is not of type AstarNode");
                            

            if (this == n1)
                return 0;
            if (n1.hvalue == this.hvalue)
                return 0;
            if (n1.hvalue > this.hvalue)
                return -1;
          
            return 1;
      
        }

        public override string ToString()
        {
            return "[" + this.xpos + "," + this.ypos + "]";
        }

    }

    public class fringePair
    {
        public int g;
        public AstarNode parent;

        public fringePair(int gcost, AstarNode p)
        {
            this.g = gcost;
            this.parent = p;
        }

    }
}
