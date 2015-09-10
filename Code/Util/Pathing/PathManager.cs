using System;
using System.Collections;

/* The path manager will be an interface between the Astar pathfinding algorithm 
 * and the game packets required to actually move the player from point A to point
 * B. This manager should request a list of AstarNodes from the Astar object then
 * smooth out the movements to create a simple list of MoveTo packets.
 * The pathmanager should take the list of AstarNodes and send the moveto packets
 * at the proper times so that the player will follow the path correctly. */

 

//Lets quickly just create a few new script commands to get this to work.
//      SET_TARGETING PATHFINDING 1 - Turns on pathfinding for the following commands
//          ATTACK_TARGET, TARGET, TARGET_NEAREST_X, TALK_TARGET
// We will not override the old MOVE_TO command but instead provide another one
//      MOVE_PATH [int] [int] - Attempts to move to location int1, int2 via 
//          the pathfinding algorithm.


namespace L2_login
{
    public class PathManager
    {
        // private path
        private ArrayList _path = new ArrayList();
        private bool _drawGrid = false;
        
        private readonly object pathLock = new object();
        private readonly object drawGridLock = new object();

        private readonly object nodelistLock = new object();
        
        public ArrayList path
        {
            get
            {
                ArrayList tmp;
                lock (pathLock)
                {
                    tmp = this._path;
                }
                return tmp;
            }
            set
            {
                lock (pathLock)
                {
                    _path = value;
                }
            }
        }

        public bool drawGrid
        {
            get
            {
                bool tmp;
                lock (drawGridLock)
                {
                    tmp = this._drawGrid;
                }
                return tmp;
            }
            set
            {
                lock (drawGridLock)
                {
                    _drawGrid = value;
                }

            }
        }


        public bool runASTAR(float targetX, float targetY)
        {
            Astar pathFinder = new Astar();
            bool pathFound = false;
            bool targetFound = false;
            cleanPath();
            try
            {
                
                pathFinder.allocNode(Globals.gamedata.my_char.X, Globals.gamedata.my_char.Y, 0, 0);
                pathFinder.findStartNode();
                targetFound = pathFinder.findTargetNode(targetX, targetY);
 
                if(targetFound)
                {
                  
                    pathFound = pathFinder.fringeSearch();
                    pathFinder.trimPath();
                    pathFinder.buildPathPoints();
                    path = pathFinder.pathPoints;
               
#if DEBUG
                    pathFinder.printPath();
#endif
                    pathFinder = null; //send A* object to garbage collector.


                }
                else
                    Globals.l2net_home.Add_Error("Could not find target node at " + targetX + "," + targetY);             
            }
            catch(Exception e)
            {
                //eh?
               Globals.l2net_home.Add_Debug(e.Message);
            }          
            return (pathFound && targetFound);

        }
       
        public void cleanPath()
        {
            this.path.Clear();            
        }
    }
}
