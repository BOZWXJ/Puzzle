using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoder
{
	public class Program
	{
		// ABC104-C ok
		static void Main(string[] args)
		{
			string[] vs = Console.ReadLine().Split();
			int D = int.Parse(vs[0]);   // 種類		1～10
			int G = int.Parse(vs[1]);   // 目標点
			int[] count = new int[D];   // 問題数	1～100
			int[] basic = new int[D];   // 基本点
			int[] bonus = new int[D];   // 追加点	100～1,000,000
			for (int i = 0; i < D; i++) {
				vs = Console.ReadLine().Split();
				count[i] = int.Parse(vs[0]);
				basic[i] = (i + 1) * 100;
				bonus[i] = int.Parse(vs[1]);
			}

			int answer = int.MaxValue;
			Queue<Item> items = new Queue<Item>();
			HashSet<string> hashSet = new HashSet<string>();
			items.Enqueue(new Item(D));
			while (items.Count > 0) {
				Item item = items.Dequeue();

				// debug: 
				//Console.WriteLine(item.Answer + " :{" + item.ToString() + "} " + item.Point);

				if (item.Point >= G) {
					answer = Math.Min(answer, item.Answer);
				} else {
					for (int i = D - 1; i >= 0; i--) {
						if (item.Count[i] == 0) {
							int k = (G - item.Point) / basic[i] + ((G - item.Point) % basic[i] > 0 ? 1 : 0);
							if (k < count[i]) {

								// debug:
								//Item o = new Item(item);
								//o.Answer += k;
								//o.Count[i] = k;
								//o.Point += basic[i] * k;
								//Console.WriteLine(o.Answer + " :{" + o.ToString() + "} " + o.Point + "*");

								answer = Math.Min(answer, item.Answer + k);
							}
							break;
						}
					}
				}

				for (int i = 0; i < D; i++) {
					if (item.Count[i] == 0) {
						Item tmp = new Item(item);
						tmp.Answer += count[i];
						tmp.Count[i] = count[i];
						tmp.Point += basic[i] * count[i] + bonus[i];
						if (!hashSet.Contains(tmp.ToString())) {
							items.Enqueue(tmp);
							hashSet.Add(tmp.ToString());
						}
					}
				}
			}
			Console.WriteLine(answer);
		}
	}

	public class Item
	{
		public int Answer = 0;
		public int[] Count;
		public int Point = 0;

		public Item(int c)
		{
			Count = new int[c];
		}

		public Item(Item item)
		{
			Answer = item.Answer;
			Count = (int[])item.Count.Clone();
			Point = item.Point;
		}

		public override string ToString()
		{
			return string.Join(",", Count.Select(p => p.ToString("D3")));
		}
	}

}

/*
2 400
3 500
5 800


2 700
3 500
5 800

3
100*3+500=800
3 :{003 000} 800

2 2000
3 500
5 800

7
100*2+200*5+800=2000
7 :{002 005} 2000

2 400
3 500
5 800

2
200*2=400
2 :{000 002} 400

5 25000
20 1000
40 1000
50 1000
30 1000
1 1000

66
66 :{000 000 035 030 001} 25000
100*0 + 200*0 + 300*35 + 400*30+1000 + 500*1+1000 = 25000
*/
