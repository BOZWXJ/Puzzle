using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtCoderProblem
{
	class Program
	{
		[STAThreadAttribute]
		static void Main(string[] args)
		{
			StringBuilder sb = new StringBuilder();






			// 問題文出力
			string txt = sb.ToString().TrimEnd();
			Console.WriteLine(txt);
			File.WriteAllText(@"..\..\..\AtCoder\Problem.txt", txt, Encoding.ASCII);
		}
	}
}
