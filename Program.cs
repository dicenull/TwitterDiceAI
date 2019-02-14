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

			var firstNode = mecab.ParseToNode("これはペンですか。");
			foreach(var block in firstNode.ToEnumerable().ToBlocks())
			{
				Console.WriteLine($"{block.Key} {block.Value} {block.Next} ");
			}
		}
	}
}
