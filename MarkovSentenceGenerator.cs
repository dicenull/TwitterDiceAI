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
		private List<MarkovBlock> blockDatabase = new List<MarkovBlock>();

		private MeCabTagger mecab;

		public MarkovSentenceGenerator(MeCabTagger mecab)
		{
			this.mecab = mecab;
		}

		public string Generate()
		{
			string sentence = "";

            var blocks = blockDatabase.Where(block => block.Key == null).ToList();
            
			while (blocks != null)
			{
				var blockWord = blocks.RandomSelect();
				sentence += blockWord.Value + blockWord.Next;

                if(blockWord.Next == null)
                {
                    break;
                }

                blocks = blocks.Where(block => block.Key == blockWord.Next).ToList();
			}

			return sentence;
		}

		public void AddBlock(MarkovBlock block)
		{
            blockDatabase.Add(block);
		}

		public void Regist(string text)
		{
			var firstNode = mecab.ParseToNode(text);

            // リンクや画像つきツイートを除外
			if (text.Contains("http")) return;

			foreach (var block in firstNode.ToEnumerable().ToBlocks())
			{
				AddBlock(block);
			}
		}
	}
}
