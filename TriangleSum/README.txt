* Project: TriangleMaxSum
*
* Author: Kyle Traff
* Date: 12/17/2011
* 

Contents
-------------------------------------------------------------------------------
1. Installation
   1.1 Packaging
2. Solution 
	2.1 Algorithm
	2.2 Advantages
		2.2.1 Versatility
		2.2.2 Performance
		2.2.3 Memory Footprint
	2.3 Disadvantages
		2.3.1 Recursion


1. Installation
-------------------------------------------------------------------------------
Usage: TriangleSum.exe <input-file-1> <input-file-2> ... <input-file-N>

Input File: An input file should contain one or more rows of integers.  Each row will have
one more integer than the previous row. An example of a valid input file is below:
<BOF>
3
7 4
2 5 6
8 5 9 3
<EOF>
	1.1 Packaging: The C# source is included in the "src" directory.  (Note: The solution was built using the .NET 4.0 Framework in Visual C# 2010 Express)

2. Solution
-------------------------------------------------------------------------------
Given a triangle (contained in an input file) like the one below:

        3
       7 4
      2 5 6
     8 5 9 3

Finds the path containing the largest sum.  A path is defined as adjacent nodes from the top
of the triangle to the bottom, where each child is either one node to the left of the right 
of the parent node.  For example, in the triangle above, the maximum path is 3 + 7 + 5 + 9 = 24.
If there exists two paths with equal maximal sums, the solution will choose the left-most path.

	2.1 Algorithm: the strategy used to solve this problem involves a combination of recursion to divide the problem into smaller subproblems, as well as dynamic programming used to divide the search space into a manageable size.  The basic pseudocode for the search algorithm is below:

	function findMaxPath(path)
		if (below the bottom of the triangle) return
		if (no better path exists in the cache)
			add path to the cache
			if (path > maxPath) maxPath = path
			findMaxPath(leftChild)
			findMaxPath(rightChild)

	2.2 Advantages to this approach: 
		2.2.1 Versatile: This solution is fairly versatile in that it could be extended to 
		find the maximum path to any node in the triangle.  It could also be extended to find paths 
		using different traversal constraints (for instance, the parent's children could only be 2 
		spaces to the left/right, etc...)
		2.2.2 Performance: Without caching, the complexity of this algorithm (average and worst-case) is O(2^N), where N = the height of the tree.  The caching mechanism is implemented to stop the traversal of all suboptimal paths at level L as soon as all optimal paths for L are found. This will reduce the complexity to a little more than 
		O(N^2)
		2.2.3 Memory Footprint: elements in the triangle are stored in a two-dimensional list of integers that is dynamically resized to fit the triangle's dimensions.  This minimizes object creation overhead as well as the overhead of building a bulky rectangular-shaped array. 

	2.3 Disadvantages to this approach:
		2.3.1 Recursion: the space complexity of using a recursive implementation adds O(2^N) space because Each level of the triangle requires two recursive calls (though caching minimizes this impact)