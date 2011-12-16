using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//-----------------------------------------------------------------------
// <copyright file="Program.cs" >
//     Copyright (c) Kyle Traff.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TriangleMaxSum
{
    /// <summary>Application Driver</summary>
    class FindMaxSum
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                TriangleSum triangleSum = new TriangleSum(args[0]);
                if (triangleSum.maxPath != null) Console.WriteLine("Max Path for file {0}: {1}", args[0], triangleSum.printMaxPath()); 
            }
            else
            {
                Console.WriteLine("usage: TriangleMaxSum <input-file>"); 
            }
            
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }
    }
}
