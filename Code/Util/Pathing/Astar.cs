using System;
using System.Collections.Generic;
using System.Text;


namespace L2_login
{
   public class Astar
    {
       private System.Collections.ArrayList _nodelist =  new System.Collections.ArrayList();
       private System.Collections.ArrayList _pathPoints = new System.Collections.ArrayList();
       private AstarNode _targetNode = null;
       private AstarNode _startNode = null;
       private System.Collections.ArrayList _pathNodes = new System.Collections.ArrayList();


       private readonly object startNodeLock = new object();
       private readonly object targetNodeLock = new object();
       private readonly object nodelistLock = new object();
       private readonly object pathPointsLock = new object();
       private readonly object pathNodesLock = new object();

       //50 is just slightly bigger than a player character.. so its a good size to use.
       private int gridSize = 50;
       
       //Used for calculating G values
       private int HorizontalCost = 10;
       private int DiagonalCost = 5;


       private const int NORTH = 0;
       private const int SOUTH = 1;
       private const int EAST = 2;
       private const int WEST = 3;
       private const int NORTHWEST = 4;
       private const int SOUTHWEST = 5;
       private const int NORTHEAST = 6;
       private const int SOUTHEAST = 7;

       private const int INFINITY = 9999999;


       public System.Collections.ArrayList pathPoints
       {
           get
           {
               System.Collections.ArrayList tmp;
               lock (pathPointsLock)
               {
                   tmp = this._pathPoints;
               }
               return tmp;
           }
           set
           {
               lock (pathPointsLock)
               {
                   _pathPoints = value;
               }
           }
       }

       public System.Collections.ArrayList nodelist
       {
           get
           {
               System.Collections.ArrayList tmp;
               lock (nodelistLock)
               {
                   tmp = this._nodelist;
               }
               return tmp;
           }
           set
           {
               lock (nodelistLock)
               {
                   _nodelist = value;
               }
           }
       }

       public AstarNode targetNode
       {
           get
           {
               AstarNode tmp;
               lock (targetNodeLock)
               {
                   tmp = this._targetNode;
               }
               return tmp;
           }
           set
           {
               lock (targetNodeLock)
               {
                   _targetNode = value;
               }
           }
       }

       public AstarNode startNode
       {
           get
           {
               AstarNode tmp;
               lock (startNodeLock)
               {
                   tmp = this._startNode;
               }
               return tmp;
           }
           set
           {
               lock (startNodeLock)
               {
                   _startNode = value;
               }
           }
       }

       public System.Collections.ArrayList pathNodes
       {
           get
           {
               System.Collections.ArrayList tmp;
               lock (pathNodesLock)
               {
                   tmp = this._pathNodes;
               }
               return tmp;
           }
           set
           {
               lock (pathNodesLock)
               {
                   _pathNodes = value;
               }
           }
       }


       public AstarNode findStartNode()
       {
           startNode = allocNode(Globals.gamedata.my_char.X, Globals.gamedata.my_char.Y, 0, 0);
           #if DEBUG
           Globals.l2net_home.Add_Debug("Found Start Node:" + startNode);
           #endif

           return startNode;
        }
      
       private bool isNodeTarget(AstarNode node, double targetX, double targetY)
       {
           if (targetX >= node.x)
           {
               if (targetX <= node.x2)
               {
                   if (targetY >= node.y)
                   {
                       if (targetY <= node.y2)
                       {
                   
                           return true;
                       }
                   }
               }
           }
           return false;
       }

       private int getTargetDirection(AstarNode node, double targetX, double targetY)
       {
           int direction;

           if (targetX >= node.x)
           {
               if (targetX <= node.x2)
               {
                   //in correct x ... let next section modify direction...
                   direction = -1;
               }
               else
               {
                   direction = EAST;
               }
           }
           else
           {
               direction = WEST;
           }

           if (targetY >= node.y)
           {
               if (targetY <= node.y2)
               {
                   //do nothing... we are in correct Y direction
               }
               else
               {
                   direction = SOUTH;
               }
           }
           else
           {
               direction = NORTH;
           }


           //this shouldn't happen
         //  Globals.l2net_home.Add_Debug("x:"+node.x+" tx:"+targetX+" x2:"+node.x2+" y:"+node.y+" ty:"+targetY+" y2:"+node.y2);
           return direction;
      
       }

       public bool findTargetNode(double x, double y)
       {
          
           //Generate nodes until we reach target, standard manhattan distance used to find it.
                
           //check if the current generated node is the target node.
           //determine quadrant in which the target lies
           //generate the next node by using the next quadrant


           AstarNode currentNode = startNode;
           int direction;

           while (true) //loop until we return...
           {
               if (isNodeTarget(currentNode, x, y))
               {
                   targetNode = currentNode;
#if DEBUG
                   Globals.l2net_home.Add_Debug("Found Target Node:" + currentNode.xpos + " , " + currentNode.ypos);
#endif
                   nodelist.Clear();
                   nodelist.Add(targetNode);
                   return true;
               }
               direction = getTargetDirection(currentNode, x, y);
               if (direction < 0)
               {
                   nodelist.Clear();
                   return false;
               }
               currentNode = buildNode(currentNode, direction);

           }

       }

       public void findAdjacentNodes(AstarNode node)
       {
           AstarNode tmpNodePtr;
           if (node.adjacentNodes.Count != 0)
               return; //we already have found our adjacent nodes...
           //horizontal nodes
           //north
           tmpNodePtr = getNode(node.xpos, node.ypos - 1);
           if (tmpNodePtr == null)
               tmpNodePtr = buildNode(node, NORTH);
           else
               tmpNodePtr.ivalue += 1;

           tmpNodePtr.parent = node;
           node.adjacentNodes.Add(tmpNodePtr);

           //south
           tmpNodePtr = getNode(node.xpos, node.ypos + 1);
           if (tmpNodePtr == null)
               tmpNodePtr = buildNode(node, SOUTH);
           else
               tmpNodePtr.ivalue += 1;
           tmpNodePtr.parent = node;
           node.adjacentNodes.Add(tmpNodePtr);

           //east
           tmpNodePtr = getNode(node.xpos + 1, node.ypos);
           if (tmpNodePtr == null)
               tmpNodePtr = buildNode(node, EAST);
           else
               tmpNodePtr.ivalue += 1;
           tmpNodePtr.parent = node;
           node.adjacentNodes.Add(tmpNodePtr);

           //west
           tmpNodePtr = getNode(node.xpos - 1, node.ypos);
           if (tmpNodePtr == null)
               tmpNodePtr = buildNode(node, WEST);
           else
               tmpNodePtr.ivalue += 1;
           tmpNodePtr.parent = node;
           node.adjacentNodes.Add(tmpNodePtr);

           //Diagonal nodes
           //nw
           tmpNodePtr = getNode(node.xpos - 1, node.ypos - 1);
           if (tmpNodePtr == null)
               tmpNodePtr = buildNode(node, NORTHWEST);
           else
               tmpNodePtr.ivalue += 1;
           tmpNodePtr.parent = node;
           tmpNodePtr.diagonal = true;
           node.adjacentNodes.Add(tmpNodePtr);
           //sw
           tmpNodePtr = getNode(node.xpos - 1, node.ypos + 1);
           if (tmpNodePtr == null)
               tmpNodePtr = buildNode(node, SOUTHWEST);
           else
               tmpNodePtr.ivalue += 1;
           tmpNodePtr.diagonal = true;
           node.adjacentNodes.Add(tmpNodePtr);
           //ne
           tmpNodePtr = getNode(node.xpos + 1, node.ypos - 1);
           if (tmpNodePtr == null)
               tmpNodePtr = buildNode(node, NORTHEAST);
           else
               tmpNodePtr.ivalue += 1;
           tmpNodePtr.diagonal = true;
           tmpNodePtr.parent = node;
           node.adjacentNodes.Add(tmpNodePtr);
           //se
           tmpNodePtr = getNode(node.xpos + 1, node.ypos + 1);
           if (tmpNodePtr == null)
               tmpNodePtr = buildNode(node, SOUTHEAST);
           else
               tmpNodePtr.ivalue += 1;
           tmpNodePtr.diagonal = true;
           tmpNodePtr.parent = node;
           node.adjacentNodes.Add(tmpNodePtr);
       }


       public void expand(AstarNode parent)
       {
           AstarNode tmpNode;

           tmpNode = buildNode(parent, NORTH);
           tmpNode.parent = parent;
          //parent.adjacentNodes.Add(tmpNode);
           insertSorted(parent.adjacentNodes, tmpNode);

           tmpNode = buildNode(parent, SOUTH);
           tmpNode.parent = parent;
          // parent.adjacentNodes.Add(tmpNode);
           insertSorted(parent.adjacentNodes, tmpNode);

           tmpNode = buildNode(parent, EAST);
           tmpNode.parent = parent;
          // parent.adjacentNodes.Add(tmpNode);
           insertSorted(parent.adjacentNodes, tmpNode);

           tmpNode = buildNode(parent, WEST);
           tmpNode.parent = parent;
          // parent.adjacentNodes.Add(tmpNode);
           insertSorted(parent.adjacentNodes, tmpNode);

           tmpNode = buildNode(parent, NORTHWEST);
           tmpNode.parent = parent;
           tmpNode.diagonal = true;
          // parent.adjacentNodes.Add(tmpNode);
           insertSorted(parent.adjacentNodes, tmpNode);

           tmpNode = buildNode(parent, SOUTHWEST);
           tmpNode.parent = parent;
           tmpNode.diagonal = true;
           //parent.adjacentNodes.Add(tmpNode);
           insertSorted(parent.adjacentNodes, tmpNode);

           tmpNode = buildNode(parent, NORTHEAST);
           tmpNode.parent = parent;
           tmpNode.diagonal = true;
          // parent.adjacentNodes.Add(tmpNode);
           insertSorted(parent.adjacentNodes, tmpNode);

           tmpNode = buildNode(parent, SOUTHEAST);
           tmpNode.parent = parent;
           tmpNode.diagonal = true;
          // parent.adjacentNodes.Add(tmpNode);
           insertSorted(parent.adjacentNodes, tmpNode);

       }


       public void printPath()
       {
#if DEBUG

           Globals.l2net_home.Add_Debug("Path:");
#endif
           for (int i = 0; i < pathNodes.Count; i++)
           {
               Globals.l2net_home.Add_Debug(i+":"+((AstarNode)pathNodes[i]));
           }
           
       }

       public void printPathPoints()
       {
#if DEBUG
           Globals.l2net_home.Add_Debug("Path Points:");
#endif
           for (int i = 0; i < pathPoints.Count; i++)
           {
               Globals.l2net_home.Add_Debug(((Point)pathPoints[i]).X + "," + ((Point)pathPoints[i]).Y);
           }
       }
       
       
       // Builds an arraylist of point objects to send to move_smart
       //  Sets points based on centers of nodes.
       public void buildPathPoints()
       {
           for (int i = 0; i < pathNodes.Count; i++)
           {
               Point tempPoint = new Point();
               tempPoint.X = (float)((AstarNode)pathNodes[i]).cx;
               tempPoint.Y = (float)((AstarNode)pathNodes[i]).cy;
               pathPoints.Add(tempPoint);
           }
       }

       /* Here we are going to check for walls and see if they intersect any of our 
        * nodes, if they do we mark that node as unpassable so we can move around it
        * We are going to check if either of the endpoints inside the node's box, if so
        * then the wall is colliding. Next we are going to check for vertical collision on
        * the vertical sides of the box, and horizontal collision on the horizontal sides
        * of the box. If they collide then we set the current node to unpassable.
        * 
        * This function returns FALSE if the node is NOT PASSABLE. TRUE if it IS
        * 
        */
       public bool checkNodeWalls(AstarNode node)
       {
           for (int i = 0; i < Globals.gamedata.Walls.Count; i++)
           {
               // Globals.l2net_home.Add_Debug("Checking node:" + j);
               //west wall
               if (isIntersecting(node.x, node.y,node.x, node.y2,
                                 ((Wall)Globals.gamedata.Walls[i]).P1.X,
                                 ((Wall)Globals.gamedata.Walls[i]).P1.Y,
                                 ((Wall)Globals.gamedata.Walls[i]).P2.X,
                                 ((Wall)Globals.gamedata.Walls[i]).P2.Y))
               {
                   return false;
                   // Globals.l2net_home.Add_Debug("found intersetction west!");

               }
               //east wall
               else if (isIntersecting(node.x2, node.y,node.x2, node.y2,
                                 ((Wall)Globals.gamedata.Walls[i]).P1.X,
                                 ((Wall)Globals.gamedata.Walls[i]).P1.Y,
                                 ((Wall)Globals.gamedata.Walls[i]).P2.X,
                                 ((Wall)Globals.gamedata.Walls[i]).P2.Y))
               {
                   return false;
                   //  Globals.l2net_home.Add_Debug("found intersetction east!");

               }
               //north wall
               else if (isIntersecting(node.x, node.y,node.x2, node.y,
                                 ((Wall)Globals.gamedata.Walls[i]).P1.X,
                                 ((Wall)Globals.gamedata.Walls[i]).P1.Y,
                                 ((Wall)Globals.gamedata.Walls[i]).P2.X,
                                 ((Wall)Globals.gamedata.Walls[i]).P2.Y))
               {
                   return false;
                   // Globals.l2net_home.Add_Debug("found intersetction! north");

               }

               //south wall
               else if (isIntersecting(node.x, node.y2,node.x2, node.y2,
                                 ((Wall)Globals.gamedata.Walls[i]).P1.X,
                                 ((Wall)Globals.gamedata.Walls[i]).P1.Y,
                                 ((Wall)Globals.gamedata.Walls[i]).P2.X,
                                 ((Wall)Globals.gamedata.Walls[i]).P2.Y))
               {
                   return false;
                   //   Globals.l2net_home.Add_Debug("found intersetction! south");


               }
           }
           return true;
       }
     
       private bool isIntersecting(double Line1x, double Line1y, double Line1x2, double Line1y2, double Line2x, double Line2y, double Line2x2, double Line2y2)
       {


           bool intersection = false;
           double ua = (Line2x2 - Line2x) * (Line1y - Line2y) - (Line2y2 - Line2y) * (Line1x - Line2x);
           double ub = (Line1x2 - Line1x) * (Line1y - Line2y) - (Line1y2 - Line1y) * (Line1x - Line2x);
           double denominator = (Line2y2 - Line2y) * (Line1x2 - Line1x) - (Line2x2 - Line2x) * (Line1y2 - Line1y);

           if (Math.Abs(denominator) <= 0.00001f)
           {
               if (Math.Abs(ua) <= 0.00001f && Math.Abs(ub) <= 0.00001f)
               {
                   //lines are coincident
                   intersection = true;
               }
           }
           else
           {
               ua /= denominator;
               ub /= denominator;
               if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1)
               {
                   intersection = true;
               }
           }

            return intersection;
       }

       private bool checkPathForNode(AstarNode node)
       {
           for (int i = 0; i < pathNodes.Count; i++)
           {
               if (((AstarNode)pathNodes[i]).xpos == node.xpos && ((AstarNode)pathNodes[i]).ypos == node.ypos)
                   return true;
           }
           return false;
       }

       /*
        * Returns true if the supplied node is an adjacent node on the current path list
        */
       private bool checkPathAdjacentNode(AstarNode node)
       {
           for(int i = 0; i < pathNodes.Count-1; i++)
           {
               for(int j = 0; j < ((AstarNode)pathNodes[i]).adjacentNodes.Count; j++)
               {
                   if (isNodeTarget(((AstarNode)((AstarNode)pathNodes[i]).adjacentNodes[j]), targetNode.x, targetNode.y))
                   {
#if DEBUG
                       Globals.l2net_home.Add_Debug("Found target node as adjacent to path...");
#endif

                       return true;
                   }
                   if(((AstarNode)((AstarNode)pathNodes[i]).adjacentNodes[j]).xpos == node.xpos &&
                      ((AstarNode)((AstarNode)pathNodes[i]).adjacentNodes[j]).ypos == node.ypos)
                   return true;
               }
           }
           return false;
       }

    
        /*Iterative Deepining Search A*
         * This function performs a basic depth first search with a depth limit provided by the minimum of 
         * the costs of the potential siblings. Additionally it orders the children it searchs in best first
         * order. It has linear space and memory complexity. */
       public bool IDAStar(AstarNode node)
       {
           int bound = 50;
           while (bound > 0)
           {
#if DEBUG
               Globals.l2net_home.Add_Debug("updated bound:" + bound);
#endif
               bound = DFS(node, bound);
             //  if (bound == INFINITY)
                 //  return false;
               //DFS(node, bound);
               //bound += HorizontalCost;
               
           }
           return true;
       }

       /* Depth First Search Function */
       private int DFS(AstarNode node, int bound)
       {
           node.fvalue = calcFvalue(node);
       
           if (node.fvalue > bound)
               return node.fvalue;

           pathNodes.Add(node);
           Globals.debugPath = pathNodes;

           if (calcHvalue(node) == 0)
               return 0; //Goal found?
           if (isNodeTarget(node, targetNode.x, targetNode.y))
               return 0; //goal?!
           
           //get children
           node.adjacentNodes.Clear();
           //expand(node);
           findAdjacentNodes(node); //we might be able to get away with this instead...
           
           //calc F value for each child
           foreach (AstarNode n in node.adjacentNodes)
               n.fvalue = calcFvalue(n);
           //sort adjacent nodes so we have a best first search ....
          // node.adjacentNodes.Sort();


       
           int min = INFINITY;
         // AstarNode minNode;
           foreach (AstarNode n in node.adjacentNodes)
           {
               // ignore child if its a wall
               if (n.passable)
               {
                   //also ignore child if it is already on our path, or if its a child of a node on our path.
                   //note, these functions check xpos and ypos NOT node objects.
                  if (!checkPathForNode(n) && !checkPathAdjacentNode(n))
                 // if(!checkPathForNode(n))
                  {
                       int temp;
                       temp = DFS(n, bound);
                       if (temp == 0)
                           return 0; //we wont get any lower than 0
                       if (temp == INFINITY)
                       {
                           //This child has no children worth looking at...
                       }

                       if (min > temp)
                       {
                          // Globals.debugNode = n;
                           min = temp;
                       }

                    /*   if (temp != INFINITY && min == temp)
                       {
                           Globals.debugNode2 = n;
                           Globals.l2net_home.Add_Debug("n1(" + min + "):" + Globals.debugNode + " n2(" + temp + "):" + Globals.debugNode2);
                           System.Threading.Thread.Sleep(2000);
                           //return 0;
                       }
                       else
                           System.Threading.Thread.Sleep(200);*/
                  }
               }
           }
           if (min == INFINITY)
           {
             //  Globals.debugNode3 = node;
             //  System.Threading.Thread.Sleep(1000);
             //  Globals.l2net_home.Add_Debug("WTF");
           }
           else if (min >= bound)
           {
               pathNodes.Remove(node);
               return bound;
           }
           pathNodes.Remove(node);
           return min;
       }


       /* Standard A* */
       public bool Astar_start(AstarNode startNode, AstarNode targetNode)
       {
           System.Collections.ArrayList closedlist = new System.Collections.ArrayList();
           System.Collections.ArrayList openlist = new System.Collections.ArrayList();

           closedlist.Clear();
           openlist.Clear();
           startNode.fvalue = calcFvalue(startNode); //this sets g and h

           openlist.Add(startNode);

           while (openlist.Count > 0)
           {
               Globals.debugPath = pathNodes;
               AstarNode xNode;
               openlist.Sort();
               xNode = ((AstarNode)openlist[0]);
               if (xNode == targetNode)
               {
                   pathNodes.Add(xNode);
                   return true;
               }
               openlist.Remove(xNode);
               closedlist.Add(xNode);
               pathNodes.Add(xNode);
               findAdjacentNodes(xNode);

               foreach (AstarNode yNode in xNode.adjacentNodes)
               {
                   if (closedlist.Contains(yNode))
                       continue;
                   int tempGScore;
                   bool isBetter = false;

                   //todo add diagonals.
                   tempGScore = xNode.gvalue + HorizontalCost;
                   yNode.fvalue = calcFvalue(yNode);

                   if (openlist.Contains(yNode))
                   {
                       openlist.Add(yNode);
                       isBetter = true;
                   }
                   else if (tempGScore < yNode.gvalue)
                       isBetter = true;

                   else
                       isBetter = false;
                   if (isBetter)
                   {
                       yNode.gvalue = tempGScore;
                       yNode.hvalue = calcHvalue(yNode);
                       yNode.fvalue = yNode.gvalue + yNode.hvalue;
                   }
               }      
           }
           return false;
       }

       /* Fringe Search 
        * Fringe search is a memory enchanced version of IDA* */
       public bool fringeSearch()
       {
           //initialize:
           System.Collections.ArrayList nowList = new System.Collections.ArrayList();
           System.Collections.ArrayList laterList = new System.Collections.ArrayList();
           System.Collections.ArrayList rejectedList = new System.Collections.ArrayList();
           int limit = calcHvalue(startNode);
           
           #if DEBUG
                Globals.l2net_home.Add_Debug("start limit:" + limit);
           #endif

           bool found = false;

           Globals.debugPath = nowList;
           nowList.Add(startNode);
           
           while (!found)
           {
              // Globals.l2net_home.Add_Debug("big loop...");
               
               int fmin = INFINITY;
               while (nowList.Count != 0)
               {
                   AstarNode head = ((AstarNode)nowList[0]);
                   head.fvalue = calcFvalue(head);
                   

                   #region check for goal
                   if (isNodeTarget(head, targetNode.x, targetNode.y))
                   {
                       found = true;
                       break;
                   }
                   #endregion

                   //check if head is over the limit
                   if (head.fvalue > limit)
                   {
                       
                       //transfer head from nowlist to laterlist.
                       nowList.Remove(head);
                       laterList.Add(head);

                       //find the minimum of the nodes we will look at 'later'
                       fmin = Util.MIN(fmin, head.fvalue);

                      
                   }           
                   else
                   {
                       #region expand head's children on to now list
                       expand(head); //nodes are sorted by insert sort in this function

                       bool addedChildren = false;
                       foreach (AstarNode child in head.adjacentNodes)
                       {
                           //dont allow children already on path or adjacent to path or walls...

                           if (!isNodeIn(nowList, child) && !isNodeIn(laterList, child) && !isNodeIn(rejectedList, child))
                           {
                               if (child.passable == true)
                               {
                                   //add child of head to front of nowlist 
                                   nowList.Insert(0, child);
                                   addedChildren = true;
                               }
                           }

                       }
                       if (!addedChildren)
                       {                        
                           nowList.Remove(head);
                           rejectedList.Add(head);

                       }
                       #endregion
                   }

               }
               if (found == true)
                   break;

               //set new limit
              // Globals.l2net_home.Add_Debug("new limit:" + fmin);
               limit = fmin;

               //set now list to later list.
               nowList = (System.Collections.ArrayList)laterList.Clone();
               nowList.Sort();
               Globals.debugPath = nowList;
               laterList.Clear();

           
           }

           if (found == true)
           {
#if DEBUG
               Globals.l2net_home.Add_Debug("found a path... building...");
#endif
               buildPathFromParents(((AstarNode)nowList[0]));
               return true;
           }

           return false;

       }

       private void buildPathFromParents(AstarNode goal)
       {
#if DEBUG
           Globals.l2net_home.Add_Debug("getting path...");
#endif
           AstarNode tempNode = goal;
           pathNodes.Clear();
           Globals.debugPath = pathNodes;
           while (tempNode.parent != null)
           {
               pathNodes.Add(tempNode);
               tempNode = tempNode.parent;
           }
           pathNodes.Reverse();

           return;
       }

       private void insertSorted(System.Collections.ArrayList list, AstarNode node)
       {
           node.fvalue = calcFvalue(node);
          // bool inserted = false;
           int i = 0;

           if (list.Count == 0)
               list.Add(node);
           else
           {

               for (i = 0; i < list.Count; i++)
               {
                   if (node.fvalue > ((AstarNode)list[i]).fvalue)
                   {
                       break;                      
                   }

               }
               list.Insert(i, node);
           }
       }

       private bool isNodeIn(System.Collections.ArrayList list, AstarNode node)
       {
           foreach (AstarNode n in list)
           {
               if (n.xpos == node.xpos && n.ypos == node.ypos)
                   return true;
           }
           return false;

       }





       /*
        * Calculates the total cost of a node. It computes the total cost as the cost determine by 
        * the heuristic(calcHvalue) and the cost of the current path thus far(calcGvalue)
        */
       public int calcFvalue(AstarNode node)
       {
           int ftemp;

           ftemp = calcGvalue(node) + (calcHvalue(node));
           return ftemp;
       }

       /*
       * H-value stand for heuristic and is a 'guess' to how far away
       * we are from the target. It is calculated by counting the number
       * of squares we are away from the target in both the X and Y direction
        * not counting diagonal directions. Then multiplying by our horizontal
        * move cost(in this case its 10)
       */
       public int calcHvalue(AstarNode node)
       {

           int tempX, tempY, tempH, moveCount = 0;


           tempX = node.xpos;
           while (targetNode.xpos != tempX)
           {
               if (targetNode.xpos > tempX)  //test for right side of target node
                   tempX++;
               if (targetNode.xpos < tempX) //test for left side of target node
                   tempX--;
               moveCount++;
           }
           
           tempY = node.ypos;
           while(targetNode.ypos != tempY)
           {
               if(targetNode.ypos > tempY)
                   tempY++;
               if(targetNode.ypos < tempY)
                   tempY--;
               moveCount++;
           }

           tempH = moveCount * HorizontalCost;
           node.hvalue = tempH;

           return tempH;
       }

       /* G-value is the cost of moving from the starting node to its current location
        * it includes diagonal and horizontal movement. It is calculated by adding horizontalCost
        * or diagonalcost to it's parent's G-value. Each node has a 'parent' node that will
        * eventually point back to the starting location */
       public int calcGvalue(AstarNode node)
       {
           if (node.getParent() == null)
           {
               node.gvalue = 0;
               return 0;
           }
           else
           {
               if(node.diagonal)
                   node.gvalue = node.getParent().gvalue + DiagonalCost;
               else
                   node.gvalue = (node.getParent().gvalue + (HorizontalCost));
               return node.gvalue;
           }
          
       }


    
       /*
        * This function trims the path supplied by the above pathfinding algorithms.
        */
       public void trimPath()
       {
           AstarNode CurrentNode;
           AstarNode NextNode;
           int LastMoveDirection = -1;
           int moveDirection = -1;
           System.Collections.ArrayList newpath = new System.Collections.ArrayList();
           for (int i = 0; i < (pathNodes.Count-1); i++)
           {
               CurrentNode = ((AstarNode)pathNodes[i]);
               NextNode = ((AstarNode)pathNodes[i + 1]);
               moveDirection = findMoveDirecton(CurrentNode, NextNode);
               if (LastMoveDirection == -1 || moveDirection == LastMoveDirection)
               {
                   //do nothing just contine
               }
               else
               {
                   //add current node to newpath
                   newpath.Add(CurrentNode);
               }
               //save old move direction
               LastMoveDirection = moveDirection;
           }
           //add the target point to the newpath
           if(pathNodes.Count > 0)
                newpath.Add(pathNodes[pathNodes.Count - 1]);

           pathNodes = newpath;
           Globals.debugPath = pathNodes;

       }

       private int findMoveDirecton(AstarNode cur, AstarNode next)
       {
           int direction;

           if (cur.xpos > next.xpos)
           {
               if (cur.ypos > next.ypos)
                   direction = NORTHWEST;
               else if (cur.ypos < next.ypos)
                   direction = SOUTHWEST;
               else
                   direction = WEST;
           }
           else if (cur.xpos < next.xpos)
           {
               if (cur.ypos > next.ypos)
                   direction = NORTHEAST;
               else if (cur.ypos < next.ypos)
                   direction = SOUTHEAST;
               else
                   direction = EAST;
           }
           else
           {
               if (cur.ypos > next.ypos)
                   direction = NORTH;
               else if (cur.ypos < next.ypos)
                   direction = SOUTH;
               else
                   direction = -1; //same node?
           }
           return direction;
       }

       public AstarNode getNode(int xpos, int ypos)
       {
           for (int i = 0; i < _nodelist.Count; i++)
           {
               if (((AstarNode)_nodelist[i]).xpos == xpos && ((AstarNode)_nodelist[i]).ypos == ypos)
                   return ((AstarNode)_nodelist[i]);
           }

           return null;
       }

       public AstarNode allocNode(double X, double Y, int Xpos, int Ypos)
       {
           try
           {

               AstarNode tmpNode = new AstarNode();
               tmpNode.x = X;
               tmpNode.y = Y;
               tmpNode.x2 = X + this.gridSize;
               tmpNode.y2 = Y + this.gridSize;
               tmpNode.xpos = Xpos;
               tmpNode.ypos = Ypos;

               tmpNode.passable = checkNodeWalls(tmpNode);

               tmpNode.calcMidPoint();
              // nodelist.Add(tmpNode); nodelist is now only useful for debugging...
               return tmpNode;
           }
           catch(Exception e)
           {
               Globals.l2net_home.Add_Error("ERROR:" + e.Message);              
           }
           return null;

       }

       public AstarNode buildNode(AstarNode parent, int direction)
       {
           AstarNode tmpNode;
           switch (direction)
           {
               case NORTH:
                   tmpNode = allocNode(parent.x, parent.y - gridSize, parent.xpos, parent.ypos - 1);
                   break;
               case SOUTH:
                   tmpNode = allocNode(parent.x, parent.y + gridSize, parent.xpos, parent.ypos + 1);
                   break;
               case EAST:
                   tmpNode = allocNode(parent.x + gridSize, parent.y, parent.xpos + 1, parent.ypos);
                   break;
               case WEST:
                   tmpNode = allocNode(parent.x - gridSize, parent.y, parent.xpos - 1, parent.ypos);
                   break;
               case NORTHEAST:
                   tmpNode = allocNode(parent.x + gridSize, parent.y - gridSize, parent.xpos + 1, parent.ypos - 1);
                   break;
               case NORTHWEST:
                   tmpNode = allocNode(parent.x - gridSize, parent.y - gridSize, parent.xpos - 1, parent.ypos - 1);
                   break;
               case SOUTHEAST:
                   tmpNode = allocNode(parent.x + gridSize, parent.y + gridSize, parent.xpos + 1, parent.ypos + 1);
                   break;
               case SOUTHWEST:
                   tmpNode = allocNode(parent.x - gridSize, parent.y + gridSize, parent.xpos - 1, parent.ypos + 1);
                   break;
               default:
                   tmpNode = null;
                   break;
           }
           return tmpNode;
       }

       public void clean()
       {
           pathNodes.Clear();
           pathPoints.Clear();
           nodelist.Clear();
           targetNode = null;
           startNode = null;
       }

    }
}
