using System;
using System.Collections.Generic;

namespace AtCoder
{
	public class Program
	{
		// ABC107-B ok
		static void Main(string[] args)
		{
			string[] vs = Console.ReadLine().Split();
			int H = int.Parse(vs[0]);
			int W = int.Parse(vs[1]);
			string[] map = new string[H];
			bool[] col = new bool[W];
			int y = 0;
			for (int i = 0; i < H; i++) {
				string s = Console.ReadLine();
				bool row = false;
				for (int j = 0; j < s.Length; j++) {
					bool f = s[j] == '#';
					col[j] |= f;
					row |= f;
				}
				if (row) {
					map[y] = s;
					y++;
				}
			}
			for (int i = 0; i < y; i++) {
				string s = "";
				for (int x = 0; x < map[i].Length; x++) {
					if (col[x]) {
						s += map[i][x];
					}
				}
				Console.WriteLine(s);
			}
		}
	}
}
/*

4 4
##.#
....
##.#
.#.#

###
###
.##

3 3
#..
.#.
..#

#..
.#.
..#

4 5
.....
.....
..#..
.....

#

7 6
......
....#.
.#....
..#...
..#...
......
.#..#.

..#
#..
.#.
.#.
#.#

 */

