using System;
using System.Diagnostics;

//-----------------------------------------------------------------------
// <copyright file="Statistics.cs" >
//     Copyright (c) Kyle Traff.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TriangleMaxSum
{
    /// <summary>
    /// Keeps track of relevant statistics for some associated block of code, such as time
    /// of execution and memory footprint
    /// </summary>
    class Statistics
    {
        // keep track of start and end times to measure performance
        private DateTime _startTime, _endTime;
        public DateTime startTime { get { return _startTime; } set { _startTime = value; } }
        public DateTime endTime { get { return _endTime; } set { _endTime = value; } }
        // keep track of base memory footprint to measure approximate memory used in solution
        private long _startMemory, _endMemory;
        public long baseMemory { get { return _startMemory; } set { _startMemory = value; } }

        public Statistics() 
        {
            _startMemory = -1;
            _endMemory = -1;
        }

        /// <returns>The current amount of memory this process is allocating in KB</returns>
        public long currentMemory()
        {
            Process proc = Process.GetCurrentProcess();
            return (proc.PrivateMemorySize64 / 1000);
        }

        /// <summary>
        /// Completes measuring time and memory footprints by measuring the difference in 
        /// base memory and recording the end time of execution
        /// </summary>
        public void end()
        {
            _endTime = DateTime.Now;
            _endMemory = currentMemory();
        }

        /// <summary>Prints the change in memory in KB during the executed code block</summary>
        /// <returns></returns>
        public string memoryFootprint()
        {
            if (_endMemory != -1 && _startMemory != -1)
            {
                return (_endMemory - _startMemory) + " KB";
            }
            else return "NaN"; // start/end memory hasn't been recorded
        }

        /// <summary>
        /// Begins measuring time and memory footprints by recording the base memory and start
        /// time of code execution
        /// </summary>
        public void start()
        {
            _startTime = DateTime.Now;
            _startMemory = currentMemory();
        }

        /// <returns>The time it took to calculate the path with the maximal sum in ms</returns>
        public string time()
        {
            if (_endTime != null && _startTime != null)
            {
                TimeSpan duration = _endTime - _startTime;
                return duration.Milliseconds + " ms";
            }
            else return "NaN"; // start/end time hasn't been recorded
        }
    }
}
