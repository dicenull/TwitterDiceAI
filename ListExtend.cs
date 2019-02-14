using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterDiceAI
{
	static class ListExtend
	{
		private static Random rnd = new Random();

		public static T RandomSelect<T>(this List<T> list)
		{
			return list[rnd.Next(list.Count())];
		}
	}
}
