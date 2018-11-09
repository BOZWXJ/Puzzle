using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace scissors
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
            int x = 2, y = 1, tmp;
            for (int i = 0; i < 40; i++)
            {
                tmp = x;
                x = x + y;
                y = tmp;
            }
            System.Diagnostics.Debug.WriteLine(string.Format("x={0},y={1}", x, y));
        }
    }
}
