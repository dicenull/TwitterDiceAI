using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMeCab;
using System.IO;

namespace TwitterDiceAI
{
	class Program
	{
		static void Main(string[] args)
		{
			var mecab = MeCabTagger.Create(new MeCabParam
			{
				DicDir = Path.Combine(AppContext.BaseDirectory, "ipadic"),
			});
			var generator = new MarkovSentenceGenerator();

			string[] sentences = new[] { "これはペンですか", "これはボールでしょう", "わたしはAです" };

			foreach(var sentence in sentences)
			{
				var firstNode = mecab.ParseToNode(sentence);
				foreach (var block in firstNode.ToEnumerable().ToBlocks())
				{
					generator.AddBlock(block);
				}
			}

			Console.WriteLine(generator.Generate());
		}
	}
}
