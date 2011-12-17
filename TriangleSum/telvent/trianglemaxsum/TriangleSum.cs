using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

//-----------------------------------------------------------------------
// <copyright file="TriangleSum.cs" >
//     Copyright (c) Kyle Traff.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TriangleMaxSum
{
    /// <summary>
    /// Represents a triangular matrix from which a path with the maximum
    /// sum can be calculated.  For example, In the triangle below, the 
    /// maximum total from top to bottom is 24 (allowing moves only to adjacent 
    /// numbers on the row directly below). That is, 3 + 7 + 5 + 9 = 24.
    ///
    ///        3
    ///       7 4
    ///      2 5 6
    ///     8 5 9 3
    /// </summary>
    class TriangleSum
    {
        // Contains the path with the largest sum
        private Path _maxPath;
        public Path maxPath { get { return _maxPath; } set { _maxPath = value; } }
        // Represents a triangle of integers, each index representing a level of the triangle
        private List<List<int>> _triangle;
        public List<List<int>> triangle { get { return _triangle; } set { _triangle = value; } }
        // Keeps track of the largest path at each node of the triangle in order to find solutions
        // to large problem sets
        private List<List<int>> _cache;
        public List<List<int>> cache { get { return _cache; } set { _cache = value; } }
        // Keep track of relevant statistics to measure performance
        private Statistics _stats;
        public Statistics stats { get { return _stats; } set { _stats = value; } }

        public TriangleSum(string file)
        {
            _triangle = new List<List<int>>();
            _cache = new List<List<int>>();
            _stats = new Statistics();

            if (read(file))
            { // no errors reading in the triangle from the input file, now search
                _maxPath = new Path();
                _stats.start();
                calculateMaxPath(new Path(_triangle[0][0]));
                _stats.end();
            }
        }

        private void addCacheEntry(int level, int index, int sum)
        {
            _cache[level][index] = sum;
        }

        /// <summary>
        /// Finds the path with the maximum sum by traversing (from top to bottom)
        /// moves only to adjacent numbers on the row directly below.  Declared private
        /// because we only want this method to be called if the input file contains no
        /// errors.  (Note: this can be called at any node in the tree, so a max path can
        /// be found for any node in the tree)
        /// </summary>
        /// <param name="curPath">
        /// The current path from the top of the triangle to the current node
        /// </param>
        private void calculateMaxPath(Path curPath)
        {
            if (curPath.level >= _triangle.Count) return; // finished
            if (!isCached(curPath))
            {
                // add the current node to the cache
                addCacheEntry(curPath.level, curPath.getParentIndex(), curPath.sum);

                // Create two child objects and add their child nodes
                Path leftChild = createChild(curPath, curPath.getParentIndex());
                Path rightChild = createChild(curPath, curPath.getParentIndex() + 1);

                if (curPath.sum > _maxPath.sum) _maxPath = curPath;

                // Check the adjacent children
                calculateMaxPath(leftChild);
                calculateMaxPath(rightChild);
            }
        }

        /// <summary>Creates a child path based on the parent's path and the index to the child</summary>
        /// <param name="curPath">The parent path</param>
        /// <param name="index">The new node to add to the child path</param>
        /// <returns></returns>
        public Path createChild(Path curPath, int index)
        {
            // clone the parent path
            ICloneable clone = (ICloneable)curPath;
            Path child = (Path)clone.Clone();

            child.level++; // move down the triangle
            // add the child node to the path
            if(child.level < _triangle.Count) 
            {
                child.path.Add(index);
                // update the sum
                child.sum += _triangle[child.level][index];
            } 

            return child;
        }

        /// <summary>
        /// Determines if a cached entry exists containing the path with the largest sum
        /// up to the current level of the triangle.
        /// </summary>
        /// <returns>
        /// true if, for any node on the path, a cached entry exists and is larger than 
        /// the sum given in the current path, meaning that a different path necessarily
        /// is more maximal than the current path
        /// </returns>
        public bool isCached(Path path)
        {
            List<int> curPath = path.path;
            int runningSum = 0;
            for (int i = 0; i < curPath.Count; i++)
            {
                // The current largest sum of any path visiting this node
                int cachedSum = _cache[i][curPath[i]];
                runningSum += _triangle[i][curPath[i]];
                // there exists a path with a larger sum than the current path
                if (cachedSum > runningSum) return true;
            }
            return false;
        }

        /// <summary>Prints the elements in _maxPath</summary>
        public String printMaxPath()
        {
            StringBuilder retStr = new StringBuilder();
            List<int> pathToPrint = _maxPath.path;
            int i;
            for (i = 0; i < (pathToPrint.Count - 1); i++)
            {
                retStr.Append(_triangle[i][pathToPrint[i]] + " + ");
            }
            retStr.Append(_triangle[i][pathToPrint[i]] + " = " + _maxPath.sum);

            return retStr.ToString();
        }

        /// <summary>
        /// Reads the triangle into a two-dimensional array from the specified text file
        /// </summary>
        /// <param name="file"></param>
        private bool read(string file)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(Environment.CurrentDirectory + @"\" + file);
                if (lines.Length == 0)
                { // ensure at least one element exists in the triangle
                    Console.WriteLine("Input file is empty. Please add at least one element to {0}" +
                                        " and try again.", file);
                    return false;
                }
                int expectedElements = 1; // each row should add one extra element to the list
                for (int i = 0; i < lines.Length; i++)
                {
                    int elementCount = 0;
                    string line = lines[i];
                    Array numbers = line.Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string numStr in numbers)
                    { // parse each number and insert into our triangle collection
                        int number;
                        if (int.TryParse(numStr, out number))
                        {
                            if (i < _triangle.Count)
                            {
                                _triangle[i].Add(number);
                                // add an initial cached value to be updated during calculation
                                _cache[i].Add(int.MinValue);
                            }
                            else
                            { // add a new level to the triangle and the cache collections
                                while (i >= _triangle.Count)
                                {
                                    _triangle.Add(new List<int>());
                                    _cache.Add(new List<int>());
                                }
                                _triangle[i].Add(number);
                                _cache[i].Add(int.MinValue);
                            }
                            elementCount++;
                        }
                        else
                        {
                            Console.WriteLine("Invalid number at line {0} = {1} in {2}.  Please fix and try again.", i+1, numStr, file);
                            return false;
                        }
                    }
                    if (elementCount != expectedElements)
                    {  // ensure that each row r has one more element that parent row p
                        Console.WriteLine("Expecting {0} elements to complete the next row in the " +
                                          "triangle.  Please fix line {1} in {2} and try again.", expectedElements, i+1, file);
                        return false;
                    }
                    expectedElements++;
                }
                return true;
            }
            catch (FileNotFoundException e)
            {
                // if the given file can't be found, try to use the default input.txt file
                if (!file.Equals("input.txt"))
                {
                    Console.WriteLine("Could not find file {0}, using input.txt instead", file);
                    read("input.txt");
                }
                else
                { // Could not find an input file
                    Console.WriteLine(e);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
    }
}