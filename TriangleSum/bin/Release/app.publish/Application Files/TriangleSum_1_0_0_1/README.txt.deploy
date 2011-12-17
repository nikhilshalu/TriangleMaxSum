* Project: TriangleMaxSum
*
* Author: Kyle Traff
* Date: 12/17/2011
* 

Usage: FindMaxSum <input-file-1> <input-file-2> ... <input-file-N>

Given a triangle (contained in an input file) like the one below:

        3
       7 4
      2 5 6
     8 5 9 3

Finds the path containing the largest sum.  A path is defined as adjacent nodes from the top
of the triangle to the bottom, where each child is either one node to the left of the right 
of the parent node.  For example, in the triangle above, the maximum path is 3 + 7 + 5 + 9 = 24.

Algorithm: the strategy used to solve this problem involves a combination of recursion to divide
the problem into smaller subproblems, as well as dynamic programming used to divide the search 
space into a manageable size.  The basic pseudocode for the search algorithm is below:

function findMaxPath(path)
	if (past the bottom of the triangle) return
	if (no better path exists in the cache)
		add path to the cache
		if (path > maxPath) maxPath = path
		findMaxPath(leftChild)
		findMaxPath(rightChild)

Advantages to this approach: 

- Versatile: This solution is fairly versatile in that it could be extended to 
find the maximum path to any node in the triangle.  It could also be extended to find paths 
using different traversal constraints (for instance, the parent's children could only be 2 
spaces to the left/right, etc...)
- Performance: Without caching, the complexity of this algorithm (average and worst-case) is
O(2^N), where N = the height of the tree.  The caching mechanism is implemented to stop the 
traversal of all suboptimal paths at level L as soon as all optimal paths for L are found.
This will reduce the complexity to a little more than O(N^2)
- Memory Footprint: elements in the triangle are stored in a two-dimensional list of integers
that is dynamically resized to fit the triangle's dimensions.  This minimizes object creation
overhead as well as the overhead of building a bulky rectangular-shaped array. 

Disadvantages to this approach:

- Recursion: the space complexity of using a recursive implementation adds O(2^N) space because
each level of the triangle requires two recursive calls (though caching minimizes this impact)
