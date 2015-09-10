using System;

namespace L2_login
{
	public class Pathing
	{
		static public System.Collections.ArrayList GetPath(Point start, Point dest)
		{
            System.Collections.ArrayList my_path;
            my_path = new System.Collections.ArrayList();
            if (Globals.gamedata.pathManager.runASTAR(dest.X, dest.Y))
            {
                my_path = Globals.gamedata.pathManager.path;
            }
            else
            {
                my_path = null;
            }

 			return my_path;
		}
	}

	public class Polygon
	{
		public System.Collections.ArrayList PointList = new System.Collections.ArrayList();

		public void ClearBorder()
		{
			PointList.Clear();
		}

		public bool IsPointInside(int x, int y)
		{
			Point np = new Point();
			np.X = x;
			np.Y = y;

			return IsPointInside(np);
		}

        public bool FindIntersect(Point start, Point end, ref Point intersect, ref int index)
        {
            bool found = false;

            double A1 = end.Y - start.Y;
            double B1 = end.X - start.X;
            double C1 = A1 * start.X + B1 * start.Y;

            double path_dist = Get_Dist(start, end);
            double i_dist = double.MaxValue;

            //find which border get intersected by our line
            //return the point of intersection and the index of the side intersected

            for (int i = 0; i < PointList.Count ; i++)
            {
                Point left = (Point)PointList[i];
                Point right = (Point)PointList[Get_Right_Point(i)];

                double A2 = right.Y - left.Y;
                double B2 = right.X - left.X;
                double C2 = A2 * left.X + B2 * left.Y;

                double det = A1 * B2 - A2 * B1;
                double c_x = (B2 * C1 - B1 * C2) / det;
                double c_y = (A1 * C2 - A2 * C1) / det;

                if ((c_x == left.X && c_y == left.Y) || (c_x == right.X && c_y == right.Y))
                {
                    //ignore collision on the end points
                }
                else
                {
                    //need to determine if this collision is between start <-> end
                    if (Util.MIN(left.X, right.X) <= c_x && c_x <= Util.MAX(left.X, right.X) &&
                        Util.MIN(left.Y, right.Y) <= c_y && c_y <= Util.MAX(left.Y, right.Y))
                    {
                        //if so... get the dist from start to the collision
                        //  set intersect and i_dist
                        double n_dist = Math.Sqrt(Math.Pow(start.X - c_x, 2) + Math.Pow(start.Y - c_y, 2));

                        if (n_dist < i_dist)
                        {
                            i_dist = n_dist;
                            intersect.X = (float)c_x;
                            intersect.Y = (float)c_y;
                            index = i;//left point
                            found = true;
                        }
                    }
                    else
                    {
                        //not inside our line segment
                    }
                }
            }
            return found;
        }

        public System.Collections.ArrayList GeneratePath(Point start, Point dest)
        {
            System.Collections.ArrayList my_path = new System.Collections.ArrayList();

            Point intersect = new Point();
            int index = 0;

            if (FindIntersect(start, dest, ref intersect, ref index))
            {
                my_path.Add(intersect);

                //need to trace the left and right paths
                double left_dist = 0;
                double right_dist = 0;

                System.Collections.ArrayList left_path = new System.Collections.ArrayList();
                System.Collections.ArrayList right_path = new System.Collections.ArrayList();

                Point t1 = new Point();
                Point t2 = new Point();
                Point tp = new Point();
                int ti = 0;

                //generate left path
                t1 = (Point)PointList[index];
                left_path.Add(t1);
                left_dist += Get_Dist(intersect, t1);

                while (FindIntersect(t1, dest, ref tp, ref ti))
                {
                    t2.X = t1.X;
                    t2.Y = t1.Y;
                    t2.Z = t1.Z;

                    index = Get_Left_Point(index);
                    t1 = (Point)PointList[index];
                    left_path.Add(t1);
                    left_dist += Get_Dist(t1, t2);
                }
                left_dist += Get_Dist(t1, dest);
                left_path.Add(dest);

                //generate right path
                index = Get_Right_Point(index);
                t1 = (Point)PointList[index];
                right_path.Add(t1);
                right_dist += Get_Dist(intersect, t1);

                while (FindIntersect(t1, dest, ref tp, ref ti))
                {
                    t2.X = t1.X;
                    t2.Y = t1.Y;
                    t2.Z = t1.Z;

                    index = Get_Right_Point(index);
                    t1 = (Point)PointList[index];
                    right_path.Add(t1);
                    right_dist += Get_Dist(t1, t2);

                }
                right_dist += Get_Dist(t1, dest);
                right_path.Add(dest);

                if (left_dist > right_dist)
                {
                    //use our right path
                    for (int i = 0; i < right_path.Count; i++)
                    {
                        my_path.Add(right_path[i]);
                    }
                }
                else
                {
                    //use our left path
                    for (int i = 0; i < left_path.Count; i++)
                    {
                        my_path.Add(left_path[i]);
                    }
                }

                //clean up our pathing
                bool cleaned = false;

                do
                {
                    for (int i = 0; i < my_path.Count - 2; i++)
                    {
                        if (!FindIntersect((Point)my_path[i], (Point)my_path[i + 2], ref tp, ref ti))
                        {
                            my_path.RemoveAt(i + 1);
                            cleaned = true;
                            break;
                        }
                    }
                } while (cleaned);
            }
            else
            {
                //no intersection...
                //straight shot to the destination
                my_path.Add(dest);
            }

            return my_path;
        }

        public double Get_Dist(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2) + Math.Pow(p1.Z - p2.Z, 2));
        }

        public int Get_Left_Point(int index)
        {
            if (index == 0)
            {
                return PointList.Count - 1;
            }

            return index - 1;
        }

        public int Get_Right_Point(int index)
        {
            if (index + 1 == PointList.Count)
            {
                return 0;
            }

            return index + 1;
        }

		public bool IsPointInside(Point p)
		{
			if(PointList.Count < 3)
				return true;

			int counter = 0;

			//need to run rays and check for collision, 
			float xinters;
			Point p1,p2;
			int i;

			try
			{
				p1 = (Point)PointList[0];
				for (i=1;i<=PointList.Count;i++) 
				{
					p2 = (Point)PointList[i % PointList.Count];
					if (p.Y > Util.MIN(p1.Y,p2.Y)) 
					{
						if (p.Y <= Util.MAX(p1.Y,p2.Y)) 
						{
							if (p.X <= Util.MAX(p1.X,p2.X)) 
							{
								if (p1.Y != p2.Y) 
								{
									xinters = (p.Y-p1.Y)*(p2.X-p1.X)/(p2.Y-p1.Y)+p1.X;
									if (p1.X == p2.X || p.X <= xinters)
									{
										counter++;
									}
								}
							}
						}
					}
					p1 = p2;
				}
			
				//next we do a modulus and return out result
				if(counter % 2 == 0)
					return false;
				return true;
			}
			catch
			{
				Globals.l2net_home.Add_Error("border check failed... get those mexicans out of here!");
				return false;
			}
		}
	}//end of polygon class

	public class Wall
	{
		private Point _p1;
		private Point _p2;

		private readonly object p1Lock = new object();
		private readonly object p2Lock = new object();

     /*   public Wall()
        {
            _p1 = new Point();
            _p2 = new Point();
        }*/

		public Point P1
		{
			get
			{
				Point tmp;
				lock(p1Lock)
				{
					tmp = this._p1;
				}
				return tmp;
			}
			set
			{
				lock(p1Lock)
				{
					_p1 = value;
				}
			}
		}
		public Point P2
		{
			get
			{
				Point tmp;
				lock(p2Lock)
				{
					tmp = this._p2;
				}
				return tmp;
			}
			set
			{
				lock(p2Lock)
				{
					_p2 = value;
				}
			}
		}
	}//end of wall class

	public class Point
	{
        public volatile float X;
        public volatile float Y;
        public volatile float Z;

        public Point(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

		public void Normalize()
		{
            float len = System.Convert.ToSingle(System.Math.Sqrt(System.Math.Pow(X, 2) + System.Math.Pow(Z, 2) + System.Math.Pow(Z, 2)));

			X = X/len;
			Y = Y/len;
			Z = Z/len;
		}
	}//end of point class
}//end of namespace
