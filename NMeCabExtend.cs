using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMeCab;

namespace TwitterDiceAI
{
	static class NMeCabExtend
	{
		public static IEnumerable<MeCabNode> ToEnumerable(this MeCabNode node)
		{
			while(node != null)
			{
				if(node.CharType != 0)
				{
					yield return node;
				}

				node = node.Next;
			}
		}

		public static IEnumerable<MarkovBlock> ToBlocks(this IEnumerable<MeCabNode> nodes)
		{
			if(nodes.Count() < 2)
			{
				yield break;
			}

			var surfaces = new List<string>();
			surfaces.Add(null);
			surfaces.AddRange(nodes.Select(node => node.Surface));
			surfaces.Add(null);
			
			for(int i = 0;i < surfaces.Count() - 2;i++)
			{
				yield return new MarkovBlock(surfaces.Skip(i).Take(3).ToArray());
			}
		}
	}
}
