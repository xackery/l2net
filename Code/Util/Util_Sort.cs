using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace L2_login
{
    public static partial class Util
    {
        public static void Sort(ArrayList array, ListViewColumnSorter compare)
        {
            //quick sort - unstable
            //array.Sort(compare);

            //insertion sort - stable
            int i;
            int j;
            object index;

            for (i = 1; i < array.Count; i++)
            {
                index = array[i];
                j = i;

                while ((j > 0) && (compare.Compare(array[j - 1], index) > 0))
                {
                    array[j] = array[j - 1];
                    j = j - 1;
                }

                array[j] = index;
            }

            //old bubble sort - stable
            /*bool change = false;

            for (int i = 0; i < array.Count; i++)
            {
                for (int j = 0; j < array.Count - i - 1; j++)
                {
                    if (compare.Compare(array[j], array[j + 1]) > 0)
                    {
                        array.Reverse(j, 2);
                        change = true;
                    }
                }

                if (!change)
                {
                    //no changes...fully sorted
                    return;
                }
            }*/
        }
    }
}
