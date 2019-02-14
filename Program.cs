using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMeCab;
using System.IO;
using System.Configuration;
using CoreTweet;
using System.Diagnostics;

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
			var app = new TwitterApp("dicenull");

			foreach(var text in app.Tweets)
			{
				var firstNode = mecab.ParseToNode(text);
				if(text.Contains("http"))
				{
					continue;
				}
				foreach (var block in firstNode.ToEnumerable().ToBlocks())
				{
					generator.AddBlock(block);
				}
			}
			
			Console.WriteLine(generator.Generate());
		}
	}
}
