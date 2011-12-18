using System;

//-----------------------------------------------------------------------
// <copyright file="FindMaxSum.cs" >
//     Copyright (c) Kyle Traff.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TriangleMaxSum
{
    /// <summary>Drives the application</summary>
    class FindMaxSum
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    TriangleSum triangleSum = new TriangleSum(args[i]);
                    if (triangleSum.maxPath != null)
                    {
                        Console.WriteLine("Max Path for file {0}: {1}", args[i], triangleSum.printMaxPath());
                        Console.WriteLine("Time: " + triangleSum.stats.time());
                        Console.WriteLine("Memory Footprint: " + triangleSum.stats.memoryFootprint());
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
            else
            { // no input file specified
                Console.WriteLine("Usage: TriangleSum.exe <input-file-1> <input-file-2> ... <input-file-N>"); 
            }
            
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }
    }
}
