using System;
using System.Collections.Generic;
using System.Text;

//-----------------------------------------------------------------------
// <copyright file="Path.cs" >
//     Copyright (c) Kyle Traff.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TriangleMaxSum
{
    /// <summary>
    /// Represents a path through a triangle with an associated sum.  For example,
    /// The path 3 + 7 + 5 + 9 is a path through the triangle below with a sum = 24.
    ///
    ///        3
    ///       7 4
    ///      2 5 6
    ///     8 5 9 3
    /// </summary>
    class Path : ICloneable
    {
        // The current sum of the path
        private int _sum;
        public int sum { get { return _sum; } set { _sum = value; } }
        // The current level of the triangle (with the top level = 0)
        private int _level;
        public int level { get { return _level; } set { _level = value; } }
        // List of indexes of all nodes in the path (Not the node value!)
        private List<int> _path;
        public List<int> path { get { return _path; } set { _path = value; } }

        public Path()
        {
            _sum = 0;
            _level = 0;
            _path = new List<int>();
        }

        /// <summary>Creates a path initialized to the top (or root) of a triangle</summary>
        public Path(int root)
        {
            _sum = root;
            _level = 0;
            _path = new List<int>();
            _path.Add(0);
        }

        /// <summary>
        /// Performs a deep copy of the object. For use in creating child path
        /// objects when finding the maximum triangle sum
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            Path clone = new Path();
            clone.level = _level;
            clone.sum = _sum;
            // clone each node in the path
            List<int> pathClone = new List<int>();
            foreach (int index in _path) pathClone.Add(index);
            clone.path = pathClone;
            
            return clone;
        }

        /// <summary>Returns the index of the parent node</summary>
        public int getParentIndex()
        {
            return path.Count > 0 ? _path[path.Count - 1] : -1;
        }

        public override string ToString()
        {
            StringBuilder retStr = new StringBuilder();
            int i = 0;
            for (i = 0; i < (_path.Count - 1); i++)
            {
                retStr.Append(i + "[" + _path[i] + "]" + " > ");
            }
            retStr.Append(i + "[" + _path[i] + "] = " + _sum);
            
            return retStr.ToString();
        }
    }
}