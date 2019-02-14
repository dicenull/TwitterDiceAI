using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMeCab;

namespace TwitterDiceAI
{
	class MarkovSentenceGenerator
	{
		private Dictionary<int, List<MarkovBlock>> blockDatabase 
				= new Dictionary<int, List<MarkovBlock>>();

		private MeCabTagger mecab;

		public MarkovSentenceGenerator(MeCabTagger mecab)
		{
			this.mecab = mecab;
		}

		public string Generate()
		{
			string sentence = "";

			int hash = 0;
			
			while (blockDatabase.ContainsKey(hash))
			{
				var block = blockDatabase[hash].RandomSelect();
				sentence += block.Value + block.Next;

				if(block.Next == null)
				{
					break;
				}

				hash = block.Next.GetHashCode();
			}

			return sentence;
		}

		public void AddBlock(MarkovBlock block)
		{
			var hash = 0;
			if (block.Key != null)
			{
				hash = block.Key.GetHashCode();

				if (hash == 0) hash = 1;
			}

			if (!blockDatabase.ContainsKey(hash))
			{
				blockDatabase[hash] = new List<MarkovBlock>();
			}
			blockDatabase[hash].Add(block);
		}

		public void Regist(string text)
		{
			var firstNode = mecab.ParseToNode(text);

			if (text.Contains("http")) return;

			foreach (var block in firstNode.ToEnumerable().ToBlocks())
			{
				AddBlock(block);
			}
		}
	}
}
